using CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public Canvas worldUILayer;
    public Canvas normalUILayer;
    public Canvas topUILayer;
    public Canvas popupUILayer;

    //현재 활성화된 Normal UI 목록 (스택 관리 대상)
    private Stack<GameObject> activeNormalUIStack = new Stack<GameObject>();
    private Stack<GameObject> tempDeactivatedNormalUIStack = new Stack<GameObject>();

    //유물 관련 변수는 TooltipPanel에서 개별 관리하므로 제거되었습니다.

    //카드 상세 확인 관련 변수
    public CardDetailPanel cardCheckHandler;

    //상단 HUD 관련 변수
    public TopHUDPanel topHUDPanel;
    private Player boundPlayer;
    private ExploreUI exploreMapInstance;
    public CardDeckViewPanel cardDeckViewPanel;


    //카드 범위 확인 관련 변수
    public EffectAreaManager effectAreaManager;
    public EffectAreaTile effectAreaTile;
    public EffectAreaTile AdditionalEffectAreaTile;

    System.Collections.IEnumerator Start()
    {
        yield return StartCoroutine(InitializeAddressableUI());
    }

    private System.Collections.IEnumerator InitializeAddressableUI()
    {
        // RunManager와 player가 생성될 때까지 대기
        while (RunManager.instance == null || RunManager.instance.player == null)
        {
            yield return null;
        }

        // AssetCacheManager 인스턴스가 준비되고 모든 데이터 로드가 완료될 때까지 대기
        while (AssetCacheManager.instance == null || !AssetCacheManager.instance.isLoadComplete)
        {
            yield return null;
        }
        GameObject hudPrefab = null;
        GameObject deckPrefab = null;
        // AssetCacheManager가 필요한 프리팹들을 캐시할 때까지 대기
        while (true)
        {
            bool relicReady = AssetCacheManager.instance.TryGetUI("RelicDescription", out _);
            bool hudReady = AssetCacheManager.instance.TryGetUI("TopHUDPanel", out hudPrefab);
            bool deckReady = AssetCacheManager.instance.TryGetUI("CardDeckCanvas", out deckPrefab);
            if (relicReady && hudReady && deckReady)
            {
                break;
            }
            yield return null;
        }
        if (hudPrefab != null)
        {
            Transform parentTransform = topUILayer != null ? topUILayer.transform : transform;
            GameObject inst = Instantiate(hudPrefab, parentTransform, false);
            inst.name = "TopHUDPanel";

            topHUDPanel = inst.GetComponent<TopHUDPanel>();

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
        }
        if (deckPrefab != null)
        {
            Transform parentTransform = topUILayer != null ? topUILayer.transform : transform;
            GameObject inst = Instantiate(deckPrefab, parentTransform, false);
            inst.name = "CardDeckCanvas";

            cardDeckViewPanel = inst.GetComponent<CardDeckViewPanel>();

            if (cardDeckViewPanel != null)
            {
                cardDeckViewPanel.gameObject.SetActive(false);
            }
            Debug.Log("[UIManager] CardDeckCanvas UI dynamically initialized via Addressables.");
        }

        // 어드레서블 초기 로딩이 완료되었으므로, 인게임 탐색에 필요한 지도를 즉각 스폰 및 초기화합니다.
        GetOrSpawnExploreMap();
    }


    private void Update()
    {
        // 유물 툴팁 위치 업데이트는 TooltipPanel 내부에서 처리되므로 UIManager Update에서는 제거되었습니다.
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

    public GameObject OpenUI(string uiName, UILayerType layerType = UILayerType.Normal, bool startActive = true)
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

        // 프리팹 단계에서 UIPanelBase의 레이어 타입을 먼저 체크합니다.
        UILayerType targetLayerType = layerType;
        if (prefab.TryGetComponent<UIPanelBase>(out var panel))
        {
            targetLayerType = panel.uiLayerType;
        }

        RectTransform targetLayer = GetLayerTransform(targetLayerType);

        // 생성과 동시에 부모를 명사해 스케일 1,1,1 및 앵커가 프리팹 세팅 그대로 자연스럽게 자리 잡도록 합니다.
        GameObject uiInstance = Instantiate(prefab, targetLayer, false);
        uiInstance.name = uiName;

        if (startActive)
        {
            PushActiveUIPanel(uiInstance, targetLayerType);
        }
        else
        {
            uiInstance.SetActive(false);
        }

        return uiInstance;
    }

    private RectTransform GetLayerTransform(UILayerType layerType)
    {
        switch (layerType)
        {
            case UILayerType.World: return worldUILayer != null ? worldUILayer.transform as RectTransform : null;
            case UILayerType.Normal: return normalUILayer != null ? normalUILayer.transform as RectTransform : null;
            case UILayerType.Top: return topUILayer != null ? topUILayer.transform as RectTransform : null;
            case UILayerType.Popup: return popupUILayer != null ? popupUILayer.transform as RectTransform : null;
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
        if (targetLayer != null)
        {
            if (newObject.transform.parent != targetLayer)
            {
                newObject.transform.SetParent(targetLayer, false);
            }
            newObject.transform.SetAsLastSibling();
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
        // 이제 InputManager가 Reference Counting을 통해 터치 잠금을 자동으로 관리하므로,
        // UIManager에서 직접 PlayerInputController의 입력을 활성화/비활성화할 필요가 없습니다.
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
            topHUDPanel.UpdateGold(boundPlayer.playerStat.InGameCurrencyGold);
            topHUDPanel.UpdateSpecialResource(boundPlayer.playerStat.InGameCurrencyMemorySharp);

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
                exploreUI.UIDeactive();
            }
            else
            {
                PushActiveUIPanel(exploreUI.gameObject, UILayerType.Top);
            }
        }
    }

    public ExploreUI GetOrSpawnExploreMap()
    {
        if (exploreMapInstance == null)
        {
            GameObject exploreMapObj = OpenUI("ExploreMap", UILayerType.Top);
            if (exploreMapObj != null)
            {
                exploreMapInstance = exploreMapObj.GetComponent<ExploreUI>();
                if (exploreMapInstance != null)
                {
                    exploreMapInstance.CreateExploreMap(15);
                }
            }
        }
        return exploreMapInstance;
    }

    public void DestroyExploreMap()
    {
        if (exploreMapInstance != null)
        {
            RemoveActiveUIFromStack(exploreMapInstance.gameObject);
            Destroy(exploreMapInstance.gameObject);
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

        if (cardDeckViewPanel != null)
        {
            if (cardDeckViewPanel.gameObject.activeSelf)
            {
                RemoveActiveUIFromStack(cardDeckViewPanel.gameObject);
                cardDeckViewPanel.gameObject.SetActive(false);
            }
            else
            {
                PushActiveUIPanel(cardDeckViewPanel.gameObject, UILayerType.Normal);
            }
        }
    }
    #endregion

    #region Async UI & Dialog Support
    public System.Collections.IEnumerator OpenUIAsyncCoroutine<T>(string uiName, UILayerType layerType, bool blockWorldInput, Action<T> onComplete) where T : UIPanelBase
    {
        bool acquiredPreLock = false;

        // 1. 에셋 비동기 로딩을 시작하기 전에 '선제적'으로 터치 입력 차단
        if (blockWorldInput && UIInputManager.instance != null)
        {
            UIInputManager.instance.AcquireUILock();
            acquiredPreLock = true;
        }

        // 2. 비동기 에셋 캐시 획득 및 인스턴스화
        GameObject prefab = null;
        yield return StartCoroutine(AssetCacheManager.instance.GetUIAsyncCoroutine(uiName, (result) => prefab = result));

        if (prefab == null)
        {
            Debug.LogError($"[UIManager] Failed to load UI async: {uiName}");
            if (acquiredPreLock && UIInputManager.instance != null)
            {
                UIInputManager.instance.ReleaseUILock();
            }
            onComplete?.Invoke(null);
            yield break;
        }

        RectTransform targetLayer = GetLayerTransform(layerType);
        GameObject uiInstance = Instantiate(prefab, targetLayer, false);
        uiInstance.name = uiName;

        T panel = uiInstance.GetComponent<T>();
        if (panel != null)
        {
            panel.blockWorldInput = blockWorldInput;
            panel.uiLayerType = layerType;
        }

        if (uiInstance != null)
        {
            PushActiveUIPanel(uiInstance, layerType);
        }

        // 선제 락을 해제합니다.
        if (acquiredPreLock && UIInputManager.instance != null)
        {
            UIInputManager.instance.ReleaseUILock();
        }

        onComplete?.Invoke(panel);
    }

    public void ShowConfirmDialog(string title, string message, Action onConfirm, Action onCancel)
    {
        // 팝업 레이어(popupUILayer)에 띄우도록 설정
        GameObject dialogObj = OpenUI("ConfirmDialog", UILayerType.Popup, true);
        if (dialogObj != null)
        {
            UIDialogPanel dialogPanel = dialogObj.GetComponent<UIDialogPanel>();
            if (dialogPanel != null)
            {
                dialogPanel.Setup(title, message, onConfirm, onCancel);
            }
            else
            {
                Debug.LogError("[UIManager] ConfirmDialog prefab does not have UIDialogPanel component!");
            }
        }
        else
        {
            Debug.LogWarning("[UIManager] ConfirmDialog prefab not found in cache. Executing confirm callback as fallback.");
            onConfirm?.Invoke();
        }
    }
    #endregion
}
