using System;
using System.Collections.Generic;
using UnityEngine;
using EventStructs;

/// <summary>
/// 전투 중 플레이어의 카드 덱 상태 및 동작을 전담하는 클래스.
/// 드로우 더미, 손패, 버림패, 소멸패의 데이터를 소유하고 관련 로직(드로우, 셔플 등)을 처리합니다.
/// </summary>
public class BattleDeck
{
    private readonly Character owner;

    // 전투 중 카드 더미
    public List<CardBase> drawPile { get; } = new();
    public List<CardBase> hand { get; } = new();
    public List<CardBase> discardPile { get; } = new();
    public List<CardBase> exhaustPile { get; } = new();

    public BattleDeck(Character owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// 전투 시작 시 덱 상태 초기화
    /// </summary>
    public void InitDeck(List<CardBase> masterDeck)
    {
        drawPile.Clear();
        drawPile.AddRange(masterDeck);
        ShuffleDrawPile();

        hand.Clear();
        discardPile.Clear();
        exhaustPile.Clear();
    }

    /// <summary>
    /// 지정된 개수만큼 카드를 드로우합니다.
    /// </summary>
    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (drawPile.Count == 0)
            {
                // 뽑을 카드가 없으면 묘지(버림패) -> 드로우 파일로 셔플
                if (discardPile.Count == 0) return;
                drawPile.AddRange(discardPile);
                discardPile.Clear();
                ShuffleDrawPile();
            }

            var card = drawPile[0];
            drawPile.RemoveAt(0);

            owner?.eventBus.Invoke<IOnDrawCard>(c => c.OnDrawCard(card));

            hand.Add(card);
        }
    }

    /// <summary>
    /// 드로우 더미를 셔플합니다.
    /// </summary>
    public void ShuffleDrawPile()
    {
        if (owner != null)
        {
            owner.eventBus.Invoke<IOnShuffleDeck>(c => c.OnShuffleDeck(drawPile));
        }

        for (int i = drawPile.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (drawPile[i], drawPile[j]) = (drawPile[j], drawPile[i]);
        }
    }

    /// <summary>
    /// 현재 손패의 모든 카드를 버림패 더미로 보냅니다.
    /// </summary>
    public void DiscardHand()
    {
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            var card = hand[i];

            // 카드 자체의 마스터리 등으로 Preserve 기능이 켜져 있는지 확인하여 기본 플래그로 설정
            CardFlag initialFlags = CardFlag.Normal;
            if (card != null && card.HasTag("Preserve"))
            {
                initialFlags |= CardFlag.NoDiscard;
            }

            CardInfo info = new CardInfo(owner, new List<Character>(), Vector2Int.zero, card, initialFlags);
            owner?.eventBus.Invoke<IOnDiscardCard>(c => c.OnDiscardCard(info));

            // NoDiscard 플래그가 세팅되어 있다면 버리지 않고 보존
            if (info.cardFlags.HasFlag(CardFlag.NoDiscard))
            {
                continue;
            }

            discardPile.Add(card);
            hand.RemoveAt(i);
        }
    }

    /// <summary>
    /// 손패에서 특정 카드를 제거합니다. (단순 제거용)
    /// </summary>
    public bool RemoveFromHand(CardBase card)
    {
        return hand.Remove(card);
    }

    /// <summary>
    /// 특정 카드를 소거(Erase)하여 손패에서 제거하고 이벤트를 발생시킵니다.
    /// </summary>
    public void EraseCard(CardBase card, CardInfo info)
    {
        owner?.eventBus.Invoke<IOnEraseCard>(c => c.OnEraseCard(info));
        hand.Remove(card);
    }

    /// <summary>
    /// 특정 카드를 버림패 더미에 추가합니다.
    /// </summary>
    public void DiscardCard(CardBase card, CardInfo info)
    {
        owner?.eventBus.Invoke<IOnDiscardCard>(c => c.OnDiscardCard(info));
        discardPile.Add(card);
    }

    /// <summary>
    /// 특정 카드를 소멸패 더미에 추가합니다.
    /// </summary>
    public void ExhaustCard(CardBase card, CardInfo info)
    {
        owner?.eventBus.Invoke<IOnExhaustCard>(c => c.OnExhaustCard(info));
        exhaustPile.Add(card);
    }
}
