using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RelicUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RelicData relicData;
    public Relic relicInstance;

    public TextMeshProUGUI relicName;

    public Image relicImage;

    public UnityEvent OnRelicClick; //클릭 시 호출될 이벤트

    public void UpdateRelicData(RelicData newRelicData)
    {
        relicData = newRelicData;
        relicInstance = null;

        if (relicData != null && relicData.iconSprite != null && relicImage != null)
        {
            relicImage.sprite = relicData.iconSprite;
        }

        if (relicData != null && relicName != null)
        {
            relicName.text = relicData.relicName;
        }
    }

    public void UpdateRelicData(Relic instance)
    {
        relicInstance = instance;
        if (instance != null)
        {
            relicData = instance.Data;
        }

        if (relicData != null && relicData.iconSprite != null && relicImage != null)
        {
            relicImage.sprite = relicData.iconSprite;
        }

        if (relicData != null && relicName != null)
        {
            relicName.text = relicData.relicName;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRelicClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (relicData == null) return;

        string descriptionText = relicData.description;
        if (relicInstance != null)
        {
            descriptionText = relicInstance.GetDescription();
        }

        if (TooltipPanel.Instance != null)
        {
            TooltipPanel.Instance.ShowTooltip(relicData.relicName, descriptionText);
        }
        else if (UIManager.instance != null && UIManager.instance.relicDescription != null)
        {
            UIManager.instance.UpdateRelicDescription(relicData.relicName + "\n" + descriptionText);
            UIManager.instance.OnRelicDescription();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipPanel.Instance != null)
        {
            TooltipPanel.Instance.HideTooltip();
        }
        else if (UIManager.instance != null && UIManager.instance.relicDescription != null)
        {
            UIManager.instance.OffRelicDescription();
        }
    }

    private void OnDisable()
    {
        if (TooltipPanel.Instance != null)
        {
            TooltipPanel.Instance.HideTooltip();
        }
    }
}
