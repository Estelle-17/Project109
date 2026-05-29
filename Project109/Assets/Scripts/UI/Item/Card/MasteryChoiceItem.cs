using GameItem.Types;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MasteryChoiceHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private ActionCardData selectedCardData;
    private string selectedMasteryPath;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject highlightImage;

    void Start()
    {

    }

    public void SetChoice(ActionCardData newCardData, MasteryDescription newMastreyDescription)
    {
        selectedCardData = newCardData;

        selectedMasteryPath = newMastreyDescription.path;
        nameText.SetText(newMastreyDescription.name);
        descriptionText.SetText(newMastreyDescription.description);
    }

    public void StartMasteryUpgradeCard()
    {
        CardMasteryManager.instance.AddMastery(selectedCardData.ID, selectedMasteryPath);

        UIManager.instance.HideCardExtraDescription();
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
