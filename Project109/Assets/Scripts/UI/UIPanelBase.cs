using UnityEngine;

public class UIPanelBase : MonoBehaviour
{
    [Header("UI Layer Type Settings")]
    public UILayerType uiLayerType = UILayerType.Normal;

    [Header("Input Block Settings")]
    public bool blockWorldInput = true;

    protected virtual void OnEnable()
    {
        if (blockWorldInput && UIInputManager.instance != null)
        {
            UIInputManager.instance.AcquireUILock();
        }

        transform.SetAsLastSibling();
    }

    protected virtual void OnDisable()
    {
        if (blockWorldInput && UIInputManager.instance != null)
        {
            UIInputManager.instance.ReleaseUILock();
        }

        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.RemoveActiveUIFromStack(this.gameObject, uiLayerType);
    }

    public void UIActive()
    {
        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        if(gameObject.activeSelf)
        {
            return;
        }

        UIManager.instance.PushActiveUIPanel(this.gameObject, uiLayerType);
    }

    public void UIDeactive()
    {
        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.RemoveActiveUIFromStack(this.gameObject, uiLayerType);
        this.gameObject.SetActive(false);
    }

    public void TempDeactivateCurrentUI()
    {
        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.TempDeactivateCurrentActiveUIPanel();
    }

    public void ReActivateTempUI()
    {
        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.ReactivateTempDeactiveUIPanel();
    }
}
