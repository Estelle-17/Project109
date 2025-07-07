using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public RectTransform canvasRect;

    public GameObject relicDescription;
    public TextMeshProUGUI relicDescriptionText;

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();

        if(relicDescription != null)
        {
            OffRelicDescription();
        }
    }

    public void UpdateRelicDescription(string newDescription)
    {
        relicDescriptionText.text = newDescription;
    }

    public void OnRelicDescription(Vector3 newItemPos)
    {
        relicDescription.transform.GetComponent<RectTransform>().position = newItemPos + new Vector3(75, -50, 0);
        relicDescription.SetActive(true);
    }

    public void OffRelicDescription()
    {
        relicDescription.SetActive(false);
    }
}
