using UnityEngine;
using XLua;
using System.IO;
using System.Collections.Generic;

public class LuaManager
{
    // 순수 C# 싱글톤 인스턴스 (Lazy Initialization)
    private static LuaManager _instance;
    public static LuaManager Instance 
    { 
        get 
        { 
            if (_instance == null)
            {
                _instance = new LuaManager();
            }
            return _instance; 
        } 
    }
    
    public LuaEnv luaEnv { get; private set; }
    
    // 외부(ModLoader 등)에서 동적으로 추가해줄 루아 스크립트 검색 경로들
    private List<string> searchPaths = new List<string>();

    // Key: tagName, Value: 프로토타입 LuaTable
    private Dictionary<string, LuaTable> cardTagPrototypes = new Dictionary<string, LuaTable>();

    // 생성자 (접근 제어자를 private으로 막아 외부 생성을 방지)
    private LuaManager()
    {
        InitLuaEnv();

        // 순수 C# 객체는 Unity의 생명주기를 따르지 않으므로, 앱 종료 이벤트를 직접 구독하여 해제
        Application.quitting += Dispose;
    }

    private void InitLuaEnv()
    {
        luaEnv = new LuaEnv();
        
        // XLua 커스텀 로더 등록
        luaEnv.AddLoader(CustomModLoader);

        // 전역 모딩 헬퍼 함수 주입
        luaEnv.DoString(@"
            -- 프로토타입으로부터 개별 인스턴스 테이블 생성 함수
            function NewInstance(proto)
                if not proto then return nil end
                local inst = {}
                setmetatable(inst, { __index = proto })
                return inst
            end

            -- 이펙트 정의 헬퍼 함수
            function DefineEffect(name)
                local effect = {}
                function effect:OnInit(effectBase)
                    self.base = effectBase
                end
                _G[name] = effect -- return 누락 시 폴백용 전역 등록
                return effect
            end

            -- 유물 정의 헬퍼 함수
            function DefineRelic(name)
                local relic = {}
                function relic:OnInit(relicBase)
                    self.base = relicBase
                end
                _G[name] = relic -- return 누락 시 폴백용 전역 등록
                return relic
            end

            -- 카드 태그 정의 헬퍼 함수
            function DefineCardTag(name)
                local cardTag = {}
                function cardTag:OnInit(tagBase, card)
                    self.base = tagBase
                    self.card = card
                end
                _G[name] = cardTag -- return 누락 시 폴백용 전역 등록
                return cardTag
            end

            -- 화이트리스트 테이블 생성 및 CS 전역 공간 제거
            EventStructs = {
                DamageInfo = CS.EventStructs.DamageInfo,
                HealInfo = CS.EventStructs.HealInfo,
                StaminaInfo = CS.EventStructs.StaminaInfo,
                ShieldInfo = CS.EventStructs.ShieldInfo,
                CardInfo = CS.EventStructs.CardInfo,
                MoveInfo = CS.EventStructs.MoveInfo,
                EffectInfo = CS.EventStructs.EffectInfo,

                DamageFlag = CS.EventStructs.DamageFlag,
                HealFlag = CS.EventStructs.HealFlag,
                StaminaFlag = CS.EventStructs.StaminaFlag,
                ShieldFlag = CS.EventStructs.ShieldFlag,
                CardFlag = CS.EventStructs.CardFlag,
                MoveFlag = CS.EventStructs.MoveFlag,
                EffectFlag = CS.EventStructs.EffectFlag,
                
                CardTag = CS.CardTag,
            }

            CS = nil
        ");
    }

    /// <summary>
    /// 모드 로딩 시 루아 스크립트가 존재하는 폴더 경로를 등록합니다.
    /// </summary>
    public void AddSearchPath(string path)
    {
        if (!searchPaths.Contains(path))
        {
            searchPaths.Add(path);
        }
    }

    private byte[] CustomModLoader(ref string filepath)
    {
        string fileName = filepath;
        if (!fileName.EndsWith(".lua"))
        {
            fileName += ".lua";
        }

        // 등록된 모든 검색 경로를 순회하며 파일을 찾음
        foreach (string searchPath in searchPaths)
        {
            if (Directory.Exists(searchPath))
            {
                // 해당 모드 경로 내 모든 하위 디렉토리에서 검색
                string[] files = Directory.GetFiles(searchPath, fileName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    return File.ReadAllBytes(files[0]);
                }
            }
        }

        return null;
    }

    /// <summary>
    /// XLua 내부의 가비지 컬렉션을 수행합니다.
    /// 어딘가(예: RunManager의 Update)에서 주기적으로 호출해주는 것이 좋습니다.
    /// </summary>
    public void Tick()
    {
        luaEnv?.Tick();
    }

    /// <summary>
    /// 모드가 등록한 경로들에서 카드 태그 스크립트(CardTags/{tagName}.lua)를 검색하여 캐싱 및 반환합니다.
    /// </summary>
    public LuaTable GetCardTagPrototype(string tagName)
    {
        if (cardTagPrototypes.TryGetValue(tagName, out var proto))
        {
            return proto;
        }

        string scriptFileName = tagName + ".lua";
        byte[] scriptBytes = null;

        foreach (string searchPath in searchPaths)
        {
            if (Directory.Exists(searchPath))
            {
                string[] files = Directory.GetFiles(searchPath, scriptFileName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    try
                    {
                        scriptBytes = File.ReadAllBytes(files[0]);
                        break;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"[LuaManager] 카드 태그 파일 읽기 실패 ({files[0]}): {ex.Message}");
                    }
                }
            }
        }

        if (scriptBytes != null)
        {
            try
            {
                object[] results = luaEnv.DoString(scriptBytes, tagName);
                LuaTable resultProto = null;
                if (results != null && results.Length > 0)
                {
                    resultProto = results[0] as LuaTable;
                }

                if (resultProto == null)
                {
                    resultProto = luaEnv.Global.Get<LuaTable>(tagName);
                }

                if (resultProto != null)
                {
                    cardTagPrototypes[tagName] = resultProto;
                    return resultProto;
                }
                else
                {
                    Debug.LogError($"[LuaManager] 카드 태그 {tagName} 로드 실패: 리턴된 루아 테이블이 없습니다.");
                }

                luaEnv.Global.Set<string, object>(tagName, null);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[LuaManager] 카드 태그 {tagName} 루아 컴파일 구문 오류:\n{e.Message}");
            }
        }
        else
        {
            Debug.LogError($"[LuaManager] 카드 태그 {tagName}.lua 파일을 찾을 수 없습니다.");
        }

        return null;
    }

    /// <summary>
    /// 게임 종료 시 호출되어 XLua 환경을 안전하게 파괴합니다.
    /// </summary>
    private void Dispose()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
            luaEnv = null;
        }
    }
}
