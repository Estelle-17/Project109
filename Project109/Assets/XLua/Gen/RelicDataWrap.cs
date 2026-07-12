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
    public class RelicDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(RelicData);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 8, 8);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanAppearForClass", _m_CanAppearForClass);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResolveAndValidate", _m_ResolveAndValidate);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "relicName", _g_get_relicName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "classTypes", _g_get_classTypes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rarity", _g_get_rarity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "upgradedRelicId", _g_get_upgradedRelicId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "description", _g_get_description);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "flavorText", _g_get_flavorText);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "iconSprite", _g_get_iconSprite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaPrototype", _g_get_luaPrototype);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "relicName", _s_set_relicName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "classTypes", _s_set_classTypes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rarity", _s_set_rarity);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "upgradedRelicId", _s_set_upgradedRelicId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "description", _s_set_description);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "flavorText", _s_set_flavorText);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "iconSprite", _s_set_iconSprite);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "luaPrototype", _s_set_luaPrototype);
            
			
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
					
					var gen_ret = new RelicData();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to RelicData constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanAppearForClass(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _playerClass = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.CanAppearForClass( _playerClass );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResolveAndValidate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _modDirectory = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.ResolveAndValidate( _modDirectory );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_relicName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.relicName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_classTypes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.classTypes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rarity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rarity);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_upgradedRelicId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.upgradedRelicId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_description(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.description);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_flavorText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.flavorText);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_iconSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.iconSprite);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaPrototype(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.luaPrototype);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_relicName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.relicName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_classTypes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.classTypes = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rarity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                GameItem.Types.RelicRarity gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.rarity = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_upgradedRelicId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.upgradedRelicId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_description(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.description = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_flavorText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.flavorText = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_iconSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.iconSprite = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaPrototype(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RelicData gen_to_be_invoked = (RelicData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.luaPrototype = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
