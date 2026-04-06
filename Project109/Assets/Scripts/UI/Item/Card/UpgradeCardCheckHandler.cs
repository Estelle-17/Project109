using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardCheckHandler : MonoBehaviour
{
    private ActionCardData currentCardData;
    private ActionCardData selectedUpgradeCardData;

    [SerializeField] private ActionCardHandler currentCard;
    [SerializeField] private ActionCardHandler upgradeCard;

    public Button upgradeCardButton;

    void Start()
    {
        upgradeCardButton.onClick.AddListener(StartUpgradeCards);
        gameObject.SetActive(false);
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌
    public void OnCardCheckUI(ActionCardData newCardData)
    {
        if (currentCard == null || upgradeCard == null)
            return;

        currentCardData = newCardData;

        currentCard.UpdateActionCardData(newCardData);
        currentCard.bIsCardHighlight = false;

        if (AssetCacheManager.instance.TryGetCard(newCardData.upgradeCardPath, out ActionCardData upgradeCardData))
        {
            selectedUpgradeCardData = upgradeCardData;

            upgradeCard.UpdateActionCardData(selectedUpgradeCardData);
            upgradeCard.bIsCardHighlight = false;
        }

        gameObject.SetActive(true);
    }

    public void StartUpgradeCards()
    {
        CardDeckManager.instance.UpgradeCard(currentCardData.runtimeID);
        UIManager.instance.HideCardExtraDescription();
        transform.root.gameObject.SetActive(false);
        Destroy(transform.root.gameObject);
    }

    private void OnDestroy()
    {
        if (selectedUpgradeCardData != null)
        {
            //Destroy(selectedUpgradeCardData);
        }
    }
}
