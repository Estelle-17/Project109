using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using XLua;

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

    // 네임스페이스 키워드 정규식: [타입.ID] (예: [effect.burning], [cardtag.exhaust] 3)
    private static readonly Regex KeywordRegex = new Regex(@"\[(effect|card|relic|cardtag)\.([a-zA-Z0-9_]+)\](?:\s*(\d+))?", RegexOptions.Compiled);

    [Header("Layout Settings")]
    [SerializeField] private Vector2 mouseOffset = new Vector2(15, -15);
    [SerializeField] private float padding = 15f;

    private RectTransform rectTransform;
    private Canvas parentCanvas;
    private VerticalLayoutGroup layoutGroup;
    private ContentSizeFitter sizeFitter;

    [Header("Prefabs")]
    [SerializeField] private TooltipPanel panelTemplatePrefab;
    private List<TooltipPanel> activePanels = new List<TooltipPanel>();
    private List<TooltipPanel> panelPool = new List<TooltipPanel>();

    // 고정 배치 타겟 임시 보관
    private RectTransform currentTargetRect;
    private readonly Vector3[] cornersCache = new Vector3[4];

    /// <summary>
    /// [type.id] 키워드 텍스트를 유저 화면용 컬러/이름으로 치환해줍니다.
    /// 예: [effect.burning] ➡ <color=#FF7F00>[화상]</color>
    /// </summary>
    public static string ReplaceKeywordsForDisplay(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        return KeywordRegex.Replace(text, match =>
        {
            string type = match.Groups[1].Value;
            string id = match.Groups[2].Value;

            string displayName = GetDisplayName(type, id);
            if (string.IsNullOrEmpty(displayName))
            {
                return match.Value; // 찾지 못한 경우 원본 복원
            }

            string colorHex = GetColorForType(type);
            return $"<color={colorHex}>[{displayName}]</color>";
        });
    }

    private static string GetDisplayName(string type, string id)
    {
        switch (type.ToLower())
        {
            case "effect":
                if (ModLoader.Instance != null && ModLoader.Instance.EffectDatabase != null)
                {
                    if (ModLoader.Instance.EffectDatabase.TryGetValue(id, out EffectData effectData))
                    {
                        return effectData.effectName;
                    }
                }
                break;
            case "card":
                if (ModLoader.Instance != null && ModLoader.Instance.CardDatabase != null)
                {
                    if (ModLoader.Instance.CardDatabase.TryGetValue(id, out CardData cardData))
                    {
                        return cardData.cardName;
                    }
                }
                break;
            case "relic":
                if (ModLoader.Instance != null && ModLoader.Instance.RelicDatabase != null)
                {
                    if (ModLoader.Instance.RelicDatabase.TryGetValue(id, out RelicData relicData))
                    {
                        return relicData.relicName;
                    }
                }
                break;
            case "cardtag":
                if (LuaManager.Instance != null)
                {
                    LuaTable proto = LuaManager.Instance.GetCardTagPrototype(id);
                    if (proto != null)
                    {
                        var getDisplayName = proto.Get<LuaFunction>("GetDisplayName");
                        if (getDisplayName != null)
                        {
                            object[] results = getDisplayName.Call(proto);
                            if (results != null && results.Length > 0 && results[0] is string str)
                            {
                                return str;
                            }
                        }
                    }
                }
                break;
        }
        return null;
    }

    private static string GetColorForType(string type)
    {
        switch (type.ToLower())
        {
            case "effect": return "#FF7F00"; // 주황 (버프/디버프)
            case "card": return "#32CD32";   // 연두 (카드 링크)
            case "relic": return "#DA70D6";  // 연보라 (유물 링크)
            case "cardtag": return "#FFCC00";    // 노랑 (카드 태그)
            default: return "#FFFFFF";
        }
    }

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

        // 3. 캐시 매니저를 통해 독립적인 TooltipPanel 프리팹 로드
        if (panelTemplatePrefab == null && AssetCacheManager.instance != null)
        {
            if (AssetCacheManager.instance.TryGetUI("TooltipPanel", out GameObject panelPrefab))
            {
                panelTemplatePrefab = panelPrefab.GetComponent<TooltipPanel>();
                if (panelTemplatePrefab == null)
                {
                    Debug.LogError("[TooltipManager] TooltipPanel component not found on 'TooltipPanel' prefab.");
                }
            }
            else
            {
                Debug.LogError("[TooltipManager] Failed to find 'TooltipPanel' prefab in AssetCacheManager.");
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
            Transform parentTransform = UIManager.instance.popupUILayer != null ? UIManager.instance.popupUILayer.transform : UIManager.instance.transform;
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
        // 1. 본문의 [type.id] 키워드를 유저용 강조 텍스트로 치환
        string displayTitle = ReplaceKeywordsForDisplay(title);
        string displayContent = ReplaceKeywordsForDisplay(content);

        List<TooltipData> tooltips = new List<TooltipData> { new TooltipData(displayTitle, displayContent) };
        
        // 2. 원본 본문(content) 스캔 후 추가 키워드 검출
        if (!string.IsNullOrEmpty(content))
        {
            MatchCollection matches = KeywordRegex.Matches(content);
            HashSet<string> scannedKeywords = new HashSet<string>();

            foreach (Match match in matches)
            {
                string type = match.Groups[1].Value;
                string id = match.Groups[2].Value;
                string valueStr = match.Groups[3].Success ? match.Groups[3].Value : string.Empty;
                
                string uniqueKey = $"{type}.{id}".ToLower();
                if (!scannedKeywords.Add(uniqueKey)) continue; // 중복 추가 방지

                string subTitle = string.Empty;
                string subDescription = string.Empty;

                switch (type.ToLower())
                {
                    case "effect":
                        if (ModLoader.Instance != null && ModLoader.Instance.EffectDatabase != null)
                        {
                            if (ModLoader.Instance.EffectDatabase.TryGetValue(id, out EffectData effectData))
                            {
                                subTitle = effectData.effectName;
                                subDescription = effectData.description ?? string.Empty;

                                // 이펙트 템플릿의 {stacks} 등 플레이스홀더 치환
                                if (!string.IsNullOrEmpty(valueStr))
                                {
                                    subDescription = Regex.Replace(subDescription, @"\{stacks?\}", valueStr, RegexOptions.IgnoreCase);
                                    subDescription = Regex.Replace(subDescription, @"\{values?\}", valueStr, RegexOptions.IgnoreCase);
                                    subDescription = Regex.Replace(subDescription, @"\{amt\}", valueStr, RegexOptions.IgnoreCase);
                                }
                            }
                        }
                        break;

                    case "card":
                        if (ModLoader.Instance != null && ModLoader.Instance.CardDatabase != null)
                        {
                            if (ModLoader.Instance.CardDatabase.TryGetValue(id, out CardData cardData))
                            {
                                subTitle = cardData.cardName;
                                subDescription = cardData.description ?? string.Empty;
                            }
                        }
                        break;

                    case "relic":
                        if (ModLoader.Instance != null && ModLoader.Instance.RelicDatabase != null)
                        {
                            if (ModLoader.Instance.RelicDatabase.TryGetValue(id, out RelicData relicData))
                            {
                                subTitle = relicData.relicName;
                                subDescription = relicData.description ?? string.Empty;
                            }
                        }
                        break;

                    case "cardtag":
                        if (LuaManager.Instance != null)
                        {
                            LuaTable proto = LuaManager.Instance.GetCardTagPrototype(id);
                            if (proto != null)
                            {
                                var getDisplayName = proto.Get<LuaFunction>("GetDisplayName");
                                var getDescription = proto.Get<LuaFunction>("GetDescription");
                                
                                string dispName = id;
                                if (getDisplayName != null)
                                {
                                    object[] resName = getDisplayName.Call(proto);
                                    if (resName != null && resName.Length > 0 && resName[0] is string strName)
                                    {
                                        dispName = strName;
                                    }
                                }

                                string descText = string.Empty;
                                if (getDescription != null)
                                {
                                    object[] resDesc = getDescription.Call(proto);
                                    if (resDesc != null && resDesc.Length > 0 && resDesc[0] is string strDesc)
                                    {
                                        descText = strDesc;
                                    }
                                }

                                subTitle = dispName;
                                subDescription = descText;
                            }
                        }
                        break;
                }

                if (!string.IsNullOrEmpty(subTitle))
                {
                    // 서브 툴팁 본문에 포함된 키워드도 렌더링 치환 적용
                    string finalSubTitle = ReplaceKeywordsForDisplay(subTitle);
                    string finalSubDescription = ReplaceKeywordsForDisplay(subDescription);
                    tooltips.Add(new TooltipData(finalSubTitle, finalSubDescription));
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
        if (panelTemplatePrefab == null) return null;

        if (panelPool.Count > 0)
        {
            TooltipPanel panel = panelPool[panelPool.Count - 1];
            panelPool.RemoveAt(panelPool.Count - 1);
            return panel;
        }

        TooltipPanel inst = Instantiate(panelTemplatePrefab, transform, false);
        return inst;
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
            currentTargetRect.GetWorldCorners(cornersCache);
            
            // cornersCache: 0=좌하, 1=좌상, 2=우상, 3=우하
            float minX = cornersCache[0].x;
            float maxX = cornersCache[2].x;
            float minY = cornersCache[0].y;
            float maxY = cornersCache[1].y;

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
