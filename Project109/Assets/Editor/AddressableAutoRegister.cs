using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.IO;

public class AddressableAutoRegister : Editor
{
    // Addressables 창에 미리 만들어둘 그룹 이름 (없으면 Default Local Group에 들어갑니다)
    private const string TARGET_GROUP_NAME = "MapDataGroup";

    [MenuItem("Tools/Addressables/맵 데이터 자동 등록 (MapDataSO)")]
    public static void RegisterAllMapData()
    {
        // Addressable Settings 파일 가져오기
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressable Settings를 찾을 수 없습니다. Addressables 창을 열어 초기화해주세요.");
            return;
        }

        //타겟 그룹 찾기
        AddressableAssetGroup targetGroup = settings.FindGroup(TARGET_GROUP_NAME);
        if (targetGroup == null)
        {
            Debug.LogWarning($"'{TARGET_GROUP_NAME}' 그룹이 없어 기본 그룹(Default)에 등록합니다.");
            targetGroup = settings.DefaultGroup;
        }

        // 프로젝트 내의 모든 MapDataSO 에셋 검색 (t:타입명)
        // 만약 이름으로 찾고 싶다면 "t:ScriptableObject 특정어원" 식으로 작성 가능합니다.
        string[] guids = AssetDatabase.FindAssets("t:MapDataSO");
        int registeredCount = 0;

        foreach (string guid in guids)
        {
            // GUID를 통해 파일의 실제 경로와 이름 가져오기
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = Path.GetFileNameWithoutExtension(assetPath);

            // Addressable 그룹에 에셋 추가 (이미 있다면 덮어씌움)
            AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, targetGroup);

            if (entry != null)
            {
                // Addressable Key를 파일 이름과 동일하게 설정
                // 이렇게 하면 이전 코드에서 작성한 cell.fixedId = "Spike_Level_3" 처럼 
                // string으로 에셋을 로드할 때 파일명 그대로 호출할 수 있습니다.
                entry.SetAddress(assetName);
                entry.SetLabel("Map", true); // "MapData" 라는 라벨도 추가 (필요에 따라 라벨은 자유롭게 설정 가능)
                registeredCount++;
            }
        }

        // 변경 사항 저장
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true, true);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=green>총 {registeredCount}개의 MapDataSO가 Addressables에 성공적으로 등록/갱신되었습니다!</color>");
    }

    private const string UI_GROUP_NAME = "UI_Prefabs";
    private const string UI_PREFAB_PATH = "Assets/Prefab/UI";

    [MenuItem("Tools/Addressables/UI 프리팹 자동 등록 (UI)")]
    public static void RegisterAllUIPrefabs()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressable Settings를 찾을 수 없습니다. Addressables 창을 열어 초기화해주세요.");
            return;
        }

        //타겟 그룹 찾거나 생성
        AddressableAssetGroup targetGroup = settings.FindGroup(UI_GROUP_NAME);
        if (targetGroup == null)
        {
            targetGroup = settings.CreateGroup(UI_GROUP_NAME, false, false, false, settings.DefaultGroup.Schemas);
        }

        if (!Directory.Exists(UI_PREFAB_PATH))
        {
            Debug.LogWarning($"UI 프리팹 폴더를 찾을 수 없습니다: {UI_PREFAB_PATH}");
            return;
        }

        string[] fileEntries = Directory.GetFiles(UI_PREFAB_PATH, "*.prefab", SearchOption.AllDirectories);
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
                entry.SetLabel("UI", true);
                registeredCount++;
            }
        }

        // 변경 사항 저장
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true, true);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=green>총 {registeredCount}개의 UI 프리팹이 Addressables에 성공적으로 등록/갱신되었습니다!</color>");
    }
}
