using EventInfo;
using System.Collections.Generic;

public interface ICharacterEvent { }

#region ?°λ?μ§€ κ΄€???Έν„°?μ΄??(Damage Interfaces)

    public interface IOnBeforeDealDamage : ICharacterEvent { void OnBeforeDealDamage(ref DamageInfo info); }
    public interface IOnBeforeTakeDamage : ICharacterEvent { void OnBeforeTakeDamage(ref DamageInfo info); }
    public interface IOnAfterDealDamage : ICharacterEvent { void OnAfterDealDamage(DamageInfo info); }
    public interface IOnAfterTakeDamage : ICharacterEvent { void OnAfterTakeDamage(DamageInfo info); }
    public interface IOnBreakShield : ICharacterEvent { void OnBreakShield(DamageInfo info); }
    public interface IOnShieldBroken : ICharacterEvent { void OnShieldBroken(DamageInfo info); }
    public interface IOnKill : ICharacterEvent { void OnKill(DamageInfo info); }

#endregion

#region ?λ³µ κ΄€???Έν„°?μ΄??(Heal Interfaces)

    public interface IOnBeforeGiveHeal : ICharacterEvent { void OnBeforeGiveHeal(ref HealInfo info); }
    public interface IOnBeforeTakeHeal : ICharacterEvent { void OnBeforeTakeHeal(ref HealInfo info); }
    public interface IOnAfterGiveHeal : ICharacterEvent  { void OnAfterGiveHeal(HealInfo info); }
    public interface IOnAfterTakeHeal : ICharacterEvent  { void OnAfterTakeHeal(HealInfo info); }

#endregion

#region λ°©μ–΄??κ΄€???Έν„°?μ΄??(Shield/Armor Interfaces)

    public interface IOnBeforeGiveShield : ICharacterEvent { void OnBeforeGiveShield(ref ShieldInfo info); }
    public interface IOnBeforeTakeShield : ICharacterEvent { void OnBeforeTakeShield(ref ShieldInfo info); }
    public interface IOnAfterGiveShield : ICharacterEvent  { void OnAfterGiveShield(ShieldInfo info); }
    public interface IOnAfterTakeShield : ICharacterEvent  { void OnAfterTakeShield(ShieldInfo info); }

#endregion

#region ?¤νƒλ―Έλ‚ κ΄€???Έν„°?μ΄??(Stamina Interfaces)

    public interface IOnBeforeSpendStamina : ICharacterEvent { void OnBeforeSpendStamina(ref StaminaInfo info); }
    public interface IOnAfterSpendStamina : ICharacterEvent  { void OnAfterSpendStamina(StaminaInfo info); }
    public interface IOnBeforeTakeStamina : ICharacterEvent  { void OnBeforeTakeStamina(ref StaminaInfo info); }
    public interface IOnAfterTakeStamina : ICharacterEvent   { void OnAfterTakeStamina(StaminaInfo info); }

#endregion

#region ?΄λ™ κ΄€???Έν„°?μ΄??(Movement Interfaces)

    public interface IOnBeforeMove : ICharacterEvent { void OnBeforeMove(MoveInfo info); }
    public interface IOnAfterMove : ICharacterEvent  { void OnAfterMove(MoveInfo info); }
    public interface IOnBeforeForcedMove : ICharacterEvent { void OnBeforeForcedMove(MoveInfo info); }
    public interface IOnAfterForcedMove : ICharacterEvent { void OnAfterForcedMove(MoveInfo info); }

#endregion

#region ?„ν¬ κ΄€???Έν„°?μ΄??(Core State Interfaces)

    public interface IOnBattleStart : ICharacterEvent { void OnBattleStart(); }
    public interface IOnBattleEnd : ICharacterEvent   { void OnBattleEnd(); }
    public interface IOnTurnStart : ICharacterEvent { void OnTurnStart(); }
    public interface IOnTurnEnd : ICharacterEvent   { void OnTurnEnd(); }
    public interface IOnDeath : ICharacterEvent     { void OnDeath(DamageInfo fatalDamageInfo); }

#endregion

#region μΉ΄λ“ κ΄€???Έν„°?μ΄??(Card Interfaces)

    public interface IOnBeforeUseCard : ICharacterEvent { void OnBeforeUseCard(CardInfo info); }
    public interface IOnAfterUseCard : ICharacterEvent  { void OnAfterUseCard(CardInfo info); }
    public interface IOnTryUseCard : ICharacterEvent { void OnTryUseCard(CardInfo info); }
    public interface IOnDiscardCard : ICharacterEvent { void OnDiscardCard(CardInfo info); }
    public interface IOnDrawCard : ICharacterEvent { void OnDrawCard(ActionCardData cardData); }
    public interface IOnExhaustCard : ICharacterEvent { void OnExhaustCard(CardInfo info); }
    public interface IOnShuffleDeck : ICharacterEvent { void OnShuffleDeck(List<ActionCardData> deck); }
    public interface IOnEraseCard : ICharacterEvent { void OnEraseCard(CardInfo info); }

#endregion

#region ?΄ν™??λ²„ν”„/?”λ²„?? κ΄€???Έν„°?μ΄??(Effect Interfaces)

    public interface IOnBeforeApplyEffect : ICharacterEvent { void OnBeforeApplyEffect(ref EffectInfo info); }
    public interface IOnAfterApplyEffect : ICharacterEvent  { void OnAfterApplyEffect(EffectInfo info); }
    public interface IOnBeforeRemoveEffect : ICharacterEvent { void OnBeforeRemoveEffect(EffectBase effect); }
    public interface IOnAfterRemoveEffect : ICharacterEvent  { void OnAfterRemoveEffect(EffectBase effect); }

#endregion
