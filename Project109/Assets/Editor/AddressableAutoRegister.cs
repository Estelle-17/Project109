using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.IO;

public class AddressableAutoRegister : Editor
{
    private const string MAP_DATA_GROUP = "MapDataGroup";
    private const string UI_GROUP = "UI_Prefabs";
    private const string NPC_GROUP = "NPC_Prefabs";
    private const string MAP_PREFAB_GROUP = "Map_Prefabs";

    private const string UI_PATH = "Assets/Prefab/UI";
    private const string NPC_PATH = "Assets/Prefab/NPC";
    private const string MAP_PATH = "Assets/Prefab/Map";

    [MenuItem("Tools/Addressables/모든 에셋 자동 등록")]
    public static void RegisterAll()
    {
        RegisterAllMapData();
        RegisterAllUIPrefabs();
        RegisterAllNPCPrefabs();
        RegisterAllMapPrefabs();
    }

    [MenuItem("Tools/Addressables/맵 데이터 자동 등록 (MapDataSO)")]
    public static void RegisterAllMapData()
    {
        RegisterByType(MAP_DATA_GROUP, "t:MapDataSO", "Map");
    }

    [MenuItem("Tools/Addressables/UI 프리팹 자동 등록 (UI)")]
    public static void RegisterAllUIPrefabs()
    {
        RegisterFromFolder(UI_GROUP, UI_PATH, "UI");
    }

    [MenuItem("Tools/Addressables/NPC 프리팹 자동 등록 (NPC)")]
    public static void RegisterAllNPCPrefabs()
    {
        // NPC 프리팹들은 3D 공간에 생성되는 오브젝트이므로 AssetCacheManager의 modelKey("Model") 라벨을 부여해 modelDict에 로드되도록 합니다.
        RegisterFromFolder(NPC_GROUP, NPC_PATH, "Model");
    }

    [MenuItem("Tools/Addressables/맵 프리팹 자동 등록 (Map)")]
    public static void RegisterAllMapPrefabs()
    {
        // 맵 생성 루트 프리팹이나 맵 관련 프리팹들을 Model 라벨로 등록하여 캐싱에 포함되도록 합니다.
        RegisterFromFolder(MAP_PREFAB_GROUP, MAP_PATH, "Model");
    }

    /// <summary>
    /// 특정 에셋 타입을 기반으로 Addressables에 등록합니다.
    /// </summary>
    private static void RegisterByType(string groupName, string filterString, string labelName)
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressable Settings를 찾을 수 없습니다. Addressables 창을 열어 초기화해주세요.");
            return;
        }

        AddressableAssetGroup targetGroup = settings.FindGroup(groupName);
        if (targetGroup == null)
        {
            targetGroup = settings.CreateGroup(groupName, false, false, false, settings.DefaultGroup.Schemas);
        }

        string[] guids = AssetDatabase.FindAssets(filterString);
        int registeredCount = 0;

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = Path.GetFileNameWithoutExtension(assetPath);

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, targetGroup);
            if (entry != null)
            {
                entry.SetAddress(assetName);
                if (!string.IsNullOrEmpty(labelName))
                {
                    entry.SetLabel(labelName, true);
                }
                registeredCount++;
            }
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true, true);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=green>[Addressables] '{groupName}' 그룹에 {registeredCount}개의 에셋(타입: {filterString}) 등록 완료!</color>");
    }

    /// <summary>
    /// 특정 폴더 아래의 모든 프리팹(.prefab) 파일을 찾아 Addressables에 등록합니다.
    /// </summary>
    private static void RegisterFromFolder(string groupName, string folderPath, string labelName)
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressable Settings를 찾을 수 없습니다. Addressables 창을 열어 초기화해주세요.");
            return;
        }

        AddressableAssetGroup targetGroup = settings.FindGroup(groupName);
        if (targetGroup == null)
        {
            targetGroup = settings.CreateGroup(groupName, false, false, false, settings.DefaultGroup.Schemas);
        }

        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning($"폴더를 찾을 수 없습니다: {folderPath}");
            return;
        }

        string[] fileEntries = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);
        int registeredCount = 0;

        foreach (string filePath in fileEntries)
        {
            string relativePath = filePath.Replace("\\", "/");
            string guid = AssetDatabase.AssetPathToGUID(relativePath);

            if (string.IsNullOrEmpty(guid)) continue;

            string assetName = Path.GetFileNameWithoutExtension(relativePath);

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, targetGroup);
            if (entry != null)
            {
                entry.SetAddress(assetName);
                if (!string.IsNullOrEmpty(labelName))
                {
                    entry.SetLabel(labelName, true);
                }
                registeredCount++;
            }
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true, true);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=green>[Addressables] '{groupName}' 그룹에 {registeredCount}개의 프리팹(폴더: {folderPath}) 등록 완료!</color>");
    }
}
