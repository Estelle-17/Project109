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

    // UI 부모 레이어 관리 (각 레이어는 독립된 Canvas와 Sorting Order를 갖게 됨)
    [Header("UI Parent Layers")]
    public RectTransform worldUILayer;
    public RectTransform normalUILayer;
    public RectTransform topUILayer;
    public RectTransform popupUILayer;

    //현재 활성화된 Normal UI 목록 (스택 관리 대상)
    private Stack<GameObject> activeNormalUIStack = new Stack<GameObject>();
    private Stack<GameObject> tempDeactivatedNormalUIStack = new Stack<GameObject>();

    //유물 관련 변수
    public GameObject relicDescription;
    RectTransform relicDescriptionTransform;
    public TextMeshProUGUI relicDescriptionText;

    //카드 상세 확인 관련 변수
    public CardDetailPanel cardCheckHandler;

    //상단 HUD 관련 변수
    public TopHUDPanel topHUDPanel;
    private Player boundPlayer;
    private GameObject exploreMapInstance;


    //카드 범위 확인 관련 변수
    public EffectAreaManager effectAreaManager;
    public EffectAreaTile effectAreaTile;
    public EffectAreaTile AdditionalEffectAreaTile;

    System.Collections.IEnumerator Start()
    {
        if (relicDescription == null)
        {
            yield return StartCoroutine(InitializeAddressableUI());
        }
        else
        {
            OffRelicDescription();
            relicDescriptionTransform = relicDescription.transform.GetComponent<RectTransform>();
        }
    }

    private System.Collections.IEnumerator InitializeAddressableUI()
    {
        // AssetCacheManager 인스턴스가 준비될 때까지 대기
        while (AssetCacheManager.instance == null)
        {
            yield return null;
        }
        GameObject relicPrefab = null;
        GameObject hudPrefab = null;
        // AssetCacheManager가 RelicDescription 및 TopHUDPanel 프리팹을 캐시할 때까지 대기
        while (true)
        {
            bool relicReady = AssetCacheManager.instance.TryGetUI("RelicDescription", out relicPrefab);
            bool hudReady = AssetCacheManager.instance.TryGetUI("TopHUDPanel", out hudPrefab);
            if (relicReady && hudReady)
            {
                break;
            }
            yield return null;
        }
        if (relicPrefab != null)
        {
            GameObject inst = Instantiate(relicPrefab);
            inst.name = "RelicDescription";
            
            Transform parentTransform = topUILayer != null ? topUILayer : transform;
            inst.transform.SetParent(parentTransform, false);

            RectTransform prefabRect = relicPrefab.GetComponent<RectTransform>();
            relicDescriptionTransform = inst.GetComponent<RectTransform>();
            
            if (relicDescriptionTransform != null && prefabRect != null)
            {
                CopyRectTransform(prefabRect, relicDescriptionTransform);
            }

            relicDescription = inst;
            relicDescriptionText = inst.GetComponentInChildren<TextMeshProUGUI>();
            OffRelicDescription();
            Debug.Log("[UIManager] RelicDescription UI dynamically initialized via Addressables.");
        }
        if (hudPrefab != null)
        {
            GameObject inst = Instantiate(hudPrefab);
            inst.name = "TopHUDPanel";
            
            Transform parentTransform = topUILayer != null ? topUILayer : transform;
            inst.transform.SetParent(parentTransform, false);

            topHUDPanel = inst.GetComponent<TopHUDPanel>();
            
            // Nested Canvas의 스케일 및 왜곡을 방지하기 위해 앵커를 부모 Canvas에 맞춘 Full Stretch로 강제 정의합니다.
            RectTransform hudRect = inst.GetComponent<RectTransform>();
            if (hudRect != null)
            {
                hudRect.anchorMin = Vector2.zero;
                hudRect.anchorMax = Vector2.one;
                hudRect.pivot = new Vector2(0.5f, 0.5f);
                hudRect.anchoredPosition = Vector2.zero;
                hudRect.sizeDelta = Vector2.zero;
                hudRect.offsetMin = Vector2.zero;
                hudRect.offsetMax = Vector2.zero;
                hudRect.localScale = Vector3.one; // 프리팹의 0,0,0 스케일 오류 원천 차단
            }

            // 중첩 Canvas 구조에서는 독립적인 CanvasScaler가 동작하지 않으므로 컴포넌트를 제거합니다.
            CanvasScaler scaler = inst.GetComponent<CanvasScaler>();
            if (scaler != null)
            {
                Destroy(scaler);
            }

            if (topHUDPanel != null)
            {
                topHUDPanel.gameObject.SetActive(true);
                // 이미 RunManager에 player가 생성되어 있다면 즉시 바인딩
                if (RunManager.instance != null && RunManager.instance.player != null)
                {
                    BindPlayerToHUD(RunManager.instance.player);
                }
            }
            Debug.Log("[UIManager] TopHUDPanel UI dynamically initialized via Addressables.");
            
            // 어드레서블 초기 로딩이 완료되었으므로, 인게임 탐색에 필요한 지도를 즉각 스폰 및 초기화합니다.
            GetOrSpawnExploreMap();
        }
    }


    private void Update()
    {
        if (relicDescription == null) return;

        if (relicDescription.activeSelf && relicDescriptionTransform)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            mousePos += new Vector2(50, 50);    //offset
            relicDescriptionTransform.position = mousePos;
        }
    }

    void OnDestroy()
    {
        if (boundPlayer != null && boundPlayer.playerStat != null)
        {
            boundPlayer.playerStat.OnGoldChanged -= OnHUDGoldChanged;
            boundPlayer.playerStat.OnMemorySharpChanged -= OnHUDMemorySharpChanged;
        }

        if (instance == this)
        {
            instance = null;
        }
    }

    #region UIStack Check

    public GameObject OpenUI(string uiName, UILayerType layerType = UILayerType.Normal)
    {
        if (AssetCacheManager.instance == null)
        {
            Debug.LogError("[UIManager] AssetCacheManager is null!");
            return null;
        }

        if (!AssetCacheManager.instance.TryGetUI(uiName, out GameObject prefab))
        {
            Debug.LogError($"[UIManager] Failed to find UI prefab in AssetCacheManager cache: {uiName}");
            return null;
        }

        GameObject uiInstance = Instantiate(prefab);
        uiInstance.name = uiName;

        UILayerType targetLayerType = layerType;
        if (uiInstance.TryGetComponent<UIPanelBase>(out var panel))
        {
            targetLayerType = panel.uiLayerType;
        }

        PushActiveUIPanel(uiInstance, targetLayerType);
        return uiInstance;
    }

    private RectTransform GetLayerTransform(UILayerType layerType)
    {
        switch (layerType)
        {
            case UILayerType.World: return worldUILayer;
            case UILayerType.Normal: return normalUILayer;
            case UILayerType.Top: return topUILayer;
            case UILayerType.Popup: return popupUILayer;
            default: return null;
        }
    }

    public void PushActiveUIPanel(GameObject newObject)
    {
        UILayerType layerType = UILayerType.Normal;
        if (newObject.TryGetComponent<UIPanelBase>(out var panel))
        {
            layerType = panel.uiLayerType;
        }
        PushActiveUIPanel(newObject, layerType);
    }

    public void PushActiveUIPanel(GameObject newObject, UILayerType layerType)
    {
        // 1. 해당 레이어 하위로 부모 재설정
        RectTransform targetLayer = GetLayerTransform(layerType);
        if (targetLayer != null && newObject.transform.parent != targetLayer)
        {
            newObject.transform.SetParent(targetLayer, false);
        }

        // 2. Normal 타입만 스택으로 관리
        if (layerType == UILayerType.Normal)
        {
            activeNormalUIStack.Push(newObject);
            Debug.Log($"[UIManager] Pushed Normal UI: {newObject.name}. Current Normal Stack Count: {activeNormalUIStack.Count}");
        }
        else
        {
            Debug.Log($"[UIManager] Activated UI: {newObject.name} (Layer: {layerType})");
        }

        newObject.SetActive(true);

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    public void PopActiveUIPanel()
    {
        if (activeNormalUIStack.Count == 0)
            return;

        GameObject popObject = activeNormalUIStack.Pop();
        popObject.SetActive(false);
        Debug.Log($"[UIManager] Poped Normal UI: {popObject.name}. Current Normal Stack Count: {activeNormalUIStack.Count}");

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    //현재 활성화된 Normal UI들 임시 비활성화
    public void TempDeactivateCurrentActiveUIPanel()
    {
        if (activeNormalUIStack.Count == 0)
            return;

        //현재 활성화된 Normal UI들 비활성화 후 임시 스택에 저장
        while (activeNormalUIStack.Count > 0)
        {
            GameObject ui = activeNormalUIStack.Pop();
            ui.SetActive(false);
            tempDeactivatedNormalUIStack.Push(ui);
        }
        Debug.Log($"[UIManager] TempDeactivate Stack Count: {tempDeactivatedNormalUIStack.Count}.");

        //초기화 후 새로운 UI 활성화
        activeNormalUIStack.Clear();

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    //임시로 비활성화된 Normal UI들 다시 활성화
    public void ReactivateTempDeactiveUIPanel()
    {
        if (tempDeactivatedNormalUIStack.Count == 0)
            return;

        //현재 임시로 비활성화된 UI들 활성화 진행
        while (tempDeactivatedNormalUIStack.Count > 0)
        {
            GameObject ui = tempDeactivatedNormalUIStack.Pop();
            ui.SetActive(true);
            activeNormalUIStack.Push(ui);
        }
        tempDeactivatedNormalUIStack.Clear();
        Debug.Log($"[UIManager] TempDeactivate Normal UIs Reactivated. Current Normal Stack Count: {activeNormalUIStack.Count}");

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    public void RemoveActiveUIFromStack(GameObject targetObject)
    {
        UILayerType layerType = UILayerType.Normal;
        if (targetObject.TryGetComponent<UIPanelBase>(out var panel))
        {
            layerType = panel.uiLayerType;
        }
        RemoveActiveUIFromStack(targetObject, layerType);
    }

    public void RemoveActiveUIFromStack(GameObject targetObject, UILayerType layerType)
    {
        if (layerType != UILayerType.Normal)
        {
            return;
        }

        if (activeNormalUIStack.Count == 0)
            return;

        //최상위 UI가 제거된 UI와 일치하는지 확인
        if (activeNormalUIStack.Peek() == targetObject)
        {
            activeNormalUIStack.Pop();
            Debug.Log($"[UIManager] Removed top Normal UI: {targetObject.name}. Current Normal Stack Count: {activeNormalUIStack.Count}");
        }
        else
        {
            //최상위 UI가 아닌 경우 스택을 순회하여 강제로 제거
            Stack<GameObject> tempStack = new Stack<GameObject>();
            bool bfoundObject = false;
            while (activeNormalUIStack.Count > 0)
            {
                GameObject currentObject = activeNormalUIStack.Pop();
                if (currentObject == targetObject)
                {
                    bfoundObject = true;
                    Debug.Log($"[UIManager] Force Removed Normal UI: {targetObject.name}.");
                    break;
                }
                tempStack.Push(currentObject);
            }
            //상위에 있던 Object들 다시 채워넣기
            while (tempStack.Count > 0)
            {
                activeNormalUIStack.Push(tempStack.Pop());
            }
            if (!bfoundObject)
            {
                Debug.LogWarning($"[UIManager] Normal UI {targetObject.name} not found in stack to remove!");
            }
            else
            {
                Debug.Log($"Current Normal Stack Count: {activeNormalUIStack.Count}");
            }
        }

        //터치 활성화 여부 확인
        SetPlayerTouchSystemActiveInGame();
    }

    public void SetPlayerTouchSystemActiveInGame()
    {
        if (activeNormalUIStack.Count == 0)
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
        return activeNormalUIStack.Contains(checkObject);
    }

    public GameObject GetCurrentTopActiveUI()
    {
        return activeNormalUIStack.Count > 0 ? activeNormalUIStack.Peek() : null;
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

    #region 상단 HUD 바인딩
    public void BindPlayerToHUD(Player player)
    {
        if (topHUDPanel == null)
        {
            Debug.LogWarning("[UIManager] Cannot bind player because topHUDPanel is null.");
            return;
        }

        if (boundPlayer != null && boundPlayer.playerStat != null)
        {
            boundPlayer.playerStat.OnGoldChanged -= OnHUDGoldChanged;
            boundPlayer.playerStat.OnMemorySharpChanged -= OnHUDMemorySharpChanged;
        }

        boundPlayer = player;

        if (boundPlayer != null && boundPlayer.playerStat != null)
        {
            topHUDPanel.UpdateGold(boundPlayer.playerStat.inGame_Currency_Gold);
            topHUDPanel.UpdateSpecialResource(boundPlayer.playerStat.inGame_Currency_MemorySharp);

            boundPlayer.playerStat.OnGoldChanged += OnHUDGoldChanged;
            boundPlayer.playerStat.OnMemorySharpChanged += OnHUDMemorySharpChanged;

            topHUDPanel.SetupHUD(OnMapButtonClicked, OnDeckButtonClicked);

            Debug.Log("[UIManager] Successfully bound player stats to TopHUDPanel.");
        }
    }

    private void OnHUDGoldChanged(int newGold)
    {
        if (topHUDPanel != null)
        {
            topHUDPanel.UpdateGold(newGold);
        }
    }

    private void OnHUDMemorySharpChanged(int newMemorySharp)
    {
        if (topHUDPanel != null)
        {
            topHUDPanel.UpdateSpecialResource(newMemorySharp);
        }
    }

    private void OnMapButtonClicked()
    {
        Debug.Log("[UIManager] Map Button Clicked");
        
        ExploreUI exploreUI = GetOrSpawnExploreMap();
        if (exploreUI != null)
        {
            if (exploreUI.gameObject.activeSelf)
            {
                exploreUI.CloseUI();
            }
            else
            {
                PushActiveUIPanel(exploreUI.gameObject, UILayerType.Normal);
            }
        }
    }

    public ExploreUI GetOrSpawnExploreMap()
    {
        if (exploreMapInstance == null)
        {
            exploreMapInstance = OpenUI("ExploreMap", UILayerType.Normal);
            if (exploreMapInstance != null)
            {
                ExploreUI exploreUI = exploreMapInstance.GetComponent<ExploreUI>();
                if (exploreUI != null)
                {
                    exploreUI.CreateExploreMap();
                }
            }
        }
        return exploreMapInstance != null ? exploreMapInstance.GetComponent<ExploreUI>() : null;
    }

    public void DestroyExploreMap()
    {
        if (exploreMapInstance != null)
        {
            RemoveActiveUIFromStack(exploreMapInstance);
            Destroy(exploreMapInstance);
            exploreMapInstance = null;

            if (RunManager.instance != null)
            {
                RunManager.instance.currentExploreUI = null;
            }
            Debug.Log("[UIManager] Existing ExploreMap instance destroyed for stage reset.");
        }
    }

    private void OnDeckButtonClicked()
    {
        Debug.Log("[UIManager] Deck Button Clicked");
        // TODO: 덱 UI 연동 필요 시 처리
    }

    private void CopyRectTransform(RectTransform source, RectTransform target)
    {
        if (source == null || target == null) return;

        target.anchorMin = source.anchorMin;
        target.anchorMax = source.anchorMax;
        target.pivot = source.pivot;
        target.anchoredPosition = source.anchoredPosition;
        target.sizeDelta = source.sizeDelta;

        Vector3 sourceScale = source.localScale;
        if (sourceScale == Vector3.zero)
        {
            sourceScale = Vector3.one;
        }
        target.localScale = sourceScale;

        target.offsetMin = source.offsetMin;
        target.offsetMax = source.offsetMax;
    }
    #endregion
}
