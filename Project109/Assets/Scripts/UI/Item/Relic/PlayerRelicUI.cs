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

    public void AddRelicUI(Relic newRelic)
    {
        if (newRelic == null || newRelic.Data == null) return;
        
        if (relicSpawnTransform && relicUIPrefab)
        {
            RelicUI relicUI = Instantiate(relicUIPrefab, relicSpawnTransform).GetComponent<RelicUI>();
            relicUIObjects.Add(newRelic.Data.relicName, relicUI.gameObject);

            if (relicUI)
            {
                relicUI.UpdateRelicData(newRelic.Data); // Or update it to use Relic if RelicUI supports it
            }
        }
    }

    public void RemoveRelicUI(Relic relic)
    {
        if (relic == null || relic.Data == null) return;
        
        if (relicUIObjects.TryGetValue(relic.Data.relicName, out GameObject relicUIObject))
        {
            Destroy(relicUIObject);
            relicUIObjects.Remove(relic.Data.relicName);
        }
    }
}
