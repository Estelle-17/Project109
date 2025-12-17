using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using System.Text;

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

    public EffectAreaCheckButton effectAreaCheckButton; //공격 범위 확인용 버튼

    public UnityEvent OnCardClick;  //클릭 시 호출될 이벤트

    public bool bIsCardHighlight;

    void Start()
    {
        selectHighlightObject.SetActive(false);
    }

    public void UpdateActionCardData(ActionCardData newCardData)
    {
        cardData = newCardData;

        if (cardData.isUpgrade)
        {
            cardName.text = cardData.cardName + "+";
        }
        else
        {
            cardName.text = cardData.cardName;
        }

        UpdateCardDescription();

        if (AssetCacheManager.instance.TryGetTexture(cardData.texturePath, out Sprite texture))
        {
            cardImage.sprite = texture;
        }
    }

    public void UpgradeCard()
    {
        if(cardData == null)
        {
            return;
        }

        //현재 가지고 있는 카드 데이터를 기반으로 업그레이드 진행
        //currentCardEffect = cardData.upgradeEffects;

        cardName.SetText(cardData.cardName + "+");

        cardData.isUpgrade = true;

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
        if(bIsCardHighlight)
            OnSelectHighlight();       //카드 하이라이트on

        //카드 범위 세팅 진행
        if (cardData.isUpgrade)
        {
            //UIManager.instance.effectAreaManager.SetEffectArea(cardData.upgradeEffects.effectArea);      
        }
        else
        {
            //UIManager.instance.effectAreaManager.SetEffectArea(cardData.defaultEffects.effectArea);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (bIsCardHighlight)
            OffSelectHighlight();       //카드 하이라이트off
    }
}
