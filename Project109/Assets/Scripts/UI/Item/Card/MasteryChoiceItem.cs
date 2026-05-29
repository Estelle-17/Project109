using GameItem.Types;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MasteryChoiceItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Card selectedCard;
    private string selectedMasteryPath;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject highlightImage;

    void Start()
    {

    }

    public void SetChoice(Card newCard, string masteryId)
    {
        selectedCard = newCard;

        selectedMasteryPath = masteryId;
        // masteryId를 이름으로 표시 (추후 로컬라이즈 키 또는 Lua에서 표시명 제공 가능)
        nameText.SetText(masteryId);
        // 설명은 현재 masteryId를 표시 (추후 CardData.masteryNames에서 표시명 조회)
        descriptionText.SetText(masteryId);
    }

    public void StartMasteryUpgradeCard()
    {
        // 직접 호출 대신 PlayerDeck.ApplyMastery()를 경유해야 IOnCardMasteryUpgrade 이벤트가 발행됩니다.
        if (RunManager.instance?.player?.deck != null)
        {
            RunManager.instance.player.deck.ApplyMastery(selectedCard, selectedMasteryPath);
        }

        transform.root.gameObject.SetActive(false);
        Destroy(transform.root.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartMasteryUpgradeCard();
    }
}
