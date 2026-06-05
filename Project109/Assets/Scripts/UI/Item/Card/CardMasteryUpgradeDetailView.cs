using UnityEngine;
using UnityEngine.UI;

public class CardMasteryUpgradeDetailView : MonoBehaviour
{
    private Card selectedCardInstance;

    [SerializeField] private MasteryPointUI currentMasteryPointUI;

    public Button upgradeCardButton;

    void Start()
    {
        upgradeCardButton.onClick.AddListener(StartUpgradeCards);
        gameObject.SetActive(false);
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌
    public void OnCardCheckUI(Card card)
    {
        if (currentMasteryPointUI == null || card == null)
            return;

        selectedCardInstance = card;

        currentMasteryPointUI.PreviewUpgradeMasteryPointUI(selectedCardInstance,
                                                           RunManager.instance.player.playerStat.UpgradeMasteryPointValue);

        gameObject.SetActive(true);
    }

    public void StartUpgradeCards()
    {
        if (selectedCardInstance != null)
        {
            selectedCardInstance.AddMasteryPoint(RunManager.instance.player.playerStat.UpgradeMasteryPointValue);
        }
        transform.root.gameObject.SetActive(false);
        Destroy(transform.root.gameObject);
    }
}
