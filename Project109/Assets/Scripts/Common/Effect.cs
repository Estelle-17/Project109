using System;
using System.Collections.Generic;
using XLua;

public enum EffectType
{
    Buff,
    Debuff
}

public class EffectBase
{
    // Lua 스크립트에서 접근할 수 있도록 getter를 public으로 개방
    public Character caster { get; protected set; }
    public Character target { get; protected set; }
    
    public const float tickInterval = 1f;
    public float elapsedSinceLastTick;
    public int currentStack;
    public float duration;
    public float currentDuration;

    public EffectData Data { get; private set; }
    private LuaTable luaTable;
    private List<object> activeProxies = new List<object>();

    // 데이터 기반(모딩) 이펙트용 단일 생성자
    public EffectBase(EffectData data, LuaTable luaLogic)
    {
        this.Data = data;
        this.luaTable = luaLogic;

        // 추가적인 Lua 자체 초기화 호출
        var luaOnInit = luaTable?.Get<Action<LuaTable, EffectBase>>("OnInit");
        luaOnInit?.Invoke(luaTable, this);
    }

    public virtual void OnAdded(Character caster, Character target, int stack, float duration)
    {
        this.caster = caster;
        this.target = target;
        this.currentStack = stack;
        this.duration = duration;
        this.currentDuration = duration;

        if (luaTable != null)
        {
            // 1. Lua 측 OnAdded 기본 로직 실행
            var luaOnAdded = luaTable.Get<Action<LuaTable, EffectBase, Character, Character, int, float>>("OnAdded");
            luaOnAdded?.Invoke(luaTable, this, caster, target, stack, duration);

            // 2. 캐릭터 이벤트 버스 자동 바인딩
            if (target != null && target.eventBus != null)
            {
                LuaEventBinder.BindCharacterEvents(luaTable, target.eventBus, activeProxies);
            }
        }
    }

    public virtual void OnStacked(int stack, float duration) 
    {
        int maxStack = Data != null ? Data.maxStack : int.MaxValue;
        currentStack = System.Math.Min(currentStack + stack, maxStack);
        this.currentDuration = duration;

        var luaOnStacked = luaTable?.Get<Action<LuaTable, EffectBase, int, float>>("OnStacked");
        luaOnStacked?.Invoke(luaTable, this, stack, duration);
    }

    public virtual void OnTick() 
    { 
        var luaOnTick = luaTable?.Get<Action<LuaTable, EffectBase>>("OnTick");
        luaOnTick?.Invoke(luaTable, this);
    }

    public virtual void OnTimeOut()
    {
        currentStack--;
        if (currentStack > 0)   
        {
            currentDuration = duration;
        }

        var luaOnTimeOut = luaTable?.Get<Action<LuaTable, EffectBase>>("OnTimeOut");
        luaOnTimeOut?.Invoke(luaTable, this);
    }

    public virtual void OnRemoved()
    {
        if (luaTable != null)
        {
            // 1. Lua 측 OnRemoved 실행
            var luaOnRemoved = luaTable.Get<Action<LuaTable, EffectBase>>("OnRemoved");
            luaOnRemoved?.Invoke(luaTable, this);

            // 2. 이벤트 버스 구독 해제
            if (target != null && target.eventBus != null)
            {
                LuaEventBinder.UnbindCharacterEvents(target.eventBus, activeProxies);
            }
        }

        // 3. 리소스 정리
        activeProxies.Clear();
        luaTable?.Dispose();

        caster = null;
        target = null;
    }
}
