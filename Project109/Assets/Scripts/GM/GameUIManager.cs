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
    [Header("InGame Currency UI")]
    public Image inGame_Currency_Gold_Image;
    public TextMeshProUGUI inGame_Currency_Gold_Text;
    public Image inGame_Currency_MemorySharp_Image;
    public TextMeshProUGUI inGame_Currency_MemorySharp_Text;

    [Header("InGame Relic UI")]
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

            Debug.Log("GameUI is Done!");
        }
        else
        {
            Debug.LogWarning("RelicManager.instance is null!");
        }
    }

    public void UpdateGoldText(string newText)
    {
        inGame_Currency_Gold_Text.text = newText;
    }

    public void UpdateMemorySharpText(string newText)
    {
        inGame_Currency_MemorySharp_Text.text = newText;
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
