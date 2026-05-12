using UnityEngine;
using EventInfo;
using EventFlag;

public class StrengthEffect : EffectBase, IOnBeforeDealDamage
{
    public override void OnAdded(Character caster, Character target, int stack, float duration)
    {
        base.OnAdded(caster, target, stack, duration);
        id = 1;
        effectType = EffectType.Buff;
        isPermanent = true;
        
        target.eventBus.Add<IOnBeforeDealDamage>(this);
    }

    public void OnBeforeDealDamage(ref DamageInfo info)
    {
        // ??ê³µê²©??ê°€ ?°ë?ì§€ë¥?ê°€?˜ê¸° ì§ì „, ???©ì—°????ì¦ê???strength)???”í•´ì¤€??
        info.baseDamageAmount += currentStack;
    }

    public override void OnRemoved()
    {
        target.eventBus.Remove<IOnBeforeDealDamage>(this);
        base.OnRemoved();
    }
}
