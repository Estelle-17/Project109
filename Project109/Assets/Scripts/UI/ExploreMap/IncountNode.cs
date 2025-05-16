using System.Collections.Generic;
using UnityEngine;
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

public class IncountNode : MonoBehaviour
{
    public IncountType incountType;
    public List<IncountNode> nextIncountNode;

    public Vector2 arrowRelativePos;
    public GameObject ExtraMoney;
    public GameObject ExtraCard;

    public bool isExtraMoney;
    public bool isExtraCard;

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

        if (incountType == IncountType.Battle && ExtraCard && ExtraMoney)
        {
            if (Random.Range(0, 2) == 0)
            {
                isExtraCard = true;
                ExtraCard.SetActive(true);
            }
            else
            {
                isExtraMoney = true;
                ExtraMoney.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 아무것도 없는 상태를 제외한 모든 노드 종류들 중 랜덤으로 노드 변경
    /// </summary>
    public void SetRandomIncountNode()
    {
        var enumValue = System.Enum.GetValues(enumType:typeof(IncountType));
        incountType = (IncountType)enumValue.GetValue(Random.Range(1, enumValue.Length));
        SetNodeTexture();

        if(incountType == IncountType.Battle && ExtraCard && ExtraMoney)
        {
            if(Random.Range(0, 2) == 0)
            {
                isExtraCard = true;
                ExtraCard.SetActive(true);
            }
            else
            {
                isExtraMoney = true;
                ExtraMoney.SetActive(true);
            }
        }
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
        ExtraCard.SetActive(false);
        ExtraMoney.SetActive(false);
    }
}
