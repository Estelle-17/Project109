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
    public class CardDataWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CardData);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 21, 21);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetDescription", _m_GetDescription);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResolveAndValidate", _m_ResolveAndValidate);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardName", _g_get_cardName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "classTypes", _g_get_classTypes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardType", _g_get_cardType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetType", _g_get_targetType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetMinDistance", _g_get_targetMinDistance);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetMaxDistance", _g_get_targetMaxDistance);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "additionalEffectAreaList", _g_get_additionalEffectAreaList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rarity", _g_get_rarity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stamina", _g_get_stamina);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxMasteryPoint", _g_get_maxMasteryPoint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isUpgradable", _g_get_isUpgradable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isUpgraded", _g_get_isUpgraded);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "upgradedCardName", _g_get_upgradedCardName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "description", _g_get_description);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "baseValues", _g_get_baseValues);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryUpgrades", _g_get_masteryUpgrades);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryMaxUpgrades", _g_get_masteryMaxUpgrades);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryTags", _g_get_masteryTags);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryNames", _g_get_masteryNames);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardSprite", _g_get_cardSprite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaPrototype", _g_get_luaPrototype);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardName", _s_set_cardName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "classTypes", _s_set_classTypes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardType", _s_set_cardType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetType", _s_set_targetType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetMinDistance", _s_set_targetMinDistance);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetMaxDistance", _s_set_targetMaxDistance);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "additionalEffectAreaList", _s_set_additionalEffectAreaList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rarity", _s_set_rarity);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stamina", _s_set_stamina);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "maxMasteryPoint", _s_set_maxMasteryPoint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isUpgradable", _s_set_isUpgradable);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isUpgraded", _s_set_isUpgraded);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "upgradedCardName", _s_set_upgradedCardName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "description", _s_set_description);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "baseValues", _s_set_baseValues);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "masteryUpgrades", _s_set_masteryUpgrades);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "masteryMaxUpgrades", _s_set_masteryMaxUpgrades);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "masteryTags", _s_set_masteryTags);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "masteryNames", _s_set_masteryNames);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardSprite", _s_set_cardSprite);
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
					
					var gen_ret = new CardData();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CardData constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDescription(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetDescription(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
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
            
            
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _g_get_cardName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.cardName);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.classTypes);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.additionalEffectAreaList);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rarity);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.stamina);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.maxMasteryPoint);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isUpgradable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isUpgradable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isUpgraded(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isUpgraded);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_upgradedCardName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.upgradedCardName);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.description);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_baseValues(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.baseValues);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masteryUpgrades(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.masteryUpgrades);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masteryMaxUpgrades(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.masteryMaxUpgrades);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masteryTags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.masteryTags);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masteryNames(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.masteryNames);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cardSprite);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.luaPrototype);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cardName = LuaAPI.lua_tostring(L, 2);
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.classTypes = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.additionalEffectAreaList = (System.Collections.Generic.List<EffectArea>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<EffectArea>));
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                GameItem.Types.CardRarity gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.rarity = gen_value;
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stamina = LuaAPI.xlua_tointeger(L, 2);
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.maxMasteryPoint = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isUpgradable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isUpgradable = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isUpgraded(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isUpgraded = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_upgradedCardName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.upgradedCardName = LuaAPI.lua_tostring(L, 2);
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.description = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_baseValues(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.baseValues = (System.Collections.Generic.Dictionary<string, float>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, float>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_masteryUpgrades(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.masteryUpgrades = (System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, float>>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, float>>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_masteryMaxUpgrades(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.masteryMaxUpgrades = (System.Collections.Generic.Dictionary<string, int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_masteryTags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.masteryTags = (System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_masteryNames(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.masteryNames = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cardSprite = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
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
			
                CardData gen_to_be_invoked = (CardData)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.luaPrototype = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
