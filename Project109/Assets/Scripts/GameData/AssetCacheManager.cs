using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

struct StageMapDataBundle : IIdentifiable
{
    public string stageName;
    public List<MapDataSO> battleMapDataList;
    public List<MapDataSO> eliteMapDataList;
    public List<MapDataSO> bossMapDataList;
    public List<MapDataSO> secretMapDataList;
    public List<MapDataSO> storeMapDataList;
    public List<MapDataSO> restoreMapDataList;


    public string ID => stageName;
}

public class AssetCacheManager : MonoBehaviour
{
    public static AssetCacheManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string cardKey = "Card";
    public string eventKey = "Event";
    public string battleKey = "Battle";
    public string monsterKey = "Monster";
    public string monsterRewardKey = "Reward";
    public string characterKey = "Character";
    public string mapKey = "Map";
    public string mapInfoKey = "MapInfo";
    public string modelKey = "Model";
    public string textureKey = "Texture";
    public string obstacleKey = "Obstacle";
    public string trapKey = "Trap";


    public IList<EventData> eventList;
    private Dictionary<string, EventData> eventDict = new Dictionary<string, EventData>();

    public IList<EventData> specificEventList;
    private Dictionary<string, EventData> specificEventDict = new Dictionary<string, EventData>();

    public IList<BattleData> battleList;
    private Dictionary<string, BattleData> battleDict = new Dictionary<string, BattleData>();

    public IList<MonsterData> monsterList;
    private Dictionary<string, MonsterData> monsterDict = new Dictionary<string, MonsterData>();

    public IList<MonsterRewardData> monsterRewardList;
    private Dictionary<string, MonsterRewardData> monsterRewardDict = new Dictionary<string, MonsterRewardData>();

    public IList<CharacterData> characterList;
    private Dictionary<string, CharacterData> characterDict = new Dictionary<string, CharacterData>();

    public IList<MapDataSO> mapList;
    private Dictionary<string, MapDataSO> mapDict = new Dictionary<string, MapDataSO>();
    private Dictionary<string, StageMapDataBundle> stageMapDataDict = new Dictionary<string, StageMapDataBundle>();

    public IList<MapDataInfo> mapInfoList;
    private Dictionary<string, MapDataInfo> mapInfoDict = new Dictionary<string, MapDataInfo>();

    public IList<GameObject> modelList;
    private Dictionary<string, GameObject> modelDict = new Dictionary<string, GameObject>();

    public IList<Sprite> textureList;
    private Dictionary<string, Sprite> textureDict = new Dictionary<string, Sprite>();



    public IList<ObstacleData> obstacleList;
    private Dictionary<string, ObstacleData> obstacleDict = new Dictionary<string, ObstacleData>();

    public IList<TrapData> trapList;
    private Dictionary<string, TrapData> trapDict = new Dictionary<string, TrapData>();

    private IEnumerator Start()
    {


        //이벤트 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheFromAddressableData<EventData>(eventKey, (list, dict) =>
        {
            eventList = list;
            eventDict = dict;

            //특정 이벤트 데이터 분리 작업
            specificEventList = new List<EventData>();
            specificEventDict = new Dictionary<string, EventData>();
            //역순으로 순회
            for (int index = list.Count - 1; index >= 0; index--)
            {
                //4: 랜덤으로 발견할 수 없는 특정 이벤트
                if (list[index].eventAppearLevel == 4)
                {
                    specificEventList.Add(list[index]);
                    specificEventDict[list[index].ID] = list[index];

                    //원본 리스트 및 딕셔너리에서 제거
                    eventDict.Remove(list[index].ID);
                    eventList.Remove(list[index]);
                }
            }
        }));

        //전투 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheFromAddressableData<BattleData>(battleKey, (list, dict) =>
        {
            battleList = list;
            battleDict = dict;
        }));

        //몬스터 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheFromAddressableData<MonsterData>(monsterKey, (list, dict) =>
        {
            monsterList = list;
            monsterDict = dict;
        }));

        //몬스터 보상 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheFromAddressableData<MonsterRewardData>(monsterRewardKey, (list, dict) =>
        {
            monsterRewardList = list;
            monsterRewardDict = dict;
        }));

        //캐릭터 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheFromAddressableData<CharacterData>(characterKey, (list, dict) =>
        {
            characterList = list;
            characterDict = dict;
        }));

        //맵 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheFromAddressableData<MapDataSO>(mapKey, (list, dict) =>
        {
            mapList = list;
            mapDict = dict;

            //맵 데이터를 스테이지별로 분류하여 저장
            foreach (var mapData in list)
            {
                if (!stageMapDataDict.ContainsKey(mapData.stageName))
                {
                    stageMapDataDict[mapData.stageName] = new StageMapDataBundle
                    {
                        stageName = mapData.stageName,
                        battleMapDataList = new List<MapDataSO>(),
                        eliteMapDataList = new List<MapDataSO>(),
                        bossMapDataList = new List<MapDataSO>(),
                        secretMapDataList = new List<MapDataSO>(),
                        storeMapDataList = new List<MapDataSO>(),
                        restoreMapDataList = new List<MapDataSO>()
                    };
                }
                switch (mapData.incountType)
                {
                    case IncountType.Battle:
                        stageMapDataDict[mapData.stageName].battleMapDataList.Add(mapData);
                        break;
                    case IncountType.Elite:
                        stageMapDataDict[mapData.stageName].eliteMapDataList.Add(mapData);
                        break;
                    case IncountType.Boss:
                        stageMapDataDict[mapData.stageName].bossMapDataList.Add(mapData);
                        break;
                    case IncountType.Secret:
                        stageMapDataDict[mapData.stageName].secretMapDataList.Add(mapData);
                        break;
                    case IncountType.Store:
                        stageMapDataDict[mapData.stageName].storeMapDataList.Add(mapData);
                        break;
                    case IncountType.Restore:
                        stageMapDataDict[mapData.stageName].restoreMapDataList.Add(mapData);
                        break;
                }
            }
        }));

        yield return StartCoroutine(LoadAndCacheFromAddressableData<MapDataInfo>(mapInfoKey, (list, dict) =>
        {
            mapInfoList = list;
            mapInfoDict = dict;
        }));

        //모델링 및 오브젝트 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheGameObjectFromAddressableData(modelKey, (list, dict) =>
        {
            modelList = list;
            modelDict = dict;
        }));

        //텍스처 데이터 할당 시작
        yield return StartCoroutine(LoadAndCacheTextureFromAddressableData(textureKey, (list, dict) =>
        {
            textureList = list;
            textureDict = dict;
        }));


        yield return StartCoroutine(LoadAndCacheFromAddressableData<ObstacleData>(obstacleKey, (list, dict) =>
        {
            obstacleList = list;
            obstacleDict = dict;
        }));

        yield return StartCoroutine(LoadAndCacheFromAddressableData<TrapData>(trapKey, (list, dict) =>
        {
            trapList = list;
            trapDict = dict;
        }));

        ModManager modManager = GetComponent<ModManager>();
        if (modManager != null)
        {
            yield return StartCoroutine(modManager.StartModLoading());
        }
        else
        {
            Debug.LogWarning("Cannot find ModManager.");
        }

        // YAML 기반 모드 로드 (이펙트, 유물, 카드 데이터 데이터베이스 구축)
        ModLoader.Instance.LoadAllMods();

        Debug.Log("All Data Load is Complete.");

        if (SceneLoadManager.instance != null)
        {
            SceneLoadManager.instance.ActiveStartButton();
        }
    }

    public IEnumerator LoadAllAssetsFromBundle(string key, string bundlePath)
    {
        if(!File.Exists(bundlePath))
        {
            Debug.Log($"번들 파일을 찾을 수 없습니다. {bundlePath}");
            yield break;
        }

        //에셋 번들 로드
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return request;

        AssetBundle loadedAssetBundle = request.assetBundle;
        if (loadedAssetBundle == null)
        {
            Debug.LogError("에셋 번들 로드에 실패하였습니다.");
            yield break;
        }

        //번들 내에 저장된 모든 에셋 이름 가져오기
        string[] allAssetNames = loadedAssetBundle.GetAllAssetNames();

        Debug.Log($"Find {allAssetNames.Length} Assets");

        //각 에셋을 반복문으로 로드
        foreach (string assetName in allAssetNames)
        {
            Debug.Log($"Load: {assetName}");

            GameObject asset = loadedAssetBundle.LoadAsset<GameObject>(assetName);
            if (asset != null)
            {
                //새로운 오브젝트를 캐시에 등록
                SetNewAssetInCache(key, asset);
            }
            else
            {
                Debug.Log($"Load falied: {assetName}");
            }
        }

        loadedAssetBundle.Unload(false);
    }

    private void SetNewAssetInCache(string key, GameObject newObject)
    {
        switch (key)
        {
            case "Model":
                Debug.Log($"Cache new Data {modelDict[newObject.name].name} -> {newObject.name}");
                modelDict[newObject.name] = newObject;
                break;
        }
    }

    /// <summary>
    /// Addressable 키를 통해 데이터를 비동기로 불러오고 후처리 실행
    /// </summary>
    IEnumerator LoadAndCacheFromAddressableData<T>(string key, Action<IList<T>, Dictionary<string, T>> onLoaded) where T : ScriptableObject, IIdentifiable
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(key, null);

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //불러온 데이터 저장
            var list = handle.Result;
            var dict = new Dictionary<string, T>();

            //Dictionary에 데이터 저장
            foreach (var item in list)
            {
                if (!dict.ContainsKey(item.ID))
                {
                    dict[item.ID] = item;
                    //Debug.Log($"{item.ID} Load");
                }
            }

            Debug.Log($"{key}, {dict.Count} item Load Complete");
            //저장된 데이터 반환
            onLoaded?.Invoke(list, dict);
        }
        else
        {
            Debug.LogError($"{typeof(T).Name} Addressables 로드 실패");
            onLoaded?.Invoke(null, null);
        }
    }

    /// <summary>
    /// Addressable 키를 통해 오브젝트를 비동기로 불러오고 후처리 실행
    /// </summary>
    IEnumerator LoadAndCacheGameObjectFromAddressableData(string key, Action<IList<GameObject>, Dictionary<string, GameObject>> onLoaded)
    {
        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(key, null);

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //불러온 데이터 저장
            var list = handle.Result;
            var dict = new Dictionary<string, GameObject>();

            //Dictionary에 데이터 저장
            foreach (var item in list)
            {
                if (!dict.ContainsKey(item.name))
                {
                    dict[item.name] = item;
                    Debug.Log($"{item.name} Load");
                }
            }

            Debug.Log($"{key}, {dict.Count} item Load Complete");
            //저장된 데이터 반환
            onLoaded?.Invoke(list, dict);
        }
        else
        {
            Debug.LogError($"{key} Addressables 로드 실패");
            onLoaded?.Invoke(null, null);
        }
    }

    /// <summary>
    /// Addressable 키를 통해 텍스처를 비동기로 불러오고 후처리 실행
    /// </summary>
    IEnumerator LoadAndCacheTextureFromAddressableData(string key, Action<IList<Sprite>, Dictionary<string, Sprite>> onLoaded)
    {
        AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetsAsync<Sprite>(key, null);

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //불러온 데이터 저장
            var list = handle.Result;
            var dict = new Dictionary<string, Sprite>();

            //Dictionary에 데이터 저장
            foreach (var item in list)
            {
                if (!dict.ContainsKey(item.name))
                {
                    dict[item.name] = item;
                }
            }

            Debug.Log($"{key}, {dict.Count} item Load Complete");
            //저장된 데이터 반환
            onLoaded?.Invoke(list, dict);
        }
        else
        {
            Debug.LogError($"{key} Addressables 로드 실패");
            onLoaded?.Invoke(null, null);
        }
    }


    public bool TryGetMonster(string name, out MonsterData monster) => monsterDict.TryGetValue(name, out monster);
    public bool TryGetMonsterReward(string name, out MonsterRewardData reward) => monsterRewardDict.TryGetValue(name, out reward);
    public bool TryGetEvent(string name, out EventData ev) => eventDict.TryGetValue(name, out ev);
    public bool TryGetSpecificEvent(string name, out EventData ev) => specificEventDict.TryGetValue(name, out ev);
    public bool TryGetBattle(string name, out BattleData battle) => battleDict.TryGetValue(name, out battle);
    public bool TryGetCharacter(string name, out CharacterData character) => characterDict.TryGetValue(name, out character);
    public bool TryGetMap(string name, out MapDataSO mapData) => mapDict.TryGetValue(name, out mapData);
    public bool TryGetMapInfo(string name, out MapDataInfo mapDataInfo) => mapInfoDict.TryGetValue(name, out mapDataInfo);
    public bool TryGetModel(string name, out GameObject model) => modelDict.TryGetValue(name, out model);
    public bool TryGetTexture(string name, out Sprite texture) => textureDict.TryGetValue(name, out texture);
    public bool TryGetObstacle(string name, out ObstacleData obstacle) => obstacleDict.TryGetValue(name, out obstacle);
    public bool TryGetTrap(string name, out TrapData trap) => trapDict.TryGetValue(name, out trap);
}
