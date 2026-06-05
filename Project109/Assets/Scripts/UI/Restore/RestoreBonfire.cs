using UnityEngine;

namespace RestoreUI
{
    // We can define RestoreBonfire in the global namespace or namespace matching files.
    // Let's use global namespace just like other NPCs, or match original context.
}

public class RestoreBonfire : InteractableObject
{
    public GameObject restoreUIPrefab;
    private RestoreUIHandler restoreUI;

    public override void OnInteract()
    {
        if (interactableData != null && !string.IsNullOrEmpty(interactableData.targetDialogueID))
        {
            TriggerDialogue();
        }
        else
        {
            OpenRestoreDirectly();
        }
    }

    public void CreateRestoreUI()
    {
        if (restoreUIPrefab == null || restoreUI != null)
        {
            return;
        }

        GameObject spawned = Instantiate(restoreUIPrefab);
        if (spawned.transform.childCount > 0)
        {
            restoreUI = spawned.transform.GetChild(0).GetComponent<RestoreUIHandler>();
        }
        else
        {
            restoreUI = spawned.GetComponent<RestoreUIHandler>();
        }

        if (restoreUI != null)
        {
            restoreUI.gameObject.SetActive(false);
        }
    }

    public RestoreUIHandler GetRestoreUI()
    {
        return restoreUI;
    }

    public void OpenRestoreDirectly()
    {
        if (restoreUI == null)
        {
            CreateRestoreUI();
        }

        if (restoreUI != null)
        {
            restoreUI.UIActive();

            if (RunManager.instance != null && RunManager.instance.currentMap != null)
            {
                if (!RunManager.instance.currentMap.currentSpawnUIList.Contains(restoreUI.gameObject))
                {
                    RunManager.instance.currentMap.currentSpawnUIList.Add(restoreUI.gameObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("[RestoreBonfire] RestoreUI를 생성할 수 없습니다.");
        }
    }

    public void CloseRestoreUI()
    {
        if (restoreUI != null)
        {
            restoreUI.UIDeactive();
        }
    }
}
