using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

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

    public RectTransform canvasRect;

    //현재 활성화된 UI정보 리스트
    private Stack<GameObject> activeUICanvasList = new Stack<GameObject>();

    //유물 관련 변수
    public GameObject relicDescription;
    RectTransform relicDescriptionTransform;
    public TextMeshProUGUI relicDescriptionText;

    //카드 효과 범위 관련 변수
    public GameObject cardEffectAreaBackground;

    //카드 상세 확인 관련 변수
    public CardCheckHandler cardCheckHandler;

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();

        if(relicDescription != null)
        {
            OffRelicDescription();
            relicDescriptionTransform = relicDescription.transform.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if(relicDescription.activeSelf && relicDescriptionTransform)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos += new Vector2(100, 35);    //offset
            relicDescriptionTransform.position = mousePos;
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

    public void RemoveActiveUIFromStack(GameObject targetObject)
    {
        if (activeUICanvasList.Count == 0)
            return;

        //최상위 UI가 제거된 UI와 일치하는지 확인
        if(activeUICanvasList.Peek() == targetObject)
        {
            activeUICanvasList.Pop();
            Debug.Log($"[UIManager] Removed top : {targetObject.name}. Current UIList Stack Count : {activeUICanvasList.Count}");
        }
        else
        {
            //최상위 UI가 아닌 경우 스택을 순회하여 강제로 제거
            Stack<GameObject> tempStack = new Stack<GameObject>();
            bool bfoundObject = false;
            while(activeUICanvasList.Count > 0)
            {
                GameObject currentObject = activeUICanvasList.Pop();
                if(currentObject == targetObject)
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
        if(activeUICanvasList.Count == 0)
        {
            TouchSystem.instance.EnableObjectInteractionInput();
        }
        else
        {
            TouchSystem.instance.DisableObjectInteractionInput();
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

    public void OnCardEffectAreaBackground(Vector3 newItemPos)
    {
        if (cardEffectAreaBackground == null)
            return;

        cardEffectAreaBackground.transform.GetComponent<RectTransform>().position = newItemPos + new Vector3(125, 60, 0);
        cardEffectAreaBackground.SetActive(true);
    }

    public void OffCardEffectAreaBackground()
    {
        if (cardEffectAreaBackground == null)
            return;

        cardEffectAreaBackground.SetActive(false);
    }

    #endregion
}
