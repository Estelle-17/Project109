using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance { get; private set; }

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

    public Image inGame_Currency_Image;

    public TextMeshProUGUI inGame_Currency_Text;

    public GameObject relicUIPrefab;
    public Transform relicSpawnTransform;
    private Dictionary<string, GameObject> relicUIObjects = new Dictionary<string, GameObject>();

    void Start()
    {
        //메인UI의 Image 등 다양한 설정

        if (RelicManager.instance != null)
        {
            RelicManager.instance.OnRelicAdded += UpdateRelicItems;
            RelicManager.instance.OnRelicRemoved += RemoveRelicItem;

            Debug.LogWarning("GameUI is Done!");
        }
        else
        {
            Debug.LogWarning("RelicManager.instance is null!");
        }
    }

    public void UpdateInGameCurrencyText(string newText)
    {
        inGame_Currency_Text.text = newText;
    }

    private void UpdateRelicItems(RelicData newRelicData)
    {
        //유물UI생성 및 동기화
        if(relicSpawnTransform && relicUIPrefab)
        {
            if (relicUIObjects.ContainsKey(newRelicData.relicName))
                return;

            RelicHandler relic = Instantiate(relicUIPrefab, relicSpawnTransform).GetComponent<RelicHandler>();
            relicUIObjects.Add(newRelicData.relicName, relic.gameObject);

            if (relic)
            {
                relic.UpdateRelicData(newRelicData);
            }
        }
    }

    private void RemoveRelicItem(string relicName)
    {
        if (relicUIObjects.TryGetValue(relicName, out GameObject relicUIObject))
        {
            Destroy(relicUIObject);
            relicUIObjects.Remove(relicName);
        }
    }
}
