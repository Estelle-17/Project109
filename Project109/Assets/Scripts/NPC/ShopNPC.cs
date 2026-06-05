using UnityEngine;
using System.Collections.Generic;
using GameItem.Types;

public class ShopNPC : InteractableObject
{
    public GameObject shopUICanvasPrefab;
    private ShopPanel shopUI;

    public int cardCount = 6;
    public int relicCount = 3;

    public List<CardData> actionCards = new List<CardData>();
    public List<RelicData> relics = new List<RelicData>();

    private bool isInitialized = false;

    public override void OnInteract()
    {
        if (interactableData != null && !string.IsNullOrEmpty(interactableData.targetDialogueID))
        {
            TriggerDialogue();
        }
        else
        {
            OpenShopDirectly();
        }
    }

    public void InitializeShop()
    {
        if (isInitialized) return;

        AddRandomItems();
        UpdateShopItems();
        isInitialized = true;
    }

    public void AddRandomItems()
    {
        actionCards.Clear();
        relics.Clear();

        for (int i = 0; i < cardCount; i++)  //랜덤한 카드 데이터 저장
        {
            if (GameItemRewardManager.instance != null)
            {
                var card = GameItemRewardManager.instance.GetRandomCardDataByDropTable("Shop_Card_Table");
                if (card != null) actionCards.Add(card);
            }
        }
        for (int i = 0; i < relicCount; i++) //랜덤한 유물 데이터 저장
        {
            if (GameItemRewardManager.instance != null)
            {
                var relic = GameItemRewardManager.instance.GetRandomRelicDataByDropTable("Shop_Relic_Table");
                if (relic != null) relics.Add(relic);
            }
        }
    }

    public void UpdateShopItems() //상점UI 생성 후 아이템 진열
    {
        if (shopUICanvasPrefab == null || shopUI != null)
        {
            return;
        }

        GameObject spawned = Instantiate(shopUICanvasPrefab);
        shopUI = spawned.GetComponent<ShopPanel>();
        if (shopUI != null)
        {
            shopUI.CreateStoreItemCollections(actionCards.Count, relics.Count, 3);
            shopUI.UpdateCardList(actionCards);
            shopUI.UpdateRelicList(relics);
            shopUI.gameObject.SetActive(false);
        }
    }

    public ShopPanel GetShopUI()
    {
        return shopUI;
    }

    public void OpenShopDirectly()
    {
        InitializeShop();

        if (shopUI != null)
        {
            shopUI.UIActive();

            if (RunManager.instance != null && RunManager.instance.currentMap != null)
            {
                if (!RunManager.instance.currentMap.currentSpawnUIList.Contains(shopUI.gameObject))
                {
                    RunManager.instance.currentMap.currentSpawnUIList.Add(shopUI.gameObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("[ShopNPC] ShopUI가 로드되지 않았습니다.");
        }
    }

    public void CloseShopUI()
    {
        if (shopUI != null)
        {
            shopUI.UIDeactive();
        }
    }
}
