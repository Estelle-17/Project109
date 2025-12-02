using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance { get; private set; }

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

    [SerializeField]
    private List<RelicData> relics;

    //카드 데이터 변경 이벤트
    public event Action<RelicData> OnRelicAdded;
    public event Action<string> OnRelicRemoved;

    void Start()
    {
        
    }

    public List<RelicData> GetRelicList()
    {
        return new List<RelicData>(relics);
    }

    //플레이어 덱에 카드 추가
    public RelicData AddRelic(RelicData newCardData)
    {
        RelicData newRelic = CreateNewRelic(newCardData);

        relics.Add(newRelic);
        OnRelicAdded?.Invoke(newRelic);

        //획득한 유물은 보상 목록에서 제거
        GameItemRewardManager.instance.EraseRelicFromList(newRelic);

        Debug.Log($"Relic Added : {newRelic.relicName}");

        return newRelic;
    }

    //플레이어의 유물 제거(유물 이름 기반)
    public void RemoveRelic(string name)
    {
        RelicData relicToRemove = relics.FirstOrDefault(r => r.relicName == name);
        if (relicToRemove != null) //유물이 삭제되었을 경우
        {
            if (relics.Remove(relicToRemove))
            {
                OnRelicRemoved?.Invoke(name);

                //삭제된 유물을 보상 목록에 추가
                GameItemRewardManager.instance.AddRelicFromList(relicToRemove);

                Debug.Log($"Card Removed : {relicToRemove.relicName}");
            }
        }
    }

    public RelicData GetRandomRelic()
    {
        RelicData randomRelic = relics[UnityEngine.Random.Range(0, relics.Count)];

        return randomRelic;
    }

    public RelicData GetSpecificRelic(string name)
    {
        RelicData specificRelic = relics.FirstOrDefault(r => r.relicName == name);

        return specificRelic;
    }

    private RelicData CreateNewRelic(RelicData newRelicData)
    {
        RelicData newRelic = Instantiate(newRelicData);

        return newRelic;
    }
}
