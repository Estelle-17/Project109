using UnityEngine;
using XLua;

/// <summary>
/// ModLoader를 통해 메모리에 캐싱된 Data Class(YAML)를 바탕으로
/// 실제 인게임에서 동작하는 런타임 Object(인스턴스)를 생성하는 팩토리 클래스입니다.
/// </summary>
public static class ModObjectFactory
{
    /// <summary>
    /// 이펙트 ID를 기반으로 EffectBase 인스턴스를 생성합니다.
    /// </summary>
    public static EffectBase CreateEffect(string effectId)
    {
        // 1. 원본 데이터 검색
        if (!ModLoader.Instance.EffectDatabase.TryGetValue(effectId, out EffectData data))
        {
            Debug.LogError($"[ModObjectFactory] '{effectId}' 이펙트 데이터를 찾을 수 없습니다.");
            return null;
        }

        // 2. Lua 로직 테이블 로드 (require 사용)
        LuaTable luaLogic = null;
        if (!string.IsNullOrEmpty(data.effectName))
        {
            try
            {
                // LuaEnv를 통해 스크립트를 실행하여 반환된 테이블을 가져옴
                object[] results = LuaManager.Instance.luaEnv.DoString($"return require('{data.effectName}')");
                if (results != null && results.Length > 0)
                {
                    luaLogic = results[0] as LuaTable;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[ModObjectFactory] '{data.effectName}' Lua 스크립트 로드 실패:\n{e.Message}");
            }
        }

        // 3. 인스턴스 조립 및 반환
        return new EffectBase(data, luaLogic);
    }

    /// <summary>
    /// 유물 ID를 기반으로 RelicBase 인스턴스를 생성합니다.
    /// (추후 ModLoader에 RelicDatabase가 추가되었다고 가정)
    /// </summary>
    public static RelicBase CreateRelic(string relicId, Player owner)
    {
        // 1. 원본 데이터 검색
        if (!ModLoader.Instance.RelicDatabase.TryGetValue(relicId, out RelicData data))
        {
            Debug.LogError($"[ModObjectFactory] '{relicId}' 유물 데이터를 찾을 수 없습니다.");
            return null;
        }

        // 2. Lua 로직 테이블 로드
        LuaTable luaLogic = null;
        if (!string.IsNullOrEmpty(data.relicName))
        {
            try
            {
                object[] results = LuaManager.Instance.luaEnv.DoString($"return require('{data.relicName}')");
                if (results != null && results.Length > 0)
                {
                    luaLogic = results[0] as LuaTable;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[ModObjectFactory] '{data.relicName}' Lua 스크립트 로드 실패:\n{e.Message}");
            }
        }

        // 3. 인스턴스 조립 및 반환 (RelicBase는 owner를 받음)
        return new RelicBase(data, owner, luaLogic);
    }
}
