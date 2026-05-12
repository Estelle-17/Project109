using System;
using UnityEngine;

namespace EventFlag
{
    [Flags]
    public enum DamageFlag
    {
        Normal = 0,
        IgnoreArmor = 1 << 0,     // ?¥ê°‘/ë°©ì–´??ë¬´ì‹œ
        IgnoreShield = 1 << 1,    // ?´ë“œ ë¸”ë¡ ë¬´ì‹œ (ex. ?? ê´€??
        NoCasterEvents = 1 << 2,  // ê³µê²©?ì˜ ì½œë°± ?´ë²¤???? ?¡í˜ˆ ?? ë¬´ë°œ??
        NoTargetEvents = 1 << 3,  // ?¼ê²©?ì˜ ì½œë°± ?´ë²¤??ê°€?? ?¼ê²©???¨ê³¼) ë¬´ë°œ??

        // --- ?¸ì˜??ì¡°í•© ?Œëž˜ê·?(Presets) ---
        Reflected = NoCasterEvents | NoTargetEvents,                      // ê°€??ë°˜ì‚¬ ?°ë?ì§€
        HPLoss = IgnoreShield | NoCasterEvents | NoTargetEvents,          // ?œìˆ˜ ì²´ë ¥ ?ì‹¤ (????
    }

    [Flags]
    public enum HealFlag
    {
        Normal = 0,
        OverHeal = 1 << 0,       // ìµœë? ?ëª…?¥ì„ ì´ˆê³¼?´ì„œ ?Œë³µ
        NoCasterEvents = 1 << 1, // ?œì „?ì˜ ?Œë³µ ì¦ê? ë²„í”„ ??ë¬´ì‹œ
        NoTargetEvents = 1 << 2, // ?€?ì˜ ?¼ê²©/?Œë³µ ? ë¬¼ ??ë¬´ì‹œ

        // --- ?¸ì˜??ì¡°í•© ?Œëž˜ê·?---
        Regen = NoCasterEvents | NoTargetEvents, // ?¼ë°˜ ?Œë³µ???„ë‹Œ ?¬ìƒ/???Œë³µ
    }

    [Flags]
    public enum StaminaFlag
    {
        Normal = 0,
        OverStamina = 1 << 0,
        NoCasterEvents = 1 << 1,
        NoTargetEvents = 1 << 2,

        // --- ?¸ì˜??ì¡°í•© ?Œëž˜ê·?---
        Regen = NoCasterEvents | NoTargetEvents,
    }

    [Flags]
    public enum ShieldFlag
    {
        Normal = 0,
        NoCasterEvents = 1 << 1,
        NoTargetEvents = 1 << 2,
    }

    [Flags]
    public enum CardFlag
    {
        Normal = 0,
        NoExhaust = 1 << 0,      // ì¹´ë“œê°€ ?ëž˜ ê°€ì§€ê³??ˆëŠ” ?Œë©¸ ë¬´ì‹œ
        NoDiscard = 1 << 1,      // ?¬ìš© ??ë¬˜ì?ë¡?ê°€ì§€ ?ŠìŒ (?¹ìˆ˜ ì²˜ë¦¬??
        NoCasterEvents = 1 << 2, // ì¹´ë“œ ?¬ìš© ê´€???´ë²¤??ë°œë™ ?ˆí•¨
        FreeToPlay = 1 << 3,         // ì½”ìŠ¤???Œëª¨ ?†ì´ ?¬ìš©??(?´ê±´ ?…ë¦½ ?Œëž˜ê·¸ë¡œ ??

        // --- ?¸ì˜??ì¡°í•© ?Œëž˜ê·?---
        AutoPlayed = NoCasterEvents, // ê°•ì œ ?œì „
    }

    [Flags]
    public enum MoveFlag
    {
        Normal = 0,
        Teleport = 1 << 0,       // ?´ë™ ê²½ë¡œ???¨ì •/?¨ê³¼ ë¬´ì‹œ (?œê°„?´ë™)
        Forced = 1 << 1,         // ë°€ì¹˜ê¸°, ?¹ê¸°ê¸???ê°•ì œ ?´ë™
        NoCasterEvents = 1 << 2,
    }

    [Flags]
    public enum EffectFlag
    {
        Normal = 0,
        Unremovable = 1 << 0,    // '?”ë²„???´ì œ' ê³„ì—´ë¡?ì§€?Œì?ì§€ ?ŠëŠ” ê³ ìœ /?êµ¬ ë²„í”„
        NoTargetEvents = 1 << 1, // ?€?ì˜ "ë²„í”„ë¥?ë°›ì„ ?? ?°ì????´ë²¤??? ë¬¼ ?? ë¬´ì‹œ
        Cancel = 1 << 2,         // ?Œì´?„ë¼???„ì¤‘ ë¶€?¬ê? ì·¨ì†Œ??
    }
}

