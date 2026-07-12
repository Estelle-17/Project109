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
    public class DropTableDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DropTableData);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 10, 9);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "dropTableID", _g_get_dropTableID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "commonWeight", _g_get_commonWeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uncommonWeight", _g_get_uncommonWeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rareWeight", _g_get_rareWeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uniqueWeight", _g_get_uniqueWeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "allowedClassTypes", _g_get_allowedClassTypes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "allowedCardTypes", _g_get_allowedCardTypes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "excludedItemIDs", _g_get_excludedItemIDs);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "specificItemIDs", _g_get_specificItemIDs);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ID", _g_get_ID);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "dropTableID", _s_set_dropTableID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "commonWeight", _s_set_commonWeight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uncommonWeight", _s_set_uncommonWeight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rareWeight", _s_set_rareWeight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uniqueWeight", _s_set_uniqueWeight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "allowedClassTypes", _s_set_allowedClassTypes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "allowedCardTypes", _s_set_allowedCardTypes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "excludedItemIDs", _s_set_excludedItemIDs);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "specificItemIDs", _s_set_specificItemIDs);
            
			
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
					
					var gen_ret = new DropTableData();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to DropTableData constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dropTableID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.dropTableID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_commonWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.commonWeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uncommonWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.uncommonWeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rareWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.rareWeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uniqueWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.uniqueWeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_allowedClassTypes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.allowedClassTypes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_allowedCardTypes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.allowedCardTypes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_excludedItemIDs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.excludedItemIDs);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_specificItemIDs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.specificItemIDs);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dropTableID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.dropTableID = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_commonWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.commonWeight = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uncommonWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uncommonWeight = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rareWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rareWeight = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uniqueWeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uniqueWeight = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_allowedClassTypes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.allowedClassTypes = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_allowedCardTypes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.allowedCardTypes = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_excludedItemIDs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.excludedItemIDs = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_specificItemIDs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DropTableData gen_to_be_invoked = (DropTableData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.specificItemIDs = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
