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
            if (proxy is IOnBeforeDealDamage p1) eventBus.Remove<IOnBeforeDealDamage>(p1);
            else if (proxy is IOnBeforeTakeDamage p2) eventBus.Remove<IOnBeforeTakeDamage>(p2);
            else if (proxy is IOnAfterDealDamage p3) eventBus.Remove<IOnAfterDealDamage>(p3);
            else if (proxy is IOnAfterTakeDamage p4) eventBus.Remove<IOnAfterTakeDamage>(p4);
            else if (proxy is IOnBreakShield p5) eventBus.Remove<IOnBreakShield>(p5);
            else if (proxy is IOnShieldBroken p6) eventBus.Remove<IOnShieldBroken>(p6);
            else if (proxy is IOnKill p7) eventBus.Remove<IOnKill>(p7);
            
            else if (proxy is IOnBeforeGiveHeal p8) eventBus.Remove<IOnBeforeGiveHeal>(p8);
            else if (proxy is IOnBeforeTakeHeal p9) eventBus.Remove<IOnBeforeTakeHeal>(p9);
            else if (proxy is IOnAfterGiveHeal p10) eventBus.Remove<IOnAfterGiveHeal>(p10);
            else if (proxy is IOnAfterTakeHeal p11) eventBus.Remove<IOnAfterTakeHeal>(p11);

            else if (proxy is IOnBeforeGiveShield p12) eventBus.Remove<IOnBeforeGiveShield>(p12);
            else if (proxy is IOnBeforeTakeShield p13) eventBus.Remove<IOnBeforeTakeShield>(p13);
            else if (proxy is IOnAfterGiveShield p14) eventBus.Remove<IOnAfterGiveShield>(p14);
            else if (proxy is IOnAfterTakeShield p15) eventBus.Remove<IOnAfterTakeShield>(p15);

            else if (proxy is IOnBeforeSpendStamina p16) eventBus.Remove<IOnBeforeSpendStamina>(p16);
            else if (proxy is IOnAfterSpendStamina p17) eventBus.Remove<IOnAfterSpendStamina>(p17);
            else if (proxy is IOnBeforeTakeStamina p18) eventBus.Remove<IOnBeforeTakeStamina>(p18);
            else if (proxy is IOnAfterTakeStamina p19) eventBus.Remove<IOnAfterTakeStamina>(p19);

            else if (proxy is IOnBeforeMove p20) eventBus.Remove<IOnBeforeMove>(p20);
            else if (proxy is IOnAfterMove p21) eventBus.Remove<IOnAfterMove>(p21);
            else if (proxy is IOnBeforeForcedMove p22) eventBus.Remove<IOnBeforeForcedMove>(p22);
            else if (proxy is IOnAfterForcedMove p23) eventBus.Remove<IOnAfterForcedMove>(p23);

            else if (proxy is IOnBattleStart p24) eventBus.Remove<IOnBattleStart>(p24);
            else if (proxy is IOnBattleEnd p25) eventBus.Remove<IOnBattleEnd>(p25);
            else if (proxy is IOnTurnStart p26) eventBus.Remove<IOnTurnStart>(p26);
            else if (proxy is IOnTurnEnd p27) eventBus.Remove<IOnTurnEnd>(p27);
            else if (proxy is IOnDeath p28) eventBus.Remove<IOnDeath>(p28);

            else if (proxy is IOnBeforeUseCard p29) eventBus.Remove<IOnBeforeUseCard>(p29);
            else if (proxy is IOnAfterUseCard p30) eventBus.Remove<IOnAfterUseCard>(p30);
            else if (proxy is IOnTryUseCard p31) eventBus.Remove<IOnTryUseCard>(p31);
            else if (proxy is IOnDiscardCard p32) eventBus.Remove<IOnDiscardCard>(p32);
            else if (proxy is IOnDrawCard p33) eventBus.Remove<IOnDrawCard>(p33);
            else if (proxy is IOnExhaustCard p34) eventBus.Remove<IOnExhaustCard>(p34);
            else if (proxy is IOnShuffleDeck p35) eventBus.Remove<IOnShuffleDeck>(p35);
            else if (proxy is IOnEraseCard p36) eventBus.Remove<IOnEraseCard>(p36);

            else if (proxy is IOnBeforeGiveEffect p37) eventBus.Remove<IOnBeforeGiveEffect>(p37);
            else if (proxy is IOnBeforeTakeEffect p38) eventBus.Remove<IOnBeforeTakeEffect>(p38);
            else if (proxy is IOnAfterGiveEffect p39) eventBus.Remove<IOnAfterGiveEffect>(p39);
            else if (proxy is IOnAfterTakeEffect p40) eventBus.Remove<IOnAfterTakeEffect>(p40);
            else if (proxy is IOnBeforeRemoveEffect p41) eventBus.Remove<IOnBeforeRemoveEffect>(p41);
            else if (proxy is IOnAfterRemoveEffect p42) eventBus.Remove<IOnAfterRemoveEffect>(p42);
        }
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
            if (proxy is IOnAddRelic p1) eventBus.Remove<IOnAddRelic>(p1);
            else if (proxy is IOnRemoveRelic p2) eventBus.Remove<IOnRemoveRelic>(p2);
            else if (proxy is IOnAddCard p3) eventBus.Remove<IOnAddCard>(p3);
            else if (proxy is IOnRemoveCard p4) eventBus.Remove<IOnRemoveCard>(p4);
            else if (proxy is IOnCardUpgrade p9) eventBus.Remove<IOnCardUpgrade>(p9);
            else if (proxy is IOnCardMasteryUpgrade p10) eventBus.Remove<IOnCardMasteryUpgrade>(p10);
            else if (proxy is IOnCardsRefreshed p11) eventBus.Remove<IOnCardsRefreshed>(p11);
            else if (proxy is IOnAddGold p5) eventBus.Remove<IOnAddGold>(p5);
            else if (proxy is IOnRemoveGold p6) eventBus.Remove<IOnRemoveGold>(p6);
            else if (proxy is IOnAddMemorySharp p7) eventBus.Remove<IOnAddMemorySharp>(p7);
            else if (proxy is IOnRemoveMemorySharp p8) eventBus.Remove<IOnRemoveMemorySharp>(p8);
        }
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
