using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private void Start()
    {
        if (PlayerInputController.instance != null)
        {
            PlayerInputController.instance.OnTouchClickEvent += HandleTouchClick;
        }
    }

    private void HandleTouchClick(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction * 10000.0f, Color.red, 1.0f);

        int layerMask = LayerMask.GetMask("Player", "Enemy", "NPC", "Map");

        if (Physics.Raycast(ray, out RaycastHit hit, 10000.0f, layerMask))
        {
            Debug.Log($"Detect {hit.collider.transform.root.tag}");

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable == null)
            {
                interactable = hit.collider.transform.root.GetComponent<IInteractable>();
            }

            if (interactable != null)
            {
                if (interactable.RequiresCameraFocus && CameraController.instance != null)
                {
                    // 패턴 매칭을 사용하여 안전하게 캐스팅 및 null 체크를 동시에 수행
                    if (interactable is Component comp)
                    {
                        CameraController.instance.CameraFocusToTarget(comp.transform.position);
                    }
                }
                interactable.OnInteract();
            }
        }
    }

    public void OnEnable()
    {
        if (PlayerInputController.instance != null)
        {
            PlayerInputController.instance.OnTouchClickEvent -= HandleTouchClick;
            PlayerInputController.instance.OnTouchClickEvent += HandleTouchClick;
        }
    }

    public void OnDisable()
    {
        if (PlayerInputController.instance != null)
        {
            PlayerInputController.instance.OnTouchClickEvent -= HandleTouchClick;
        }
    }
}
