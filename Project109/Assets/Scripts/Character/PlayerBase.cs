using UnityEngine;
using System.Collections.Generic;

public class PlayerBase : MonoBehaviour
{
    void Start()
    {

    }

    void AddStartCardsInDeck()
    {
        CharacterData characterData;
        AddressableDataLoader.instance.TryGetCharacter("전투광", out characterData);
        if(characterData != null && CardDeckManager.instance != null)
        {
            foreach (StartCard cards in characterData.startCards)
            {
                AddressableDataLoader.instance.TryGetCard(cards.cardName, out ActionCardData cardData);

                for (int i = 0; i < cards.number; i++)
                {
                    ActionCardData tempCardData = cardData;
                    CardDeckManager.instance.AddCard(tempCardData);
                }
            }
            CardDeckManager.instance.RequestAllCardRefresh();
        }
        else
        {
            Debug.LogWarning("Addressable에서 캐릭터 로드 실패");
        }
    }

    public bool cardDeckTest;
    private void Update()
    {
        if (cardDeckTest)
        {
            cardDeckTest = false;
            AddStartCardsInDeck();
        }
    }
}
