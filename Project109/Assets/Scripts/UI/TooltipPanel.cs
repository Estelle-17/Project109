using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class TooltipPanel : MonoBehaviour
{
    private static TooltipPanel instance;
    public static TooltipPanel Instance
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

    private RectTransform rectTransform;
    private TextMeshProUGUI tooltipText;

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
        tooltipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private static void CreateInstance()
    {
        if (AssetCacheManager.instance == null || UIManager.instance == null)
        {
            Debug.LogWarning("[TooltipPanel] UIManager or AssetCacheManager is not initialized yet.");
            return;
        }

        if (AssetCacheManager.instance.TryGetUI("RelicDescription", out GameObject prefab))
        {
            Transform parentTransform = UIManager.instance.popupUILayer != null ? UIManager.instance.popupUILayer : UIManager.instance.transform;
            GameObject inst = Instantiate(prefab, parentTransform, false);
            inst.name = "RelicDescription";

            instance = inst.GetComponent<TooltipPanel>();
            if (instance == null)
            {
                instance = inst.AddComponent<TooltipPanel>();
            }
            instance.InitializeComponents();
            inst.SetActive(false);
            Debug.Log("[TooltipPanel] RelicDescription UI dynamically initialized and TooltipPanel component attached.");
        }
        else
        {
            Debug.LogError("[TooltipPanel] Failed to find RelicDescription prefab in AssetCacheManager.");
        }
    }

    public void ShowTooltip(string title, string content)
    {
        if (tooltipText != null)
        {
            tooltipText.text = title + "\n" + content;
        }
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf && rectTransform != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            mousePos += new Vector2(50, 50);    // offset
            rectTransform.position = mousePos;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
