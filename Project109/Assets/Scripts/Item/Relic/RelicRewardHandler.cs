using System.Collections.Generic;
using UnityEngine;

public class RelicRewardHandler : MonoBehaviour
{
    public GameObject rewardNPCObject;
    public GameObject relicObjectPrefab;
    [SerializeField] private Transform relicSpawnTransform;

    RandomItemPicker<RelicData> commonRelicPicker;
    RandomItemPicker<RelicData> rareRelicPicker;
    RandomItemPicker<RelicData> uniqueRelicPicker;

    //등급별 확률
    int commonRate;
    int rareRate;
    int uniqueRate;

    void Start()
    {
        //기본 확률(유물 및 다양한 요소에 의해 변경 가능)
        commonRate = 60;
        rareRate = 30;
        uniqueRate = 10;
    }

    public void SettingRelics(RandomItemPickupType pickupType, int rewardRelicCount)
    {
        int pickNumber;

        commonRelicPicker = new RandomItemPicker<RelicData>(GameItemContainer.instance.GetCommonRelicList());
        rareRelicPicker = new RandomItemPicker<RelicData>(GameItemContainer.instance.GetRareRelicList());
        uniqueRelicPicker = new RandomItemPicker<RelicData>(GameItemContainer.instance.GetUniqueRelicList());

        for (int count = 0; count < rewardRelicCount; count++)
        {
            //유물UI 생성
            RelicHandler relic = Instantiate(relicObjectPrefab, relicSpawnTransform).GetComponent<RelicHandler>();

            if (relic == null)
                continue;

            //랜덤한 유물 선택 후 등록
            pickNumber = Random.Range(1, 101);

            RelicData relicData = new RelicData();
            if (pickNumber <= uniqueRate)
            {
                //unique유물들 중 랜덤한 1장 선택
                if (uniqueRelicPicker.TryGetNext(out RelicData data))
                {
                    relicData = data;
                }
                else
                {
                    uniqueRelicPicker.Reset();
                    if (uniqueRelicPicker.TryGetNext(out RelicData newData))
                    {
                        relicData = newData;
                    }
                }
                relic.UpdateRelicData(relicData);
            }
            else if (pickNumber > uniqueRate && pickNumber <= uniqueRate + rareRate)
            {
                //rare유물들 중 랜덤한 1장 선택
                if (rareRelicPicker.TryGetNext(out RelicData data))
                {
                    relicData = data;
                }
                else
                {
                    rareRelicPicker.Reset();
                    if (rareRelicPicker.TryGetNext(out RelicData newData))
                    {
                        relicData = newData;
                    }
                }
                relic.UpdateRelicData(relicData);
            }
            else
            {
                //common유물들 중 랜덤한 1장 선택
                if (commonRelicPicker.TryGetNext(out RelicData data))
                {
                    relicData = data;
                }
                else
                {
                    commonRelicPicker.Reset();
                    if (commonRelicPicker.TryGetNext(out RelicData newData))
                    {
                        relicData = newData;
                    }
                }
                relic.UpdateRelicData(relicData);
            }

            relic.OnRelicClick.AddListener(() => GetRelic(relicData));
        }
    }


    void GetRelic(RelicData newRelicData)
    {
        RelicManager.instance.AddRelic(newRelicData);
        //이 카드 선택지를 제공한 NPC오브젝트 제거 및 캔버스 제거
        Destroy(rewardNPCObject);
        Destroy(gameObject);
    }
}
