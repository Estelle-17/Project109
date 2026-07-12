#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class EventStructsDamageFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.DamageFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.DamageFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.DamageFlag), L, null, 8, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.DamageFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IgnoreArmor", EventStructs.DamageFlag.IgnoreArmor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IgnoreShield", EventStructs.DamageFlag.IgnoreShield);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoCasterEvents", EventStructs.DamageFlag.NoCasterEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoTargetEvents", EventStructs.DamageFlag.NoTargetEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Reflected", EventStructs.DamageFlag.Reflected);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HPLoss", EventStructs.DamageFlag.HPLoss);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.DamageFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsDamageFlag(L, (EventStructs.DamageFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IgnoreArmor"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.IgnoreArmor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IgnoreShield"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.IgnoreShield);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoCasterEvents"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.NoCasterEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoTargetEvents"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.NoTargetEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Reflected"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.Reflected);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "HPLoss"))
                {
                    translator.PushEventStructsDamageFlag(L, EventStructs.DamageFlag.HPLoss);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.DamageFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.DamageFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsHealFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.HealFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.HealFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.HealFlag), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.HealFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OverHeal", EventStructs.HealFlag.OverHeal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoCasterEvents", EventStructs.HealFlag.NoCasterEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoTargetEvents", EventStructs.HealFlag.NoTargetEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Regen", EventStructs.HealFlag.Regen);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.HealFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsHealFlag(L, (EventStructs.HealFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsHealFlag(L, EventStructs.HealFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OverHeal"))
                {
                    translator.PushEventStructsHealFlag(L, EventStructs.HealFlag.OverHeal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoCasterEvents"))
                {
                    translator.PushEventStructsHealFlag(L, EventStructs.HealFlag.NoCasterEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoTargetEvents"))
                {
                    translator.PushEventStructsHealFlag(L, EventStructs.HealFlag.NoTargetEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Regen"))
                {
                    translator.PushEventStructsHealFlag(L, EventStructs.HealFlag.Regen);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.HealFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.HealFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsStaminaFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.StaminaFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.StaminaFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.StaminaFlag), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.StaminaFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OverStamina", EventStructs.StaminaFlag.OverStamina);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoCasterEvents", EventStructs.StaminaFlag.NoCasterEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoTargetEvents", EventStructs.StaminaFlag.NoTargetEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Drain", EventStructs.StaminaFlag.Drain);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Regen", EventStructs.StaminaFlag.Regen);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.StaminaFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsStaminaFlag(L, (EventStructs.StaminaFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsStaminaFlag(L, EventStructs.StaminaFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OverStamina"))
                {
                    translator.PushEventStructsStaminaFlag(L, EventStructs.StaminaFlag.OverStamina);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoCasterEvents"))
                {
                    translator.PushEventStructsStaminaFlag(L, EventStructs.StaminaFlag.NoCasterEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoTargetEvents"))
                {
                    translator.PushEventStructsStaminaFlag(L, EventStructs.StaminaFlag.NoTargetEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Drain"))
                {
                    translator.PushEventStructsStaminaFlag(L, EventStructs.StaminaFlag.Drain);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Regen"))
                {
                    translator.PushEventStructsStaminaFlag(L, EventStructs.StaminaFlag.Regen);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.StaminaFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.StaminaFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsShieldFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.ShieldFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.ShieldFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.ShieldFlag), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.ShieldFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoCasterEvents", EventStructs.ShieldFlag.NoCasterEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoTargetEvents", EventStructs.ShieldFlag.NoTargetEvents);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.ShieldFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsShieldFlag(L, (EventStructs.ShieldFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsShieldFlag(L, EventStructs.ShieldFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoCasterEvents"))
                {
                    translator.PushEventStructsShieldFlag(L, EventStructs.ShieldFlag.NoCasterEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoTargetEvents"))
                {
                    translator.PushEventStructsShieldFlag(L, EventStructs.ShieldFlag.NoTargetEvents);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.ShieldFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.ShieldFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsCardFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.CardFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.CardFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.CardFlag), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.CardFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoExhaust", EventStructs.CardFlag.NoExhaust);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoDiscard", EventStructs.CardFlag.NoDiscard);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoCasterEvents", EventStructs.CardFlag.NoCasterEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FreeToPlay", EventStructs.CardFlag.FreeToPlay);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoPlayed", EventStructs.CardFlag.AutoPlayed);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.CardFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsCardFlag(L, (EventStructs.CardFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsCardFlag(L, EventStructs.CardFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoExhaust"))
                {
                    translator.PushEventStructsCardFlag(L, EventStructs.CardFlag.NoExhaust);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoDiscard"))
                {
                    translator.PushEventStructsCardFlag(L, EventStructs.CardFlag.NoDiscard);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoCasterEvents"))
                {
                    translator.PushEventStructsCardFlag(L, EventStructs.CardFlag.NoCasterEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FreeToPlay"))
                {
                    translator.PushEventStructsCardFlag(L, EventStructs.CardFlag.FreeToPlay);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoPlayed"))
                {
                    translator.PushEventStructsCardFlag(L, EventStructs.CardFlag.AutoPlayed);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.CardFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.CardFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsMoveFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.MoveFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.MoveFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.MoveFlag), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.MoveFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Teleport", EventStructs.MoveFlag.Teleport);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Forced", EventStructs.MoveFlag.Forced);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoCasterEvents", EventStructs.MoveFlag.NoCasterEvents);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.MoveFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsMoveFlag(L, (EventStructs.MoveFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsMoveFlag(L, EventStructs.MoveFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Teleport"))
                {
                    translator.PushEventStructsMoveFlag(L, EventStructs.MoveFlag.Teleport);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Forced"))
                {
                    translator.PushEventStructsMoveFlag(L, EventStructs.MoveFlag.Forced);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoCasterEvents"))
                {
                    translator.PushEventStructsMoveFlag(L, EventStructs.MoveFlag.NoCasterEvents);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.MoveFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.MoveFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsEffectFlagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.EffectFlag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.EffectFlag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.EffectFlag), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.EffectFlag.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unremovable", EventStructs.EffectFlag.Unremovable);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NoTargetEvents", EventStructs.EffectFlag.NoTargetEvents);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Cancel", EventStructs.EffectFlag.Cancel);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.EffectFlag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsEffectFlag(L, (EventStructs.EffectFlag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsEffectFlag(L, EventStructs.EffectFlag.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Unremovable"))
                {
                    translator.PushEventStructsEffectFlag(L, EventStructs.EffectFlag.Unremovable);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NoTargetEvents"))
                {
                    translator.PushEventStructsEffectFlag(L, EventStructs.EffectFlag.NoTargetEvents);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Cancel"))
                {
                    translator.PushEventStructsEffectFlag(L, EventStructs.EffectFlag.Cancel);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.EffectFlag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.EffectFlag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class CharacterFactionWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(CharacterFaction), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(CharacterFaction), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(CharacterFaction), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Player", CharacterFaction.Player);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ally", CharacterFaction.Ally);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Enemy", CharacterFaction.Enemy);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Neutral", CharacterFaction.Neutral);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(CharacterFaction), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushCharacterFaction(L, (CharacterFaction)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Player"))
                {
                    translator.PushCharacterFaction(L, CharacterFaction.Player);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Ally"))
                {
                    translator.PushCharacterFaction(L, CharacterFaction.Ally);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Enemy"))
                {
                    translator.PushCharacterFaction(L, CharacterFaction.Enemy);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Neutral"))
                {
                    translator.PushCharacterFaction(L, CharacterFaction.Neutral);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for CharacterFaction!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for CharacterFaction! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class EventStructsDefaultDamageTypeIDWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(EventStructs.DefaultDamageTypeID), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(EventStructs.DefaultDamageTypeID), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(EventStructs.DefaultDamageTypeID), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", EventStructs.DefaultDamageTypeID.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Magic", EventStructs.DefaultDamageTypeID.Magic);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(EventStructs.DefaultDamageTypeID), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushEventStructsDefaultDamageTypeID(L, (EventStructs.DefaultDamageTypeID)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushEventStructsDefaultDamageTypeID(L, EventStructs.DefaultDamageTypeID.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Magic"))
                {
                    translator.PushEventStructsDefaultDamageTypeID(L, EventStructs.DefaultDamageTypeID.Magic);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for EventStructs.DefaultDamageTypeID!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for EventStructs.DefaultDamageTypeID! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialTestEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.TestEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.TestEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.TestEnum), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", Tutorial.TestEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", Tutorial.TestEnum.E2);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.TestEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialTestEnum(L, (Tutorial.TestEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.TestEnum!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.TestEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialDerivedClassTestEnumInnerWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E3", Tutorial.DerivedClass.TestEnumInner.E3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E4", Tutorial.DerivedClass.TestEnumInner.E4);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialDerivedClassTestEnumInner(L, (Tutorial.DerivedClass.TestEnumInner)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E3"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E4"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E4);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.DerivedClass.TestEnumInner!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.DerivedClass.TestEnumInner! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}