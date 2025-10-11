using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BattleData", menuName = "Battle/BattleData")]
public class BattleData : ScriptableObject, IIdentifiable
{
    public string battleDataName;
    public string dataPath;
    public int battleAppearLevel;
    public List<MonsterSpawnInfo> monsterAppearInformation;

    public string ID => dataPath;
}
