using System;
using System.Collections.Generic;
using XLua;
using EventStructs;

public class RelicBase : IDescribable
{
    public RelicData Data { get; private set; }
    public int Counter { get; set; }

    private Player owner;
    private LuaTable luaLogic;
    
    // 이벤트 구독 해제를 위해 만들어진 프록시 객체들을 담아둘 리스트
    private List<object> activeProxies = new List<object>();

    public RelicBase(RelicData data, Player owner, LuaTable luaLogic)
    {
        this.Data = data;
        this.owner = owner;
        this.luaLogic = luaLogic;

        // 1. Lua 측 OnInit 함수 호출 (초기화, 카운터 세팅 등)
        var luaOnInit = luaLogic.Get<Action<LuaTable, RelicBase, Player>>("OnInit");
        luaOnInit?.Invoke(luaLogic, this, owner);

        // 2. 캐릭터 이벤트 (전투, 체력, 이동 등) 자동 바인딩
        if (owner.character != null)
        {
            LuaEventBinder.BindCharacterEvents(luaLogic, owner.character.eventBus, activeProxies);
        }

        // 3. 플레이어 이벤트 (골드, 카드, 유물 획득 등) 자동 바인딩
        if (owner.eventBus != null)
        {
            LuaEventBinder.BindPlayerEvents(luaLogic, owner.eventBus, activeProxies);
        }
    }

    public void Dispose()
    {
        // 1. Lua 측 OnRemoved 함수 호출 (유물이 파괴되거나 게임오버 시)
        var luaOnRemoved = luaLogic.Get<Action<LuaTable, RelicBase, Player>>("OnRemoved");
        luaOnRemoved?.Invoke(luaLogic, this, owner);

        // 2. 이벤트 버스 구독 해제
        if (owner.character != null)
        {
            LuaEventBinder.UnbindCharacterEvents(owner.character.eventBus, activeProxies);
        }
        
        if (owner.eventBus != null)
        {
            LuaEventBinder.UnbindPlayerEvents(owner.eventBus, activeProxies);
        }

        // 3. 리소스 정리
        activeProxies.Clear();
        luaLogic?.Dispose();
    }

    /// <summary>
    /// 현재 Counter 수치를 반영한 효과 설명 텍스트를 반환합니다.
    /// Lua에 GetDescription 함수가 정의되어 있으면 Lua 결과를,
    /// 없으면 YAML description의 {Counter} 플레이스홀더를 치환한 값을 반환합니다.
    /// </summary>
    public string GetDescription()
    {
        var luaFunc = luaLogic?.Get<Func<LuaTable, RelicBase, string>>("GetDescription");
        if (luaFunc != null)
            return luaFunc(luaLogic, this);

        return Data?.description
            ?.Replace("{Counter}", Counter.ToString())
            ?? string.Empty;
    }

    /// <summary>
    /// 유물의 flavorText(lore/풍미 텍스트)를 반환합니다.
    /// </summary>
    public string GetFlavorText() => Data?.flavorText ?? string.Empty;
}
