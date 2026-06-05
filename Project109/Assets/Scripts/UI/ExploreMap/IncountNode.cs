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
}

public enum ExtraIncountType
{
    None,
    Insight,    //천리안
    ShineWell   //빛나는 우물
}

public class IncountNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public IncountType incountType;
    public ExtraIncountType extraIncountType;
    public List<GameObject> nextIncountNode;
    public ExploreUI exploreUI;
    public bool isNodeChanged;  //노드가 생성되고 노드 타입이 한번 이상 변경되었는지 여부

    public BattleData battleNodeData;
    public InteractableData eventNodeData;

    //화살표 기준 노드의 위치
    public Vector2 arrowRelativePos;

    //가리기, 선택 등 노드의 추가적인 생김새 변경을 위한 오브젝트들
    public GameObject IncountNodeCoverObject;
    public GameObject IncountNodeHighlightCircleObject;
    public GameObject IncountNodeCurrentHighlightCircleObject;
    public GameObject IncountNodeExtraRewardObject;

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

    [Header("ExtraNodeTexture")]
    [SerializeField] private Sprite insightTexture;
    [SerializeField] private Sprite shiningWellTexture;

    public void SetIncountNode(IncountType newIncountType, ExtraIncountType newExtraIncountType)
    {
        incountType = newIncountType;
        extraIncountType = newExtraIncountType;
        SetNodeTexture();
        SetExtraNodeTexture();
    }

    /// <summary>
    /// 아무것도 없는 상태를 제외한 모든 노드 종류들 중 랜덤으로 노드 변경
    /// </summary>
    public void SetRandomIncountNode()
    {
        var enumValue = System.Enum.GetValues(enumType:typeof(IncountType));
        incountType = (IncountType)enumValue.GetValue(Random.Range(1, enumValue.Length));
        SetNodeTexture();
        SetExtraNodeTexture();
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
            default:
                image.sprite = noneTexture;
                break;
        }
    }

    void SetExtraNodeTexture()
    {
        Image image = IncountNodeExtraRewardObject.GetComponent<Image>();
        switch (extraIncountType)
        {
            case ExtraIncountType.None:
                image.sprite = noneTexture;
                break;
            case ExtraIncountType.Insight:
                image.sprite = insightTexture;
                break;
            case ExtraIncountType.ShineWell:
                image.sprite = shiningWellTexture;
                break;
            default:
                image.sprite = noneTexture;
                break;
        }
    }

    public void OpenNodeCoverTexture()
    {
        IncountNodeCoverObject.SetActive(false);
        if (extraIncountType != ExtraIncountType.None)
        {
            IncountNodeExtraRewardObject.SetActive(true);
        }

    }

    public void CloseNodeCoverTexture()
    {
        IncountNodeCoverObject.SetActive(true);
        IncountNodeExtraRewardObject.SetActive(false);
    }

    public void LoadMapDataFromIncountNode()
    {
        if (RunManager.instance.currentIncountNode.nextIncountNode.Contains(this.gameObject))
        {
            //이전의 노드를 저장 후 다음 노드로 변경
            RunManager.instance.beforeIncountNode = RunManager.instance.currentIncountNode;
            RunManager.instance.currentIncountNode.transform.GetComponent<IncountNode>().IncountNodeCurrentHighlightCircleObject.SetActive(false);
            RunManager.instance.currentIncountNode = this; //다음 맵 로딩을 위해 이동할 노드 정보를 저장
            RunManager.instance.currentExploreMapFloor += 1;
            if (exploreUI != null)
            {
                IncountNodeCurrentHighlightCircleObject.SetActive(true);

                RunManager.instance.loadMapHandler.StartFadeInOut(true);
            }
        }
        else
        {
            Debug.Log("This node is nextIncountNode");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LoadMapDataFromIncountNode();
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
