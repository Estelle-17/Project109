using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MonsterRewardData", menuName = "Reward/MonsterRewardData")]
public class MonsterRewardData : ScriptableObject, IIdentifiable
{
    public string monsterName;
    public int minDropGoldAmount;
    public int maxDropGoldAmount;

    public string ID => monsterName;
}
