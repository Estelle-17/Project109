using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseWhenClickedScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject parentObject;
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DisableParentObject();
    }

    void DisableParentObject()
    {
        parentObject.SetActive(false);
    }
}
