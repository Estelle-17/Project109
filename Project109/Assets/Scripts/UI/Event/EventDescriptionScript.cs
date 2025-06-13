using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDescriptionScript : MonoBehaviour
{
    public TextMeshProUGUI description;

    public GameObject eventButtonSpawnPoint;
    public GameObject eventButtonPrefab;

    void Start()
    {
        
    }

    public void SetDescription(string newDescription)
    {
        description.text = newDescription;
    }

    public Button CreateChoiceButton(string buttonDescription)
    {
        GameObject button = GameObject.Instantiate(eventButtonPrefab, eventButtonSpawnPoint.transform);

        if(button != null)
        {
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonDescription;
        }

        return button.GetComponent<Button>();
    }
}
