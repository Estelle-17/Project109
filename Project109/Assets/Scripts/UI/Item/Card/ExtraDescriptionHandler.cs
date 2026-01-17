using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ExtraDescriptionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    void Start()
    {
        
    }

    public void UpdateExtraDescription(ExtraDescription newDescription)
    {
        nameText.SetText(newDescription.name);
        descriptionText.SetText(newDescription.description);
    }
}
