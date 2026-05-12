using EventFlag;
using System.Collections;
using UnityEngine;

namespace EventInfo
{
    public enum DefaultDamageTypeID
    {
        Normal = 0,
        Magic,
    }
    
    public struct DamageInfo
    {
        public Character caster;
        public Character target;
        
        public float originalDamageAmount;
        public float baseDamageAmount;
        public float finalDamageAmount;
        public int damageTypeID;
        public DamageFlag damageFlags;
        
        public int armorPiercing;
        public float damageMultiplier;
        public float shieldMultiplier;
        public float healthMultiplier;

        // ?°ë?ì§€ ?¸ë? ?•ë³´ (Damage breakdown)
        public float shieldDamageAmount;
        public float hpDamageAmount;
        
        // ê²°ê³¼ ?ìˆ˜ì¦?(Result Flags)
        public bool isFatal;
        public bool isShieldBroken;
        public bool isBlocked;
        // ?„ì´???¬ë¦¬?°ì»¬ ??(ë¯¸ì • ?”ì†Œ?´ë?ë¡??„ì‹œ ì£¼ì„ ì²˜ë¦¬)
        // public bool isCritical;
        // public bool isDodged;

        public DamageInfo(Character caster, Character target, float baseAmount, DamageFlag flags = DamageFlag.Normal) : this()
        {
            this.caster = caster;
            this.target = target;
            this.originalDamageAmount = baseAmount;
            this.baseDamageAmount = baseAmount;
            this.damageFlags = flags;
            this.damageMultiplier = 1.0f;
            this.shieldMultiplier = 1.0f;
            this.healthMultiplier = 1.0f;
        }
    }

    public struct HealInfo 
    {
        public Character caster;
        public Character target;

        public float originalHealAmount;
        public float baseHealAmount;
        public float finalHealAmount;
        public float healMultiplier;
        public HealFlag healFlags;

        public HealInfo(Character caster, Character target, float baseAmount, HealFlag flags = HealFlag.Normal) : this()
        {
            this.caster = caster;
            this.target = target;
            this.originalHealAmount = baseAmount;
            this.baseHealAmount = baseAmount;
            this.healFlags = flags;
            this.healMultiplier = 1.0f;
        }
    }

    public struct StaminaInfo 
    {
        public Character caster;
        public Character target;
        
        public float originalStaminaAmount;
        public float baseStaminaAmount;
        public float finalStaminaAmount;
        public float staminaMultiplier;
        public StaminaFlag staminaFlags;

        public StaminaInfo(Character caster, Character target, float baseAmount, StaminaFlag flags = StaminaFlag.Normal) : this()
        {
            this.caster = caster;
            this.target = target;
            this.originalStaminaAmount = baseAmount;
            this.baseStaminaAmount = baseAmount;
            this.staminaFlags = flags;
            this.staminaMultiplier = 1.0f;
        }
    }
    
    public struct ShieldInfo
    {
        public Character caster;
        public Character target;
        
        public float originalShieldAmount;
        public float baseShieldAmount;
        public float finalShieldAmount;
        public float shieldMultiplier;
        public ShieldFlag shieldFlags;

        public ShieldInfo(Character caster, Character target, float baseAmount, ShieldFlag flags = ShieldFlag.Normal) : this()
        {
            this.caster = caster;
            this.target = target;
            this.originalShieldAmount = baseAmount;
            this.baseShieldAmount = baseAmount;
            this.shieldFlags = flags;
            this.shieldMultiplier = 1.0f;
        }
    }

    public struct CardInfo
    {
        public Character caster;
        // ê²©ì ê²Œì„?´ë?ë¡??¹ì • ìºë¦­???˜ë‚˜ê°€ ?„ë‹Œ, ë²”ìœ„ ???¤ìˆ˜ ?¹ì? ë¹?ë§??€ê²ŸíŒ… ê³ ë ¤
        public System.Collections.Generic.List<Character> targets;
        public Vector2Int targetPosition; 
        
        public object cardData; 
        public CardFlag cardFlags;

        public CardInfo(Character caster, System.Collections.Generic.List<Character> targets, Vector2Int targetPos, object cardData, CardFlag flags = CardFlag.Normal) : this()
        {
            this.caster = caster;
            this.targets = targets;
            this.targetPosition = targetPos;
            this.cardData = cardData;
            this.cardFlags = flags;
        }
    }

    public struct MoveInfo
    {
        public Character mover;
        public Vector2Int fromUnscaled;
        public Vector2Int toUnscaled;
        public MoveFlag moveFlags;
        public bool isCanceled;
        
        // ?´ë™ ê±°ë¦¬, ?€?????±ì˜ ë°°ìˆ˜ ì²˜ë¦¬ë¥??„í•œ multiplier ì¶”ê? ê°€??
        public float moveMultiplier;

        public MoveInfo(Character mover, Vector2Int from, Vector2Int to, MoveFlag flags = MoveFlag.Normal) : this()
        {
            this.mover = mover;
            this.fromUnscaled = from;
            this.toUnscaled = to;
            this.moveFlags = flags;
            this.moveMultiplier = 1.0f;
        }
    }

    public struct EffectInfo
    {
        public Character caster;
        public Character target;
        public EffectBase effect;
        
        public int baseStack;
        public float baseDuration;
        public int bonusStack;
        public float durationMultiplier;

        public EffectFlag effectFlags;

        public EffectInfo(Character caster, Character target, EffectBase effect, int stack = 1, float duration = 0f, EffectFlag flags = EffectFlag.Normal) : this()
        {
            this.caster = caster;
            this.target = target;
            this.effect = effect;
            this.baseStack = stack;
            this.baseDuration = duration;
            this.bonusStack = 0;
            this.durationMultiplier = 1.0f;
            this.effectFlags = flags;
        }

        // ?ìš©??ìµœì¢… ê²°ê³¼ê°’ì„ ê³„ì‚°?˜ëŠ” ?„ë¡œ?¼í‹°
        public int FinalStack => baseStack + bonusStack;
        public float FinalDuration => baseDuration * durationMultiplier;
    }
}
