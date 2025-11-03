using UnityEngine;

public class UIPanelBase : MonoBehaviour
{
    protected virtual void OnDisable()
    {
        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.RemoveActiveUIFromStack(this.gameObject);
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

        UIManager.instance.PushActiveUIPanel(this.gameObject);
    }

    public void UIDeactive()
    {
        if (UIManager.instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.RemoveActiveUIFromStack(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
