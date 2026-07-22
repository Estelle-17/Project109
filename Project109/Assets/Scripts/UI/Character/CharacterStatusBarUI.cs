using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusBarUI : MonoBehaviour
{
    [Header("Target & Position Settings")]
    [SerializeField] private Character targetCharacter;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, -2.2f, 0f);
    [SerializeField] private bool useBillboard = true;

    [Header("Orthographic Camera Settings")]
    [SerializeField] private bool matchOrthographicScale = true;
    [SerializeField] private float referenceOrthographicSize = 5.0f;
    [SerializeField] private Vector3 baseLocalScale = Vector3.one;

    [Header("Health Bar (Red / Blue when Shielded)")]
    [SerializeField] private GaugeUI healthGaugeUI;
    [SerializeField] private Image healthFillImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Color healthColor = new Color(0.9f, 0.2f, 0.2f, 1.0f);
    [SerializeField] private Color shieldColor = new Color(0.2f, 0.6f, 1.0f, 1.0f);

    [Header("Stamina Bar (Green)")]
    [SerializeField] private GaugeUI staminaGaugeUI;
    [SerializeField] private Image staminaFillImage;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private Color staminaColor = new Color(0.2f, 0.85f, 0.3f, 1.0f);

    [Header("Buff/Debuff Container")]
    [SerializeField] private Transform effectContainer;
    [SerializeField] private CharacterEffectItemUI effectItemPrefab;

    private readonly List<CharacterEffectItemUI> activeEffectItems = new List<CharacterEffectItemUI>();
    private Camera targetCamera;
    private RectTransform rectTransform;
    private Canvas parentCanvas;

    public Character TargetCharacter => targetCharacter;

    private void Awake()
    {
        targetCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
        baseLocalScale = transform.localScale != Vector3.zero ? transform.localScale : Vector3.one;

        if (rectTransform != null)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        ApplyGaugeColors();
    }

    private void ApplyGaugeColors()
    {
        if (healthFillImage != null) healthFillImage.color = healthColor;
        if (staminaFillImage != null) staminaFillImage.color = staminaColor;
    }

    /// <summary>
    /// 대상 캐릭터와 StatusBar UI를 연결하고 이벤트를 바인딩합니다.
    /// </summary>
    public void Bind(Character character)
    {
        if (targetCharacter != null)
        {
            UnbindEvents();
        }

        targetCharacter = character;
        if (targetCharacter == null) return;

        BindEvents();
        RefreshAll();
    }

    private void BindEvents()
    {
        if (targetCharacter == null) return;

        targetCharacter.OnCharacterHealthChanged += OnHealthChanged;
        targetCharacter.OnCharacterStaminaChanged += OnStaminaChanged;
        targetCharacter.OnCharacterShieldChanged += OnShieldChanged;
        targetCharacter.OnCharacterDied += OnCharacterDied;

        if (targetCharacter.effectManager != null)
        {
            targetCharacter.effectManager.OnEffectAdded += OnEffectAdded;
            targetCharacter.effectManager.OnEffectStacked += OnEffectStacked;
            targetCharacter.effectManager.OnEffectRemoved += OnEffectRemoved;
        }
    }

    private void UnbindEvents()
    {
        if (targetCharacter == null) return;

        targetCharacter.OnCharacterHealthChanged -= OnHealthChanged;
        targetCharacter.OnCharacterStaminaChanged -= OnStaminaChanged;
        targetCharacter.OnCharacterShieldChanged -= OnShieldChanged;
        targetCharacter.OnCharacterDied -= OnCharacterDied;

        if (targetCharacter.effectManager != null)
        {
            targetCharacter.effectManager.OnEffectAdded -= OnEffectAdded;
            targetCharacter.effectManager.OnEffectStacked -= OnEffectStacked;
            targetCharacter.effectManager.OnEffectRemoved -= OnEffectRemoved;
        }
    }

    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void LateUpdate()
    {
        UpdatePosition();
        UpdateEffectDurations();
    }

    /// <summary>
    /// Canvas 모드(ScreenSpace-Overlay, ScreenSpace-Camera, WorldSpace)를 감지하여 캐릭터 머리 위 위치 추적
    /// </summary>
    private void UpdatePosition()
    {
        if (targetCharacter == null) return;

        if (targetCamera == null) targetCamera = Camera.main;
        if (targetCamera == null) return;

        if (parentCanvas == null) parentCanvas = GetComponentInParent<Canvas>();

        Vector3 targetWorldPos = targetCharacter.transform.position + worldOffset;

        if (parentCanvas != null && parentCanvas.renderMode != RenderMode.WorldSpace)
        {
            Vector3 screenPos = targetCamera.WorldToScreenPoint(targetWorldPos);

            if (screenPos.z < 0f)
            {
                if (rectTransform != null) rectTransform.anchoredPosition = new Vector2(-9999f, -9999f);
                return;
            }

            RectTransform parentRect = transform.parent as RectTransform;
            if (parentRect != null && rectTransform != null)
            {
                Camera uiCamera = (parentCanvas.renderMode == RenderMode.ScreenSpaceCamera) ? targetCamera : null;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenPos, uiCamera, out Vector2 localPoint))
                {
                    rectTransform.anchoredPosition = localPoint;
                }
            }
            else if (rectTransform != null)
            {
                rectTransform.position = screenPos;
            }
        }
        else
        {
            transform.position = targetWorldPos;

            if (useBillboard)
            {
                transform.rotation = targetCamera.transform.rotation;
            }

            if (targetCamera.orthographic && matchOrthographicScale && referenceOrthographicSize > 0f)
            {
                float scaleMultiplier = targetCamera.orthographicSize / referenceOrthographicSize;
                transform.localScale = baseLocalScale * scaleMultiplier;
            }
        }
    }

    /// <summary>
    /// 전체 상태(체력, 스태미너, 방어도, 버프 목록)를 수동 갱신합니다.
    /// </summary>
    public void RefreshAll()
    {
        if (targetCharacter == null) return;

        OnHealthChanged(targetCharacter);
        OnStaminaChanged(targetCharacter);
        OnShieldChanged(targetCharacter);
        RebuildEffectItems();
    }

    private void OnHealthChanged(Character c)
    {
        if (c == null) return;

        if (c.curCharacterStat != null)
        {
            float rate = Mathf.Clamp01(c.curHealthRate);
            if (healthGaugeUI != null) healthGaugeUI.Refresh(rate);
            else if (healthFillImage != null) healthFillImage.fillAmount = rate;
        }

        UpdateHealthBarColor(c);
        UpdateHealthText(c);
    }

    private void OnStaminaChanged(Character c)
    {
        if (c == null || c.curCharacterStat == null) return;
        float rate = Mathf.Clamp01(c.curStaminaRate);
        if (staminaGaugeUI != null) staminaGaugeUI.Refresh(rate);
        else if (staminaFillImage != null) staminaFillImage.fillAmount = rate;

        if (staminaText != null)
        {
            staminaText.text = $"{Mathf.CeilToInt(c.curStamina)} / {Mathf.CeilToInt(c.curCharacterStat.maxStamina)}";
        }
    }

    private void OnShieldChanged(Character c)
    {
        if (c == null) return;

        UpdateHealthBarColor(c);
        UpdateHealthText(c);
    }

    /// <summary>
    /// 방어막 보유 여부에 따라 체력 바 게이지 색상을 전환합니다 (방어막 소유: 파란색, 기본: 빨간색).
    /// </summary>
    private void UpdateHealthBarColor(Character c)
    {
        if (c == null || healthFillImage == null) return;
        healthFillImage.color = (c.shield > 0f) ? shieldColor : healthColor;
    }

    /// <summary>
    /// 체력 및 방어막 수치를 "(방어막) 현재 체력 / 최대 체력" 포맷으로 통합 업데이트합니다.
    /// </summary>
    private void UpdateHealthText(Character c)
    {
        if (c == null) return;

        int curHp = Mathf.CeilToInt(c.curHealth);
        int maxHp = (c.curCharacterStat != null) ? Mathf.CeilToInt(c.curCharacterStat.maxHealth) : 0;
        int shieldVal = Mathf.CeilToInt(c.shield);

        string formattedText;
        if (c.shield > 0f)
        {
            formattedText = $"({shieldVal}) {curHp} / {maxHp}";
        }
        else
        {
            formattedText = $"{curHp} / {maxHp}";
        }

        if (healthText != null)
        {
            healthText.text = formattedText;
        }
    }

    private void OnCharacterDied(Character c)
    {
        gameObject.SetActive(false);
    }

    #region Effect Event Handlers & Item Management

    private void RebuildEffectItems()
    {
        ClearEffectItems();
        if (targetCharacter == null || targetCharacter.effectManager == null) return;

        var effects = targetCharacter.effectManager.GetEffects();
        if (effects == null) return;

        foreach (var effect in effects)
        {
            AddEffectItem(effect);
        }
    }

    private void AddEffectItem(Effect effect)
    {
        if (effect == null || effectContainer == null) return;

        CharacterEffectItemUI itemUI = null;
        GameObject prefabToInstantiate = null;

        if (effectItemPrefab != null)
        {
            prefabToInstantiate = effectItemPrefab.gameObject;
        }
        else if (AssetCacheManager.instance != null && AssetCacheManager.instance.TryGetUI("CharacterEffectItemUI", out GameObject cachedPrefab))
        {
            prefabToInstantiate = cachedPrefab;
        }

        if (prefabToInstantiate != null)
        {
            GameObject uiGo = Instantiate(prefabToInstantiate, effectContainer);
            itemUI = uiGo.GetComponent<CharacterEffectItemUI>();
            if (itemUI == null)
            {
                itemUI = uiGo.AddComponent<CharacterEffectItemUI>();
            }
        }
        else
        {
            GameObject go = new GameObject($"EffectItem_{effect.Data?.effectName}");
            go.transform.SetParent(effectContainer, false);
            itemUI = go.AddComponent<CharacterEffectItemUI>();
        }

        itemUI.Setup(effect);
        activeEffectItems.Add(itemUI);
    }

    private void OnEffectAdded(Effect effect)
    {
        AddEffectItem(effect);
    }

    private void OnEffectStacked(Effect effect)
    {
        var item = activeEffectItems.Find(x => x != null && x.TargetEffect == effect);
        if (item != null)
        {
            item.Refresh();
        }
        else
        {
            AddEffectItem(effect);
        }
    }

    private void OnEffectRemoved(Effect effect)
    {
        var item = activeEffectItems.Find(x => x != null && x.TargetEffect == effect);
        if (item != null)
        {
            activeEffectItems.Remove(item);
            Destroy(item.gameObject);
        }
    }

    private void UpdateEffectDurations()
    {
        for (int i = 0; i < activeEffectItems.Count; i++)
        {
            if (activeEffectItems[i] != null)
            {
                activeEffectItems[i].Refresh();
            }
        }
    }

    private void ClearEffectItems()
    {
        for (int i = activeEffectItems.Count - 1; i >= 0; i--)
        {
            if (activeEffectItems[i] != null)
            {
                Destroy(activeEffectItems[i].gameObject);
            }
        }
        activeEffectItems.Clear();
    }

    #endregion
}
