using EventStructs;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투 중 플레이어 캐릭터의 턴을 제어하는 컨트롤러.
/// 전투 시작 ~ 전투 종료까지 유지된다.
/// </summary>
public class PlayerCharacterController : ICharacterController
{
    #region ICharacterController

    [SerializeField]
    private Character _controlledCharacter;
    public Character controlledCharacter => _controlledCharacter;

    #endregion

    #region References

    private Player player;

    [SerializeField]
    private SOCharacterStatData playerStatDataSO;

    private PlayerMove playerMove;

    #endregion

    #region Card Piles (전투 중 카드 더미)

    private List<ActionCardData> drawPile = new();
    private List<ActionCardData> hand = new();
    private List<ActionCardData> discardPile = new();
    private List<ActionCardData> exhaustPile = new();

    [SerializeField] private int drawCount = 5;

    #endregion

    public PlayerCharacterController(Player player)
    {
        this.player = player;
        this._controlledCharacter = player.character;

        // PlayerMove 초기화
        CharacterMove charMove = this._controlledCharacter.characterMove;
        if (charMove != null)
        {
            playerMove = new PlayerMove(charMove);
        }
        else
        {
            Debug.LogWarning("PlayerCharacterController: CharacterMove 또는 InputController가 없습니다.");
        }
    }

    public void OnBattleStart()
    {
        // masterDeck 에서 drawPile 복사 후 셔플
        drawPile = new List<ActionCardData>(player.masterDeck);
        ShuffleDeck(drawPile);
        hand.Clear();
        discardPile.Clear();
        exhaustPile.Clear();
    }

    public void OnTurnStart()
    {
        // 1. 이벤트 버스 — 등록된 IOnTurnStart들 호출 (버프, 유물 등)
        controlledCharacter.eventBus.Invoke<IOnTurnStart>(a => a.OnTurnStart());

        // 2. 카드 드로우
        DrawCards(drawCount);

        // 3. UI 갱신
        // TODO: 턴 시작 UI 처리

        // 4. 이동 활성화
        if (playerMove != null)
        {
            playerMove.EnableMove();
        }
    }

    public void OnTurnEnd()
    {
        // 1. 이벤트 버스 — 등록된 IOnTurnEnd들 호출
        controlledCharacter.eventBus.Invoke<IOnTurnEnd>(a => a.OnTurnEnd());

        // 2. 손패 → 묘지 이동
        discardPile.AddRange(hand);
        hand.Clear();

        // 3. 이동 비활성화
        if (playerMove != null)
        {
            playerMove.ClearCanMoveTiles();
            playerMove.DisableMove();
        }

        // 4. UI 정리
        // TODO: 턴 종료 UI 처리
    }

    public void OnDie()
    {
        // TODO: 사망 처리
    }

    #region Card Operations

    private void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (drawPile.Count == 0)
            {
                // 뽑을 카드가 없으면 묘지 → 드로우파일로 셔플
                if (discardPile.Count == 0) return;
                drawPile.AddRange(discardPile);
                discardPile.Clear();
                ShuffleDeck(drawPile);
            }

            var card = drawPile[0];
            drawPile.RemoveAt(0);

            controlledCharacter.eventBus.Invoke<IOnDrawCard>(c => c.OnDrawCard(card));

            hand.Add(card);
        }
    }

    private void ShuffleDeck(List<ActionCardData> deck)
    {
        controlledCharacter.eventBus.Invoke<IOnShuffleDeck>(c => c.OnShuffleDeck(deck));

        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }
    }

    /// <summary>
    /// 카드를 실제로 사용하는 파이프라인
    /// </summary>
    public void UseCard(ActionCardData card, List<Character> targets, Vector2Int targetPosition)
    {
        // 1. 이벤트 버스로 전송할 Payload 생성
        CardInfo info = new CardInfo(controlledCharacter, targets, targetPosition, card, CardFlag.Normal);

        // 2. 사용 직전 발동 처리 (IOnBeforeUseCard)

        controlledCharacter.eventBus.Invoke<IOnBeforeUseCard>(c => c.OnBeforeUseCard(info));

        // 3. 실제 카드 로직 실행 (카드 데이터 쪽에 구현된 함수 호출)
        // ex) card.Execute(info);

        // 4. 리소스 소모 및 손패/묘지 처리

        hand.Remove(card);

        // 카드가 소모되는 특수 상태(NoDiscard)가 아니면 버리기 혹은 소멸로 이동

        if (!info.cardFlags.HasFlag(CardFlag.NoDiscard))
        {
            // 임시로 카드 자체 속성에 isExhaust 가 있다고 가정. (현재는 강제 discard 처리)
            bool isExhaustCard = false;


            if (!info.cardFlags.HasFlag(CardFlag.NoExhaust) && isExhaustCard)
            {
                exhaustPile.Add(card);
            }
            else
            {
                discardPile.Add(card);
            }
        }

        // 5. 사용 직후 발동 처리 (IOnAfterUseCard)

        controlledCharacter.eventBus.Invoke<IOnAfterUseCard>(c => c.OnAfterUseCard(info));
    }

    #endregion
}

