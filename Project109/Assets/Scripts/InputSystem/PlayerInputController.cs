using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInputAction playerInputController;
    private void Awake()
    {
        playerInputController = new PlayerInputAction();
    }

    public void OnEnable()
    {
        playerInputController.Enable();
    }
    public void OnDisable()
    {
        playerInputController.Disable();
    }
}
