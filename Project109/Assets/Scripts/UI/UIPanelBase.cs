using UnityEngine;

public class UIPanelBase : MonoBehaviour
{
    protected virtual void OnDisable()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogWarning("UIManager is null. Check UIManager setting in hierarchy.");
            return;
        }

        UIManager.instance.RemoveActiveUIFromStack(this.gameObject);
    }

    public void UIActive()
    {
        UIManager.instance.PushActiveUIPanel(this.gameObject);

    }
}
