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
            getDescription += GetItemDescription(getItemData, choiceData);
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
            case "Gold":
                description += useItemData.value + " 소모";
                break;
            case "MaxHp":
                description += "최대 체력" + useItemData.value + " 감소";
                break;
            case "MaxHpPercent":
                description += "최대 체력 " + useItemData.value + "%(" + GameManager.instance.currentCharacter.GetCharacterStat().maxHp / useItemData.value + ") 소모";
                break;
            case "Hp":
                description += "체력" + useItemData.value + " 소모";
                break;
            case "MaxStamina":
                description += "최대 스태미나" + useItemData.value + " 감소";
                break;
            case "SpecificCard":
                description += useItemData.name + " 제거";
                break;
            case "RandomCard":
                if(choiceData.randomLoseCard != null && choiceData.randomLoseCard.Count > 0)
                    description += choiceData.randomLoseCard[0].cardName + " 제거";
                break;
            case "SpecificRelic":
                description += useItemData.name + " 제거";
                break;
            case "RandomRelic":
                if (choiceData.randomLoseRelic != null && choiceData.randomLoseRelic.Count  > 0)
                    description += choiceData.randomLoseRelic[0].relicName + " 제거";
                break;
            case "RandomPotion":
                description += "랜덤한 포션 제거";
                break;
        }

        return description;
    }

    string GetItemDescription(Choice_GetItem getItemData, Choice_Data choiceData)
    {
        string description = "";

        switch (getItemData.itemType)
        {
            case "Gold":
                description += "재화를 " + getItemData.value + " 획득합니다.";
                break;
            case "MaxHp":
                description += "체력이" + getItemData.value + " 증가합니다.";
                break;
            case "MaxHpPercent":
                description += "최대 체력이 " + getItemData.value + "%(" + GameManager.instance.currentCharacter.GetCharacterStat().maxHp / getItemData.value + ") 증가합니다.";
                break;
            case "Hp":
                description += "체력을 " + getItemData.value + " 회복합니다.";
                break;
            case "HpPercent":
                description += "체력을 " + getItemData.value + "%(" + GameManager.instance.currentCharacter.GetCharacterStat().maxHp / getItemData.value + ") 회복합니다.";
                break;
            case "MaxStamina":
                description += "최대 스태미나가" + getItemData.value + " 증가합니다.";
                break;
            case "SpecificCard":
                description += getItemData.name + " 획득합니다.";
                break;
            case "CardReward":
                description += "카드 보상을" + getItemData.value + " 번 획득합니다.";
                break;
            case "EraseCard":
                description += "카드를 " + getItemData.value + " 번 제거합니다.";
                break;
            case "RandomEraseCard":
                description += choiceData.randomLoseCard[0].cardName + " 카드를 제거합니다.";
                break;
            case "UpgradeCard":
                description += "강화를 " + getItemData.value + " 번 진행합니다.";
                break;
            case "RandomUpgradeCard":
                description += "랜덤한 카드 " + getItemData.value + "장을 강화합니다.";
                break;
            case "SpecificRelic":
                description += getItemData.name + "을 획득합니다.";
                break;
            case "RelicReward":
                description += "유물 보상을" + getItemData.value + "번 획득합니다.";
                break;
            case "RandomRelic":
                description += choiceData.randomLoseRelic[0].relicName + "을 획득합니다.";
                break;
            case "Potion":
                description += getItemData.name + "을 획득합니다.";
                break;
            case "RandomPotion":
                description += "랜덤 포션을 획득합니다.";
                break;
            case "Battle":
                description += "전투를 진행합니다.";
                break;
            case "Event":
                description += "이벤트를 진행합니다.";
                break;
                //전투와 이벤트는 특정 Description이 필요할 경우 제작
        }

        return description;
    }
}
