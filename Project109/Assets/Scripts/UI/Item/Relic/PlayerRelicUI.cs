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

    public void AddRelicUI(RelicData newRelicData)
    {
        if (relicSpawnTransform && relicUIPrefab)
        {
            RelicHandler relic = Instantiate(relicUIPrefab, relicSpawnTransform).GetComponent<RelicHandler>();
            relicUIObjects.Add(newRelicData.relicName, relic.gameObject);

            if (relic)
            {
                relic.UpdateRelicData(newRelicData);
            }
        }
    }

    public void RemoveRelicUI(RelicData relicData)
    {
        if (relicUIObjects.TryGetValue(relicData.relicName, out GameObject relicUIObject))
        {
            Destroy(relicUIObject);
            relicUIObjects.Remove(relicData.relicName);
        }
    }
}
