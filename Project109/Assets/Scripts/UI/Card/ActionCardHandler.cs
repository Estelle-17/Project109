using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class ActionCardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ActionCardData cardData;

    private CardEffect currentCardEffect;   //현재 카드효과
    [SerializeField] private CardDescriptionHandler cardDescriptionHandler;

    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI useStamina;

    public RawImage cardImage;  //카드 데이터에 맞는 이미지

    public GameObject selectHighlightObject;    //선택을 알려주는 하이라이트 UI

    public EffectAreaCheckButton effectAreaCheckButton; //공격 범위 확인용 버튼

    public List<GameObject> checkActiveTiles;

    public UnityEvent OnCardClick;  //클릭 시 호출될 이벤트

    public bool bIsCardHighlight;

    void Start()
    {
        selectHighlightObject.SetActive(false);
    }

    public void UpdateActionCardData(ActionCardData newCardData, bool isUpgrade)
    {
        cardData = newCardData;

        currentCardEffect = new CardEffect();

        if (isUpgrade)
        {
            currentCardEffect = newCardData.upgradeEffects;
            cardName.text = cardData.cardName + "+";
        }
        else
        {
            currentCardEffect = newCardData.defaultEffects;
            cardName.text = cardData.cardName;
        }

        if(AssetCacheManager.instance.TryGetTexture(cardData.texturePath, out Texture2D texture))
        {
            cardImage.texture = texture;
        }

        UpdateCardDescription();
    }

    public void UpgradeCard()
    {
        if(cardData == null)
        {
            return;
        }

        //현재 가지고 있는 카드 데이터를 기반으로 업그레이드 진행
        currentCardEffect = cardData.upgradeEffects;

        cardName.text = cardData.cardName + "+";

        UpdateCardDescription();
    }

    public void UpdateCardDescription()
    {
        //현재 버프/디버프 및 유물 상태에 따른 변경점 업데이트
        useStamina.text = currentCardEffect.useStamina.ToString();

        if (cardDescriptionHandler != null)
        {
            cardDescription.text = cardDescriptionHandler.MakeCardDescription(currentCardEffect);
        }
    }
    
    static void ConvertStringToIntArray(string input, int[] resultBuffer)
    {
        int len = input.Length;
        if(resultBuffer.Length < len)
        {
            Debug.Log("공격 범위 설정 중 버퍼 크기가 문자열보다 작습니다.");
            return;
        }

        for(int i = 0; i < len; i++)
        {
            if (input[i] < '0' || input[i] > '9')
            {
                Debug.LogError($"유효하지 않은 숫자 문자: {input[i]}");
            }
            else
            {
                resultBuffer[i] = input[i] - '0';
            }
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

        if (cardData == null)
            return;

        //현재 선택된 카드와 맞는 효과 범위 변경
        Tile[,] map = GameManager.instance.ActionCard_EffectArea.GetTileMap();

        int[] convertBuffer;

        for (int index = 0; index < currentCardEffect.effectArea.Count; index++)
        {
            int areaLength = currentCardEffect.effectArea[index].area[0].Length; //공격 범위의 최대 길이
            convertBuffer = new int[areaLength];
            int startColumn = GameManager.instance.ActionCard_EffectArea.centerColumn - (areaLength / 2);   //변경할 타일의 시작 column
            int startRow = GameManager.instance.ActionCard_EffectArea.centerRow - (areaLength / 2); //변경할 타일의 시작 row

            for (int i = 0; i < areaLength; i++)
            {
                ConvertStringToIntArray(currentCardEffect.effectArea[index].area[i], convertBuffer);    //string -> intArray로 변환. 데이터는 convertBuffer로 저장됨
                for (int j = 0; j < convertBuffer.Length; j++)
                {
                    if (convertBuffer[j] == 1)
                    {
                        if(i == areaLength / 2 && j == areaLength / 2)  //공격을 시전 할 위치는 다른 색으로 변경
                        {
                            map[startColumn + i, startRow + j].centerTileColor.SetActive(true);
                            checkActiveTiles.Add(map[startColumn + i, startRow + j].centerTileColor);
                        }
                        else
                        {
                            map[startColumn + i, startRow + j].canMoveAreaColor.SetActive(true);
                            checkActiveTiles.Add(map[startColumn + i, startRow + j].canMoveAreaColor);
                        }
                    }
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (bIsCardHighlight)
            OffSelectHighlight();       //카드 하이라이트on

        //현재 선택된 카드와 맞는 효과 범위 초기화
        foreach (GameObject obj in checkActiveTiles)
        {
            obj.SetActive(false);
        }

        checkActiveTiles = new List<GameObject>();
    }
}
