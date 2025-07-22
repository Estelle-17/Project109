using UnityEngine;

public class RestoreUIManager : MonoBehaviour
{
    public GameObject restoreUIPrefab;
    public RestoreUIHandler restoreUI;
    
    void Start()
    {
        
    }

    public void CreateRestoreUI()
    {
        restoreUI = GameObject.Instantiate(restoreUIPrefab).transform.GetChild(0).GetComponent<RestoreUIHandler>();
        if(restoreUI != null)
        {
            restoreUI.gameObject.SetActive(false);
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
            restoreUI.gameObject.SetActive(true);
        }
    }

    public void DisableRestoreUI()
    {
        if (restoreUI != null)
        {
            restoreUI.gameObject.SetActive(false);
        }
    }
}
