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
                RunManager.instance.player.playerStat.inGame_Currency_Gold -= item.value;
                break;
            case "MemorySharp":
                RunManager.instance.player.playerStat.inGame_Currency_MemorySharp -= item.value;
                break;
            case "MaxHp":
                //value값만큼 플레이어 최대 체력 차감
                break;
            case "Hp":
                //value값만큼 플레이어 체력 차감
                break;
            case "SpecificCard":
                if (RunManager.instance != null && RunManager.instance.player != null)
                {
                    Card specificCard = RunManager.instance.player.deck.GetSpecificCard(item.name);
                    if (specificCard != null)
                    {
                        RunManager.instance.player.deck.RemoveCard(specificCard.runtimeID);
                    }
                }
                break;
            case "RandomCard":
                //랜덤한 카드 선택 후 제거
                if (RunManager.instance != null && RunManager.instance.player != null && choiceData.randomLoseCard.Count > 0)
                {
                    RunManager.instance.player.deck.RemoveCard(choiceData.randomLoseCard[0].runtimeID);
                }
                break;
            case "SpecificRelic":
                Relic specificRelic = RunManager.instance.player.GetSpecificRelic(item.name);
                if (specificRelic != null)
                {
                    RunManager.instance.player.RemoveRelic(specificRelic.Data.relicName);
                }
                break;
            case "RandomRelic":
                RunManager.instance.player.RemoveRelic(choiceData.randomLoseRelic[0].relicName);
                break;
        }
    }
    void GetItem(Choice_GetItem item)
    {
        switch (item.itemType)
        {
            case "Money":
                RunManager.instance.player.playerStat.inGame_Currency_Gold += item.value;
                break;
            case "MemorySharp":
                RunManager.instance.player.playerStat.inGame_Currency_MemorySharp += item.value;
                break;
            case "MaxHp":
                //value값만큼 플레이어 최대 체력 증가
                break;
            case "Hp":
                //value값만큼 플레이어 체력 증가
                break;
            case "SpecificCard":
                if (RunManager.instance != null && RunManager.instance.player != null)
                {
                    if (ModLoader.Instance.CardDatabase.TryGetValue(item.name, out CardData specificCard))
                    {
                        RunManager.instance.player.deck.AddCard(specificCard);
                    }
                }
                break;
            case "CardReward":
                //카드 획득 기회를 x번 획득
                break;
            case "SpecificRelic":
                if (ModLoader.Instance.RelicDatabase.TryGetValue(item.name, out RelicData specificRelicData))
                {
                    RunManager.instance.player.AddRelic(specificRelicData.relicName);
                }
                break;
            case "RelicReward":
                //랜덤한 유물 기회를 획득
                break;
            case "Battle":
                //전투 진행
                if (AssetCacheManager.instance.TryGetBattle(item.name, out BattleData battleData))
                {
                    RunManager.instance.SpawnMonsterInBattleNodeData(battleData);
                }
                else
                {
                    Debug.Log("알맞은 전투 데이터가 존재하지 않습니다.");
                }
                break;
            case "Event":
                //특정 이벤트를 불러올 때 사용될 예정
                //모든 탐험 노드 활성화
                if (item.name == "OpenAllExploreNodes")
                {
                    RunManager.instance.currentExploreUI.OpenAllExploreMapNodes();
                }
                break;
        }
    }
}
