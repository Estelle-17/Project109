using UnityEngine;

public class ChoiceDescriptionHandler : MonoBehaviour
{
    public string MakeChoiceDescription(Choice_Data choiceData)
    {
        string useDescription = "";
        string getDescription = "";

        foreach (Choice_UseItem useItemData in choiceData.useItems)
        {
            useDescription += UseItemDescription(useItemData, choiceData);
        }

        foreach (Choice_GetItem getItemData in choiceData.getItems)
        {
            getDescription += GetItemDescription(getItemData);
        }

        string description = "";

        if (useDescription != "")
        {
            description += "<color=red>" + useDescription + "</color>" + ", ";
        }

        return description + getDescription;
    }

    string UseItemDescription(Choice_UseItem useItemData, Choice_Data choiceData)
    {
        string description = "";

        switch (useItemData.itemType)
        {
            case "Money":
                description += useItemData.value + " 소모";
                break;
            case "MaxHp":
                description += "최대 체력" + useItemData.value + " 소모";
                break;
            case "Hp":
                description += "체력" + useItemData.value + " 소모";
                break;
            case "SpecificCard":
                description += useItemData.name + " 제거";
                break;
            case "RandomCard":
                if(choiceData.randomLoseCard != null)
                    description += choiceData.randomLoseCard.cardName + " 제거";
                break;
            case "SpecificRelic":
                description += useItemData.name + " 제거";
                break;
            case "RandomRelic":
                if (choiceData.randomLoseRelic != null)
                    description += choiceData.randomLoseRelic.relicName + " 제거";
                break;
        }

        return description;
    }

    string GetItemDescription(Choice_GetItem getItemData)
    {
        string description = "";

        switch (getItemData.itemType)
        {
            case "Money":
                description += getItemData.value + " 획득";
                break;
            case "MaxHp":
                description += "체력" + getItemData.value + " 획득";
                break;
            case "Hp":
                description += "체력" + getItemData.value + " 획득";
                break;
            case "SpecificCard":
                description += getItemData.name + " 획득";
                break;
            case "SpecificRelic":
                description += getItemData.name + " 획득";
                break;
            case "CardReward":
                description += "카드 보상" + getItemData.value + "번 획득";
                break;
            case "RelicReward":
                description += "유물 보상" + getItemData.value + "번 획득";
                break;
            case "Battle":
                description += "전투 진행";
                break;
                //전투와 이벤트는 특정 Description이 필요할 경우 제작
        }

        return description;
    }
}
