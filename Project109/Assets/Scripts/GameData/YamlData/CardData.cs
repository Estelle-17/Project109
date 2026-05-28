using GameItem.Types;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using YamlDotNet.Serialization;

public class CardData : IModAssetResolver
{
    // cardName이 고유 식별자(ID) 역할을 겸합니다.
    public string cardName { get; set; }

    // 카드가 속한 영웅 클래스 (공용 카드일 경우 비어있거나 "All")
    public List<string> classTypes { get; set; } = new List<string>();

    public string cardType { get; set; }
    public string targetType { get; set; }
    public int targetMinDistance { get; set; }
    public int targetMaxDistance { get; set; }
    public List<EffectArea> additionalEffectAreaList { get; set; } = new();

    public int rarity { get; set; }
    public int stamina { get; set; }

    // 마스터리 최대 포인트
    public int maxMasteryPoint { get; set; }

    // 업그레이드 가능한 카드인지 여부
    public bool isUpgradable { get; set; }

    // 업그레이드된 카드인지 여부
    public bool isUpgraded { get; set; }

    // 업그레이드 시 변환될 카드의 이름(ID)
    public string upgradedCardName { get; set; }

    // 카드 효과 템플릿 설명 (동적 토큰 지원 가능)
    public string description { get; set; }

    // 대안 B: 기본 수치 및 마스터리 업그레이드 수치 정의
    public Dictionary<string, float> baseValues { get; set; } = new();
    public Dictionary<string, Dictionary<string, float>> masteryUpgrades { get; set; } = new();

    // 런타임에 cardName을 통해 자동으로 로드되는 리소스들 (YAML 파싱에서 제외)
    [YamlIgnore]
    public Sprite cardSprite { get; set; }

    [YamlIgnore]
    public LuaTable luaPrototype { get; set; }

    /// <summary>
    /// 현재 적용된 마스터리 업그레이드 레벨에 따라 최종 수치를 계산하여 반환합니다.
    /// </summary>
    public float GetEffectiveValue(string valueKey)
    {
        float val = 0;
        if (baseValues != null && baseValues.TryGetValue(valueKey, out float baseVal))
        {
            val = baseVal;
        }

        // CardMasteryManager에서 현재 카드의 업그레이드 목록(Dictionary<masteryId, level>)을 가져옵니다.

        if (CardMasteryManager.instance != null)
        {
            var activeUpgrades = CardMasteryManager.instance.GetCardMasteryUpgrades(this.cardName);
            if (activeUpgrades != null)
            {
                foreach (var upgrade in activeUpgrades)
                {
                    string masteryId = upgrade.Key;
                    int level = upgrade.Value;

                    if (masteryUpgrades != null && masteryUpgrades.TryGetValue(masteryId, out var modifierDict))
                    {
                        if (modifierDict != null && modifierDict.TryGetValue(valueKey, out float modifier))
                        {
                            val += modifier * level;
                        }
                    }
                }
            }
        }
        return val;
    }

    public bool ResolveAndValidate(string modDirectory)
    {
        bool isValid = true;

        // 1. 카드 일러스트/아이콘 로드 (cardName으로 자동 매핑)
        string expectedImagePath = Path.Combine(modDirectory, "Icons", "Cards", cardName + ".png");
        if (File.Exists(expectedImagePath))
        {
            this.cardSprite = ImageLoader.LoadCustomSprite(expectedImagePath, 256);
        }
        else
        {
            // 폴백 이미지 로드
            string defaultImagePath = Path.Combine(modDirectory, "Icons", "Cards", "Default.png");
            if (File.Exists(defaultImagePath))
            {
                this.cardSprite = ImageLoader.LoadCustomSprite(defaultImagePath, 256);
            }
            else
            {
                Debug.LogWarning($"[CardData: {cardName}] 일러스트를 찾을 수 없지만 실행을 계속합니다.");
            }
        }

        // 2. Lua 스크립트 검증 및 사전 로딩 (cardName으로 경로 자동 추론)
        string expectedScriptPath = Path.Combine(modDirectory, "Scripts", "Cards", cardName + ".lua");
        if (File.Exists(expectedScriptPath))
        {
            try
            {
                byte[] scriptBytes = File.ReadAllBytes(expectedScriptPath);

                // 루아 스크립트 실행 (컴파일 및 프로토타입 획득)

                object[] results = LuaManager.Instance.luaEnv.DoString(scriptBytes, cardName);


                LuaTable proto = null;
                if (results != null && results.Length > 0)
                {
                    proto = results[0] as LuaTable;
                }

                // [폴백] return이 누락되었을 경우를 대비하여 전역 환경에서 변수 검색
                if (proto == null)
                {
                    proto = LuaManager.Instance.luaEnv.Global.Get<LuaTable>("card");
                    proto ??= LuaManager.Instance.luaEnv.Global.Get<LuaTable>(cardName);
                }

                if (proto != null)
                {
                    this.luaPrototype = proto;
                }
                else
                {
                    Debug.LogError($"[CardData: {cardName}] 루아 파일 로드 실패: 테이블 형식이 아닙니다.");
                    isValid = false;
                }

                // 글로벌 오염 방지를 위해 임시 등록 변수 해제
                LuaManager.Instance.luaEnv.Global.Set<string, object>("card", null);
                LuaManager.Instance.luaEnv.Global.Set<string, object>(cardName, null);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[CardData: {cardName}] 루아 구문 오류:\n{e.Message}");
                isValid = false;
            }
        }
        else
        {
            Debug.LogError($"[CardData: {cardName}] 연동할 루아 스크립트 파일을 찾을 수 없습니다: {expectedScriptPath}");
            isValid = false;
        }

        return isValid;
    }
}
