using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BattleNodeData", menuName = "BattleNode/BattleNodeData")]
public class BattleNodeData : ScriptableObject, IIdentifiable
{
    public string battleNodeName;
    public int battleAppearLevel;
    public List<MonsterSpawnInfo> monsterAppearInformation;

    public string ID => battleNodeName;
}
