using UnityEngine;

public class ChoiceDescriptionHandler : MonoBehaviour
{
    public string MakeChoiceDescription(Choice_Data choiceData)
    {
        string useDescription = "";
        string getDescription = "";

        foreach (Choice_UseItem useItemData in choiceData.useItems)
        {
            useDescription += UseItemDescription(useItemData);
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

    string UseItemDescription(Choice_UseItem useItemData)
    {
        string description = "";

        switch (useItemData.itemType)
        {
            case "Money":
                description += useItemData.value + " 소모";
                break;
            case "Hp":
                description += "체력" + useItemData.value + " 소모";
                break;
            case "Card":
                description += useItemData.name + " 제거";
                break;
            case "Relic":
                description += useItemData.name + " 제거";
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
            case "Hp":
                description += "체력" + getItemData.value + " 획득";
                break;
            case "Card":
                description += getItemData.name + " 획득";
                break;
            case "Relic":
                description += getItemData.name + " 획득";
                break;
                //전투와 이벤트는 특정 Description이 필요할 경우 제작
        }

        return description;
    }
}
