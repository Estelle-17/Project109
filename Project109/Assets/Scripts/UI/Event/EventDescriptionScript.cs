using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDescriptionScript : UIPanelBase
{
    public TextMeshProUGUI description;

    public Transform eventButtonSpawnPoint;
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
        GameObject button = GameObject.Instantiate(eventButtonPrefab, eventButtonSpawnPoint);

        if(button != null)
        {
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonDescription;
        }

        return button.GetComponent<Button>();
    }

    public void ClearChoiceButton()
    {
        if (eventButtonSpawnPoint.childCount > 0)
        {
            foreach (Transform t in eventButtonSpawnPoint)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
