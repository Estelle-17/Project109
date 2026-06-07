using UnityEngine;

public class RestoreUIManager : MonoBehaviour, IInteractable
{
    public GameObject restoreUIPrefab;
    public RestoreUIHandler restoreUI;
    
    [SerializeField] private string modelName = "NPC_Restore_Bonfire_Model";
    
    void Start()
    {
        InstantiateModel();
    }

    private void InstantiateModel()
    {
        if (string.IsNullOrEmpty(modelName)) return;

        if (AssetCacheManager.instance != null && AssetCacheManager.instance.TryGetModel(modelName, out GameObject modelPrefab))
        {
            var defaultRenderer = GetComponent<MeshRenderer>();
            if (defaultRenderer != null)
            {
                defaultRenderer.enabled = false;
            }

            GameObject model = Instantiate(modelPrefab, this.transform);
            if (model != null)
            {
                model.tag = this.tag;
                ChangeAllLayer(model, LayerMask.NameToLayer("NPC"));
            }
        }
    }

    private void ChangeAllLayer(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            ChangeAllLayer(child.gameObject, layer);
        }
    }

    public void CreateRestoreUI()
    {
        if (restoreUI != null)
        {
            return;
        }

        if (UIManager.instance != null)
        {
            GameObject inst = UIManager.instance.OpenUI("RestoreNPCUI", UILayerType.Normal, false);
            if (inst != null)
            {
                restoreUI = inst.GetComponent<RestoreUIHandler>();
            }
        }

        if (restoreUI != null)
        {
            restoreUI.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("[RestoreUIManager] Failed to load or instantiate RestoreNPCUI via UIManager!");
        }
    }

    public RestoreUIHandler GetRestoreUI()
    {
        return restoreUI;
    }

    public void EnableRestoreUI()
    {
        if (restoreUI != null)
        {
            restoreUI.UIActive();
        }
    }

    public void DisableRestoreUI()
    {
        if (restoreUI != null)
        {
            restoreUI.UIDeactive();
        }
    }

    public bool RequiresCameraFocus => true;

    public void OnInteract()
    {
        EnableRestoreUI();
    }
}
