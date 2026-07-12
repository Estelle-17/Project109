#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class ObjectTranslator
    {
        
        class IniterAdderUnityEngineVector2
        {
            static IniterAdderUnityEngineVector2()
            {
                LuaEnv.AddIniter(Init);
            }
			
			static void Init(LuaEnv luaenv, ObjectTranslator translator)
			{
			
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector2>(translator.PushUnityEngineVector2, translator.Get, translator.UpdateUnityEngineVector2);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector3>(translator.PushUnityEngineVector3, translator.Get, translator.UpdateUnityEngineVector3);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector4>(translator.PushUnityEngineVector4, translator.Get, translator.UpdateUnityEngineVector4);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Color>(translator.PushUnityEngineColor, translator.Get, translator.UpdateUnityEngineColor);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Quaternion>(translator.PushUnityEngineQuaternion, translator.Get, translator.UpdateUnityEngineQuaternion);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray>(translator.PushUnityEngineRay, translator.Get, translator.UpdateUnityEngineRay);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Bounds>(translator.PushUnityEngineBounds, translator.Get, translator.UpdateUnityEngineBounds);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray2D>(translator.PushUnityEngineRay2D, translator.Get, translator.UpdateUnityEngineRay2D);
				translator.RegisterPushAndGetAndUpdate<EventStructs.DamageFlag>(translator.PushEventStructsDamageFlag, translator.Get, translator.UpdateEventStructsDamageFlag);
				translator.RegisterPushAndGetAndUpdate<EventStructs.HealFlag>(translator.PushEventStructsHealFlag, translator.Get, translator.UpdateEventStructsHealFlag);
				translator.RegisterPushAndGetAndUpdate<EventStructs.StaminaFlag>(translator.PushEventStructsStaminaFlag, translator.Get, translator.UpdateEventStructsStaminaFlag);
				translator.RegisterPushAndGetAndUpdate<EventStructs.ShieldFlag>(translator.PushEventStructsShieldFlag, translator.Get, translator.UpdateEventStructsShieldFlag);
				translator.RegisterPushAndGetAndUpdate<EventStructs.CardFlag>(translator.PushEventStructsCardFlag, translator.Get, translator.UpdateEventStructsCardFlag);
				translator.RegisterPushAndGetAndUpdate<EventStructs.MoveFlag>(translator.PushEventStructsMoveFlag, translator.Get, translator.UpdateEventStructsMoveFlag);
				translator.RegisterPushAndGetAndUpdate<EventStructs.EffectFlag>(translator.PushEventStructsEffectFlag, translator.Get, translator.UpdateEventStructsEffectFlag);
				translator.RegisterPushAndGetAndUpdate<CharacterFaction>(translator.PushCharacterFaction, translator.Get, translator.UpdateCharacterFaction);
				translator.RegisterPushAndGetAndUpdate<EventStructs.DefaultDamageTypeID>(translator.PushEventStructsDefaultDamageTypeID, translator.Get, translator.UpdateEventStructsDefaultDamageTypeID);
				translator.RegisterPushAndGetAndUpdate<Tutorial.TestEnum>(translator.PushTutorialTestEnum, translator.Get, translator.UpdateTutorialTestEnum);
				translator.RegisterPushAndGetAndUpdate<Tutorial.DerivedClass.TestEnumInner>(translator.PushTutorialDerivedClassTestEnumInner, translator.Get, translator.UpdateTutorialDerivedClassTestEnumInner);
			
			}
        }
        
        static IniterAdderUnityEngineVector2 s_IniterAdderUnityEngineVector2_dumb_obj = new IniterAdderUnityEngineVector2();
        static IniterAdderUnityEngineVector2 IniterAdderUnityEngineVector2_dumb_obj {get{return s_IniterAdderUnityEngineVector2_dumb_obj;}}
        
        
        int UnityEngineVector2_TypeID = -1;
        public void PushUnityEngineVector2(RealStatePtr L, UnityEngine.Vector2 val)
        {
            if (UnityEngineVector2_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector2_TypeID = getTypeId(L, typeof(UnityEngine.Vector2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, UnityEngineVector2_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector2 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector2");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector2)objectCasters.GetCaster(typeof(UnityEngine.Vector2))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector2(RealStatePtr L, int index, UnityEngine.Vector2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector2 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector3_TypeID = -1;
        public void PushUnityEngineVector3(RealStatePtr L, UnityEngine.Vector3 val)
        {
            if (UnityEngineVector3_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector3_TypeID = getTypeId(L, typeof(UnityEngine.Vector3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, UnityEngineVector3_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector3 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector3");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector3)objectCasters.GetCaster(typeof(UnityEngine.Vector3))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector3(RealStatePtr L, int index, UnityEngine.Vector3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector3 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector4_TypeID = -1;
        public void PushUnityEngineVector4(RealStatePtr L, UnityEngine.Vector4 val)
        {
            if (UnityEngineVector4_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector4_TypeID = getTypeId(L, typeof(UnityEngine.Vector4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineVector4_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector4 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector4");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector4)objectCasters.GetCaster(typeof(UnityEngine.Vector4))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector4(RealStatePtr L, int index, UnityEngine.Vector4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector4 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineColor_TypeID = -1;
        public void PushUnityEngineColor(RealStatePtr L, UnityEngine.Color val)
        {
            if (UnityEngineColor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineColor_TypeID = getTypeId(L, typeof(UnityEngine.Color), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineColor_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Color ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Color val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Color");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Color)objectCasters.GetCaster(typeof(UnityEngine.Color))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineColor(RealStatePtr L, int index, UnityEngine.Color val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Color ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineQuaternion_TypeID = -1;
        public void PushUnityEngineQuaternion(RealStatePtr L, UnityEngine.Quaternion val)
        {
            if (UnityEngineQuaternion_TypeID == -1)
            {
			    bool is_first;
                UnityEngineQuaternion_TypeID = getTypeId(L, typeof(UnityEngine.Quaternion), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineQuaternion_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Quaternion ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Quaternion val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Quaternion");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Quaternion)objectCasters.GetCaster(typeof(UnityEngine.Quaternion))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineQuaternion(RealStatePtr L, int index, UnityEngine.Quaternion val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Quaternion ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay_TypeID = -1;
        public void PushUnityEngineRay(RealStatePtr L, UnityEngine.Ray val)
        {
            if (UnityEngineRay_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay_TypeID = getTypeId(L, typeof(UnityEngine.Ray), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineRay_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray)objectCasters.GetCaster(typeof(UnityEngine.Ray))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay(RealStatePtr L, int index, UnityEngine.Ray val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineBounds_TypeID = -1;
        public void PushUnityEngineBounds(RealStatePtr L, UnityEngine.Bounds val)
        {
            if (UnityEngineBounds_TypeID == -1)
            {
			    bool is_first;
                UnityEngineBounds_TypeID = getTypeId(L, typeof(UnityEngine.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineBounds_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Bounds ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Bounds");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Bounds)objectCasters.GetCaster(typeof(UnityEngine.Bounds))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineBounds(RealStatePtr L, int index, UnityEngine.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Bounds ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay2D_TypeID = -1;
        public void PushUnityEngineRay2D(RealStatePtr L, UnityEngine.Ray2D val)
        {
            if (UnityEngineRay2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay2D_TypeID = getTypeId(L, typeof(UnityEngine.Ray2D), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineRay2D_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray2D ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray2D");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray2D)objectCasters.GetCaster(typeof(UnityEngine.Ray2D))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay2D(RealStatePtr L, int index, UnityEngine.Ray2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsDamageFlag_TypeID = -1;
		int EventStructsDamageFlag_EnumRef = -1;
        
        public void PushEventStructsDamageFlag(RealStatePtr L, EventStructs.DamageFlag val)
        {
            if (EventStructsDamageFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsDamageFlag_TypeID = getTypeId(L, typeof(EventStructs.DamageFlag), out is_first);
				
				if (EventStructsDamageFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.DamageFlag));
				    EventStructsDamageFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsDamageFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsDamageFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.DamageFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsDamageFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.DamageFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsDamageFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.DamageFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.DamageFlag");
                }
				val = (EventStructs.DamageFlag)e;
                
            }
            else
            {
                val = (EventStructs.DamageFlag)objectCasters.GetCaster(typeof(EventStructs.DamageFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsDamageFlag(RealStatePtr L, int index, EventStructs.DamageFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsDamageFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.DamageFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.DamageFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsHealFlag_TypeID = -1;
		int EventStructsHealFlag_EnumRef = -1;
        
        public void PushEventStructsHealFlag(RealStatePtr L, EventStructs.HealFlag val)
        {
            if (EventStructsHealFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsHealFlag_TypeID = getTypeId(L, typeof(EventStructs.HealFlag), out is_first);
				
				if (EventStructsHealFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.HealFlag));
				    EventStructsHealFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsHealFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsHealFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.HealFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsHealFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.HealFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsHealFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.HealFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.HealFlag");
                }
				val = (EventStructs.HealFlag)e;
                
            }
            else
            {
                val = (EventStructs.HealFlag)objectCasters.GetCaster(typeof(EventStructs.HealFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsHealFlag(RealStatePtr L, int index, EventStructs.HealFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsHealFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.HealFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.HealFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsStaminaFlag_TypeID = -1;
		int EventStructsStaminaFlag_EnumRef = -1;
        
        public void PushEventStructsStaminaFlag(RealStatePtr L, EventStructs.StaminaFlag val)
        {
            if (EventStructsStaminaFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsStaminaFlag_TypeID = getTypeId(L, typeof(EventStructs.StaminaFlag), out is_first);
				
				if (EventStructsStaminaFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.StaminaFlag));
				    EventStructsStaminaFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsStaminaFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsStaminaFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.StaminaFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsStaminaFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.StaminaFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsStaminaFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.StaminaFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.StaminaFlag");
                }
				val = (EventStructs.StaminaFlag)e;
                
            }
            else
            {
                val = (EventStructs.StaminaFlag)objectCasters.GetCaster(typeof(EventStructs.StaminaFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsStaminaFlag(RealStatePtr L, int index, EventStructs.StaminaFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsStaminaFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.StaminaFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.StaminaFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsShieldFlag_TypeID = -1;
		int EventStructsShieldFlag_EnumRef = -1;
        
        public void PushEventStructsShieldFlag(RealStatePtr L, EventStructs.ShieldFlag val)
        {
            if (EventStructsShieldFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsShieldFlag_TypeID = getTypeId(L, typeof(EventStructs.ShieldFlag), out is_first);
				
				if (EventStructsShieldFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.ShieldFlag));
				    EventStructsShieldFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsShieldFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsShieldFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.ShieldFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsShieldFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.ShieldFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsShieldFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.ShieldFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.ShieldFlag");
                }
				val = (EventStructs.ShieldFlag)e;
                
            }
            else
            {
                val = (EventStructs.ShieldFlag)objectCasters.GetCaster(typeof(EventStructs.ShieldFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsShieldFlag(RealStatePtr L, int index, EventStructs.ShieldFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsShieldFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.ShieldFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.ShieldFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsCardFlag_TypeID = -1;
		int EventStructsCardFlag_EnumRef = -1;
        
        public void PushEventStructsCardFlag(RealStatePtr L, EventStructs.CardFlag val)
        {
            if (EventStructsCardFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsCardFlag_TypeID = getTypeId(L, typeof(EventStructs.CardFlag), out is_first);
				
				if (EventStructsCardFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.CardFlag));
				    EventStructsCardFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsCardFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsCardFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.CardFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsCardFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.CardFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsCardFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.CardFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.CardFlag");
                }
				val = (EventStructs.CardFlag)e;
                
            }
            else
            {
                val = (EventStructs.CardFlag)objectCasters.GetCaster(typeof(EventStructs.CardFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsCardFlag(RealStatePtr L, int index, EventStructs.CardFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsCardFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.CardFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.CardFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsMoveFlag_TypeID = -1;
		int EventStructsMoveFlag_EnumRef = -1;
        
        public void PushEventStructsMoveFlag(RealStatePtr L, EventStructs.MoveFlag val)
        {
            if (EventStructsMoveFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsMoveFlag_TypeID = getTypeId(L, typeof(EventStructs.MoveFlag), out is_first);
				
				if (EventStructsMoveFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.MoveFlag));
				    EventStructsMoveFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsMoveFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsMoveFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.MoveFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsMoveFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.MoveFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsMoveFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.MoveFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.MoveFlag");
                }
				val = (EventStructs.MoveFlag)e;
                
            }
            else
            {
                val = (EventStructs.MoveFlag)objectCasters.GetCaster(typeof(EventStructs.MoveFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsMoveFlag(RealStatePtr L, int index, EventStructs.MoveFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsMoveFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.MoveFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.MoveFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsEffectFlag_TypeID = -1;
		int EventStructsEffectFlag_EnumRef = -1;
        
        public void PushEventStructsEffectFlag(RealStatePtr L, EventStructs.EffectFlag val)
        {
            if (EventStructsEffectFlag_TypeID == -1)
            {
			    bool is_first;
                EventStructsEffectFlag_TypeID = getTypeId(L, typeof(EventStructs.EffectFlag), out is_first);
				
				if (EventStructsEffectFlag_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.EffectFlag));
				    EventStructsEffectFlag_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsEffectFlag_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsEffectFlag_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.EffectFlag ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsEffectFlag_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.EffectFlag val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsEffectFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.EffectFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.EffectFlag");
                }
				val = (EventStructs.EffectFlag)e;
                
            }
            else
            {
                val = (EventStructs.EffectFlag)objectCasters.GetCaster(typeof(EventStructs.EffectFlag))(L, index, null);
            }
        }
		
        public void UpdateEventStructsEffectFlag(RealStatePtr L, int index, EventStructs.EffectFlag val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsEffectFlag_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.EffectFlag");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.EffectFlag ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int CharacterFaction_TypeID = -1;
		int CharacterFaction_EnumRef = -1;
        
        public void PushCharacterFaction(RealStatePtr L, CharacterFaction val)
        {
            if (CharacterFaction_TypeID == -1)
            {
			    bool is_first;
                CharacterFaction_TypeID = getTypeId(L, typeof(CharacterFaction), out is_first);
				
				if (CharacterFaction_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(CharacterFaction));
				    CharacterFaction_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, CharacterFaction_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, CharacterFaction_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for CharacterFaction ,value="+val);
            }
			
			LuaAPI.lua_getref(L, CharacterFaction_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out CharacterFaction val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CharacterFaction_TypeID)
				{
				    throw new Exception("invalid userdata for CharacterFaction");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for CharacterFaction");
                }
				val = (CharacterFaction)e;
                
            }
            else
            {
                val = (CharacterFaction)objectCasters.GetCaster(typeof(CharacterFaction))(L, index, null);
            }
        }
		
        public void UpdateCharacterFaction(RealStatePtr L, int index, CharacterFaction val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CharacterFaction_TypeID)
				{
				    throw new Exception("invalid userdata for CharacterFaction");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for CharacterFaction ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int EventStructsDefaultDamageTypeID_TypeID = -1;
		int EventStructsDefaultDamageTypeID_EnumRef = -1;
        
        public void PushEventStructsDefaultDamageTypeID(RealStatePtr L, EventStructs.DefaultDamageTypeID val)
        {
            if (EventStructsDefaultDamageTypeID_TypeID == -1)
            {
			    bool is_first;
                EventStructsDefaultDamageTypeID_TypeID = getTypeId(L, typeof(EventStructs.DefaultDamageTypeID), out is_first);
				
				if (EventStructsDefaultDamageTypeID_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(EventStructs.DefaultDamageTypeID));
				    EventStructsDefaultDamageTypeID_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, EventStructsDefaultDamageTypeID_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, EventStructsDefaultDamageTypeID_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for EventStructs.DefaultDamageTypeID ,value="+val);
            }
			
			LuaAPI.lua_getref(L, EventStructsDefaultDamageTypeID_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out EventStructs.DefaultDamageTypeID val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsDefaultDamageTypeID_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.DefaultDamageTypeID");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for EventStructs.DefaultDamageTypeID");
                }
				val = (EventStructs.DefaultDamageTypeID)e;
                
            }
            else
            {
                val = (EventStructs.DefaultDamageTypeID)objectCasters.GetCaster(typeof(EventStructs.DefaultDamageTypeID))(L, index, null);
            }
        }
		
        public void UpdateEventStructsDefaultDamageTypeID(RealStatePtr L, int index, EventStructs.DefaultDamageTypeID val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != EventStructsDefaultDamageTypeID_TypeID)
				{
				    throw new Exception("invalid userdata for EventStructs.DefaultDamageTypeID");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for EventStructs.DefaultDamageTypeID ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int TutorialTestEnum_TypeID = -1;
		int TutorialTestEnum_EnumRef = -1;
        
        public void PushTutorialTestEnum(RealStatePtr L, Tutorial.TestEnum val)
        {
            if (TutorialTestEnum_TypeID == -1)
            {
			    bool is_first;
                TutorialTestEnum_TypeID = getTypeId(L, typeof(Tutorial.TestEnum), out is_first);
				
				if (TutorialTestEnum_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Tutorial.TestEnum));
				    TutorialTestEnum_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, TutorialTestEnum_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, TutorialTestEnum_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Tutorial.TestEnum ,value="+val);
            }
			
			LuaAPI.lua_getref(L, TutorialTestEnum_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Tutorial.TestEnum val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialTestEnum_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.TestEnum");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Tutorial.TestEnum");
                }
				val = (Tutorial.TestEnum)e;
                
            }
            else
            {
                val = (Tutorial.TestEnum)objectCasters.GetCaster(typeof(Tutorial.TestEnum))(L, index, null);
            }
        }
		
        public void UpdateTutorialTestEnum(RealStatePtr L, int index, Tutorial.TestEnum val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialTestEnum_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.TestEnum");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Tutorial.TestEnum ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int TutorialDerivedClassTestEnumInner_TypeID = -1;
		int TutorialDerivedClassTestEnumInner_EnumRef = -1;
        
        public void PushTutorialDerivedClassTestEnumInner(RealStatePtr L, Tutorial.DerivedClass.TestEnumInner val)
        {
            if (TutorialDerivedClassTestEnumInner_TypeID == -1)
            {
			    bool is_first;
                TutorialDerivedClassTestEnumInner_TypeID = getTypeId(L, typeof(Tutorial.DerivedClass.TestEnumInner), out is_first);
				
				if (TutorialDerivedClassTestEnumInner_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Tutorial.DerivedClass.TestEnumInner));
				    TutorialDerivedClassTestEnumInner_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, TutorialDerivedClassTestEnumInner_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, TutorialDerivedClassTestEnumInner_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Tutorial.DerivedClass.TestEnumInner ,value="+val);
            }
			
			LuaAPI.lua_getref(L, TutorialDerivedClassTestEnumInner_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Tutorial.DerivedClass.TestEnumInner val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialDerivedClassTestEnumInner_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.DerivedClass.TestEnumInner");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Tutorial.DerivedClass.TestEnumInner");
                }
				val = (Tutorial.DerivedClass.TestEnumInner)e;
                
            }
            else
            {
                val = (Tutorial.DerivedClass.TestEnumInner)objectCasters.GetCaster(typeof(Tutorial.DerivedClass.TestEnumInner))(L, index, null);
            }
        }
		
        public void UpdateTutorialDerivedClassTestEnumInner(RealStatePtr L, int index, Tutorial.DerivedClass.TestEnumInner val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TutorialDerivedClassTestEnumInner_TypeID)
				{
				    throw new Exception("invalid userdata for Tutorial.DerivedClass.TestEnumInner");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Tutorial.DerivedClass.TestEnumInner ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        
		// table cast optimze
		
        
    }
	
	public partial class StaticLuaCallbacks
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.PushUnityEngineVector2(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.PushUnityEngineVector3(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.PushUnityEngineVector4(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.PushUnityEngineColor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.PushUnityEngineQuaternion(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.PushUnityEngineRay(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.PushUnityEngineBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.PushUnityEngineRay2D(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.DamageFlag[]))
			{
			    EventStructs.DamageFlag[] array = obj as EventStructs.DamageFlag[];
				translator.PushEventStructsDamageFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.HealFlag[]))
			{
			    EventStructs.HealFlag[] array = obj as EventStructs.HealFlag[];
				translator.PushEventStructsHealFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.StaminaFlag[]))
			{
			    EventStructs.StaminaFlag[] array = obj as EventStructs.StaminaFlag[];
				translator.PushEventStructsStaminaFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.ShieldFlag[]))
			{
			    EventStructs.ShieldFlag[] array = obj as EventStructs.ShieldFlag[];
				translator.PushEventStructsShieldFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.CardFlag[]))
			{
			    EventStructs.CardFlag[] array = obj as EventStructs.CardFlag[];
				translator.PushEventStructsCardFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.MoveFlag[]))
			{
			    EventStructs.MoveFlag[] array = obj as EventStructs.MoveFlag[];
				translator.PushEventStructsMoveFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.EffectFlag[]))
			{
			    EventStructs.EffectFlag[] array = obj as EventStructs.EffectFlag[];
				translator.PushEventStructsEffectFlag(L, array[index]);
				return true;
			}
			else if (type == typeof(CharacterFaction[]))
			{
			    CharacterFaction[] array = obj as CharacterFaction[];
				translator.PushCharacterFaction(L, array[index]);
				return true;
			}
			else if (type == typeof(EventStructs.DefaultDamageTypeID[]))
			{
			    EventStructs.DefaultDamageTypeID[] array = obj as EventStructs.DefaultDamageTypeID[];
				translator.PushEventStructsDefaultDamageTypeID(L, array[index]);
				return true;
			}
			else if (type == typeof(Tutorial.TestEnum[]))
			{
			    Tutorial.TestEnum[] array = obj as Tutorial.TestEnum[];
				translator.PushTutorialTestEnum(L, array[index]);
				return true;
			}
			else if (type == typeof(Tutorial.DerivedClass.TestEnumInner[]))
			{
			    Tutorial.DerivedClass.TestEnumInner[] array = obj as Tutorial.DerivedClass.TestEnumInner[];
				translator.PushTutorialDerivedClassTestEnumInner(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.DamageFlag[]))
			{
			    EventStructs.DamageFlag[] array = obj as EventStructs.DamageFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.HealFlag[]))
			{
			    EventStructs.HealFlag[] array = obj as EventStructs.HealFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.StaminaFlag[]))
			{
			    EventStructs.StaminaFlag[] array = obj as EventStructs.StaminaFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.ShieldFlag[]))
			{
			    EventStructs.ShieldFlag[] array = obj as EventStructs.ShieldFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.CardFlag[]))
			{
			    EventStructs.CardFlag[] array = obj as EventStructs.CardFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.MoveFlag[]))
			{
			    EventStructs.MoveFlag[] array = obj as EventStructs.MoveFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.EffectFlag[]))
			{
			    EventStructs.EffectFlag[] array = obj as EventStructs.EffectFlag[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(CharacterFaction[]))
			{
			    CharacterFaction[] array = obj as CharacterFaction[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(EventStructs.DefaultDamageTypeID[]))
			{
			    EventStructs.DefaultDamageTypeID[] array = obj as EventStructs.DefaultDamageTypeID[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Tutorial.TestEnum[]))
			{
			    Tutorial.TestEnum[] array = obj as Tutorial.TestEnum[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Tutorial.DerivedClass.TestEnumInner[]))
			{
			    Tutorial.DerivedClass.TestEnumInner[] array = obj as Tutorial.DerivedClass.TestEnumInner[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}