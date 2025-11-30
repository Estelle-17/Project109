using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ItemRewardUIType
{
    Gold,
    MemorySharp,
    Card,
    Relic
}

public class ItemRewardUIHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject cardRewardUIPrefab;

    private GameObject currentRewardUIObject;
    private ItemRewardUIType currentItemRewardUIType;

    [SerializeField] private RelicData rewardRelicData;
    private int rewardValue;

    [SerializeField] private Image itemTexture;
    [SerializeField] private TextMeshProUGUI itemText;

    [Header("ItemTextures")]
    [SerializeField] private Sprite goldTexture;
    [SerializeField] private Sprite memorySharpTexture;

    void Start()
    {

    }

    public void SetReward(ItemRewardUIType npcType, RandomItemPickupType itemPickupType, int value)
    {
        currentItemRewardUIType = npcType;

        switch (npcType)
        {
            case ItemRewardUIType.Gold:
                rewardValue = value;
                itemTexture.sprite = goldTexture;
                itemText.text = $"{value}";
                break;
            case ItemRewardUIType.MemorySharp:
                rewardValue = value;
                itemTexture.sprite = memorySharpTexture;
                itemText.text = $"{value}";
                break;
            case ItemRewardUIType.Card:
                currentRewardUIObject = Instantiate(cardRewardUIPrefab);
                CardRewardHandler cardRewardHandler = currentRewardUIObject.GetComponent<CardRewardHandler>();
                cardRewardHandler.SettingCards(itemPickupType, PlayerDataManager.instance.GetPlayerStat().reward_Card_Count);    //보여줄 아이템의 수는 상황에 따라 변경 가능
                cardRewardHandler.rootObject = this.gameObject;
                cardRewardHandler.gameObject.SetActive(false);

                GameManager.instance.currentSpawnUIList.Add(currentRewardUIObject);

                itemTexture.gameObject.SetActive(false);
                itemText.text = "새로운 카드 보상!";
                break;
            case ItemRewardUIType.Relic:    //단일 유물 획득 보상
                rewardRelicData = GameItemRewardManager.instance.GetRandomRelicDataByPickupType(itemPickupType);

                if (AssetCacheManager.instance.TryGetTexture(rewardRelicData.texturePath, out Sprite texture))
                {
                    itemTexture.sprite = texture;
                }
                else
                {
                    Debug.LogWarning($"[ItemRewardUIHandler] 유물 텍스처 로드 실패: {rewardRelicData.texturePath}");
                }
                itemText.text = $"{rewardRelicData.relicName}";
                break;
        }
    }

    public GameObject GetRewardUI() { return currentRewardUIObject; }

    public void OnPointerClick(PointerEventData eventData)
    {
        //UI 클릭 시 보상 UI 활성화
        switch(currentItemRewardUIType)
        {
            case ItemRewardUIType.Gold:
                GameManager.instance.AddInGame_Currency(CurrencyType.Gold, rewardValue);
                break;
            case ItemRewardUIType.MemorySharp:
                GameManager.instance.AddInGame_Currency(CurrencyType.MemorySharp, rewardValue);
                break;
            case ItemRewardUIType.Card:
                UIManager.instance.TempDeactivateCurrentActiveUIPanel();
                currentRewardUIObject.GetComponent<CardRewardHandler>().UIActive();
                break;
            case ItemRewardUIType.Relic:
                RelicManager.instance.AddRelic(rewardRelicData);
                //이 유물 선택지를 제공한 UI 제거
                UIManager.instance.OffRelicDescription();
                break;
        }

        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스 오버 시 UI 하이라이트 활성화
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //마우스 오버 시 UI 하이라이트 비활성화
    }
}
