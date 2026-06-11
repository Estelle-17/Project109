using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GaugeUI : MonoBehaviour
{
    [SerializeField] 
    private Image gaugeImageFilled;
    private float _lastFillAmount = -1f;
    private const float UpdateThreshold = 0.01f;

    private void Awake()
    {
        if (gaugeImageFilled == null)
        {
            gaugeImageFilled = GetComponent<Image>();
        }
    }

    public void Refresh(float fillRate) 
    {
        if (Mathf.Abs(fillRate - _lastFillAmount) >= UpdateThreshold || fillRate == 0f || fillRate == 1f)
        {
            _lastFillAmount = fillRate;
            if (gaugeImageFilled != null)
            {
                gaugeImageFilled.fillAmount = fillRate;
            }
        }
    }
}
