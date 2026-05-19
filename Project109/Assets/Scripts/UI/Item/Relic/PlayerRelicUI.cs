using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 유물 인벤토리를 UI로 보여주는 클래스 (순수 View)
/// PlayerUIController에 의해 제어됨
/// </summary>
public class PlayerRelicUI : MonoBehaviour
{
    [Header("InGame Relic UI")]
    public GameObject relicUIPrefab;
    public Transform relicSpawnTransform;
    
    private Dictionary<string, GameObject> relicUIObjects = new Dictionary<string, GameObject>();

    public void AddRelicUI(RelicBase newRelic)
    {
        if (newRelic == null || newRelic.Data == null) return;
        
        if (relicSpawnTransform && relicUIPrefab)
        {
            RelicHandler relicHandler = Instantiate(relicUIPrefab, relicSpawnTransform).GetComponent<RelicHandler>();
            relicUIObjects.Add(newRelic.Data.relicName, relicHandler.gameObject);

            if (relicHandler)
            {
                relicHandler.UpdateRelicData(newRelic.Data); // Or update it to use RelicBase if RelicHandler supports it
            }
        }
    }

    public void RemoveRelicUI(RelicBase relic)
    {
        if (relic == null || relic.Data == null) return;
        
        if (relicUIObjects.TryGetValue(relic.Data.relicName, out GameObject relicUIObject))
        {
            Destroy(relicUIObject);
            relicUIObjects.Remove(relic.Data.relicName);
        }
    }
}
