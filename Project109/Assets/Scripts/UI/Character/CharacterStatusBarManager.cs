using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusBarManager : MonoBehaviour
{
    public static CharacterStatusBarManager Instance { get; private set; }

    [Header("Prefab & Parent Layer")]
    [SerializeField] private CharacterStatusBarUI statusBarPrefab;
    [SerializeField] private Transform statusBarParent;

    private readonly Dictionary<Character, CharacterStatusBarUI> activeStatusBars = new Dictionary<Character, CharacterStatusBarUI>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitParentLayer();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitParentLayer()
    {
        if (statusBarParent == null && UIManager.instance != null && UIManager.instance.worldUILayer != null)
        {
            statusBarParent = UIManager.instance.worldUILayer.transform;
        }
    }

    private void Start()
    {
        InitParentLayer();
        if (RunManager.instance != null && RunManager.instance.currentMap != null && RunManager.instance.currentMap.currentMapState == MapState.Battle)
        {
            TryAutoRegisterExistingCharacters();
        }
    }

    /// <summary>
    /// 지정된 캐릭터에 대해 StatusBar UI를 UIManager worldUILayer 하위에 생성 및 바인딩합니다.
    /// </summary>
    public CharacterStatusBarUI RegisterCharacter(Character character)
    {
        if (character == null) return null;

        if (activeStatusBars.TryGetValue(character, out var existingUI) && existingUI != null)
        {
            existingUI.gameObject.SetActive(true);
            existingUI.RefreshAll();
            return existingUI;
        }

        Transform parentTransform = statusBarParent;
        if (parentTransform == null && UIManager.instance != null && UIManager.instance.worldUILayer != null)
        {
            parentTransform = UIManager.instance.worldUILayer.transform;
        }
        if (parentTransform == null)
        {
            parentTransform = transform;
        }

        CharacterStatusBarUI newUI = null;
        GameObject prefabToInstantiate = null;

        if (statusBarPrefab != null)
        {
            prefabToInstantiate = statusBarPrefab.gameObject;
        }
        else if (AssetCacheManager.instance != null && AssetCacheManager.instance.TryGetUI("CharacterStatusBarUI", out GameObject cachedPrefab))
        {
            prefabToInstantiate = cachedPrefab;
        }

        if (prefabToInstantiate != null)
        {
            GameObject uiGo = Instantiate(prefabToInstantiate, parentTransform);
            uiGo.name = $"CharacterStatusBar_{character.name}";
            newUI = uiGo.GetComponent<CharacterStatusBarUI>();
            if (newUI == null)
            {
                newUI = uiGo.AddComponent<CharacterStatusBarUI>();
            }
        }
        else
        {
            GameObject go = new GameObject($"CharacterStatusBar_{character.name}");
            go.transform.SetParent(parentTransform, false);
            newUI = go.AddComponent<CharacterStatusBarUI>();
        }

        newUI.Bind(character);
        activeStatusBars[character] = newUI;
        return newUI;
    }

    /// <summary>
    /// 캐릭터 등록을 해제하고 UI를 제거합니다.
    /// </summary>
    public void UnregisterCharacter(Character character)
    {
        if (character == null) return;

        if (activeStatusBars.TryGetValue(character, out var ui) && ui != null)
        {
            activeStatusBars.Remove(character);
            Destroy(ui.gameObject);
        }
    }

    /// <summary>
    /// 씬 내 존재하는 모든 캐릭터를 찾아 StatusBar를 부착합니다.
    /// </summary>
    public void TryAutoRegisterExistingCharacters()
    {
        Character[] allCharacters = FindObjectsByType<Character>(FindObjectsSortMode.None);
        foreach (var c in allCharacters)
        {
            if (c != null && !c.isDead)
            {
                RegisterCharacter(c);
            }
        }
    }

    /// <summary>
    /// 관리 중인 모든 StatusBar UI를 명시적으로 파괴하고 정리합니다. 다음 전투 시 새로 생성됩니다.
    /// </summary>
    public void ClearAll()
    {
        foreach (var kvp in activeStatusBars)
        {
            if (kvp.Value != null && kvp.Value.gameObject != null)
            {
                Destroy(kvp.Value.gameObject);
            }
        }
        activeStatusBars.Clear();

        Transform parentTransform = statusBarParent;
        if (parentTransform == null && UIManager.instance != null && UIManager.instance.worldUILayer != null)
        {
            parentTransform = UIManager.instance.worldUILayer.transform;
        }

        if (parentTransform != null)
        {
            for (int i = parentTransform.childCount - 1; i >= 0; i--)
            {
                Transform child = parentTransform.GetChild(i);
                if (child != null && child.GetComponent<CharacterStatusBarUI>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
