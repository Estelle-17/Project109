using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableDataLoader : MonoBehaviour
{
    public static AddressableDataLoader instance;

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

    public static AddressableDataLoader Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public string cardKey = "Card";
    public string relicKey = "Relic";
    public string eventKey = "Event";
    public string battleNodeKey = "BattleNode";
    public string monsterKey = "Monster";

    public IList<ActionCardData> cardList;
    private Dictionary<string, ActionCardData> cardDict = new Dictionary<string, ActionCardData>();

    public IList<RelicData> relicList;
    private Dictionary<string, RelicData> relicDict = new Dictionary<string, RelicData>();

    public IList<EventData> eventList;
    private Dictionary<string, EventData> eventDict = new Dictionary<string, EventData>();

    public IList<BattleNodeData> battleNodeList;
    private Dictionary<string, BattleNodeData> battleNodeDict = new Dictionary<string, BattleNodeData>();

    public IList<MonsterData> monsterList;
    private Dictionary<string, MonsterData> monsterDict = new Dictionary<string, MonsterData>();

    void Start()
    {
        LoadAllData();
    }

    void LoadAllData()
    {
        LoadAndCache<ActionCardData>(cardKey, (list, dict) =>
        {
            cardList = list;
            cardDict = dict;
        });
        LoadAndCache<RelicData>(relicKey, (list, dict) =>
        {
            relicList = list;
            relicDict = dict;
        });
        LoadAndCache<EventData>(eventKey, (list, dict) =>
        {
            eventList = list;
            eventDict = dict;
        });
        LoadAndCache<BattleNodeData>(battleNodeKey, (list, dict) =>
        {
            battleNodeList = list;
            battleNodeDict = dict;
        });
        LoadAndCache<MonsterData>(monsterKey, (list, dict) =>
        {
            monsterList = list;
            monsterDict = dict;
        });
    }

    /// <summary>
    /// Addressable 키를 통해 데이터를 비동기로 불러오고 후처리 실행
    /// </summary>
    void LoadAndCache<T>(string key, Action<IList<T>, Dictionary<string, T>> onLoaded) where T : ScriptableObject, IIdentifiable
    {
        Addressables.LoadAssetsAsync<T>(key, null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //불러온 데이터 저장
                var list = handle.Result;
                var dict = new Dictionary<string, T>();

                //Dictionary에 데이터 저장
                foreach (var item in list)
                {
                    if (!dict.ContainsKey(item.ID))
                        dict[item.ID] = item;
                }

                //저장된 데이터 반환
                onLoaded?.Invoke(list, dict);
            }
            else
            {
                Debug.LogError($"{typeof(T).Name} Addressables 로드 실패");
                onLoaded?.Invoke(null, null);
            }
        };
    }

    public bool TryGetCard(string name, out ActionCardData card) => cardDict.TryGetValue(name, out card);
    public bool TryGetMonster(string name, out MonsterData monster) => monsterDict.TryGetValue(name, out monster);
    public bool TryGetRelic(string name, out RelicData relic) => relicDict.TryGetValue(name, out relic);
    public bool TryGetEvent(string name, out EventData ev) => eventDict.TryGetValue(name, out ev);
    public bool TryGetBattleNode(string name, out BattleNodeData battleNode) => battleNodeDict.TryGetValue(name, out battleNode);
}
