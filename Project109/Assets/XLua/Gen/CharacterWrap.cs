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
    public class CharacterWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Character);
			Utils.BeginObjectRegister(type, L, translator, 0, 18, 16, 12);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsHostileTo", _m_IsHostileTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsFriendlyTo", _m_IsFriendlyTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeStat", _m_InitializeStat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BattleTick", _m_BattleTick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResetStamina", _m_ResetStamina);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResetMoveStat", _m_ResetMoveStat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TakeDamage", _m_TakeDamage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TakeHeal", _m_TakeHeal);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TakeStamina", _m_TakeStamina);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpendStamina", _m_SpendStamina);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TakeShield", _m_TakeShield);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateShieldDuration", _m_UpdateShieldDuration);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TakeEffect", _m_TakeEffect);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCharacterHealthChanged", _e_OnCharacterHealthChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCharacterStaminaChanged", _e_OnCharacterStaminaChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCharacterShieldChanged", _e_OnCharacterShieldChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCharacterDied", _e_OnCharacterDied);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCharacterStateChanged", _e_OnCharacterStateChanged);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "curCharacterStat", _g_get_curCharacterStat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "characterMove", _g_get_characterMove);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curMoveCount", _g_get_curMoveCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curTilesPerMove", _g_get_curTilesPerMove);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "currentState", _g_get_currentState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curHealth", _g_get_curHealth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curHealthRate", _g_get_curHealthRate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curStamina", _g_get_curStamina);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curStaminaRate", _g_get_curStaminaRate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "faction", _g_get_faction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "effectManager", _g_get_effectManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "eventBus", _g_get_eventBus);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isDead", _g_get_isDead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shield", _g_get_shield);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shieldDurationTurns", _g_get_shieldDurationTurns);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "currentTurn", _g_get_currentTurn);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "curMoveCount", _s_set_curMoveCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "curTilesPerMove", _s_set_curTilesPerMove);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "currentState", _s_set_currentState);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "curHealth", _s_set_curHealth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "curStamina", _s_set_curStamina);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "faction", _s_set_faction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "effectManager", _s_set_effectManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "eventBus", _s_set_eventBus);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isDead", _s_set_isDead);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shield", _s_set_shield);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shieldDurationTurns", _s_set_shieldDurationTurns);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "currentTurn", _s_set_currentTurn);
            
			
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
					
					var gen_ret = new Character();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Character constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsHostileTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Character _other = (Character)translator.GetObject(L, 2, typeof(Character));
                    
                        var gen_ret = gen_to_be_invoked.IsHostileTo( _other );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsFriendlyTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Character _other = (Character)translator.GetObject(L, 2, typeof(Character));
                    
                        var gen_ret = gen_to_be_invoked.IsFriendlyTo( _other );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeStat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CharacterStat _characterStat = (CharacterStat)translator.GetObject(L, 2, typeof(CharacterStat));
                    
                    gen_to_be_invoked.InitializeStat( _characterStat );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BattleTick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _dt = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.BattleTick( _dt );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetStamina(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResetStamina(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetMoveStat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResetMoveStat(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TakeDamage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<EventStructs.DamageInfo>(L, 2)) 
                {
                    EventStructs.DamageInfo _info;translator.Get(L, 2, out _info);
                    
                    gen_to_be_invoked.TakeDamage( _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<EventStructs.DamageFlag>(L, 4)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseDamageAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    EventStructs.DamageFlag _flags;translator.Get(L, 4, out _flags);
                    
                    gen_to_be_invoked.TakeDamage( _caster, _baseDamageAmount, _flags );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseDamageAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.TakeDamage( _caster, _baseDamageAmount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Character.TakeDamage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TakeHeal(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<EventStructs.HealInfo>(L, 2)) 
                {
                    EventStructs.HealInfo _info;translator.Get(L, 2, out _info);
                    
                    gen_to_be_invoked.TakeHeal( _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<EventStructs.HealFlag>(L, 4)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseHealAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    EventStructs.HealFlag _flags;translator.Get(L, 4, out _flags);
                    
                    gen_to_be_invoked.TakeHeal( _caster, _baseHealAmount, _flags );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseHealAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.TakeHeal( _caster, _baseHealAmount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Character.TakeHeal!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TakeStamina(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<EventStructs.StaminaInfo>(L, 2)) 
                {
                    EventStructs.StaminaInfo _info;translator.Get(L, 2, out _info);
                    
                    gen_to_be_invoked.TakeStamina( _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<EventStructs.StaminaFlag>(L, 4)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseStaminaAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    EventStructs.StaminaFlag _flags;translator.Get(L, 4, out _flags);
                    
                    gen_to_be_invoked.TakeStamina( _caster, _baseStaminaAmount, _flags );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseStaminaAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.TakeStamina( _caster, _baseStaminaAmount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Character.TakeStamina!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpendStamina(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<EventStructs.StaminaInfo>(L, 2)) 
                {
                    EventStructs.StaminaInfo _info;translator.Get(L, 2, out _info);
                    
                    gen_to_be_invoked.SpendStamina( _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<EventStructs.StaminaFlag>(L, 3)) 
                {
                    float _baseStaminaAmount = (float)LuaAPI.lua_tonumber(L, 2);
                    EventStructs.StaminaFlag _flags;translator.Get(L, 3, out _flags);
                    
                    gen_to_be_invoked.SpendStamina( _baseStaminaAmount, _flags );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _baseStaminaAmount = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SpendStamina( _baseStaminaAmount );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<EventStructs.StaminaFlag>(L, 4)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseStaminaAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    EventStructs.StaminaFlag _flags;translator.Get(L, 4, out _flags);
                    
                    gen_to_be_invoked.SpendStamina( _caster, _baseStaminaAmount, _flags );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseStaminaAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SpendStamina( _caster, _baseStaminaAmount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Character.SpendStamina!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TakeShield(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<EventStructs.ShieldInfo>(L, 2)) 
                {
                    EventStructs.ShieldInfo _info;translator.Get(L, 2, out _info);
                    
                    gen_to_be_invoked.TakeShield( _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<EventStructs.ShieldInfo>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    EventStructs.ShieldInfo _info;translator.Get(L, 2, out _info);
                    int _durationTurns = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.TakeShield( _info, _durationTurns );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<EventStructs.ShieldFlag>(L, 5)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseShieldAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    int _durationTurns = LuaAPI.xlua_tointeger(L, 4);
                    EventStructs.ShieldFlag _flags;translator.Get(L, 5, out _flags);
                    
                    gen_to_be_invoked.TakeShield( _caster, _baseShieldAmount, _durationTurns, _flags );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseShieldAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    int _durationTurns = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.TakeShield( _caster, _baseShieldAmount, _durationTurns );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<Character>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    float _baseShieldAmount = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.TakeShield( _caster, _baseShieldAmount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Character.TakeShield!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateShieldDuration(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateShieldDuration(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TakeEffect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<EventStructs.EffectInfo>(L, 2)) 
                {
                    EventStructs.EffectInfo _info;translator.Get(L, 2, out _info);
                    
                    gen_to_be_invoked.TakeEffect( _info );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<Character>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    Character _caster = (Character)translator.GetObject(L, 2, typeof(Character));
                    string _effectId = LuaAPI.lua_tostring(L, 3);
                    int _stack = LuaAPI.xlua_tointeger(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.TakeEffect( _caster, _effectId, _stack, _duration );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Character.TakeEffect!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curCharacterStat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.curCharacterStat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_characterMove(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.characterMove);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curMoveCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.curMoveCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curTilesPerMove(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.curTilesPerMove);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_currentState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.currentState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curHealth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.curHealth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curHealthRate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.curHealthRate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curStamina(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.curStamina);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curStaminaRate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.curStaminaRate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_faction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                translator.PushCharacterFaction(L, gen_to_be_invoked.faction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_effectManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.effectManager);
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
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.eventBus);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isDead);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shield(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.shield);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shieldDurationTurns(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.shieldDurationTurns);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_currentTurn(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.currentTurn);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_curMoveCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.curMoveCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_curTilesPerMove(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.curTilesPerMove = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_currentState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                CharacterState gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.currentState = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_curHealth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.curHealth = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_curStamina(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.curStamina = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_faction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                CharacterFaction gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.faction = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_effectManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.effectManager = (EffectManager)translator.GetObject(L, 2, typeof(EffectManager));
            
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
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.eventBus = (EventBus<ICharacterEvent>)translator.GetObject(L, 2, typeof(EventBus<ICharacterEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isDead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isDead = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shield(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.shield = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shieldDurationTurns(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.shieldDurationTurns = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_currentTurn(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.currentTurn = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnCharacterHealthChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                System.Action<Character> gen_delegate = translator.GetDelegate<System.Action<Character>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<Character>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnCharacterHealthChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnCharacterHealthChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to Character.OnCharacterHealthChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnCharacterStaminaChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                System.Action<Character> gen_delegate = translator.GetDelegate<System.Action<Character>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<Character>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnCharacterStaminaChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnCharacterStaminaChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to Character.OnCharacterStaminaChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnCharacterShieldChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                System.Action<Character> gen_delegate = translator.GetDelegate<System.Action<Character>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<Character>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnCharacterShieldChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnCharacterShieldChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to Character.OnCharacterShieldChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnCharacterDied(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                System.Action<Character> gen_delegate = translator.GetDelegate<System.Action<Character>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<Character>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnCharacterDied += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnCharacterDied -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to Character.OnCharacterDied!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnCharacterStateChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			Character gen_to_be_invoked = (Character)translator.FastGetCSObj(L, 1);
                System.Action<Character, CharacterState> gen_delegate = translator.GetDelegate<System.Action<Character, CharacterState>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<Character, CharacterState>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnCharacterStateChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnCharacterStateChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to Character.OnCharacterStateChanged!");
            return 0;
        }
        
		
		
    }
}
