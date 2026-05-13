using UnityEngine;
using EventStructs;

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
        // 내가 공격자가 되어 데미지를 가하기 직전, 현재 스택 수치만큼 데미지를 증가시켜준다.
        info.baseDamageAmount += currentStack;
    }

    public override void OnRemoved()
    {
        target.eventBus.Remove<IOnBeforeDealDamage>(this);
        base.OnRemoved();
    }
}
