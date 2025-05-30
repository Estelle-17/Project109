using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum IncountType
{ 
    None,
    Elite,
    Boss,
    Store,
    Restore,
    Battle,
    SecretBox,
    Secret
}

public class IncountNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public IncountType incountType;
    public List<GameObject> nextIncountNode;
    public ExploreUI exploreUI;

    //ȭ��ǥ ���� ����� ��ġ
    public Vector2 arrowRelativePos;

    //������, ���� �� ����� �߰����� ����� ������ ���� ������Ʈ��
    public GameObject ExtraMoneyObject;
    public GameObject ExtraCardObject;
    public GameObject IncountNodeCoverObject;
    public GameObject IncountNodeHighlightCircleObject;
    public GameObject IncountNodeCurrentHighlightCircleObject;

    public bool isExtraMoney;
    public bool isExtraCard;

    //�ؽ�ó
    [Header("NodeTexture")]
    [SerializeField] private Sprite NoneTexture;
    [SerializeField] private Sprite BattleTexture;
    [SerializeField] private Sprite EliteTexture;
    [SerializeField] private Sprite BossTexture;
    [SerializeField] private Sprite RestoreTexture;
    [SerializeField] private Sprite StoreTexture;
    [SerializeField] private Sprite SecretBoxTexture;
    [SerializeField] private Sprite SecretTexture;

    public void SetIncountNode(IncountType newIncountType)
    {
        incountType = newIncountType;
        SetNodeTexture();

        if (incountType == IncountType.Battle && ExtraCardObject && ExtraMoneyObject)
        {
            if (Random.Range(0, 2) == 0)
            {
                isExtraCard = true;
                ExtraCardObject.SetActive(true);
            }
            else
            {
                isExtraMoney = true;
                ExtraMoneyObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// �ƹ��͵� ���� ���¸� ������ ��� ��� ������ �� �������� ��� ����
    /// </summary>
    public void SetRandomIncountNode()
    {
        var enumValue = System.Enum.GetValues(enumType:typeof(IncountType));
        incountType = (IncountType)enumValue.GetValue(Random.Range(1, enumValue.Length));
        SetNodeTexture();

        if(incountType == IncountType.Battle && ExtraCardObject && ExtraMoneyObject)
        {
            if(Random.Range(0, 2) == 0)
            {
                isExtraCard = true;
                ExtraCardObject.SetActive(true);
            }
            else
            {
                isExtraMoney = true;
                ExtraMoneyObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// ��� Ÿ�Կ� �´� sprite ���
    /// </summary>
    void SetNodeTexture()
    {
        Image image = GetComponent<Image>();
        switch (incountType)
        {
            case IncountType.None:
                image.sprite = NoneTexture;
                break; 
            case IncountType.Battle:
                image.sprite = BattleTexture;
                break;
            case IncountType.Elite:
                image.sprite = EliteTexture;
                break;
            case IncountType.Boss:
                image.sprite = BossTexture;
                break;
            case IncountType.Restore:
                image.sprite = RestoreTexture;
                break;   
            case IncountType.Store:
                image.sprite = StoreTexture;
                break;
            case IncountType.SecretBox:
                image.sprite = SecretBoxTexture;
                break;
            case IncountType.Secret:
                image.sprite = SecretTexture;
                break;
            default:
                image.sprite = NoneTexture;
                break;
        }
    }

    public void DisableExtraType()
    {
        ExtraCardObject.SetActive(false);
        ExtraMoneyObject.SetActive(false);
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
            GameManager.instance.currentIncountNode.transform.GetComponent<IncountNode>().IncountNodeCurrentHighlightCircleObject.SetActive(false);
            GameManager.instance.currentIncountNode = this;
            GameManager.instance.currentExploreMapFloor += 1;
            if(exploreUI != null)
            {
                IncountNodeCurrentHighlightCircleObject.SetActive(true);
                exploreUI.OpenExploreMapNodesBasedOnFloorLength();
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
