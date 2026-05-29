using UnityEngine;
using UnityEngine.UI;

public class UpgradeMasteryCardCheckHandler : MonoBehaviour
{
    private ActionCardData selectedCardData;

    [SerializeField] private MasteryPointHandler currentMasteryPointUI;

    public Button upgradeCardButton;

    void Start()
    {
        upgradeCardButton.onClick.AddListener(StartUpgradeCards);
        gameObject.SetActive(false);
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌
    public void OnCardCheckUI(ActionCardData newCardData)
    {
        if (currentMasteryPointUI == null)
            return;

        selectedCardData = Instantiate(newCardData);

        currentMasteryPointUI.PreviewUpgradeMasteryPointUI(selectedCardData.path,
                                                           RunManager.instance.player.playerStat.Upgrade_MasteryPoint_Value);

        gameObject.SetActive(true);
    }

    public void StartUpgradeCards()
    {
        CardMasteryManager.instance.ReportAction(Card.Types.CardMasteryType.UpgradeCard,
                                                 RunManager.instance.player.playerStat.Upgrade_MasteryPoint_Value,
                                                 selectedCardData);
        UIManager.instance.HideCardExtraDescription();
        transform.root.gameObject.SetActive(false);
        Destroy(transform.root.gameObject);
    }

    private void OnDestroy()
    {
        if (selectedCardData != null)
        {
            Destroy(selectedCardData);
        }
    }
}
