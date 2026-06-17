using XLua;
using System.Collections.Generic;

public static partial class LuaEventBinder
{
    // =========================================================
    // 1. Character Events (전투 및 캐릭터 상태 관련)
    // =========================================================
    public static partial void BindCharacterEvents(LuaTable luaTable, EventBus<ICharacterEvent> eventBus, List<object> activeProxies);
    public static partial void UnbindCharacterEvents(EventBus<ICharacterEvent> eventBus, List<object> activeProxies);

    // =========================================================
    // 2. Player Events (게임 진행, 덱, 재화 관련)
    // =========================================================
    public static partial void BindPlayerEvents(LuaTable luaTable, EventBus<IPlayerEvent> eventBus, List<object> activeProxies);
    public static partial void UnbindPlayerEvents(EventBus<IPlayerEvent> eventBus, List<object> activeProxies);


    // =========================================================
    // 3. Helper Methods (내부 동작용)
    // =========================================================
    
    // 제네릭과 리플렉션 없이 안전하게 바인딩을 수행하는 헬퍼 함수
    private static void BindCharacter<T>(LuaTable luaTable, EventBus<ICharacterEvent> eventBus, List<object> activeProxies) where T : class, ICharacterEvent
    {
        // 인터페이스 이름에서 'I'를 제외한 문자열 추출 (예: IOnBattleStart -> OnBattleStart)
        string methodName = typeof(T).Name.Substring(1);
        
        // Lua 스크립트에 해당 이름의 함수가 선언되어 있다면
        if (luaTable.ContainsKey(methodName))
        {
            // 프록시 객체를 생성하고 구독
            var proxy = luaTable.Cast<T>();
            eventBus.Add<T>(proxy);
            activeProxies.Add(proxy);
        }
    }

    private static void BindPlayer<T>(LuaTable luaTable, EventBus<IPlayerEvent> eventBus, List<object> activeProxies) where T : class, IPlayerEvent
    {
        string methodName = typeof(T).Name.Substring(1);
        
        if (luaTable.ContainsKey(methodName))
        {
            var proxy = luaTable.Cast<T>();
            eventBus.Add<T>(proxy);
            activeProxies.Add(proxy);
        }
    }
}
