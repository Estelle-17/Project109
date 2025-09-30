using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class RelicHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RelicData relicData;

    public TextMeshProUGUI relicName;

    public RawImage relicImage;

    public UnityEvent OnRelicClick; //클릭 시 호출될 이벤트

    void Start()
    {
        
    }

    public void UpdateRelicData(RelicData newRelicData)
    {
        relicData = newRelicData;

        if (AssetCacheManager.instance.TryGetTexture(relicData.texturePath, out Texture2D texture))
        {
            relicImage.texture = texture;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRelicClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.instance.relicDescription == null || relicData == null)
            return;

        UIManager.instance.UpdateRelicDescription(relicData.description);
        UIManager.instance.OnRelicDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       if (UIManager.instance.relicDescription == null || relicData == null)
            return;

        UIManager.instance.OffRelicDescription();
    }
}
