#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(PlayerStat), PlayerStatWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(CharacterStat), CharacterStatWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(CardData), CardDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Card), CardWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(RelicData), RelicDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Relic), RelicWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Player), PlayerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Character), CharacterWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(RelicManager), RelicManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DialogueManager), DialogueManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(ChoiceData), ChoiceDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DialogueData), DialogueDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(InteractableData), InteractableDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(InteractableObject), InteractableObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(ShopNPC), ShopNPCWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(RestoreBonfire), RestoreBonfireWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(RewardChest), RewardChestWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(RewardData), RewardDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DropTableData), DropTableDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Effect), EffectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.DamageInfo), EventStructsDamageInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.HealInfo), EventStructsHealInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.StaminaInfo), EventStructsStaminaInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.ShieldInfo), EventStructsShieldInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.CardInfo), EventStructsCardInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.MoveInfo), EventStructsMoveInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.EffectInfo), EventStructsEffectInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(CardTag), CardTagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.DamageFlag), EventStructsDamageFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.HealFlag), EventStructsHealFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.StaminaFlag), EventStructsStaminaFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.ShieldFlag), EventStructsShieldFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.CardFlag), EventStructsCardFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.MoveFlag), EventStructsMoveFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.EffectFlag), EventStructsEffectFlagWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(CharacterFaction), CharacterFactionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(EventStructs.DefaultDamageTypeID), EventStructsDefaultDamageTypeIDWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<CardData>), SystemCollectionsGenericList_1_CardData_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<Card>), SystemCollectionsGenericList_1_Card_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<RelicData>), SystemCollectionsGenericList_1_RelicData_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<Relic>), SystemCollectionsGenericList_1_Relic_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.IReadOnlyList<Relic>), SystemCollectionsGenericIReadOnlyList_1_Relic_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<int>), SystemCollectionsGenericList_1_SystemInt32_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<float>), SystemCollectionsGenericList_1_SystemSingle_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<string>), SystemCollectionsGenericList_1_SystemString_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass), TutorialDerivedClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClassExtensions), TutorialDerivedClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass.TestEnumInner), TutorialDerivedClassTestEnumInnerWrap.__Register);
        
        }
        
        
        
        
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeDealDamage), IOnBeforeDealDamageBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeTakeDamage), IOnBeforeTakeDamageBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterDealDamage), IOnAfterDealDamageBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterTakeDamage), IOnAfterTakeDamageBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBreakShield), IOnBreakShieldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnShieldBroken), IOnShieldBrokenBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnKill), IOnKillBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeGiveHeal), IOnBeforeGiveHealBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeTakeHeal), IOnBeforeTakeHealBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterGiveHeal), IOnAfterGiveHealBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterTakeHeal), IOnAfterTakeHealBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeGiveShield), IOnBeforeGiveShieldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeTakeShield), IOnBeforeTakeShieldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterGiveShield), IOnAfterGiveShieldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterTakeShield), IOnAfterTakeShieldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeSpendStamina), IOnBeforeSpendStaminaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterSpendStamina), IOnAfterSpendStaminaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeTakeStamina), IOnBeforeTakeStaminaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterTakeStamina), IOnAfterTakeStaminaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeGiveStamina), IOnBeforeGiveStaminaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterGiveStamina), IOnAfterGiveStaminaBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeMove), IOnBeforeMoveBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterMove), IOnAfterMoveBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeForcedMove), IOnBeforeForcedMoveBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterForcedMove), IOnAfterForcedMoveBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBattleStart), IOnBattleStartBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBattleEnd), IOnBattleEndBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnTurnStart), IOnTurnStartBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnTurnEnd), IOnTurnEndBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnDeath), IOnDeathBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeUseCard), IOnBeforeUseCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterUseCard), IOnAfterUseCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnTryUseCard), IOnTryUseCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnDiscardCard), IOnDiscardCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnDrawCard), IOnDrawCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnExhaustCard), IOnExhaustCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnShuffleDeck), IOnShuffleDeckBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnEraseCard), IOnEraseCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeGiveEffect), IOnBeforeGiveEffectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeTakeEffect), IOnBeforeTakeEffectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterGiveEffect), IOnAfterGiveEffectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterTakeEffect), IOnAfterTakeEffectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnBeforeRemoveEffect), IOnBeforeRemoveEffectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAfterRemoveEffect), IOnAfterRemoveEffectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAddRelic), IOnAddRelicBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnRemoveRelic), IOnRemoveRelicBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAddCard), IOnAddCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnRemoveCard), IOnRemoveCardBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnCardUpgrade), IOnCardUpgradeBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnCardMasteryUpgrade), IOnCardMasteryUpgradeBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnCardsRefreshed), IOnCardsRefreshedBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAddGold), IOnAddGoldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnRemoveGold), IOnRemoveGoldBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnAddMemorySharp), IOnAddMemorySharpBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IOnRemoveMemorySharp), IOnRemoveMemorySharpBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
