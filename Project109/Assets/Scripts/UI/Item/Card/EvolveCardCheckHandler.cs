using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GameItem.Types;

public class EvolveCardCheckHandler : MonoBehaviour
{
    private ActionCardData selectedCardData;

    [SerializeField] private ActionCardHandler costEvolveCard;
    [SerializeField] private ActionCardHandler amountEvolveCard;
    [SerializeField] private ActionCardHandler customEvolveCard;

    void Start()
    {
        costEvolveCard.OnCardClick.AddListener(() => StartEvolveCard(EvolveType.Cost));
        amountEvolveCard.OnCardClick.AddListener(() => StartEvolveCard(EvolveType.Amount));
        customEvolveCard.OnCardClick.AddListener(() => StartEvolveCard(EvolveType.Custom));
        gameObject.SetActive(false);
    }

    //클릭한 카드 데이터를 확인하고 화면 상에 보여줌
    public void OnCardCheckUI(ActionCardData newCardData)
    {
        selectedCardData = Instantiate(newCardData);

        costEvolveCard.UpdateActionCardData(selectedCardData);
        costEvolveCard.EvolveCard(EvolveType.Cost);
        costEvolveCard.bIsCardHighlight = true;

        amountEvolveCard.UpdateActionCardData(selectedCardData);
        amountEvolveCard.EvolveCard(EvolveType.Amount);
        amountEvolveCard.bIsCardHighlight = true;

        if (string.IsNullOrEmpty(selectedCardData.evolvedCardPath))
        {
            customEvolveCard.gameObject.SetActive(false);
        }
        else
        {
            customEvolveCard.UpdateActionCardData(selectedCardData);
            customEvolveCard.EvolveCard(EvolveType.Custom);
            customEvolveCard.bIsCardHighlight = true;
            customEvolveCard.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    public void StartEvolveCard(EvolveType type)
    {
        CardDeckManager.instance.EvolveCard(selectedCardData.runtimeID, type);
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
