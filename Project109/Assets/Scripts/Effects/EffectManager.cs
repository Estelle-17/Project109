using System;
using System.Collections.Generic;

public class EffectManager
{
    public event Action<EffectBase> OnEffectAdded;
    public event Action<EffectBase> OnEffectStacked;
    public event Action<EffectBase> OnEffectRemoved;

    private List<EffectBase> effects = new List<EffectBase>();

    private Character target;

    public EffectManager(Character character)
    {
        this.target = character;
    }

    public void AddEffect(Character caster, EffectBase effect, int stack = 1, float duration = 0f)
    {
        EffectBase existing = null;
        
        // 독립적인 버프(isIndependent)가 아닐 때만 기존 동일 이름의 버프를 찾아서 스택을 쌓음
        if (effect.Data == null || !effect.Data.isIndependent)
        {
            existing = effects.Find(e => e.Data != null && e.Data.effectName == effect.Data.effectName);
        }

        if (existing != null)
        {
            existing.OnStacked(stack, duration);
            OnEffectStacked?.Invoke(existing);
        }
        else
        {
            effects.Add(effect);
            effect.OnAdded(caster, target, stack, duration);
            OnEffectAdded?.Invoke(effect);
        }
    }

    public void RemoveEffect(EffectBase effect)
    {
        if (effects.Remove(effect))
        {
            effect.OnRemoved();
            OnEffectRemoved?.Invoke(effect);
        }
    }

    public void Tick(float deltaTime)
    {
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            if(effects[i].Data != null && effects[i].Data.isPermanent) continue;

            effects[i].elapsedSinceLastTick += deltaTime;
            if (effects[i].elapsedSinceLastTick >= EffectBase.tickInterval)
            {
                effects[i].OnTick();
                effects[i].elapsedSinceLastTick -= EffectBase.tickInterval;
            }

            effects[i].currentDuration -= deltaTime;
            if (effects[i].currentDuration <= 0f)
            {
                effects[i].OnTimeOut();
                if (effects[i].currentStack <= 0)
                {
                    EffectBase removedEffect = effects[i];
                    removedEffect.OnRemoved();
                    effects.RemoveAt(i);
                    OnEffectRemoved?.Invoke(removedEffect);
                }
            }
        }
    }
}
