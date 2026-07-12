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
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 9, 9);
			
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGoldChanged", _e_OnGoldChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnMemorySharpChanged", _e_OnMemorySharpChanged);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "InGameCurrencyGold", _g_get_InGameCurrencyGold);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "InGameCurrencyMemorySharp", _g_get_InGameCurrencyMemorySharp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MapFloorCheckStartLength", _g_get_MapFloorCheckStartLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MapFloorCheckLength", _g_get_MapFloorCheckLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MapRevealRandomCount", _g_get_MapRevealRandomCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RewardCardCount", _g_get_RewardCardCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RewardRelicCount", _g_get_RewardRelicCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MasteryChoiceCount", _g_get_MasteryChoiceCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UpgradeMasteryPointValue", _g_get_UpgradeMasteryPointValue);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "InGameCurrencyGold", _s_set_InGameCurrencyGold);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "InGameCurrencyMemorySharp", _s_set_InGameCurrencyMemorySharp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MapFloorCheckStartLength", _s_set_MapFloorCheckStartLength);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MapFloorCheckLength", _s_set_MapFloorCheckLength);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MapRevealRandomCount", _s_set_MapRevealRandomCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RewardCardCount", _s_set_RewardCardCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RewardRelicCount", _s_set_RewardRelicCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MasteryChoiceCount", _s_set_MasteryChoiceCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UpgradeMasteryPointValue", _s_set_UpgradeMasteryPointValue);
            
			
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
        static int _g_get_InGameCurrencyGold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.InGameCurrencyGold);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_InGameCurrencyMemorySharp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.InGameCurrencyMemorySharp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MapFloorCheckStartLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.MapFloorCheckStartLength);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MapFloorCheckLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.MapFloorCheckLength);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MapRevealRandomCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.MapRevealRandomCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RewardCardCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.RewardCardCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RewardRelicCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.RewardRelicCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MasteryChoiceCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.MasteryChoiceCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UpgradeMasteryPointValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.UpgradeMasteryPointValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_InGameCurrencyGold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.InGameCurrencyGold = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_InGameCurrencyMemorySharp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.InGameCurrencyMemorySharp = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MapFloorCheckStartLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MapFloorCheckStartLength = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MapFloorCheckLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MapFloorCheckLength = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MapRevealRandomCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MapRevealRandomCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RewardCardCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.RewardCardCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RewardRelicCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.RewardRelicCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MasteryChoiceCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MasteryChoiceCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UpgradeMasteryPointValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UpgradeMasteryPointValue = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnGoldChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                System.Action<int> gen_delegate = translator.GetDelegate<System.Action<int>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<int>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnGoldChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnGoldChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to PlayerStat.OnGoldChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnMemorySharpChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			PlayerStat gen_to_be_invoked = (PlayerStat)translator.FastGetCSObj(L, 1);
                System.Action<int> gen_delegate = translator.GetDelegate<System.Action<int>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<int>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnMemorySharpChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnMemorySharpChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to PlayerStat.OnMemorySharpChanged!");
            return 0;
        }
        
		
		
    }
}
