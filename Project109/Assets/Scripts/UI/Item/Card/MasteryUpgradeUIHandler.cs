using UnityEngine;
using System.Collections.Generic;

public class MasteryUpgradeUIHandler : UIPanelBase
{
    [SerializeField] private GameObject masteryChoicePrefab;
    [SerializeField] private Transform contentTransform;

    private ActionCardData selectedCardData;

    public void CreateMasteryChoices(ActionCardData newCardData)
    {
        selectedCardData = newCardData;

        //선택지를 생성할 위치의 자식들 제거
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        //특정 갯수만큼 무작위 마스터리 선택지 생성
        List<MasteryDescription> availableMasteryList = CardMasteryManager.instance.GetRandomMasteryOption(selectedCardData,
                                                                                                           PlayerDataManager.instance.GetPlayerStat().mastery_Choice_Count);



        foreach (MasteryDescription masteryDescription in availableMasteryList)
        {
            GameObject choiceObj = Instantiate(masteryChoicePrefab, contentTransform);
            MasteryChoiceHandler choiceHandler = choiceObj.GetComponent<MasteryChoiceHandler>();
            if(choiceHandler != null)
            {
                //선택된 마스터리의 Desciription 추가
                choiceHandler.SetChoice(selectedCardData, masteryDescription);
            }
            else
            {
                Debug.LogError("MasteryChoiceHandler component not found on masteryChoicePrefab.");
            }
        }
    }
}
