using UnityEngine;

public class NextEntranceHandler : MonoBehaviour
{
    private IncountNode currentNode;

    public void SetCurrentNode(IncountNode node)
    {
        currentNode = node;
    }

    public void ProcessNextEntrance()
    {
        if (currentNode == null)
            return;

        currentNode.LoadMapDataFromIncountNode();
    }
}
