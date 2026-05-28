using System;
using System.Collections.Generic;
using System.Linq;
using GameItem.Types;
using XLua;

/// <summary>
/// 플레이어의 영속적인 마스터 덱(Master Deck)을 관리하는 클래스.
/// 런타임 카드 인스턴스 생성, 추가, 제거, 업그레이드 및 진화 로직을 담당합니다.
/// </summary>
public class PlayerDeck
{
    private readonly Player owner;
    private readonly List<CardBase> cards = new();
    private int nextRuntimeID = 0;

    public PlayerDeck(Player owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// 현재 덱에 보관된 카드 리스트를 반환합니다.
    /// </summary>
    public List<CardBase> GetCards()
    {
        return new List<CardBase>(cards);
    }

    /// <summary>
    /// 새로운 카드 데이터를 마스터 덱에 추가합니다.
    /// </summary>
    public CardBase AddCard(CardData cardData)
    {
        CardBase newCard = CreateNewCard(cardData);
        cards.Add(newCard);
        
        owner.eventBus.Invoke<IOnAddCard>(c => c.OnAddCard(newCard));
        return newCard;
    }

    /// <summary>
    /// 기존 카드 인스턴스를 마스터 덱에 추가합니다.
    /// </summary>
    public void AddCard(CardBase card)
    {
        cards.Add(card);
        owner.eventBus.Invoke<IOnAddCard>(c => c.OnAddCard(card));
    }

    /// <summary>
    /// 마스터 덱의 특정 카드를 제거합니다.
    /// </summary>
    public void RemoveCard(int runtimeID)
    {
        CardBase cardToRemove = cards.FirstOrDefault(c => c.runtimeID == runtimeID);
        if (cardToRemove != null)
        {
            if (cards.Remove(cardToRemove))
            {
                owner.eventBus.Invoke<IOnRemoveCard>(c => c.OnRemoveCard(cardToRemove));
                cardToRemove.Dispose();
            }
        }
    }

    /// <summary>
    /// 기존 카드 인스턴스를 마스터 덱에서 제거합니다.
    /// </summary>
    public void RemoveCard(CardBase card)
    {
        if (cards.Remove(card))
        {
            owner.eventBus.Invoke<IOnRemoveCard>(c => c.OnRemoveCard(card));
            card.Dispose();
        }
    }

    /// <summary>
    /// 특정 카드를 다음 등급으로 영구 업그레이드합니다.
    /// </summary>
    public void UpgradeCard(int runtimeID)
    {
        CardBase cardToUpgrade = cards.FirstOrDefault(c => c.runtimeID == runtimeID);
        if (cardToUpgrade != null && cardToUpgrade.cardData != null && cardToUpgrade.cardData.isUpgradable)
        {
            string upgradedName = cardToUpgrade.cardData.upgradedCardName;
            if (!string.IsNullOrEmpty(upgradedName) && AssetCacheManager.instance != null)
            {
                // ActionCardData 캐시와 연동하여 임시 매핑
                if (AssetCacheManager.instance.TryGetCard(upgradedName, out ActionCardData newCardData))
                {
                    // 추후 YAML CardData 로드가 완성되면 완전히 CardData 기반으로 교환 예정.
                }
            }

            owner.eventBus.Invoke<IOnCardUpgrade>(c => c.OnCardUpgrade(cardToUpgrade));
        }
    }

    /// <summary>
    /// 특정 카드를 영구 진화시킵니다.
    /// </summary>
    public void EvolveCard(int runtimeID, EvolveType type)
    {
        CardBase cardToEvolve = cards.FirstOrDefault(c => c.runtimeID == runtimeID);
        if (cardToEvolve != null)
        {
            // 진화 데이터 설정
            owner.eventBus.Invoke<IOnCardEvolve>(c => c.OnCardEvolve(cardToEvolve));
        }
    }

    /// <summary>
    /// 덱에서 랜덤하게 한 장의 카드를 가져옵니다.
    /// </summary>
    public CardBase GetRandomCard()
    {
        if (cards.Count == 0) return null;
        return cards[UnityEngine.Random.Range(0, cards.Count)];
    }

    /// <summary>
    /// 지정된 이름(식별자)의 카드를 가져옵니다.
    /// </summary>
    public CardBase GetSpecificCard(string name)
    {
        return cards.FirstOrDefault(c => c.cardData != null && c.cardData.cardName == name);
    }

    /// <summary>
    /// 모든 카드 변경 이벤트(새고고침)를 강제 호출합니다.
    /// </summary>
    public void RequestAllCardRefresh()
    {
        owner.eventBus.Invoke<IOnCardsRefreshed>(c => c.OnCardsRefreshed());
    }

    private CardBase CreateNewCard(CardData cardData)
    {
        LuaTable luaInstance = null;
        if (cardData.luaPrototype != null)
        {
            try
            {
                var newInstanceFunc = LuaManager.Instance.luaEnv.Global.Get<System.Func<LuaTable, LuaTable>>("NewInstance");
                if (newInstanceFunc != null)
                {
                    luaInstance = newInstanceFunc(cardData.luaPrototype);
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"[PlayerDeck] '{cardData.cardName}' Lua 인스턴스 생성 실패:\n{e.Message}");
            }
        }

        CardBase newCard = new CardBase(cardData, owner.character, luaInstance, nextRuntimeID++);
        return newCard;
    }
}
