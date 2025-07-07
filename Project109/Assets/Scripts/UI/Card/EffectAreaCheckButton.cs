using UnityEngine;
using UnityEngine.EventSystems;

public class EffectAreaCheckButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject effectAreaCheckObject;

    void Start()
    {
        effectAreaCheckObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        effectAreaCheckObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        effectAreaCheckObject.SetActive(false);
    }
}
