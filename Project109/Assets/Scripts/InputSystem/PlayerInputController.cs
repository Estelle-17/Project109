using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController instance { get; private set; }

    public PlayerInputAction playerInputAction;

    // 이벤트 정의
    public event Action<Vector2> OnTouchStartEvent;
    public event Action<Vector2> OnTouchDragEvent;
    public event Action<Vector2> OnTouchClickEvent;

    private Vector2 startTouchPos;
    private Vector2 lastTouchPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        playerInputAction = new PlayerInputAction();
    }

    private void Start()
    {
        if (playerInputAction != null)
        {
            playerInputAction.Player.Touch.started += OnTouchStart;
            playerInputAction.Player.Touch.performed += OnDrag;
            playerInputAction.Player.Touch.canceled += OnTouchClick;
        }
    }

    public void OnEnable()
    {
        playerInputAction?.Enable();
    }

    public void OnDisable()
    {
        playerInputAction?.Disable();
    }

    public void EnableObjectInteractionInput()
    {
        playerInputAction?.Enable();
    }

    public void DisableObjectInteractionInput()
    {
        playerInputAction?.Disable();
    }

    private void OnTouchStart(InputAction.CallbackContext context)
    {
        startTouchPos = context.ReadValue<Vector2>();
        OnTouchStartEvent?.Invoke(startTouchPos);
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
        lastTouchPos = context.ReadValue<Vector2>();
        OnTouchDragEvent?.Invoke(lastTouchPos);
    }

    private void OnTouchClick(InputAction.CallbackContext context)
    {
        // 클릭과 드래그 판정 로직
        if (Vector2.Distance(startTouchPos, lastTouchPos) <= 20.0f)
        {
            OnTouchClickEvent?.Invoke(lastTouchPos);
        }
    }
}
