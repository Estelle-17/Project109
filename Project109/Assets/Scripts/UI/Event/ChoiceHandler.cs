using UnityEngine;

public class ChoiceHandler : MonoBehaviour
{
    public void CheckChoiceData(Choice_Data choiceData)
    {
        foreach(Choice_UseItem item in choiceData.useItems)
        {
            LoseItem(item);
        }

        foreach (Choice_GetItem item in choiceData.getItems)
        {
            GetItem(item);
        }
    }

    void LoseItem(Choice_UseItem item)
    {
        switch (item.itemType)
        {
            case "Money":
                GameManager.instance.AddInGame_Currency(-item.value);
                break;
            case "MaxHp":
                //value값만큼 플레이어 최대 체력 차감
                break;
            case "Hp":
                //value값만큼 플레이어 체력 차감
                break;
            case "SpecificCard":
                ActionCardData specificCard = CardDeckManager.instance.GetSpecificCard(item.name);
                if(specificCard != null)
                {
                    CardDeckManager.instance.RemoveCard(specificCard.runtimeID);
                }
                break;
            case "RandomCard":
                //랜덤한 카드 선택 후 제거
                ActionCardData randomCard = CardDeckManager.instance.GetRandomCard();
                if(randomCard != null)
                {
                    CardDeckManager.instance.RemoveCard(randomCard.runtimeID);
                }
                break;
            case "SpecificRelic":
                RelicData specificRelic = RelicManager.instance.GetSpecificRelic(item.name);
                if(specificRelic != null)
                {
                    RelicManager.instance.RemoveRelic(specificRelic.relicName);
                }
                break;
            case "RandomRelic":
                RelicData randomRelic = RelicManager.instance.GetRandomRelic();
                if (randomRelic != null)
                {
                    RelicManager.instance.RemoveRelic(randomRelic.relicName);
                }
                break;
        }
    }
    void GetItem(Choice_GetItem item)
    {
        switch (item.itemType)
        {
            case "Money":
                GameManager.instance.AddInGame_Currency(item.value);
                break;
            case "MaxHp":
                //value값만큼 플레이어 최대 체력 증가
                break;
            case "Hp":
                //value값만큼 플레이어 체력 증가
                break;
            case "SpecificCard":
                ActionCardData specificCard;
                if (AssetCacheManager.instance.TryGetCard(item.name, out specificCard))
                {
                    CardDeckManager.instance.AddCard(specificCard);
                }
                break;
            case "CardReward":
                //카드 획득 기회를 x번 획득
                break;
            case "SpecificRelic":
                RelicData specificRelic;
                if(AssetCacheManager.instance.TryGetRelic(item.name, out specificRelic))
                {
                    RelicManager.instance.AddRelic(specificRelic);
                }
                break;
            case "RelicReward":
                
                break;
            case "Battle":

                break;
            case "Event":

                break;
        }
    }
}
