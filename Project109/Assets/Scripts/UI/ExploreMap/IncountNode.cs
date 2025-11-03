using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum IncountType
{ 
    None,       //비어있음
    Elite,      //엘리트 몬스터
    Boss,       //보스
    Store,      //상점
    Restore,    //휴식
    Battle,     //전투
    SecretBox,  //박스
    Secret,     //비밀
    Insight,    //천리안
    ShineWell   //빛나는 우물
}

public class IncountNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public IncountType incountType;
    public List<GameObject> nextIncountNode;
    public ExploreUI exploreUI;

    public BattleData battleNodeData;
    public EventData eventNodeData;

    //화살표 기준 노드의 위치
    public Vector2 arrowRelativePos;

    //가리기, 선택 등 노드의 추가적인 생김새 변경을 위한 오브젝트들
    public GameObject IncountNodeCoverObject;
    public GameObject IncountNodeHighlightCircleObject;
    public GameObject IncountNodeCurrentHighlightCircleObject;

    //텍스처
    [Header("NodeTexture")]
    [SerializeField] private Sprite noneTexture;
    [SerializeField] private Sprite battleTexture;
    [SerializeField] private Sprite eliteTexture;
    [SerializeField] private Sprite bossTexture;
    [SerializeField] private Sprite restoreTexture;
    [SerializeField] private Sprite storeTexture;
    [SerializeField] private Sprite secretBoxTexture;
    [SerializeField] private Sprite secretTexture;
    [SerializeField] private Sprite insightTexture;

    public void SetIncountNode(IncountType newIncountType)
    {
        incountType = newIncountType;
        SetNodeTexture();
    }

    /// <summary>
    /// 아무것도 없는 상태를 제외한 모든 노드 종류들 중 랜덤으로 노드 변경
    /// </summary>
    public void SetRandomIncountNode()
    {
        var enumValue = System.Enum.GetValues(enumType:typeof(IncountType));
        incountType = (IncountType)enumValue.GetValue(Random.Range(1, enumValue.Length));
        SetNodeTexture();
    }

    /// <summary>
    /// 노드 타입에 맞는 sprite 등록
    /// </summary>
    void SetNodeTexture()
    {
        Image image = GetComponent<Image>();
        switch (incountType)
        {
            case IncountType.None:
                image.sprite = noneTexture;
                break;
            case IncountType.Battle:
                image.sprite = battleTexture;
                break;
            case IncountType.Elite:
                image.sprite = eliteTexture;
                break;
            case IncountType.Boss:
                image.sprite = bossTexture;
                break;
            case IncountType.Restore:
                image.sprite = restoreTexture;
                break;
            case IncountType.Store:
                image.sprite = storeTexture;
                break;
            case IncountType.SecretBox:
                image.sprite = secretBoxTexture;
                break;
            case IncountType.Secret:
                image.sprite = secretTexture;
                break;
            case IncountType.Insight:
                image.sprite = insightTexture;
                break;
            case IncountType.ShineWell:
                image.sprite = insightTexture;
                break;
            default:
                image.sprite = noneTexture;
                break;
        }
    }

    public void OpenNodeCoverTexture()
    {
        IncountNodeCoverObject.SetActive(false);
    }

    public void CloseNodeCoverTexture()
    {
        IncountNodeCoverObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.instance.currentIncountNode.nextIncountNode.Contains(this.gameObject))
        {
            //이전의 노드를 저장 후 다음 노드로 변경
            GameManager.instance.beforeIncountNode = GameManager.instance.currentIncountNode;
            GameManager.instance.currentIncountNode.transform.GetComponent<IncountNode>().IncountNodeCurrentHighlightCircleObject.SetActive(false);
            GameManager.instance.currentIncountNode = this; //다음 맵 로딩을 위해 이동할 노드 정보를 저장
            GameManager.instance.currentExploreMapFloor += 1;
            if(exploreUI != null)
            {
                IncountNodeCurrentHighlightCircleObject.SetActive(true);

                GameManager.instance.loadMapHandler.StartFadeInOut(true);
            }
        }
        else
        {
            Debug.Log("This node is nextIncountNode");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IncountNodeHighlightCircleObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IncountNodeHighlightCircleObject.SetActive(false);
    }
}
