using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopHUDPanel : UIPanelBase
{
    [Header("Relic UI")]
    [SerializeField] private PlayerRelicUI playerRelicUI;

    public PlayerRelicUI PlayerRelicUI => playerRelicUI;

    private void Awake()
    {
        blockWorldInput = false;

        if (playerRelicUI == null)
        {
            Transform spawnTransform = transform.Find("HUDPanel/Relic/SpawnTransform");
            if (spawnTransform != null)
            {
                playerRelicUI = spawnTransform.gameObject.GetComponent<PlayerRelicUI>();
                if (playerRelicUI == null)
                {
                    playerRelicUI = spawnTransform.gameObject.AddComponent<PlayerRelicUI>();
                }
            }
            else
            {
                Debug.LogWarning("[TopHUDPanel] Relic/SpawnTransform not found in hierarchy!");
            }
        }
    }

    [Header("Currency TMP Texts")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI specialResourceText;

    [Header("Action Buttons")]
    [SerializeField] private Button mapButton;
    [SerializeField] private Button deckButton;

    [Header("UI Texture")]
    [SerializeField] private Image currencyImage;
    [SerializeField] private Image specialResourceImage;
    [SerializeField] private Image mapButtonImage;
    [SerializeField] private Image deckButtonImage;

    public void SetupHUD(Action onMapClicked, Action onDeckClicked)
    {
        if (mapButton != null)
        {
            mapButton.onClick.RemoveAllListeners();
            mapButton.onClick.AddListener(() => onMapClicked?.Invoke());
        }
        else
        {
            Debug.LogWarning("Map button is not assigned");
        }

        if (deckButton != null)
        {
            deckButton.onClick.RemoveAllListeners();
            deckButton.onClick.AddListener(() => onDeckClicked?.Invoke());
        }
        else
        {
            Debug.LogWarning("Deck button is not assigned");
        }

        if (AssetCacheManager.instance != null)
            UpdateUIImage();
    }

    public void UpdateGold(int amount)
    {
        if (goldText != null)
        {
            goldText.text = amount.ToString();
        }
    }

    public void UpdateSpecialResource(int amount)
    {
        if (specialResourceText != null)
        {
            specialResourceText.text = amount.ToString();
        }
    }

    private void UpdateUIImage()
    {
        if (AssetCacheManager.instance.TryGetTexture("Gold", out Sprite goldTexture))
        {
            currencyImage.sprite = goldTexture;
        }
        else
        {
            Debug.LogWarning("Failed to load Texture_UI_Gold");
        }

        if (AssetCacheManager.instance.TryGetTexture("MemoryShard", out Sprite shardTexture))
        {
            specialResourceImage.sprite = shardTexture;
        }
        else
        {
            Debug.LogWarning("Failed to load Texture_UI_MemoryShard");
        }

        if (AssetCacheManager.instance.TryGetTexture("MapButtonIcon", out Sprite mapButtonIconTexture))
        {
            mapButtonImage.sprite = mapButtonIconTexture;
        }
        else
        {
            Debug.LogWarning("Failed to load Texture_UI_MapButtonIcon");
        }

        if (AssetCacheManager.instance.TryGetTexture("DeckButtonIcon", out Sprite deckButtonIconTexture))
        {
            deckButtonImage.sprite = deckButtonIconTexture;
        }
        else
        {
            Debug.LogWarning("Failed to load Texture_UI_DeckButtonIcon");
        }
    }
}
