using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public struct TooltipData
{
    public string header;
    public string body;

    public TooltipData(string header, string body)
    {
        this.header = header;
        this.body = body;
    }
}

[System.Serializable]
public struct KeywordEntry
{
    public string keyword;
    public string header;
    [TextArea(3, 5)]
    public string description;
}

[RequireComponent(typeof(RectTransform))]
public class TooltipManager : MonoBehaviour
{
    private static TooltipManager instance;
    public static TooltipManager Instance
    {
        get
        {
            if (instance == null)
            {
                CreateInstance();
            }
            return instance;
        }
    }

    [Header("Layout Settings")]
    [SerializeField] private Vector2 mouseOffset = new Vector2(15, -15);
    [SerializeField] private float padding = 15f;

    [Header("Keyword Settings")]
    [SerializeField] private List<KeywordEntry> keywordDatabase = new List<KeywordEntry>();
    private Dictionary<string, KeywordEntry> keywordCache = new Dictionary<string, KeywordEntry>();

    private RectTransform rectTransform;
    private Canvas parentCanvas;
    private VerticalLayoutGroup layoutGroup;
    private ContentSizeFitter sizeFitter;

    private GameObject panelTemplate;
    private List<TooltipPanel> activePanels = new List<TooltipPanel>();
    private List<TooltipPanel> panelPool = new List<TooltipPanel>();

    // 고정 배치 타겟 임시 보관
    private RectTransform currentTargetRect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeComponents();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeComponents()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();

        // 1. 다중 정렬 레이아웃을 위한 Vertical Layout Group 설정
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        if (layoutGroup == null)
        {
            layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
        }
        layoutGroup.childAlignment = TextAnchor.UpperLeft;
        layoutGroup.childControlWidth = true;
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.spacing = 8f;
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);

        // 2. 전체 패널 크기 자동 조절을 위한 Content Size Fitter 설정
        sizeFitter = GetComponent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            sizeFitter = gameObject.AddComponent<ContentSizeFitter>();
        }
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // 3. 기존 프리팹에 들어있던 텍스트 요소를 찾아서 템플릿(TooltipPanel)으로 개조
        // UIManager에서 dynamic하게 스폰될 때 child에 TextMeshProUGUI가 있을 것임
        TextMeshProUGUI legacyText = GetComponentInChildren<TextMeshProUGUI>(true);
        if (legacyText != null)
        {
            panelTemplate = legacyText.gameObject;
            
            if (!panelTemplate.TryGetComponent<LayoutElement>(out _))
            {
                panelTemplate.AddComponent<LayoutElement>();
            }

            if (!panelTemplate.TryGetComponent<TooltipPanel>(out _))
            {
                panelTemplate.AddComponent<TooltipPanel>();
            }

            panelTemplate.SetActive(false);
        }
        else
        {
            Debug.LogError("[TooltipManager] Legacy text component not found in prefab children.");
        }

        // 4. 키워드 사전 캐싱
        BuildKeywordCache();
    }

    private void BuildKeywordCache()
    {
        keywordCache.Clear();
        foreach (var entry in keywordDatabase)
        {
            if (!string.IsNullOrEmpty(entry.keyword))
            {
                // [화상]이나 화상 둘 다 매칭 가능하게 처리
                string cleanedKey = entry.keyword.Replace("[", "").Replace("]", "");
                keywordCache[cleanedKey] = entry;
            }
        }
    }

    private static void CreateInstance()
    {
        if (AssetCacheManager.instance == null || UIManager.instance == null)
        {
            Debug.LogWarning("[TooltipManager] UIManager or AssetCacheManager is not initialized yet.");
            return;
        }

        if (AssetCacheManager.instance.TryGetUI("RelicDescription", out GameObject prefab))
        {
            Transform parentTransform = UIManager.instance.popupUILayer != null ? UIManager.instance.popupUILayer : UIManager.instance.transform;
            GameObject inst = Instantiate(prefab, parentTransform, false);
            inst.name = "RelicDescription";

            instance = inst.GetComponent<TooltipManager>();
            if (instance == null)
            {
                instance = inst.AddComponent<TooltipManager>();
            }
            instance.InitializeComponents();
            inst.SetActive(false);
            Debug.Log("[TooltipManager] RelicDescription UI dynamically initialized and TooltipManager attached.");
        }
        else
        {
            Debug.LogError("[TooltipManager] Failed to find RelicDescription prefab in AssetCacheManager.");
        }
    }

    public void ShowTooltip(string title, string content, RectTransform targetRect = null)
    {
        List<TooltipData> tooltips = new List<TooltipData> { new TooltipData(title, content) };
        
        // 본문(content) 스캔 후 추가 키워드 검출
        if (!string.IsNullOrEmpty(content))
        {
            // [화상], [취약] 등의 키워드를 찾는 정규식 패턴
            MatchCollection matches = Regex.Matches(content, @"\[(.*?)\]");
            HashSet<string> scannedKeywords = new HashSet<string>();

            foreach (Match match in matches)
            {
                string keyword = match.Groups[1].Value;
                if (keywordCache.TryGetValue(keyword, out KeywordEntry entry))
                {
                    if (scannedKeywords.Add(keyword)) // 중복 검사
                    {
                        tooltips.Add(new TooltipData(entry.header, entry.description));
                    }
                }
            }
        }

        ShowTooltips(tooltips, targetRect);
    }

    public void ShowTooltips(List<TooltipData> tooltips, RectTransform targetRect = null)
    {
        if (tooltips == null || tooltips.Count == 0)
        {
            HideTooltip();
            return;
        }

        gameObject.SetActive(true);
        ClearCurrentPanels();

        currentTargetRect = targetRect;

        foreach (var data in tooltips)
        {
            if (string.IsNullOrEmpty(data.header) && string.IsNullOrEmpty(data.body)) continue;

            TooltipPanel panel = GetOrCreatePanel();
            if (panel != null)
            {
                panel.Setup(data.header, data.body);
                panel.gameObject.SetActive(true);
                activePanels.Add(panel);
            }
        }

        // 레이아웃 즉시 계산 및 위치 지정
        Canvas.ForceUpdateCanvases();
        UpdatePosition();
    }

    public void HideTooltip()
    {
        ClearCurrentPanels();
        currentTargetRect = null;
        gameObject.SetActive(false);
    }

    private void ClearCurrentPanels()
    {
        foreach (var panel in activePanels)
        {
            panel.gameObject.SetActive(false);
            panelPool.Add(panel);
        }
        activePanels.Clear();
    }

    private TooltipPanel GetOrCreatePanel()
    {
        if (panelTemplate == null) return null;

        if (panelPool.Count > 0)
        {
            TooltipPanel panel = panelPool[panelPool.Count - 1];
            panelPool.RemoveAt(panelPool.Count - 1);
            return panel;
        }

        GameObject inst = Instantiate(panelTemplate, transform, false);
        return inst.GetComponent<TooltipPanel>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        if (rectTransform == null) return;

        // 1. 화면 스케일/해상도 기준 가져오기
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (parentCanvas != null)
        {
            RectTransform canvasRect = parentCanvas.GetComponent<RectTransform>();
            if (canvasRect != null)
            {
                screenWidth = canvasRect.rect.width;
                screenHeight = canvasRect.rect.height;
            }
        }

        // 2. 전체 툴팁 뭉치의 사이즈
        float H = rectTransform.rect.height;
        float W = rectTransform.rect.width;

        float targetCenterY = 0f;
        float targetCenterX = 0f;
        float targetWidth = 0f;

        if (currentTargetRect != null)
        {
            // [UI 기준 고정 모드]
            Vector3[] corners = new Vector3[4];
            currentTargetRect.GetWorldCorners(corners);
            
            // corners: 0=좌하, 1=좌상, 2=우상, 3=우하
            float minX = corners[0].x;
            float maxX = corners[2].x;
            float minY = corners[0].y;
            float maxY = corners[1].y;

            targetCenterX = (minX + maxX) / 2f;
            targetCenterY = (minY + maxY) / 2f;
            targetWidth = maxX - minX;
        }
        else
        {
            // [마우스 실시간 추적 모드]
            Vector2 mousePos = Vector2.zero;
            if (Mouse.current != null)
            {
                mousePos = Mouse.current.position.ReadValue();
            }
            else
            {
                mousePos = Input.mousePosition;
            }
            targetCenterX = mousePos.x;
            targetCenterY = mousePos.y;
            targetWidth = 0f;
        }

        // 3. X축 배치 결정 (오른쪽 vs 왼쪽)
        float pivotX = 0f;
        float finalX = 0f;

        if (currentTargetRect != null)
        {
            // 카드/유물 UI의 오른쪽 배치 시도
            float expectedRightX = targetCenterX + (targetWidth / 2f) + mouseOffset.x;
            if (expectedRightX + W > screenWidth - padding)
            {
                // 오른쪽 공간이 부족하면 왼쪽 배치
                pivotX = 1f;
                finalX = targetCenterX - (targetWidth / 2f) - mouseOffset.x;
            }
            else
            {
                // 충분하면 오른쪽 배치
                pivotX = 0f;
                finalX = expectedRightX;
            }
        }
        else
        {
            // 마우스 기준 배치
            float expectedRightX = targetCenterX + mouseOffset.x;
            if (expectedRightX + W > screenWidth - padding)
            {
                pivotX = 1f;
                finalX = targetCenterX - mouseOffset.x;
            }
            else
            {
                pivotX = 0f;
                finalX = expectedRightX;
            }
        }

        // 4. Y축 배치 결정 및 Clamp (세로 이탈 방지)
        float pivotY = 0.5f; // 기본적으로 대상 높이의 중간에 걸침
        float clampMinY = H / 2f + padding;
        float clampMaxY = screenHeight - (H / 2f) - padding;
        
        // 뭉치 높이가 화면 전체 높이를 초과할 경우를 위한 Fallback 예외처리
        if (clampMinY > clampMaxY)
        {
            // 스케일을 축소하여 뭉치 크기를 줄임
            transform.localScale = Vector3.one * 0.85f;
            H *= 0.85f;
            clampMinY = H / 2f + padding;
            clampMaxY = screenHeight - (H / 2f) - padding;
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        float finalY = Mathf.Clamp(targetCenterY, clampMinY, clampMaxY);

        // 5. 위치 적용
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        rectTransform.position = new Vector3(finalX, finalY, 0f);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
