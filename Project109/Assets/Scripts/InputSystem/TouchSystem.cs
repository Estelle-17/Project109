using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchSystem : MonoBehaviour
{
    public PlayerInputController playerInputController;

    Camera mainCamera;
    Vector3 dragOrigin;

    Vector2 startTouchPos;
    Vector2 lastTouchPos;

    private void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();

        //InputAction Section
        playerInputController = GetComponent<PlayerInputController>();
        if (playerInputController != null)
        {
            playerInputController.playerInputController.Player.Touch.started += CheckToTouchPos;
            playerInputController.playerInputController.Player.Touch.performed += DragToCameraMove;
            playerInputController.playerInputController.Player.Touch.canceled += CheckToTargetObject;

            playerInputController.OnEnable();
        }
        else
        {
            Debug.LogWarning("InputController is null!");
        }
    }

    public void CheckToTouchPos(InputAction.CallbackContext context)
    {
        startTouchPos = context.ReadValue<Vector2>();

        dragOrigin = mainCamera.ScreenToWorldPoint(startTouchPos);
    }

    public void DragToCameraMove(InputAction.CallbackContext context)
    {
        lastTouchPos = context.ReadValue<Vector2>();

        Vector3 dragDiff = dragOrigin - mainCamera.ScreenToWorldPoint(lastTouchPos);

        mainCamera.transform.position = CameraMoveInClampArea(mainCamera.transform.position + dragDiff);

        //부자연스러운 움직임을 막기 위해 마지막에 있던 마우스 위치값 업데이트
        dragOrigin = mainCamera.ScreenToWorldPoint(lastTouchPos);
    }

    public void CheckToTargetObject(InputAction.CallbackContext context)
    {
        if (Vector2.Distance(startTouchPos, lastTouchPos) > 20.0f)
            return;

        Ray ray = Camera.main.ScreenPointToRay(lastTouchPos);

        Debug.DrawRay(Camera.main.transform.localPosition, ray.direction * 100.0f, Color.blue);

        int layerMask = LayerMask.GetMask("Player", "Enemy", "ShopNPC","EventNPC");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000.0f, layerMask))
        {
            string layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            Debug.Log($"Detect {layerName}");

            CheckInfoByTargetObject(layerName, hit.collider.gameObject);
        }
    }

    //선택된 오브젝트 종류에 따른 행동 실행
    void CheckInfoByTargetObject(string newTarget, GameObject newObject)
    {
        switch (newTarget)
        {
            case "Player":

                break;
            case "Enemy":

                break;
            case "ShopNPC":
                
                break;
            case "EventNPC":
                CameraFocusToTarget(newObject.transform.position);
                EventHandler eventHandler = newObject.GetComponent<EventHandler>();
                if(eventHandler != null)
                {
                    eventHandler.EnableEventDescriptionUI();
                }
                break;
        }
    }

    void CameraFocusToTarget(Vector3 targetPos)
    {
        Vector3 worldToTarget = targetPos - mainCamera.transform.position;

        Vector3 planeOffset = mainCamera.transform.right * Vector3.Dot(worldToTarget, mainCamera.transform.right) +
                              mainCamera.transform.up * Vector3.Dot(worldToTarget, mainCamera.transform.up);

        StartCoroutine(SmoothFocus(mainCamera.transform.position + planeOffset, 0.5f));
    }

    Vector3 CameraMoveInClampArea(Vector3 targetPos)
    {
        //현재 위치 저장
        Vector3 originalPos = mainCamera.transform.position;

        //잠깐 이동해서 검사
        mainCamera.transform.position = targetPos;

        Vector2 ScreenCenter = new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2);

        Ray ray = mainCamera.ScreenPointToRay(ScreenCenter);

        Debug.DrawRay(mainCamera.transform.position, ray.direction * 1000.0f, Color.blue);

        int layerMask = LayerMask.GetMask("Map");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000.0f, layerMask))
        {
            return targetPos;
        }

        
        return originalPos; 
    }

    IEnumerator SmoothFocus(Vector3 targetPos, float duration = 0.5f)
    {
        Vector3 startPos = mainCamera.transform.position;

        float t = 0;
        while(t < 1f)
        {
            t += Time.deltaTime / duration;

            float easeOut = 1f - Mathf.Pow(1f - t, 2f);

            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, easeOut);
            yield return null;
        }

        mainCamera.transform.position = targetPos;
    }
}
