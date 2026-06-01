using XLua;
using System.Collections.Generic;

public static class LuaEventBinder
{
    // =========================================================
    // 1. Character Events (전투 및 캐릭터 상태 관련)
    // =========================================================
    public static void BindCharacterEvents(LuaTable luaTable, EventBus<ICharacterEvent> eventBus, List<object> activeProxies)
    {
        if (luaTable == null || eventBus == null) return;

        // Damage Interfaces
        BindCharacter<IOnBeforeDealDamage>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeTakeDamage>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterDealDamage>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterTakeDamage>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBreakShield>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnShieldBroken>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnKill>(luaTable, eventBus, activeProxies);

        // Heal Interfaces
        BindCharacter<IOnBeforeGiveHeal>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeTakeHeal>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterGiveHeal>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterTakeHeal>(luaTable, eventBus, activeProxies);

        // Shield Interfaces
        BindCharacter<IOnBeforeGiveShield>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeTakeShield>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterGiveShield>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterTakeShield>(luaTable, eventBus, activeProxies);

        // Stamina Interfaces
        BindCharacter<IOnBeforeSpendStamina>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterSpendStamina>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeTakeStamina>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterTakeStamina>(luaTable, eventBus, activeProxies);

        // Movement Interfaces
        BindCharacter<IOnBeforeMove>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterMove>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeForcedMove>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterForcedMove>(luaTable, eventBus, activeProxies);

        // Core State Interfaces
        BindCharacter<IOnBattleStart>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBattleEnd>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnTurnStart>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnTurnEnd>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnDeath>(luaTable, eventBus, activeProxies);

        // Card Interfaces
        BindCharacter<IOnBeforeUseCard>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterUseCard>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnTryUseCard>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnDiscardCard>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnDrawCard>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnExhaustCard>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnShuffleDeck>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnEraseCard>(luaTable, eventBus, activeProxies);

        // Effect Interfaces
        BindCharacter<IOnBeforeGiveEffect>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeTakeEffect>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterGiveEffect>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterTakeEffect>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnBeforeRemoveEffect>(luaTable, eventBus, activeProxies);
        BindCharacter<IOnAfterRemoveEffect>(luaTable, eventBus, activeProxies);
    }

    public static void UnbindCharacterEvents(EventBus<ICharacterEvent> eventBus, List<object> activeProxies)
    {
        if (eventBus == null) return;

        foreach (var proxy in activeProxies)
        {
            if (proxy == null) continue;
            var interfaces = proxy.GetType().GetInterfaces();
            foreach (var iface in interfaces)
            {
                if (typeof(ICharacterEvent).IsAssignableFrom(iface) && iface != typeof(ICharacterEvent))
                {
                    try
                    {
                        var removeMethod = eventBus.GetType().GetMethod("Remove").MakeGenericMethod(iface);
                        removeMethod.Invoke(eventBus, new object[] { proxy });
                    }
                    catch (System.Exception e)
                    {
                        UnityEngine.Debug.LogError($"[LuaEventBinder] 캐릭터 이벤트 해제 실패 ({iface.Name}): {e.Message}");
                    }
                }
            }
        }
        activeProxies.Clear();
    }


    // =========================================================
    // 2. Player Events (게임 진행, 덱, 재화 관련)
    // =========================================================
    public static void BindPlayerEvents(LuaTable luaTable, EventBus<IPlayerEvent> eventBus, List<object> activeProxies)
    {
        if (luaTable == null || eventBus == null) return;

        BindPlayer<IOnAddRelic>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnRemoveRelic>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnAddCard>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnRemoveCard>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnCardUpgrade>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnCardMasteryUpgrade>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnCardsRefreshed>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnAddGold>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnRemoveGold>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnAddMemorySharp>(luaTable, eventBus, activeProxies);
        BindPlayer<IOnRemoveMemorySharp>(luaTable, eventBus, activeProxies);
    }

    public static void UnbindPlayerEvents(EventBus<IPlayerEvent> eventBus, List<object> activeProxies)
    {
        if (eventBus == null) return;

        foreach (var proxy in activeProxies)
        {
            if (proxy == null) continue;
            var interfaces = proxy.GetType().GetInterfaces();
            foreach (var iface in interfaces)
            {
                if (typeof(IPlayerEvent).IsAssignableFrom(iface) && iface != typeof(IPlayerEvent))
                {
                    try
                    {
                        var removeMethod = eventBus.GetType().GetMethod("Remove").MakeGenericMethod(iface);
                        removeMethod.Invoke(eventBus, new object[] { proxy });
                    }
                    catch (System.Exception e)
                    {
                        UnityEngine.Debug.LogError($"[LuaEventBinder] 플레이어 이벤트 해제 실패 ({iface.Name}): {e.Message}");
                    }
                }
            }
        }
        activeProxies.Clear();
    }


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
