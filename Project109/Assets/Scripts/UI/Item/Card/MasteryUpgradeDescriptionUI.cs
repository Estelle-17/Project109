using TMPro;
using UnityEngine;

public class MasteryUpgradeCheckDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetDescriptionText(string newDescription)
    {
        descriptionText.SetText(newDescription);
    }
}
