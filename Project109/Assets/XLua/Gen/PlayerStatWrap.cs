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
    public class PlayerStatWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PlayerStat);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 9, 9);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "inGame_Currency_Gold", _g_get_inGame_Currency_Gold);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "inGame_Currency_MemorySharp", _g_get_inGame_Currency_MemorySharp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mapFloorCheck_Start_Length", _g_get_mapFloorCheck_Start_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mapFloorCheck_Length", _g_get_mapFloorCheck_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mapReveal_Random_Count", _g_get_mapReveal_Random_Count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reward_Card_Count", _g_get_reward_Card_Count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reward_Relic_Count", _g_get_reward_Relic_Count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mastery_Choice_Count", _g_get_mastery_Choice_Count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Upgrade_MasteryPoint_Value", _g_get_Upgrade_MasteryPoint_Value);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "inGame_Currency_Gold", _s_set_inGame_Currency_Gold);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "inGame_Currency_MemorySharp", _s_set_inGame_Currency_MemorySharp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mapFloorCheck_Start_Length", _s_set_mapFloorCheck_Start_Length);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mapFloorCheck_Length", _s_set_mapFloorCheck_Length);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mapReveal_Random_Count", _s_set_mapReveal_Random_Count);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reward_Card_Count", _s_set_reward_Card_Count);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reward_Relic_Count", _s_set_reward_Relic_Count);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mastery_Choice_Count", _s_set_mastery_Choice_Count);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Upgrade_MasteryPoint_Value", _s_set_Upgrade_MasteryPoint_Value);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new PlayerStat();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PlayerStat constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_inGame_Currency_Gold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.inGame_Currency_Gold);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_inGame_Currency_MemorySharp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.inGame_Currency_MemorySharp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mapFloorCheck_Start_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mapFloorCheck_Start_Length);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mapFloorCheck_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mapFloorCheck_Length);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mapReveal_Random_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mapReveal_Random_Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reward_Card_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.reward_Card_Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reward_Relic_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.reward_Relic_Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mastery_Choice_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mastery_Choice_Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Upgrade_MasteryPoint_Value(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Upgrade_MasteryPoint_Value);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_inGame_Currency_Gold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.inGame_Currency_Gold = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_inGame_Currency_MemorySharp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.inGame_Currency_MemorySharp = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mapFloorCheck_Start_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mapFloorCheck_Start_Length = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mapFloorCheck_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mapFloorCheck_Length = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mapReveal_Random_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mapReveal_Random_Count = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reward_Card_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reward_Card_Count = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reward_Relic_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reward_Relic_Count = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mastery_Choice_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mastery_Choice_Count = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Upgrade_MasteryPoint_Value(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Upgrade_MasteryPoint_Value = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
