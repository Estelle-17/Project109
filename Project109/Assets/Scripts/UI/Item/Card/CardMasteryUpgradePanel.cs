using UnityEngine;
using System.Collections.Generic;

public class CardMasteryUpgradePanel : UIPanelBase
{
    [SerializeField] private GameObject masteryChoicePrefab;
    [SerializeField] private Transform contentTransform;

    private Card selectedCardInstance;

    public void CreateMasteryChoices(Card card)
    {
        selectedCardInstance = card;

        //선택지를 생성할 위치의 자식들 제거
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        //특정 갯수만큼 무작위 마스터리 ID 목록 획득
        List<string> availableMasteryIds = card.GetRandomMasteryOption(
            RunManager.instance.player.playerStat.mastery_Choice_Count);

        foreach (string masteryId in availableMasteryIds)
        {
            GameObject choiceObj = Instantiate(masteryChoicePrefab, contentTransform);
            MasteryChoiceItem choiceHandler = choiceObj.GetComponent<MasteryChoiceItem>();
            if(choiceHandler != null)
            {
                choiceHandler.SetChoice(card, masteryId);
            }
            else
            {
                Debug.LogError("MasteryChoiceItem component not found on masteryChoicePrefab.");
            }
        }
    }
}
