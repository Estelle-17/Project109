using CardTypes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public RectTransform canvasRect;

    //현재 활성화된 UI정보 리스트
    private Stack<GameObject> activeUICanvasList = new Stack<GameObject>();
    private Stack<GameObject> tempDeactiveUICanvasList = new Stack<GameObject>();

    //유물 관련 변수
    public GameObject relicDescription;
    RectTransform relicDescriptionTransform;
    public TextMeshProUGUI relicDescriptionText;

    //카드 상세 확인 관련 변수
    public CardDetailPanel cardCheckHandler;


    //카드 범위 확인 관련 변수
    public EffectAreaManager effectAreaManager;
    public EffectAreaTile effectAreaTile;
    public EffectAreaTile AdditionalEffectAreaTile;

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();

        if (relicDescription != null)
        {
            OffRelicDescription();
            relicDescriptionTransform = relicDescription.transform.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (relicDescription.activeSelf && relicDescriptionTransform)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            mousePos += new Vector2(50, 50);    //offset
            relicDescriptionTransform.position = mousePos;
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    #region UIStack Check

    public void PushActiveUIPanel(GameObject newObject)
    {
        activeUICanvasList.Push(newObject);
        newObject.SetActive(true);
        Debug.Log($"[UIManager] Pushed : {newObject.name}. Current UIList Stack Count : {activeUICanvasList.Count}");

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    public void PopActiveUIPanel()
    {
        if (activeUICanvasList.Count == 0)
            return;

        GameObject popObject = activeUICanvasList.Pop();
        popObject.SetActive(false);
        Debug.Log($"[UIManager] Poped : {popObject.name}. Current UIList Stack Count : {activeUICanvasList.Count}");

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    //현재 활성화된 UI들 임시 비활성화
    public void TempDeactivateCurrentActiveUIPanel()
    {
        if (activeUICanvasList.Count == 0)
            return;

        //현재 활성화된 UI들 비활성화 후 임시 스택에 저장
        while (activeUICanvasList.Count > 0)
        {
            GameObject ui = activeUICanvasList.Pop();
            ui.SetActive(false);
            tempDeactiveUICanvasList.Push(ui);
        }
        Debug.Log($"[UIManager] TempDeactivate Stack Count: {tempDeactiveUICanvasList.Count}.");

        //초기화 후 새로운 UI 활성화
        activeUICanvasList.Clear();

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    //임시로 비활성화된 UI들 다시 활성화
    public void ReactivateTempDeactiveUIPanel()
    {
        if (tempDeactiveUICanvasList.Count == 0)
            return;

        //현재 임시로 비활성화된 UI들 활성화 진행
        while (tempDeactiveUICanvasList.Count > 0)
        {
            GameObject ui = tempDeactiveUICanvasList.Pop();
            ui.SetActive(true);
            activeUICanvasList.Push(ui);
        }
        tempDeactiveUICanvasList.Clear();
        Debug.Log($"[UIManager] TempDeactivate UIs Activate. Current UIList Stack Count : {activeUICanvasList.Count}");

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    public void RemoveActiveUIFromStack(GameObject targetObject)
    {
        if (activeUICanvasList.Count == 0)
            return;

        //최상위 UI가 제거된 UI와 일치하는지 확인
        if (activeUICanvasList.Peek() == targetObject)
        {
            activeUICanvasList.Pop();
            Debug.Log($"[UIManager] Removed top : {targetObject.name}. Current UIList Stack Count : {activeUICanvasList.Count}");
        }
        else
        {
            //최상위 UI가 아닌 경우 스택을 순회하여 강제로 제거
            Stack<GameObject> tempStack = new Stack<GameObject>();
            bool bfoundObject = false;
            while (activeUICanvasList.Count > 0)
            {
                GameObject currentObject = activeUICanvasList.Pop();
                if (currentObject == targetObject)
                {
                    bfoundObject = true;
                    Debug.Log($"[UIManager] Force Removed UI : {targetObject.name}.");
                    break;
                }
                tempStack.Push(currentObject);
            }
            //상위에 있던 Object들 다시 채워넣기
            while (tempStack.Count > 0)
            {
                activeUICanvasList.Push(tempStack.Pop());
            }
            if (!bfoundObject)
            {
                Debug.LogWarning($"[UIManager] UI {targetObject.name} not found in stack to remove!");
            }
            else
            {
                Debug.Log($"Current UIList Stack Count : {activeUICanvasList.Count}");
            }
        }

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    public void SetPlayerTouchSystemActiveInGame()
    {
        if (activeUICanvasList.Count == 0)
        {
            PlayerInputController.instance.EnableObjectInteractionInput();
        }
        else
        {
            PlayerInputController.instance.DisableObjectInteractionInput();
        }
    }

    public bool IsUIActiveInStack(GameObject checkObject)
    {
        return activeUICanvasList.Contains(checkObject);
    }

    public GameObject GetCurrentTopActiveUI()
    {
        return activeUICanvasList.Count > 0 ? activeUICanvasList.Peek() : null;
    }

    #endregion

    #region 유물 설명UI

    public void UpdateRelicDescription(string newDescription)
    {
        relicDescriptionText.text = newDescription;
    }

    public void OnRelicDescription()
    {
        if (relicDescription == null)
            return;

        //relicDescription.transform.GetComponent<RectTransform>().position = newItemPos + new Vector3(75, -50, 0);
        relicDescription.SetActive(true);
    }

    public void OffRelicDescription()
    {
        if (relicDescription == null)
            return;

        relicDescription.SetActive(false);
    }

    #endregion

    #region 카드 범위확인 UI


    public void UpdateEffectAreaUI(CardData newCardData)
    {
        UIManager.instance.ClearEffectAreaTiles();

        //효과 범위 설정
        UIManager.instance.SetEffectAreaFromTargetDistance(newCardData.targetType,
                                                           newCardData.targetMinDistance,
                                                           newCardData.targetMaxDistance,
                                                           TileType.TargetTile);

        //추가 효과 범위 설정
        foreach (EffectArea additionalEffectArea in newCardData.additionalEffectAreaList)
        {
            UIManager.instance.SetEffectAreaFromShapeGenerator(additionalEffectArea.areaType,
                                                               Mathf.Abs(newCardData.targetMaxDistance - newCardData.targetMinDistance),
                                                               additionalEffectArea.distance,
                                                               TileType.AdditionalEffectTile);
        }
    }

    public void UpdateEffectAreaUI(Card card)
    {
        if (card != null && card.cardData != null)
        {
            UpdateEffectAreaUI(card.cardData);
        }
    }

    public void ClearEffectAreaTiles()
    {
        if (effectAreaTile == null)
            return;

        effectAreaTile.ClearAllTiles();
        AdditionalEffectAreaTile.ClearAllTiles();

    }

    public void SetEffectAreaFromTargetDistance(string cardTargetType, int minDistance, int maxDistance, TileType type)
    {
        if (effectAreaTile == null)
            return;

        if (Enum.TryParse(cardTargetType, out TargetType parsedTargetType))
        {
            effectAreaTile.SetTileFromTargetDistance(parsedTargetType, minDistance, maxDistance, type);
        }
        else
        {
            Debug.LogWarning($"Invalid TargetType string: {cardTargetType}");
        }
    }

    public void SetEffectAreaFromShapeGenerator(string shapeName, int shapeLength, int radius, TileType type)
    {
        if (effectAreaTile == null)
            return;

        AdditionalEffectAreaTile.SetTileFromShapeGenerator(shapeName, shapeLength, radius, type);
    }

    #endregion
}
