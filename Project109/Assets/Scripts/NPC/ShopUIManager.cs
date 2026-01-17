using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using GameItem.Types;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopUICanvasPrefab;
    ShopUIHandler shopUI;

    public int cardCount;
    public int relicCount;

    public List<ActionCardData> actionCards;
    public List<RelicData> relics;
    //이후 포션 추가 예정

    private void Awake()
    {
        AssetCacheManager cacheData = AssetCacheManager.instance;

        cardCount = 6;
        relicCount = 3;
    }

    void Start()
    {
        
    }

    public void AddRandomItems()
    {
        for (int i = 0; i < cardCount; i++)  //랜덤한 카드 데이터 저장
        {
            actionCards.Add(GameItemRewardManager.instance.GetRandomCardDataByPickupType(RandomCardPickupType.CommonToUncommon));
        }
        for (int i = 0; i < relicCount; i++) //랜덤한 유물 데이터 저장
        {
            relics.Add(GameItemRewardManager.instance.GetRandomRelicDataByPickupType(RandomRelicPickupType.CommonToUnique));
        }
    }

    public void UpdateShopItems() //상점UI 생성 후 아이템 진열
    {
        if (shopUICanvasPrefab == null || shopUI != null)
        {
            return;
        }

        shopUI = GameObject.Instantiate(shopUICanvasPrefab).GetComponent<ShopUIHandler>();
        shopUI.CreateStoreItemCollections(actionCards.Count, relics.Count, 3);
        shopUI.UpdateCardList(actionCards);
        shopUI.UpdateRelicList(relics);

        shopUI.gameObject.SetActive(false);
    }

    public ShopUIHandler GetShopUI()
    {
        return shopUI;
    }

    public void EnableShopUI()
    {
        if (shopUI != null)
        {
            shopUI.gameObject.SetActive(true);
        }
    }

    public void DisableShopUI()
    {
        if (shopUI != null)
        {
            shopUI.gameObject.SetActive(false);
        }
    }
}
