using System.Collections.Generic;
using UnityEngine;
using GameItem.Types;

public class RelicRewardHandler : MonoBehaviour
{
    public GameObject rootObject;
    public GameObject relicObjectPrefab;
    [SerializeField] private Transform relicSpawnTransform;

    void Start()
    {

    }

    public void SettingRelics(RandomRelicPickupType pickupType, int rewardRelicCount)
    {
        for (int count = 0; count < rewardRelicCount; count++)
        {
            //유물UI 생성
            RelicHandler relic = Instantiate(relicObjectPrefab, relicSpawnTransform).GetComponent<RelicHandler>();

            if (relic == null)
                continue;

            RelicData relicData = GameItemRewardManager.instance.GetRandomRelicDataByPickupType(pickupType);

            relic.UpdateRelicData(relicData);

            relic.OnRelicClick.AddListener(() => GetRelic(relicData));
        }
    }


    void GetRelic(RelicData newRelicData)
    {
        RelicManager.instance.AddRelic(newRelicData);
        //이 카드 선택지를 제공한 NPC오브젝트 제거 및 캔버스 제거
        UIManager.instance.OffRelicDescription();
        Destroy(rootObject);
        Destroy(gameObject);
    }
}
