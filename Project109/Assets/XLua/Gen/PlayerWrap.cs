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
    public class PlayerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Player);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 6, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddCardToDeck", _m_AddCardToDeck);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveCardFromDeck", _m_RemoveCardFromDeck);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRelic", _m_AddRelic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveRelic", _m_RemoveRelic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRandomRelic", _m_GetRandomRelic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpecificRelic", _m_GetSpecificRelic);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "deck", _g_get_deck);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "masterDeck", _g_get_masterDeck);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "character", _g_get_character);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "playerStat", _g_get_playerStat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "eventBus", _g_get_eventBus);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "relicManager", _g_get_relicManager);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "character", _s_set_character);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "playerStat", _s_set_playerStat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "eventBus", _s_set_eventBus);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "relicManager", _s_set_relicManager);
            
			
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
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<Character>(L, 2))
				{
					Character _character = (Character)translator.GetObject(L, 2, typeof(Character));
					
					var gen_ret = new Player(_character);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Player constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddCardToDeck(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Card _card = (Card)translator.GetObject(L, 2, typeof(Card));
                    
                    gen_to_be_invoked.AddCardToDeck( _card );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveCardFromDeck(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Card _card = (Card)translator.GetObject(L, 2, typeof(Card));
                    
                    gen_to_be_invoked.RemoveCardFromDeck( _card );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddRelic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _relicId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddRelic( _relicId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRelic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _relicId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemoveRelic( _relicId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRandomRelic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetRandomRelic(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpecificRelic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _relicId = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetSpecificRelic( _relicId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_deck(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.deck);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_masterDeck(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.masterDeck);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_character(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.character);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_playerStat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.playerStat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_eventBus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.eventBus);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_relicManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.relicManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_character(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.character = (Character)translator.GetObject(L, 2, typeof(Character));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_playerStat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.playerStat = (PlayerStat)translator.GetObject(L, 2, typeof(PlayerStat));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_eventBus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.eventBus = (EventBus<IPlayerEvent>)translator.GetObject(L, 2, typeof(EventBus<IPlayerEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_relicManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Player gen_to_be_invoked = (Player)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.relicManager = (RelicManager)translator.GetObject(L, 2, typeof(RelicManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
