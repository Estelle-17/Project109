using UnityEngine;
using UnityEngine.EventSystems;

public class EffectAreaCheckButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.instance == null)
            return;

        //카드의 중앙 위치값을 줌으로써 적용범위UI의 위치 설정
        UIManager.instance.OnCardEffectAreaBackground(transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIManager.instance == null)
            return;

        UIManager.instance.OffCardEffectAreaBackground();
    }
}
