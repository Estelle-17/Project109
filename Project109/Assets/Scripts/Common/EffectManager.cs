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
        var existing = effects.Find(e => e.id == effect.id);
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
            if(effects[i].isPermanent) continue;

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
