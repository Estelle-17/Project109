using System.Collections.Generic;
using UnityEngine;

public enum IncountType
{ 
    None,
    Boss,
    Battle,
    Store,
    Restore,
    SecretBox,
    Secret
}

public class IncountNode : MonoBehaviour
{
    public IncountType incountType;
    public List<IncountNode> nextIncountNode;

    public Vector2 arrowRelativePos;

    public void SetIncountNode(IncountType newIncountType)
    {
        incountType = newIncountType;
    }

    public void SetRandomIncountNode()
    {
        var enumValue = System.Enum.GetValues(enumType:typeof(IncountType));
        incountType = (IncountType)enumValue.GetValue(Random.Range(0, enumValue.Length));
    }
}
