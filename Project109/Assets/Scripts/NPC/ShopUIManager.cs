using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using GameItem.Types;

public class ShopUIManager : MonoBehaviour, IInteractable
{
    public GameObject shopUICanvasPrefab;
    ShopUIHandler shopUI;

    public int cardCount;
    public int relicCount;

    public List<ActionCardData> actionCards;
    public List<RelicData> relics;
    //이후 포션 추가 예정

    [SerializeField] private string modelName = "NPC_Shop_Model";

    private void Awake()
    {
        AssetCacheManager cacheData = AssetCacheManager.instance;

        cardCount = 6;
        relicCount = 3;
    }

    void Start()
    {
        InstantiateModel();
    }

    private void InstantiateModel()
    {
        if (string.IsNullOrEmpty(modelName)) return;

        if (AssetCacheManager.instance != null && AssetCacheManager.instance.TryGetModel(modelName, out GameObject modelPrefab))
        {
            var defaultRenderer = GetComponent<MeshRenderer>();
            if (defaultRenderer != null)
            {
                defaultRenderer.enabled = false;
            }

            GameObject model = Instantiate(modelPrefab, this.transform);
            if (model != null)
            {
                model.tag = this.tag;
                ChangeAllLayer(model, LayerMask.NameToLayer("NPC"));
            }
        }
    }

    private void ChangeAllLayer(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            ChangeAllLayer(child.gameObject, layer);
        }
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

    public bool RequiresCameraFocus => true;

    public void OnInteract()
    {
        EnableShopUI();
    }
}
