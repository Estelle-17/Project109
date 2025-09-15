using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class RelicHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RelicData relicData;

    public TextMeshProUGUI relicName;

    public RawImage relicImage;

    void Start()
    {
        UpdateRelicData();
    }

    public void UpdateRelicData()
    {
        if (relicData == null)
            return;

        if (AssetCacheManager.instance.TryGetTexture(relicData.texturePath, out Texture2D texture))
        {
            relicImage.texture = texture;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.instance.relicDescription == null)
            return;

        UIManager.instance.UpdateRelicDescription(relicData.description);
        UIManager.instance.OnRelicDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       if (UIManager.instance.relicDescription == null)
            return;

        UIManager.instance.OffRelicDescription();
    }
}
