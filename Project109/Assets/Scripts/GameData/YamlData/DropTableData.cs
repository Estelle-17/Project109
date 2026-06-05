using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropTableData", menuName = "Reward/DropTableData")]
public class DropTableData : ScriptableObject, IIdentifiable
{
    public string dropTableID;                 // 드롭 테이블 고유 식별자

    [Header("등급별 등장 가중치")]
    public int commonWeight = 0;
    public int uncommonWeight = 0;
    public int rareWeight = 0;
    public int uniqueWeight = 0;

    [Header("고정/특정 아이템 풀 (지정 시 이 리스트 내에서만 선택)")]
    public List<string> specificItemIDs = new List<string>();

    public string ID => dropTableID;
}
