using YamlDotNet.Serialization;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class RelicData : IModAssetResolver
{
    // relicName이 고유 식별자(ID) 역할을 겸합니다.
    public string relicName { get; set; }
    
    // 등장 가능한 캐릭터 클래스 목록.
    // 비어있거나 "All" 또는 "None" 포함 시 공용 유물로 취급됩니다.
    // YAML 예시 (전사/마법사 전용): classTypes:\n  - Warrior\n  - Wizard
    public List<string> classTypes { get; set; } = new List<string>();
    
    public GameItem.Types.RelicRarity rarity { get; set; }
    
    // 업그레이드 시 변환될 유물의 ID (null이거나 비어있으면 업그레이드 불가)
    public string upgradedRelicId { get; set; }
    
    // 효과에 대한 기계적 설명 (예: "매 턴 시작 시 힘 +1")
    // {placeholder} 문법으로 동적 수치 표현 가능 (예: "공격력 {damage}만큼 피해")
    public string description { get; set; }

    // 분위기/세계관 텍스트. 효과 설명과 분리된 풍미 문구입니다.
    public string flavorText { get; set; }

    /// <summary>
    /// 플레이어의 클래스에 이 유물이 등장할 수 있는지 체크합니다.
    /// </summary>
    public bool CanAppearForClass(string playerClass)
    {
        if (classTypes == null || classTypes.Count == 0 || classTypes.Contains("None") || classTypes.Contains("All"))
        {
            return true;
        }
        return classTypes.Contains(playerClass);
    }

    [YamlIgnore]
    public Sprite iconSprite { get; set; }

    public bool ResolveAndValidate(string modDirectory)
    {
        bool isValid = true;

        // 1. 아이콘 스프라이트 링크 (relicName으로 자동 추론)
        string expectedImagePath = Path.Combine(modDirectory, "Icons", "Relics", relicName + ".png");
        
        if (File.Exists(expectedImagePath))
        {
            // 최대 128x128 픽셀로 제한하여 로드
            this.iconSprite = ImageLoader.LoadCustomSprite(expectedImagePath, 128);
        }
        else
        {
            // 전용 아이콘이 없을 경우 기본 아이콘(Default.png)으로 대체
            string defaultImagePath = Path.Combine(modDirectory, "Icons", "Relics", "Default.png");
            if (File.Exists(defaultImagePath))
            {
                this.iconSprite = ImageLoader.LoadCustomSprite(defaultImagePath, 128);
            }
            else
            {
                Debug.LogWarning($"[RelicData: {relicName}] 전용 아이콘 및 기본 아이콘(Default.png)을 찾을 수 없지만 실행을 계속합니다.");
            }
        }

        // 2. 루아 스크립트 존재 여부 검증 (relicName으로 자동 추론)
        string expectedScriptPath = Path.Combine(modDirectory, "Scripts", "Relics", relicName + ".lua");
        if (!File.Exists(expectedScriptPath))
        {
            Debug.LogError($"[RelicData: {relicName}] 연동할 루아 스크립트 파일을 찾을 수 없습니다: {expectedScriptPath}");
            isValid = false;
        }

        return isValid;
    }
}
