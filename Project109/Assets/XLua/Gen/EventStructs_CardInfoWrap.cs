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
    public class EventStructsCardInfoWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(EventStructs.CardInfo);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 5, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddFlag", _m_AddFlag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveFlag", _m_RemoveFlag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasFlag", _m_HasFlag);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "caster", _g_get_caster);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targets", _g_get_targets);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetPosition", _g_get_targetPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardData", _g_get_cardData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cardFlags", _g_get_cardFlags);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "caster", _s_set_caster);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targets", _s_set_targets);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetPosition", _s_set_targetPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardData", _s_set_cardData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cardFlags", _s_set_cardFlags);
            
			
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
				if(LuaAPI.lua_gettop(L) == 6 && translator.Assignable<Character>(L, 2) && translator.Assignable<System.Collections.Generic.List<Character>>(L, 3) && translator.Assignable<UnityEngine.Vector2Int>(L, 4) && translator.Assignable<object>(L, 5) && translator.Assignable<EventStructs.CardFlag>(L, 6))
				{
					Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
					System.Collections.Generic.List<Character> _targets = (System.Collections.Generic.List<Character>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<Character>));
					UnityEngine.Vector2Int _targetPos;translator.Get(L, 4, out _targetPos);
					object _cardData = translator.GetObject(L, 5, typeof(object));
					EventStructs.CardFlag _flags;translator.Get(L, 6, out _flags);
					
					var gen_ret = new EventStructs.CardInfo(_caster, _targets, _targetPos, _cardData, _flags);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<Character>(L, 2) && translator.Assignable<System.Collections.Generic.List<Character>>(L, 3) && translator.Assignable<UnityEngine.Vector2Int>(L, 4) && translator.Assignable<object>(L, 5))
				{
					Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
					System.Collections.Generic.List<Character> _targets = (System.Collections.Generic.List<Character>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<Character>));
					UnityEngine.Vector2Int _targetPos;translator.Get(L, 4, out _targetPos);
					object _cardData = translator.GetObject(L, 5, typeof(object));
					
					var gen_ret = new EventStructs.CardInfo(_caster, _targets, _targetPos, _cardData);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to EventStructs.CardInfo constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EventStructs.CardFlag _flag;translator.Get(L, 2, out _flag);
                    
                    gen_to_be_invoked.AddFlag( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EventStructs.CardFlag _flag;translator.Get(L, 2, out _flag);
                    
                    gen_to_be_invoked.RemoveFlag( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    EventStructs.CardFlag _flag;translator.Get(L, 2, out _flag);
                    
                        var gen_ret = gen_to_be_invoked.HasFlag( _flag );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_caster(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.caster);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targets);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targetPosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.cardData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cardFlags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                translator.PushEventStructsCardFlag(L, gen_to_be_invoked.cardFlags);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_caster(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.caster = (Character)translator.GetObject(L, 2, typeof(Character));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targets = (System.Collections.Generic.List<Character>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Character>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2Int gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.targetPosition = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cardData = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cardFlags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                EventStructs.CardInfo gen_to_be_invoked = (EventStructs.CardInfo)translator.FastGetCSObj(L, 1);
                EventStructs.CardFlag gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.cardFlags = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
