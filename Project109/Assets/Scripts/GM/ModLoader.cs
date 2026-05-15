using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ModLoader
{
    // 로딩된 데이터를 보관할 글로벌 딕셔너리
    // Key: effectName (고유 ID), Value: EffectData
    public Dictionary<string, EffectData> EffectDatabase { get; private set; } = new Dictionary<string, EffectData>();

    private static ModLoader _instance;
    public static ModLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ModLoader();
            }
            return _instance;
        }
    }

    private ModLoader() { }

    public void LoadAllMods()
    {
        EffectDatabase.Clear();

        // 1. 바닐라(Core) 모드 로딩
        // 경로: Assets/StreamingAssets/Mods/Core
        string coreModPath = Path.Combine(Application.streamingAssetsPath, "Mods", "Core");
        LoadModDirectory(coreModPath);

        // 2. 유저 모드 로딩
        // 경로: C:/Users/.../AppData/LocalLow/Company/Project109/mods
        string userModsPath = Path.Combine(Application.persistentDataPath, "mods");
        if (Directory.Exists(userModsPath))
        {
            string[] userModDirs = Directory.GetDirectories(userModsPath);
            foreach (string modDir in userModDirs)
            {
                LoadModDirectory(modDir);
            }
        }
        else
        {
            // 유저 모드 폴더가 없으면 자동 생성
            Directory.CreateDirectory(userModsPath);
        }

        Debug.Log($"[ModLoader] 총 {EffectDatabase.Count}개의 이펙트 데이터가 로드되었습니다.");
    }

    private void LoadModDirectory(string modDir)
    {
        if (!Directory.Exists(modDir)) return;

        Debug.Log($"[ModLoader] 모드 폴더 탐색 중: {modDir}");

        // [루아 스크립트 경로 등록]
        // 모드가 가진 Scripts 폴더를 LuaManager에 검색 경로로 등록합니다.
        // (LuaManager는 내부적으로 하위 폴더(Scripts/Effects, Scripts/Relics 등)를 모두 자동 탐색합니다)
        string scriptsPath = Path.Combine(modDir, "Scripts");
        if (Directory.Exists(scriptsPath))
        {
            LuaManager.Instance.AddSearchPath(scriptsPath);
        }

        // [이펙트(Effect) 로딩]
        // 모드 폴더 하위의 "YAML/Effects" 폴더만 특정하여 로드합니다.
        string effectsPath = Path.Combine(modDir, "YAML", "Effects");
        if (Directory.Exists(effectsPath))
        {
            LoadEffectsFromPath(effectsPath, modDir);
        }

        // 추후 추가될 데이터들 (예: Relics, Cards 등)
        // string relicsPath = Path.Combine(modDir, "YAML", "Relics");
        // if (Directory.Exists(relicsPath)) LoadRelicsFromPath(relicsPath, modDir);
    }

    private void LoadEffectsFromPath(string dataPath, string modRootPath)
    {
        // ModManager.cs에서 사용하신 네이밍 컨벤션 재사용
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        // Effects 폴더 내의 모든 .yaml 파일 탐색
        string[] yamlFiles = Directory.GetFiles(dataPath, "*.yaml", SearchOption.AllDirectories);

        foreach (string file in yamlFiles)
        {
            string yamlContent = File.ReadAllText(file);
            try
            {
                // 1. 파일 하나당 1개의 EffectData가 들어있다고 가정하고 파싱
                EffectData effectData = deserializer.Deserialize<EffectData>(yamlContent);

                if (effectData != null)
                {
                    // 2. IModAssetResolver 인터페이스를 통한 에셋 링킹 및 검증
                    if (effectData.ResolveAndValidate(modRootPath))
                    {
                        // 3. 딕셔너리에 저장! (Key가 같으면 덮어쓰기 됨 = 모드 Overwrite 구현 완료)
                        EffectDatabase[effectData.effectName] = effectData;
                    }
                    else
                    {
                        Debug.LogWarning($"[ModLoader] 무결성 검증 실패로 이펙트가 무시되었습니다: {file}");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[ModLoader] YAML 파싱 에러 ({file}):\n{e.Message}");
            }
        }
    }
}
