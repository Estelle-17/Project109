using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableData", menuName = "Dialogue/InteractableData")]
public class InteractableData : ScriptableObject, IIdentifiable
{
    public string interactableID;             // 상호작용 오브젝트 고유 식별자
    public string interactableName;           // 오브젝트의 표시 명칭
    public string modelPrefabPath;            // Addressables 소환 모델 경로
    public int eventAppearLevel;              // 이 이벤트가 등장할 수 있는 스테이지 층/레벨 제한
    
    [Header("상호작용 타겟 분기")]
    public string targetDialogueID;           // 대화 NPC일 때 로드할 다이얼로그 ID

    public string ID => interactableID;
}
