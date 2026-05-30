using UnityEngine;

public class OpenUIWhenClicked : MonoBehaviour
{
    [SerializeField] private GameObject specificUIObject;
    [SerializeField] private MapState requiredMapState = MapState.None;

    public void ActiveObject()
    {
        if (specificUIObject != null && MapManager.instance.currentMapState == requiredMapState)
        {
            UIPanelBase uiPanelBase = specificUIObject.GetComponent<UIPanelBase>();
            if (uiPanelBase != null)
            {
                uiPanelBase.UIActive();
            }
            else
            {
                Debug.LogWarning("The specified UI object does not have a UIPanelBase component.");
            }
        }
        else
        {
            Debug.LogWarning("Specific UI Object is not assigned or currentMapState not None");
        }
    }
}
