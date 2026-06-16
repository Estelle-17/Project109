using EventStructs;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Move,
    Attack,
    Skill,
    Hit,
    Die
}


public enum CharacterFaction
{
    Player,
    Ally,
    Enemy,
    Neutral
}

public class Character : MonoBehaviour
{
    // 캐릭터 진영 소속 정보
    public CharacterFaction faction = CharacterFaction.Enemy;

    /// <summary>
    /// 상대 캐릭터가 나와 적대적인 관계인지 판별합니다.
    /// </summary>
    public bool IsHostileTo(Character other)
    {
        if (other == null || other.isDead) return false;

        // 중립(Neutral) 진영은 누구와도 서로 적대하지 않음
        if (this.faction == CharacterFaction.Neutral || other.faction == CharacterFaction.Neutral)
            return false;

        // 플레이어(Player)와 아군 소환수(Ally)는 아군 진영이므로 서로 적대하지 않음
        if ((this.faction == CharacterFaction.Player || this.faction == CharacterFaction.Ally) &&
            (other.faction == CharacterFaction.Player || other.faction == CharacterFaction.Ally))
        {
            return false;
        }

        // 그 외 진영이 다를 경우 적대 관계로 판별
        return this.faction != other.faction;
    }

    /// <summary>
    /// 상대 캐릭터가 나와 동맹 관계인지 판별합니다.
    /// </summary>
    public bool IsFriendlyTo(Character other)
    {
        if (other == null) return false;
        return !IsHostileTo(other) && (this.faction != CharacterFaction.Neutral && other.faction != CharacterFaction.Neutral);
    }

    #region CharacterStat

    [SerializeField]
    private CharacterStat _characterStat;

    // 외부에 노출되는 런타임 현재 스탯
    public CharacterStat curCharacterStat => _characterStat;

    public EffectManager effectManager;
    public CharacterMove characterMove { get; private set; }

    private void Awake()
    {
        effectManager = new EffectManager(this);
        characterMove = new CharacterMove(this);
    }

    public void InitializeStat(CharacterStat characterStat)
    {
        if (characterStat != null)
        {
            _characterStat = characterStat;

            curHealth = characterStat.maxHealth;
            curStamina = 0f;

            curMoveCount = characterStat.maxMoveCount;
            curTilesPerMove = characterStat.maxTilesPerMove;
        }
    }

    #endregion

    #region CharacterEvents

    public EventBus<ICharacterEvent> eventBus = new EventBus<ICharacterEvent>();

    public event Action<Character> OnCharacterHealthChanged;
    public event Action<Character> OnCharacterStaminaChanged;
    public event Action<Character> OnCharacterShieldChanged;
    public event Action<Character> OnCharacterDied;

    public event Action<Character, CharacterState> OnCharacterStateChanged;

    #endregion

    #region CurrentStatValues

    [SerializeField]
    private int _curMoveCount;
    public int curMoveCount { get { return _curMoveCount; } set { _curMoveCount = value; } }

    [SerializeField]
    private int _curTilesPerMove;
    public int curTilesPerMove { get { return _curTilesPerMove; } set { _curTilesPerMove = value; } }

    [SerializeField]
    private CharacterState _currentState = CharacterState.Idle;
    public CharacterState currentState
    {
        get => _currentState;
        set
        {
            if (_currentState != value)
            {
                _currentState = value;
                OnCharacterStateChanged?.Invoke(this, _currentState);
            }
        }
    }


    public bool isDead = false;

    [SerializeField]
    private float _curHealth;
    public float curHealth { get { return _curHealth; } set { _curHealth = value; OnCharacterHealthChanged?.Invoke(this); } }
    public float curHealthRate { get { return (_curHealth == 0) ? 0 : _curHealth / curCharacterStat.maxHealth; } }

    [SerializeField]
    private float _curStamina;
    public float curStamina { get { return _curStamina; } set { _curStamina = value; OnCharacterStaminaChanged?.Invoke(this); } }
    public float curStaminaRate { get { return (_curStamina == 0) ? 0 : _curStamina / curCharacterStat.maxStamina; } }

    [SerializeField]
    public float shield = 0f;

    [SerializeField]
    public int shieldDurationTurns = 1;

    [SerializeField]
    public int currentTurn;

    #endregion

    #region BattleTick

    /// <summary>
    /// BattleManager에서 매 프레임 호출. 배속을 적용해 dt를 받는다.
    /// </summary>
    public void BattleTick(float dt)
    {
        // 스태미나 회복
        curStamina += curCharacterStat.staminaRegenPerSecond * dt;

        // 이펙트 틱
        effectManager.Tick(dt);

        if (curStamina >= curCharacterStat.maxStamina)
        {
            RunManager.instance.battleManager.RequestTurnStart(this);
        }
    }

    /// <summary>
    /// 턴 종료 시 호출하여 스태미나 리셋 및 플래그 초기화
    /// </summary>
    public void ResetStamina()
    {
        curStamina = curStamina * 0.5f;
    }

    /// <summary>
    /// 턴 시작 시 호출하여 이동 관련 스탯 초기화
    /// </summary>
    public void ResetMoveStat()
    {
        if (curCharacterStat != null)
        {
            curMoveCount = curCharacterStat.maxMoveCount;
        }
    }

    #endregion

    #region Damage and Heal Pipeline

    public void TakeDamage(DamageInfo info)
    {
        if (isDead) return;

        // 1. 공격자의 "공격 직전" 발동 (ex. 힘(Strength) 버프를 통해 baseDamageAmount 증가)
        if (!info.damageFlags.HasFlag(DamageFlag.NoCasterEvents) && info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeDealDamage>(l => l.OnBeforeDealDamage(ref info));

        // 2. 피격자의 "방어 직전" 효과 발동 (ex. 데미지 경감, 회피 처리 등)
        if (!info.damageFlags.HasFlag(DamageFlag.NoTargetEvents))
            this.eventBus?.Invoke<IOnBeforeTakeDamage>(l => l.OnBeforeTakeDamage(ref info));

        // 회피되었는지 체크 (도입 미정으로 주석 처리)
        // if (info.isDodged) return;

        float finalDamage = info.baseDamageAmount * info.damageMultiplier;
        info.finalDamageAmount = finalDamage;

        info.shieldDamageAmount = 0f;
        info.hpDamageAmount = 0f;

        // 3. 실제 데미지 적용
        if (!info.damageFlags.HasFlag(DamageFlag.IgnoreShield))
        {
            if (shield > 0f)
            {
                float damageAfterShield = finalDamage - shield;
                info.shieldDamageAmount = Mathf.Min(finalDamage, shield);
                shield -= finalDamage;
                if (shield <= 0f)
                {
                    shield = 0f;
                    shieldDurationTurns = 0; // 실드가 파괴되었으므로 지속 턴 수 리셋
                    info.isShieldBroken = true;
                    OnCharacterShieldChanged?.Invoke(this);

                    // 실드 파괴 즉시 이벤트 발생
                    if (!info.damageFlags.HasFlag(DamageFlag.NoCasterEvents) && info.caster != null)
                        info.caster.eventBus?.Invoke<IOnBreakShield>(l => l.OnBreakShield(info));

                    if (!info.damageFlags.HasFlag(DamageFlag.NoTargetEvents))
                        this.eventBus?.Invoke<IOnShieldBroken>(l => l.OnShieldBroken(info));
                }
                finalDamage = Mathf.Max(0f, damageAfterShield);
            }
        }

        info.hpDamageAmount = finalDamage;
        info.isBlocked = (info.shieldDamageAmount > 0f && info.hpDamageAmount <= 0f);

        // 남은 데미지를 체력에 적용
        if (info.hpDamageAmount > 0f)
        {
            curHealth -= info.hpDamageAmount;
        }

        // 데미지 표시기 트리거
        if (DamageIndicatorManager.Instance != null)
        {
            DamageIndicatorManager.Instance.ShowIndicator(this.transform.position, info.hpDamageAmount, info.damageFlags);
        }

        // 4. 결과 기록 (사망 여부)
        if (curHealth <= 0f)
        {
            curHealth = 0f;
            info.isFatal = true;
        }

        // 5. 공격자의 "공격 직후" 발동 (ex. 흡혈, 대상 처치 시 추가 효과 등)
        if (!info.damageFlags.HasFlag(DamageFlag.NoCasterEvents) && info.caster != null)
        {
            info.caster.eventBus?.Invoke<IOnAfterDealDamage>(l => l.OnAfterDealDamage(info));
            if (info.isFatal)
                info.caster.eventBus?.Invoke<IOnKill>(l => l.OnKill(info));
        }

        // 6. 피격자의 "방어 직후" 발동 (ex. 가시 데미지 반사 등)
        if (!info.damageFlags.HasFlag(DamageFlag.NoTargetEvents))
        {
            this.eventBus?.Invoke<IOnAfterTakeDamage>(l => l.OnAfterTakeDamage(info));
        }

        // 7. 게임 내 사망 확정
        if (info.isFatal)
        {
            isDead = true;
            currentState = CharacterState.Die;
            OnCharacterDied?.Invoke(this);
        }
    }

    public void TakeHeal(HealInfo info)
    {
        if (isDead) return;

        // 힐량 증가/감소 등의 처리
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeGiveHeal>(l => l.OnBeforeGiveHeal(ref info));
        this.eventBus?.Invoke<IOnBeforeTakeHeal>(l => l.OnBeforeTakeHeal(ref info));

        float healAmount = info.baseHealAmount; // 향후 healMultiplier 등 추가 가능

        if (info.healFlags.HasFlag(HealFlag.OverHeal))
        {
            curHealth += healAmount;
        }
        else
        {
            curHealth = Mathf.Min(curCharacterStat.maxHealth, curHealth + healAmount);
        }

        // 힐 표시기 트리거
        if (DamageIndicatorManager.Instance != null)
        {
            DamageIndicatorManager.Instance.ShowHealIndicator(this.transform.position, healAmount);
        }

        // 회복 직후 처리
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnAfterGiveHeal>(l => l.OnAfterGiveHeal(info));
        this.eventBus?.Invoke<IOnAfterTakeHeal>(l => l.OnAfterTakeHeal(info));
    }

    public void TakeStamina(StaminaInfo info)
    {
        if (isDead) return;

        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeTakeStamina>(l => l.OnBeforeTakeStamina(ref info));
        this.eventBus?.Invoke<IOnBeforeTakeStamina>(l => l.OnBeforeTakeStamina(ref info));

        float gainAmount = info.baseStaminaAmount * info.staminaMultiplier;

        if (info.staminaFlags.HasFlag(StaminaFlag.OverStamina))
        {
            curStamina += gainAmount;
        }
        else
        {
            curStamina = Mathf.Min(curCharacterStat.maxStamina, curStamina + gainAmount);
        }

        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnAfterTakeStamina>(l => l.OnAfterTakeStamina(info));
        this.eventBus?.Invoke<IOnAfterTakeStamina>(l => l.OnAfterTakeStamina(info));
    }

    public void SpendStamina(StaminaInfo info)
    {
        if (isDead) return;

        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeSpendStamina>(l => l.OnBeforeSpendStamina(ref info));
        this.eventBus?.Invoke<IOnBeforeSpendStamina>(l => l.OnBeforeSpendStamina(ref info));

        float spendAmount = info.baseStaminaAmount * info.staminaMultiplier;
        curStamina = Mathf.Max(0f, curStamina - spendAmount);

        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnAfterSpendStamina>(l => l.OnAfterSpendStamina(info));
        this.eventBus?.Invoke<IOnAfterSpendStamina>(l => l.OnAfterSpendStamina(info));
    }

    public void TakeShield(ShieldInfo info, int durationTurns = 1)
    {
        if (isDead) return;

        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeGiveShield>(l => l.OnBeforeGiveShield(ref info));
        this.eventBus?.Invoke<IOnBeforeTakeShield>(l => l.OnBeforeTakeShield(ref info));

        float shieldAmount = info.baseShieldAmount * info.shieldMultiplier;
        shield += shieldAmount;

        if (durationTurns > shieldDurationTurns)
        {
            shieldDurationTurns = durationTurns;
        }

        OnCharacterShieldChanged?.Invoke(this);

        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnAfterGiveShield>(l => l.OnAfterGiveShield(info));
        this.eventBus?.Invoke<IOnAfterTakeShield>(l => l.OnAfterTakeShield(info));
    }

    /// <summary>
    /// 턴 종료 시 호출하여 실드 지속 시간을 1 감소시킵니다.
    /// 0 이하가 되면 실드 값을 0으로 만들고 지속 시간을 0으로 리셋합니다.
    /// </summary>
    public void UpdateShieldDuration()
    {
        if (shield > 0f)
        {
            shieldDurationTurns--;
            if (shieldDurationTurns <= 0)
            {
                shield = 0f;
                shieldDurationTurns = 0; // 실드가 만료되었으므로 지속 턴 수 리셋
                OnCharacterShieldChanged?.Invoke(this);
            }
        }
    }

    public void TakeEffect(EffectInfo info)
    {
        if (isDead) return;

        // 1. 시전자(Caster)의 "내가 부여하기 직전" 유물/버프 발동 (ex. 독 부여 시 +1스택)
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeGiveEffect>(l => l.OnBeforeGiveEffect(ref info));

        // 2. 피격자(Target)의 "내가 받기 직전" 유물/버프 발동 (ex. 인공물: 디버프 무효화)
        this.eventBus?.Invoke<IOnBeforeTakeEffect>(l => l.OnBeforeTakeEffect(ref info));

        // 3. 무효화(Cancel) 판정 검사
        if (info.effectFlags.HasFlag(EffectFlag.Cancel))
            return; // 인공물 등에 의해 막혔으므로 종료

        // 4. 무사히 통과했으므로 진짜로 버프 추가 (이때 Added, Stacked 등의 UI 이벤트가 터짐)
        this.effectManager.AddEffect(info.caster, info.effect, info.FinalStack, info.FinalDuration);
        // 5. 부여 직후 파이프라인 (ex. 취약 부여 성공 시 약화도 부여)
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnAfterGiveEffect>(l => l.OnAfterGiveEffect(info));
        this.eventBus?.Invoke<IOnAfterTakeEffect>(l => l.OnAfterTakeEffect(info));
    }

    /// <summary>
    /// 이펙트 ID와 스택, 지속 시간을 기반으로 이펙트를 대상에게 부여합니다.
    /// </summary>
    public void ApplyEffect(Character caster, string effectId, int stack, float duration)
    {
        Effect effect = ModObjectFactory.CreateEffect(effectId);
        if (effect != null)
        {
            TakeEffect(new EffectInfo(caster, this, effect, stack, duration));
        }
    }

    #endregion
}
