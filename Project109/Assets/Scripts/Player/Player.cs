using System.Collections.Generic;
using System;

/// <summary>
/// 플레이어의 영속 데이터를 관리하는 클래스.
/// 던전 진입부터 게임 오버까지 유지된다.
/// </summary>
public class Player
{
    public Character character;
    public PlayerStat playerStat;

    public EventBus<IPlayerEvent> eventBus = new EventBus<IPlayerEvent>();

    // 카드 덱 (런 전체에서 유지되는 전체 카드 풀)
    public List<ActionCardData> masterDeck = new();

    // 유물 목록
    public List<RelicData> relics = new();

    public event Action<RelicData> OnRelicAddedEvent;
    public event Action<RelicData> OnRelicRemovedEvent;

    public Player(Character character)
    {
        this.character = character;
        this.playerStat = new PlayerStat();

        // 초기 스탯 세팅
        this.playerStat.inGame_Currency_Gold = 0;
        this.playerStat.mapFloorCheck_Length = 3;
        this.playerStat.Upgrade_MasteryPoint_Value = 500;
        this.playerStat.reward_Card_Count = 3;
        this.playerStat.reward_Relic_Count = 3;
        this.playerStat.mastery_Choice_Count = 3;
    }

    public void AddCardToDeck(ActionCardData card)
    {
        eventBus.Invoke<IOnAddCard>(c => c.OnAddCard(card));    
        masterDeck.Add(card);
    }

    public void RemoveCardFromDeck(ActionCardData card)
    {
        eventBus.Invoke<IOnRemoveCard>(c => c.OnRemoveCard(card));
        masterDeck.Remove(card);
    }

    public void AddRelic(RelicData relicData)
    {
        RelicData newRelic = UnityEngine.Object.Instantiate(relicData);
        eventBus.Invoke<IOnAddRelic>(c => c.OnAddRelic(newRelic));
        relics.Add(newRelic);

        OnRelicAddedEvent?.Invoke(newRelic);

        UnityEngine.Debug.Log($"Relic Added : {newRelic.relicName}");
    }

    public void RemoveRelic(RelicData relicData)
    {
        if (relics.Remove(relicData))
        {
            eventBus.Invoke<IOnRemoveRelic>(c => c.OnRemoveRelic(relicData));

            OnRelicRemovedEvent?.Invoke(relicData);

            UnityEngine.Debug.Log($"Relic Removed : {relicData.relicName}");
        }
    }

    public RelicData GetRandomRelic()
    {
        if (relics.Count == 0) return null;
        return relics[UnityEngine.Random.Range(0, relics.Count)];
    }

    public RelicData GetSpecificRelic(string name)
    {
        return relics.Find(r => r.relicName == name);
    }
}
