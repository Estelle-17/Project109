using YamlDotNet.Serialization;
using UnityEngine;
using System.IO;

public class RelicData : IModAssetResolver, IIdentifiable
{
    // relicName이 고유 식별자(ID) 역할을 겸합니다.
    public string relicName { get; set; }
    public string classType { get; set; }
    public int rarity { get; set; }
    public bool canUpgrade { get; set; }
    public string description { get; set; }
    public string upgradeDescription { get; set; }


    [YamlIgnore]
    public Sprite iconSprite { get; set; }

    public string ID => relicName;

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
