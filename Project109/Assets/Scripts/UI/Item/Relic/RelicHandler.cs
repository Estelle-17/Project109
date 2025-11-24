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

    public Image relicImage;

    public UnityEvent OnRelicClick; //클릭 시 호출될 이벤트

    public void UpdateRelicData(RelicData newRelicData)
    {
        relicData = newRelicData;

        if (AssetCacheManager.instance.TryGetTexture(relicData.texturePath, out Sprite texture))
        {
            relicImage.sprite = texture;
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

        UIManager.instance.UpdateRelicDescription(relicData.relicName + "\n" + relicData.description);
        UIManager.instance.OnRelicDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       if (UIManager.instance.relicDescription == null || relicData == null)
            return;

        UIManager.instance.OffRelicDescription();
    }
}
