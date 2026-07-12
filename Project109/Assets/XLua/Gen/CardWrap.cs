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
    public class CardWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Card);
			Utils.BeginObjectRegister(type, L, translator, 0, 18, 16, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clone", _m_Clone);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateRuntimeID", _m_UpdateRuntimeID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBaseValue", _m_GetBaseValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFormattedValue", _m_GetFormattedValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBaseFormattedValue", _m_GetBaseFormattedValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEffectiveValue", _m_GetEffectiveValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMasteryLevel", _m_GetMasteryLevel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetDescription", _m_GetDescription);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Execute", _m_Execute);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanPlay", _m_CanPlay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpendCosts", _m_SpendCosts);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddMasteryPoint", _m_AddMasteryPoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddMastery", _m_AddMastery);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasTag", _m_HasTag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTag", _m_AddTag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveTag", _m_RemoveTag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRandomMasteryOption", _m_GetRandomMasteryOption);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardData", _g_get_cardData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "owner", _g_get_owner);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "runtimeID", _g_get_runtimeID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "currentCost", _g_get_currentCost);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardName", _g_get_cardName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "path", _g_get_path);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isTemporary", _g_get_isTemporary);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryLevel", _g_get_masteryLevel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "currentMasteryXP", _g_get_currentMasteryXP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxMasteryXP", _g_get_maxMasteryXP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryXPIncreasePerLevel", _g_get_masteryXPIncreasePerLevel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hasMastery", _g_get_hasMastery);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masteryUpgrades", _g_get_masteryUpgrades);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tagNames", _g_get_tagNames);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "attachedTags", _g_get_attachedTags);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaTable", _g_get_luaTable);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "currentCost", _s_set_currentCost);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isTemporary", _s_set_isTemporary);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "masteryLevel", _s_set_masteryLevel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "currentMasteryXP", _s_set_currentMasteryXP);
            
			
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
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<CardData>(L, 2) && translator.Assignable<Character>(L, 3) && (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TTABLE) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					CardData _data = (CardData)translator.GetObject(L, 2, typeof(CardData));
					Character _owner = (Character)translator.GetObject(L, 3, typeof(Character));
					XLua.LuaTable _luaLogic = (XLua.LuaTable)translator.GetObject(L, 4, typeof(XLua.LuaTable));
					int _runtimeID = LuaAPI.xlua_tointeger(L, 5);
					
					var gen_ret = new Card(_data, _owner, _luaLogic, _runtimeID);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Card constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clone(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<Character>(L, 2)&& translator.Assignable<System.Nullable<int>>(L, 3)) 
                {
                    Character _newOwner = (Character)translator.GetObject(L, 2, typeof(Character));
                    System.Nullable<int> _newRuntimeID;translator.Get(L, 3, out _newRuntimeID);
                    
                        var gen_ret = gen_to_be_invoked.Clone( _newOwner, _newRuntimeID );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<Character>(L, 2)) 
                {
                    Character _newOwner = (Character)translator.GetObject(L, 2, typeof(Character));
                    
                        var gen_ret = gen_to_be_invoked.Clone( _newOwner );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.Clone(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Card.Clone!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateRuntimeID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _newID = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.UpdateRuntimeID( _newID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBaseValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _valueKey = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetBaseValue( _valueKey );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFormattedValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _valueKey = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetFormattedValue( _valueKey );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBaseFormattedValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _valueKey = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetBaseFormattedValue( _valueKey );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEffectiveValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _valueKey = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetEffectiveValue( _valueKey );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMasteryLevel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _masteryId = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetMasteryLevel( _masteryId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDescription(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_Execute(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EventStructs.CardInfo _info = (EventStructs.CardInfo)translator.GetObject(L, 2, typeof(EventStructs.CardInfo));
                    
                    gen_to_be_invoked.Execute( _info );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanPlay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    
                        var gen_ret = gen_to_be_invoked.CanPlay( _caster );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpendCosts(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    
                    gen_to_be_invoked.SpendCosts( _caster );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddMasteryPoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _amount = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.AddMasteryPoint( _amount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddMastery(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _masteryID = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddMastery( _masteryID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasTag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tagName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.HasTag( _tagName );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tagName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddTag( _tagName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveTag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CardTag _cardTag = (CardTag)translator.GetObject(L, 2, typeof(CardTag));
                    
                    gen_to_be_invoked.RemoveTag( _cardTag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRandomMasteryOption(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _optionCount = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetRandomMasteryOption( _optionCount );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cardData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_owner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.owner);
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
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.runtimeID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_currentCost(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.currentCost);
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
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.cardName);
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
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isTemporary(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isTemporary);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masteryLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.masteryLevel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_currentMasteryXP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.currentMasteryXP);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxMasteryXP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.maxMasteryXP);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masteryXPIncreasePerLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.masteryXPIncreasePerLevel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hasMastery(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.hasMastery);
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
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.masteryUpgrades);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tagNames(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.tagNames);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_attachedTags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.attachedTags);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.luaTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_currentCost(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.currentCost = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isTemporary(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isTemporary = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_masteryLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.masteryLevel = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_currentMasteryXP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Card gen_to_be_invoked = (Card)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.currentMasteryXP = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
