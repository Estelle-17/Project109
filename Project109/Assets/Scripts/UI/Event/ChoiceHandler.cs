using UnityEngine;

public class ChoiceHandler : MonoBehaviour
{
    Choice_Data choiceData;
    EventHandler eventHandler;

    public void CheckChoiceData(Choice_Data newChoiceData, EventHandler newEventHandler)
    {
        choiceData = newChoiceData;
        eventHandler = newEventHandler;

        foreach (Choice_UseItem item in choiceData.useItems)
        {
            LoseItem(item);
        }

        foreach (Choice_GetItem item in choiceData.getItems)
        {
            GetItem(item);
        }

        if (choiceData.nextStageID != "")
        {
            Debug.Log($"first nextStageID : {choiceData.nextStageID}");
            eventHandler.UpdateEventDescription(choiceData.nextStageID);
        }
        else
        {
            Debug.Log($"null nextStageID : {choiceData.nextStageID}");
            eventHandler.DisableEventDescriptionUI();
        }
    }

    void LoseItem(Choice_UseItem item)
    {
        switch (item.itemType)
        {
            case "Money":
                GameManager.instance.AddInGame_Currency(CurrencyType.Gold, -item.value);
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
                CardDeckManager.instance.RemoveCard(choiceData.randomLoseCard.runtimeID);
                break;
            case "SpecificRelic":
                RelicData specificRelic = RelicManager.instance.GetSpecificRelic(item.name);
                if(specificRelic != null)
                {
                    RelicManager.instance.RemoveRelic(specificRelic.relicName);
                }
                break;
            case "RandomRelic":
                RelicManager.instance.RemoveRelic(choiceData.randomLoseRelic.relicName);
                break;
        }
    }
    void GetItem(Choice_GetItem item)
    {
        switch (item.itemType)
        {
            case "Money":
                GameManager.instance.AddInGame_Currency(CurrencyType.Gold, item.value);
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
                //랜덤한 유물 기회를 획득
                break;
            case "Battle":
                //전투 진행
                BattleData battleData;
                if (AssetCacheManager.instance.TryGetBattle(item.name, out battleData))
                {
                    GameManager.instance.loadMapHandler.SpawnMonsterInBattleNodeData(battleData);
                }
                else
                {
                    Debug.Log("알맞은 전투 데이터가 존재하지 않습니다.");
                }
                    break;
            case "Event":
                //특정 이벤트를 불러올 때 사용될 예정
                //모든 탐험 노드 활성화
                if(item.name == "OpenAllExploreNodes")
                {
                    GameManager.instance.currentExploreUI.OpenAllExploreMapNodes();
                }
                break;
        }
    }
}
