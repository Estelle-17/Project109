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
    public class DialogueManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DialogueManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 30, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartDialogue", _m_StartDialogue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GoToNode", _m_GoToNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowNextLineOrChoices", _m_ShowNextLineOrChoices);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndDialogue", _m_EndDialogue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGold", _m_GetGold);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ConsumeGold", _m_ConsumeGold);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddGold", _m_AddGold);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCardCount", _m_GetCardCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCardNameAt", _m_GetCardNameAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddCard", _m_AddCard);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveCardAt", _m_RemoveCardAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRelicCount", _m_GetRelicCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRelicNameAt", _m_GetRelicNameAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveRelicAt", _m_RemoveRelicAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveRelicByName", _m_RemoveRelicByName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCurrentHealth", _m_GetCurrentHealth);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMaxHealth", _m_GetMaxHealth);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HealPlayer", _m_HealPlayer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DamagePlayer", _m_DamagePlayer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ModifyMaxHealth", _m_ModifyMaxHealth);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartBattle", _m_StartBattle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenAllExploreNodes", _m_OpenAllExploreNodes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpawnReward", _m_SpawnReward);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenEraseCardUI", _m_OpenEraseCardUI);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenUpgradeCardUI", _m_OpenUpgradeCardUI);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OverrideNextNode", _m_OverrideNextNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TriggerEndDialogue", _m_TriggerEndDialogue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenShop", _m_OpenShop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenRestore", _m_OpenRestore);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenChest", _m_OpenChest);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ActiveInteractable", _g_get_ActiveInteractable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "dialogueUICanvasPrefab", _g_get_dialogueUICanvasPrefab);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "dialogueUICanvasPrefab", _s_set_dialogueUICanvasPrefab);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new DialogueManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to DialogueManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartDialogue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<DialogueData>(L, 2)&& translator.Assignable<InteractableObject>(L, 3)) 
                {
                    DialogueData _data = (DialogueData)translator.GetObject(L, 2, typeof(DialogueData));
                    InteractableObject _trigger = (InteractableObject)translator.GetObject(L, 3, typeof(InteractableObject));
                    
                    gen_to_be_invoked.StartDialogue( _data, _trigger );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<DialogueData>(L, 2)) 
                {
                    DialogueData _data = (DialogueData)translator.GetObject(L, 2, typeof(DialogueData));
                    
                    gen_to_be_invoked.StartDialogue( _data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DialogueManager.StartDialogue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GoToNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _nodeID = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.GoToNode( _nodeID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowNextLineOrChoices(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ShowNextLineOrChoices(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndDialogue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.EndDialogue(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGold(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetGold(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConsumeGold(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _amount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ConsumeGold( _amount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddGold(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _amount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.AddGold( _amount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCardCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCardCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCardNameAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetCardNameAt( _index );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddCard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _cardName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddCard( _cardName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveCardAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.RemoveCardAt( _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRelicCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetRelicCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRelicNameAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetRelicNameAt( _index );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRelicAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.RemoveRelicAt( _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRelicByName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _relicName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemoveRelicByName( _relicName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentHealth(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCurrentHealth(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMaxHealth(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetMaxHealth(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HealPlayer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _amount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.HealPlayer( _amount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DamagePlayer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _amount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.DamagePlayer( _amount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ModifyMaxHealth(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _amount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ModifyMaxHealth( _amount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartBattle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _battleName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.StartBattle( _battleName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenAllExploreNodes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OpenAllExploreNodes(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpawnReward(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _rewardTypeStr = LuaAPI.lua_tostring(L, 2);
                    string _rarityStr = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SpawnReward( _rewardTypeStr, _rarityStr );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenEraseCardUI(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _count = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.OpenEraseCardUI( _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenUpgradeCardUI(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OpenUpgradeCardUI(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OverrideNextNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _nodeID = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.OverrideNextNode( _nodeID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TriggerEndDialogue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.TriggerEndDialogue(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenShop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OpenShop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenRestore(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OpenRestore(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenChest(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OpenChest(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, DialogueManager.Instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ActiveInteractable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ActiveInteractable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dialogueUICanvasPrefab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.dialogueUICanvasPrefab);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dialogueUICanvasPrefab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DialogueManager gen_to_be_invoked = (DialogueManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.dialogueUICanvasPrefab = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
