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

    // 유물 관리자
    public RelicManager relicManager;

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

        this.relicManager = new RelicManager(this);
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

    // 유물 관련 기능들은 backward compatibility 혹은 편의성을 위해 RelicManager로 위임
    public void AddRelic(string relicId)
    {
        relicManager.AddRelic(relicId);
    }

    public void RemoveRelic(string relicId)
    {
        relicManager.RemoveRelic(relicId);
    }

    public RelicBase GetRandomRelic()
    {
        return relicManager.GetRandomRelic();
    }

    public RelicBase GetSpecificRelic(string relicId)
    {
        return relicManager.GetSpecificRelic(relicId);
    }
}
