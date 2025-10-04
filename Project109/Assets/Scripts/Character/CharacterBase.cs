using UnityEngine;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour
{
    private CharacterStat stat;

    void Start()
    {
        stat = new CharacterStat();
        //생성된 플레이어의 캐릭터를 GameManager에 등록
        GameManager.instance.currentCharacter = this;
    }

    public CharacterStat GetCharacterStat() { return stat; }

    void AddStartCardsInDeck()
    {
        CharacterData characterData;
        AssetCacheManager.instance.TryGetCharacter("전투광", out characterData);
        if(characterData != null && CardDeckManager.instance != null)
        {
            InitializeStatSetting(characterData);

            foreach (StartCard cards in characterData.startCards)
            {
                AssetCacheManager.instance.TryGetCard(cards.cardName, out ActionCardData cardData);

                for (int i = 0; i < cards.number; i++)
                {
                    ActionCardData tempCardData = cardData;
                    CardDeckManager.instance.AddCard(tempCardData);
                }
            }
            CardDeckManager.instance.RequestAllCardRefresh();

            if (GameManager.instance != null)
            {
                GameManager.instance.currentCharacter = this;
            }
        }
        else
        {
            Debug.LogWarning("Addressable에서 캐릭터 로드 실패");
        }
    }

    void InitializeStatSetting(CharacterData newCharacterData)
    {
        stat.maxHp = newCharacterData.hp;
        stat.curHp = newCharacterData.hp;
        stat.maxStamina = newCharacterData.stamina;
        stat.curStamina = newCharacterData.stamina;
        stat.staminaRegen = newCharacterData.staminaRegen;
        stat.strength = newCharacterData.strength;
        stat.armor = newCharacterData.armor;
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
