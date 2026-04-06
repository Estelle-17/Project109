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
    public class ActionCardDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(ActionCardData);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 21, 20);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ID", _g_get_ID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardTexture", _g_get_cardTexture);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "path", _g_get_path);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "className", _g_get_className);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardName", _g_get_cardName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rarity", _g_get_rarity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stamina", _g_get_stamina);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardType", _g_get_cardType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetType", _g_get_targetType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetMinDistance", _g_get_targetMinDistance);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetMaxDistance", _g_get_targetMaxDistance);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "additionalEffectAreaList", _g_get_additionalEffectAreaList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "amountList", _g_get_amountList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "texturePath", _g_get_texturePath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "upgradeCardPath", _g_get_upgradeCardPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxMasteryPoint", _g_get_maxMasteryPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "specificProperties", _g_get_specificProperties);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isEvolved", _g_get_isEvolved);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "evolveType", _g_get_evolveType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isUpgrade", _g_get_isUpgrade);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "runtimeID", _g_get_runtimeID);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardTexture", _s_set_cardTexture);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "path", _s_set_path);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "className", _s_set_className);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardName", _s_set_cardName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rarity", _s_set_rarity);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stamina", _s_set_stamina);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardType", _s_set_cardType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetType", _s_set_targetType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetMinDistance", _s_set_targetMinDistance);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetMaxDistance", _s_set_targetMaxDistance);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "additionalEffectAreaList", _s_set_additionalEffectAreaList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "amountList", _s_set_amountList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "texturePath", _s_set_texturePath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "upgradeCardPath", _s_set_upgradeCardPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "maxMasteryPoint", _s_set_maxMasteryPoint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "specificProperties", _s_set_specificProperties);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isEvolved", _s_set_isEvolved);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "evolveType", _s_set_evolveType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isUpgrade", _s_set_isUpgrade);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "runtimeID", _s_set_runtimeID);
            
			
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
					
					var gen_ret = new ActionCardData();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to ActionCardData constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cardTexture);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_path(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_className(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.className);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.cardName);
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
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.rarity);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stamina(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.stamina);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.cardType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.targetType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetMinDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.targetMinDistance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetMaxDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.targetMaxDistance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_additionalEffectAreaList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.additionalEffectAreaList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_amountList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.amountList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_texturePath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.texturePath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_upgradeCardPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.upgradeCardPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxMasteryPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.maxMasteryPoint);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_specificProperties(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.specificProperties);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isEvolved(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isEvolved);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_evolveType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.evolveType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isUpgrade(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isUpgrade);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_runtimeID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.runtimeID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cardTexture = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_path(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.path = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_className(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.className = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cardName = LuaAPI.lua_tostring(L, 2);
            
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
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rarity = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_stamina(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stamina = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cardType = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetType = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetMinDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetMinDistance = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetMaxDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetMaxDistance = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_additionalEffectAreaList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.additionalEffectAreaList = (System.Collections.Generic.List<EffectArea>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<EffectArea>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_amountList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.amountList = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_texturePath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.texturePath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_upgradeCardPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.upgradeCardPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maxMasteryPoint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.maxMasteryPoint = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_specificProperties(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.specificProperties = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isEvolved(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isEvolved = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_evolveType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                GameItem.Types.EvolveType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.evolveType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isUpgrade(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isUpgrade = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_runtimeID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ActionCardData gen_to_be_invoked = (ActionCardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.runtimeID = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
