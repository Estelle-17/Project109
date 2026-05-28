using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject, IIdentifiable
{
    public GameObject characterObject;
    public string classType;
    public string characterName;
    public string assetPath;
    public int level;
    public string description;
    public CharacterStat characterStat;
    public List<string> startRelic;
    // 시작 덱 카드 목록. cardName을 중복 기재하여 장수를 표현합니다.
    // 예: ["검격", "검격", "방어"] → 검격 2장 + 방어 1장
    public List<string> startCardIds;

    public string ID => characterName;
}
