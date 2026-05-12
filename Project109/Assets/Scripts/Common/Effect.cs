using System.Collections.Generic;

public enum EffectType
{
    Buff,
    Debuff
}

public class EffectBase
{
    protected Character caster;
    protected Character target;
    public const float tickInterval = 1f;
    public float elapsedSinceLastTick;
    public int id;
    public int currentStack;
    public int maxStack = int.MaxValue;
    public float duration;
    public float currentDuration;
    public bool isPermanent;
    public EffectType effectType;

    public virtual void OnAdded(Character caster, Character target, int stack, float duration)
    {
        this.caster = caster;
        this.target = target;
        this.currentStack = stack;
        this.duration = duration;
        this.currentDuration = duration;
    }

    public virtual void OnStacked(int stack, float duration) 
    {
        currentStack = System.Math.Min(currentStack + stack, maxStack);
        this.currentDuration = duration;
    }

    public virtual void OnTick() { }

    public virtual void OnTimeOut()
    {
        currentStack--;
        if (currentStack > 0)   
        {
            currentDuration = duration;
        }
    }

    public virtual void OnRemoved()
    {
        caster = null;
        target = null;
    }
}
