using GameItem.Types;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionCardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ActionCardData cardData;

    //private CardEffect currentCardEffect;   //현재 카드효과
    [SerializeField] private CardDescriptionHandler cardDescriptionHandler;

    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI useStamina;

    public Image cardImage;  //카드 데이터에 맞는 이미지

    public GameObject selectHighlightObject;    //선택을 알려주는 하이라이트 UI
    [SerializeField] private GameObject extraDescriptionSpawnPos;      //추가 설명UI 스폰 위치

    public EffectAreaCheckButton effectAreaCheckButton; //공격 범위 확인용 버튼

    public UnityEvent OnCardClick;  //클릭 시 호출될 이벤트(다양한 변수들도 쉽게 호출하기 위해 UnityEvent 사용)

    public bool bIsCardHighlight;

    void Start()
    {
        selectHighlightObject.SetActive(false);
    }

    public void UpdateActionCardData(ActionCardData newCardData)
    {
        if(newCardData == null)
        {
            Debug.LogWarning("New Card Data is null!");
            return;
        }
        cardData = newCardData;

        cardName.text = cardData.cardName;

        UpdateCardDescription();

        if (AssetCacheManager.instance.TryGetTexture(cardData.texturePath, out Sprite texture))
        {
            cardImage.sprite = texture;
        }
    }

    public void UpgradeCard()
    {
        if(cardData == null)
            return;

        //카드 데이터를 기반으로 업그레이드 진행
        for(int index = 0; index < cardData.amountList.Count; index++)
        {
            cardData.amountList[index] += cardData.upgradeAmountList[index];
        }

        cardName.SetText(cardName.text + "+");

        UpdateCardDescription();
    }

    //저장된 카드 데이터로 진화 후의 카드 변경
    public void EvolveCard()
    {
        if (cardData == null)
            return;

        cardName.SetText("★" + cardName.text);

        UpdateCardDescription();
    }

    public void UpdateCardDescription()
    {
        if (AssetCacheManager.instance.TryGetCardDescription(cardData.path, out CardDescription description))
        {
            cardDescription.SetText(description.description);
        }
        else 
        { 
            Debug.LogWarning("Card description not found for path: " + cardData.path);
        }
    }

    public void UpdateExtraDescription()
    {
        if (cardData != null)
            UIManager.instance.UpdateCardExtraDescription(cardData, extraDescriptionSpawnPos.transform);
    }

    public ActionCardData GetCardData()
    {
        return cardData;
    }

    public void OnSelectHighlight()
    {
        selectHighlightObject.SetActive(true);
    }

    public void OffSelectHighlight()
    {
        selectHighlightObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCardClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.SetAsLastSibling(); //카드가 다른 UI 위에 표시되도록 설정

        if (bIsCardHighlight)
            OnSelectHighlight();       //카드 하이라이트on

        //extraDescriptionManager.ShowExtraDescription();    //추가 설명 UI 보여주기
        if(cardData != null)
            UIManager.instance.UpdateCardExtraDescription(cardData, extraDescriptionSpawnPos.transform, transform.localScale);

        //카드 범위 세팅 진행
        //UIManager.instance.effectAreaManager.SetEffectArea(cardData.effectArea, transform.localScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (bIsCardHighlight)
            OffSelectHighlight();       //카드 하이라이트off

        UIManager.instance.HideCardExtraDescription();

        //extraDescriptionManager.HideExtraDescription();    //추가 설명 UI 보여주기
    }
}
