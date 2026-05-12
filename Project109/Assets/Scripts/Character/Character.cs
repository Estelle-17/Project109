using EventFlag;
using EventInfo;
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

public class Character : MonoBehaviour
{
    #region CharacterStat

    [SerializeField]
    private CharacterStat _characterStat;

    // ?¸ë????¸ì¶œ?˜ëŠ” ?°í????„ì¬ ?¤íƒ¯
    public CharacterStat curCharacterStat => _characterStat;

    public EffectManager effectManager;
    public CharacterMove characterMove { get; private set; }

    private void Awake()
    {
        characterMove = new CharacterMove(this);
    }

    public void InitializeStat(CharacterStat characterStat)
    {
        if (characterStat != null)
        {
            _characterStat = characterStat;

            curHealth = characterStat.maxHealth;
            curStamina = 0f;
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
    public float curStaminaRate { get { return (_curStamina == 0) ? 0 : _curStamina / curCharacterStat.maxHealth; } }

    [SerializeField]
    public float shield = 0f;

    [SerializeField]
    public int shieldDurationTurns = 1;

    [SerializeField]
    public int currentTurn;

    #endregion

    #region BattleTick

    private bool staminaFullFired = false;

    /// <summary>
    /// BattleManager?ì„œ ë§??„ë ˆ???¸ì¶œ. ë°°ì†???ìš©??dtë¥?ë°›ëŠ”??
    /// </summary>
    public void BattleTick(float dt)
    {
        // ?¤íƒœë¯¸ë‚˜ ?Œë³µ
        curStamina += curCharacterStat.staminaRegenPerSecond * dt;

        // ?´í™????
        effectManager.Tick(dt);

        if (!staminaFullFired && curStamina >= curCharacterStat.maxStamina)
        {
            staminaFullFired = true;
            BattleManager.instance.RequestTurnStart(this);
        }
    }

    /// <summary>
    /// ??ì¢…ë£Œ ???¸ì¶œ?˜ì—¬ ?¤íƒœë¯¸ë‚˜ ë¦¬ì…‹ ë°??Œë˜ê·?ì´ˆê¸°??
    /// </summary>
    public void ResetStamina()
    {
        curStamina = 0;
        staminaFullFired = false;
    }

    #endregion

    #region Damage and Heal Pipeline

    public void TakeDamage(DamageInfo info)
    {
        if (isDead) return;

        // 1. ê³µê²©?ì˜ "ê³µê²© ì§ì „" ë°œë™ (ex. ??Strength) ë²„í”„ë¥??µí•´ baseDamageAmount ì¦ê?)
        if (!info.damageFlags.HasFlag(DamageFlag.NoCasterEvents) && info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeDealDamage>(l => l.OnBeforeDealDamage(ref info));

        // 2. ?¼ê²©?ì˜ "ë°©ì–´ ì§ì „" ?¨ê³¼ ë°œë™ (ex. ?°ë?ì§€ ê²½ê°, ?Œí”¼ ì²˜ë¦¬ ??
        if (!info.damageFlags.HasFlag(DamageFlag.NoTargetEvents))
            this.eventBus?.Invoke<IOnBeforeTakeDamage>(l => l.OnBeforeTakeDamage(ref info));

        // ?Œí”¼?˜ì—ˆ?”ì? ì²´í¬ (?„ì… ë¯¸ì •?¼ë¡œ ì£¼ì„ ì²˜ë¦¬)
        // if (info.isDodged) return;

        float finalDamage = info.baseDamageAmount * info.damageMultiplier;
        info.finalDamageAmount = finalDamage;
        
        info.shieldDamageAmount = 0f;
        info.hpDamageAmount = 0f;

        // 3. ?¤ì œ ?°ë?ì§€ ?ìš©
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
                    info.isShieldBroken = true;
                    OnCharacterShieldChanged?.Invoke(this);

                    // ?¤ë“œ ?Œê´´ ì¦‰ì‹œ ?´ë²¤??ë°œìƒ
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

        // ?¨ì? ?°ë?ì§€ë¥?ì²´ë ¥???ìš©
        if (info.hpDamageAmount > 0f)
        {
            curHealth -= info.hpDamageAmount;
        }

        // 4. ê²°ê³¼ ê¸°ë¡ (?¬ë§ ?¬ë?)
        if (curHealth <= 0f)
        {
            curHealth = 0f;
            info.isFatal = true;
        }

        // 5. ê³µê²©?ì˜ "ê³µê²© ì§í›„" ë°œë™ (ex. ?¡í˜ˆ, ?€??ì²˜ì¹˜ ??ì¶”ê? ?¨ê³¼ ??
        if (!info.damageFlags.HasFlag(DamageFlag.NoCasterEvents) && info.caster != null)
        {
            info.caster.eventBus?.Invoke<IOnAfterDealDamage>(l => l.OnAfterDealDamage(info));
            if (info.isFatal)
                info.caster.eventBus?.Invoke<IOnKill>(l => l.OnKill(info));
        }

        // 6. ?¼ê²©?ì˜ "ë°©ì–´ ì§í›„" ë°œë™ (ex. ê°€???°ë?ì§€ ë°˜ì‚¬ ??
        if (!info.damageFlags.HasFlag(DamageFlag.NoTargetEvents))
        {
            this.eventBus?.Invoke<IOnAfterTakeDamage>(l => l.OnAfterTakeDamage(info));
        }

        // 7. ê²Œì„ ???¬ë§ ?•ì •
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

        // ?ëŸ‰ ì¦ê?/ê°ì†Œ ?±ì˜ ì²˜ë¦¬
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeGiveHeal>(l => l.OnBeforeGiveHeal(ref info));
        this.eventBus?.Invoke<IOnBeforeTakeHeal>(l => l.OnBeforeTakeHeal(ref info));

        float healAmount = info.baseHealAmount; // ?¥í›„ healMultiplier ??ì¶”ê? ê°€??

        if (info.healFlags.HasFlag(HealFlag.OverHeal))
        {
            curHealth += healAmount;
        }
        else
        {
            curHealth = Mathf.Min(curCharacterStat.maxHealth, curHealth + healAmount);
        }

        // ?Œë³µ ì§í›„ ì²˜ë¦¬
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

    public void TakeEffect(EffectInfo info)
    {
        if (isDead) return;

        // 1. ?œì „??Caster)??"?´ê? ë¶€?¬í•˜ê¸?ì§ì „" ? ë¬¼/ë²„í”„ ë°œë™ (ex. ??ë¶€????+1?¤íƒ)
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnBeforeApplyEffect>(l => l.OnBeforeApplyEffect(ref info));

        // 2. ?¼ê²©??Target)??"?´ê? ë°›ê¸° ì§ì „" ? ë¬¼/ë²„í”„ ë°œë™ (ex. ?¸ê³µë¬? ?”ë²„??ë¬´íš¨??
        this.eventBus?.Invoke<IOnBeforeApplyEffect>(l => l.OnBeforeApplyEffect(ref info));

        // 3. ë¬´íš¨??Cancel) ?ì • ê²€??
        if (info.effectFlags.HasFlag(EffectFlag.Cancel))
            return; // ?¸ê³µë¬??±ì— ?˜í•´ ë§‰í˜”?¼ë?ë¡?ì¢…ë£Œ

        // 4. ë¬´ì‚¬???µê³¼?ˆìœ¼ë¯€ë¡?ì§„ì§œë¡?ë²„í”„ ì¶”ê? (?´ë•Œ Added, Stacked ?±ì˜ UI ?´ë²¤?¸ê? ?°ì§)
        this.effectManager.AddEffect(info.caster, info.effect, info.FinalStack, info.FinalDuration);

        // 5. ë¶€??ì§í›„ ?Œì´?„ë¼??(ex. ì·¨ì•½ ë¶€???±ê³µ ???½í™”??ë¶€??
        if (info.caster != null)
            info.caster.eventBus?.Invoke<IOnAfterApplyEffect>(l => l.OnAfterApplyEffect(info));
        this.eventBus?.Invoke<IOnAfterApplyEffect>(l => l.OnAfterApplyEffect(info));
    }

    #endregion
}
