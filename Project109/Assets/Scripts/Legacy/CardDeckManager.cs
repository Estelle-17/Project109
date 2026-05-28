#if false
using UnityEngine;
using System.Collections.Generic;
using System.Uri;
using System;
using System.Linq;
using GameItem.Types;

public class CardDeckManager : MonoBehaviour
{
    public static CardDeckManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    private List<ActionCardData> cardDeck;

    //각 카드의 고유 ID를 부여할 카운터
    private int nextRuntimeID = 0;

    //카드 데이터 변경 이벤트
    public event Action<ActionCardData> OnCardAdded;
    public event Action<int> OnCardRemoved;
    public event Action<int> OnCardUpgrade;
    public event Action<int> OnCardEvolve;
    public event Action OnCardsRefreshed;

    void Start()
    {
        
    }

    public List<ActionCardData> GetCardDeckList()
    {
        return new List<ActionCardData>(cardDeck);
    }

    //플레이어 덱에 카드 추가
    public ActionCardData AddCard(ActionCardData newCardData)
    {
        ActionCardData newCard = CreateNewCard(newCardData);

        cardDeck.Add(newCard);
        OnCardAdded?.Invoke(newCard);
        Debug.Log($"Card Added : {newCard.cardName}");

        return newCard;
    }

    //플레이어 덱의 카드 제거(카드 ID기반)
    public void RemoveCard(int runtimeID)
    {
        ActionCardData cardToRemove = cardDeck.FirstOrDefault(c => c.runtimeID == runtimeID);
        if (cardToRemove != null)
        {
            if(cardDeck.Remove(cardToRemove)) //카드가 지워졌을 경우
            {            
                OnCardRemoved?.Invoke(runtimeID);
                Debug.Log($"Card Removed : {cardToRemove.cardName} (RuntimeID {runtimeID})");
            }
        }
    }

    public void UpgradeCard(int runtimeID)
    {
        ActionCardData cardToUpgrade = cardDeck.FirstOrDefault(c => c.runtimeID == runtimeID);
        if (cardToUpgrade != null)
        {
            if (AssetCacheManager.instance.TryGetCard(cardToUpgrade.upgradeCardPath, out ActionCardData upgradeCardData))
            {
                Debug.Log($"{cardToUpgrade.name} Upgrade -> {upgradeCardData.name}");
                ActionCardData newCard = Instantiate(upgradeCardData);

                foreach(ActionCardData card in cardDeck)    //업그레이드된 카드로 변경
                {
                    if (card.runtimeID == runtimeID)
                    {
                        int index = cardDeck.IndexOf(card);
                        cardDeck[index] = newCard;
                        cardDeck[index].runtimeID = runtimeID;    //업그레이드된 카드에도 기존 카드의 RuntimeID 유지
                        break;
                    }
                }
                OnCardUpgrade?.Invoke(runtimeID);
                Debug.Log($"Card Upgraded : {cardToUpgrade.cardName} (RuntimeID {runtimeID})");
            }
        }

        //업그레이드가 진행되었으니 모든 카드 업데이트 진행
        RequestAllCardRefresh();
    }

    public void EvolveCard(int runtimeID, EvolveType type)
    {
        ActionCardData cardToEvolve = cardDeck.FirstOrDefault(c => c.runtimeID == runtimeID);
        if (cardToEvolve != null)
        {
            cardToEvolve.isEvolved = true;
            cardToEvolve.cardName = "★" + cardToEvolve.cardName;
            cardToEvolve.evolveType = type;
            OnCardEvolve?.Invoke(runtimeID);
            Debug.Log($"Card Evolved : {cardToEvolve.cardName} (RuntimeID {runtimeID})");
        }

        //Evolve가 진행되었으니 모든 카드 업데이트 진행
        RequestAllCardRefresh();
    }

    public ActionCardData GetRandomCard()
    {
        ActionCardData randomCard = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count)];

        return randomCard;
    }

    public ActionCardData GetSpecificCard(string name)
    {
        ActionCardData specificCard = cardDeck.FirstOrDefault(c => c.cardName == name);

        return specificCard;
    }
    
    //초기설정, 로딩 등 다수의 카드가 변경되었을때 사용
    public void RequestAllCardRefresh()
    {
        OnCardsRefreshed?.Invoke();
    }

    //SO데이터를 기반으로 고유ID를 가진 새로운 카드 데이터 생성
    private ActionCardData CreateNewCard(ActionCardData newCardData) 
    {
        ActionCardData newCard = Instantiate(newCardData);
        newCard.runtimeID = nextRuntimeID++;

        return newCard;
    }
}
#endif
