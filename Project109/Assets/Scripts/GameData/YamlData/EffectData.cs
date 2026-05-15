using YamlDotNet.Serialization;
using UnityEngine;
using System.IO;

public class EffectData : IModAssetResolver
{
    // effectName이 고유 식별자(ID) 역할을 겸합니다.
    public string effectName { get; set; }
    public string description { get; set; }
    public EffectType effectType { get; set; }
    public bool isPermanent { get; set; }
    public bool isIndependent { get; set; } // true일 경우, 중첩(Stack)되지 않고 새로운 인스턴스로 각각 존재합니다.
    public int maxStack { get; set; } = int.MaxValue;

    // 모더가 작성한 Lua 스크립트명 (예: "StrengthEffect")
    public string scriptName { get; set; }

    // 모더가 제공한 커스텀 아이콘의 상대 경로 (예: "Icons/strength.png")
    public string imagePath { get; set; }

    // 런타임에 imagePath를 통해 디스크에서 직접 읽어온 스프라이트 
    // (YAML 파싱 대상에서 제외하기 위해 [YamlIgnore] 사용)
    [YamlIgnore]
    public Sprite iconSprite { get; set; }

    public bool ResolveAndValidate(string modDirectory)
    {
        bool isValid = true;

        // 1. 아이콘 스프라이트 링크 (미리 로딩)
        if (!string.IsNullOrEmpty(imagePath))
        {
            string fullImagePath = Path.Combine(modDirectory, imagePath);
            if (File.Exists(fullImagePath))
            {
                // ImageLoader를 사용하여 이미지 캐싱
                this.iconSprite = ImageLoader.LoadCustomSprite(fullImagePath);
            }
            else
            {
                Debug.LogError($"[EffectData: {effectName}] 아이콘 파일을 찾을 수 없습니다: {fullImagePath}");
                isValid = false;
            }
        }

        // 2. 루아 스크립트 존재 여부 검증
        if (!string.IsNullOrEmpty(scriptName))
        {
            string expectedScriptPath = Path.Combine(modDirectory, scriptName + ".lua");
            if (!File.Exists(expectedScriptPath))
            {
                Debug.LogError($"[EffectData: {effectName}] 연동할 루아 스크립트 파일을 찾을 수 없습니다: {expectedScriptPath}");
                isValid = false;
            }
        }

        return isValid;
    }
}
