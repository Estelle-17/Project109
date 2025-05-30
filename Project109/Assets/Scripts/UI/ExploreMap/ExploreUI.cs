using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ExploreUI : MonoBehaviour
{
    public List<List<IncountNode>> ExploreMap;

    public GameObject ViewLayout;
    public GameObject ArrowObjects;
    public List<VerticalLayoutGroup> ExploreVerticalObjects;

    int mapLength;

    public int currentMapFloor;

    [SerializeField] private int VerticalLayoutSpacing = 30;

    [Header ("Map Setting")]
    [SerializeField] private int StoreNumber = 1;  //�ʿ� �����ϴ� ���� ����
    [SerializeField] private int RestoreNumber = 1;  //�ʿ� �����ϴ� �޽� ����
    [SerializeField] private int SecretNumber = 6;  //�ʿ� �����ϴ� ��ũ�� ����
    [SerializeField] private int BoxNumber = 4;  //�ʿ� �����ϴ� ���� ����
    [SerializeField] private int EliteNumber = 3;  //�ʿ� �����ϴ� ����Ʈ ����

    [Header ("Prefab")]
    [SerializeField] private GameObject NodePrefab;
    [SerializeField] private GameObject ArrowLinePrefab;
    [SerializeField] private GameObject ArrowHeadPrefab;
    [SerializeField] private GameObject ExploreMapVerticalLayoutPrefab;
    
    void Start()
    {
        ExploreMap = new List<List<IncountNode>>();

        mapLength = 15;
        currentMapFloor = 0;

        //������ ��Ƶδ� Vertical Leyout���� �̸� ��Ƶα�
        ExploreVerticalObjects = new List<VerticalLayoutGroup>();
        for(int i = 0; i < mapLength; i++)
        {
            ExploreVerticalObjects.Add(GameObject.Instantiate(ExploreMapVerticalLayoutPrefab, ViewLayout.transform).GetComponent<VerticalLayoutGroup>());
            ExploreVerticalObjects[i].spacing = VerticalLayoutSpacing;
        }
    }
    /// <summary>
    /// Vertical Layout�� padding�� ��尡 2�� �̻��� ��� 390 - (����� ���� * 65)��ŭ top�� �����ָ� �߽��� �°� ���ĵ�
    /// 1���� ���� 325�� ����
    /// </summary>
    public void CreateExploreMap()
    {
        List<List<IncountNode>> incountNodeListInSection = new List<List<IncountNode>>();

        int createNodeCount = 0;
        int sectionIndex = 0;

        incountNodeListInSection.Add(new List<IncountNode>());

        for(int index = 0; index < mapLength; index++)
        {
            ExploreMap.Add(new List<IncountNode>());

            if(index == 0)  //ó�� ���� ������ None���� ����
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.exploreUI = this;
                node.SetIncountNode(IncountType.None);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 325;
            }
            else if(index == mapLength - 1) //������ ���� ������ Boss�� ����
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.exploreUI = this;
                node.SetIncountNode(IncountType.Boss);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 325;
            }
            else if(index == mapLength / 2) //�� �߰��� ȸ�� �� ���� ��ġ ����
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.exploreUI = this;
                node.SetIncountNode(IncountType.Restore);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 260;

                IncountNode node1 = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node1.exploreUI = this;
                node1.SetIncountNode(IncountType.Store);
                ExploreMap[index].Add(node1);
                ExploreVerticalObjects[index].padding.top = 260;

                //���� ������ ��� ������ ���� List�߰�
                incountNodeListInSection.Add(new List<IncountNode>());
                sectionIndex++;
            }
            else //������ ���� ���� ������ ��ī��Ʈ ��� ����
            {
                createNodeCount = Random.Range(3, 7);
                //������ �� ��ŭ ������ ��ī���� ����
                for (int mapIndex = 0; mapIndex < createNodeCount; mapIndex++)
                {
                    IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                    node.exploreUI = this;
                    node.SetIncountNode(IncountType.Battle);
                    ExploreMap[index].Add(node);
                    ExploreVerticalObjects[index].padding.top = 390 - (createNodeCount * 65);

                    //���� ���� ���� ����
                    incountNodeListInSection[sectionIndex].Add(node);
                }
            }
        }

        //���� ������ ������ �޽� ����
        foreach (IncountNode node in ExploreMap[mapLength - 2])
        {
            node.SetIncountNode(IncountType.Restore);
            node.DisableExtraType();
        }

        /// <summary>
        /// ���� ���� ���� �߿��� Ư�� ����� ����
        /// �߰��Ǵ� ��� ������ ����Ʈ, ��ũ��, �ڽ�, �޽�, �������� �� 5����
        /// </summary>
        for (int sectionIdx = 0; sectionIdx < incountNodeListInSection.Count; sectionIdx++)
        {
            IncountNode currentNode;

            //����Ʈ �� ����
            for (int index = 0; index < EliteNumber; index++)
            {
                //�Ϲ� ������ ���� �� �������� �� �� ����
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Elite);
                currentNode.DisableExtraType();
            }

            //��ũ�� ����
            for (int index = 0; index < SecretNumber; index++)
            {
                //�Ϲ� ������ ���� �� �������� �� �� ����
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Secret);
                currentNode.DisableExtraType();
            }

            //���� ����
            for (int index = 0; index < BoxNumber; index++)
            {
                //�Ϲ� ������ ���� �� �������� �� �� ����
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.SecretBox);
                currentNode.DisableExtraType();
            }

            //�޽� ����
            for (int index = 0; index < RestoreNumber; index++)
            {
                //�Ϲ� ������ ���� �� �������� �� �� ����
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Restore);
                currentNode.DisableExtraType();
            }

            //���� ����
            for (int index = 0; index < StoreNumber; index++)
            {
                //�Ϲ� ������ ���� �� �������� �� �� ����
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Store);
                currentNode.DisableExtraType();
            }
        }

        //���� �� �ʿ��� ��ŭ ��� ������
        for (int i = GameManager.instance.checkMapNodeFloorLength; i < ExploreMap.Count; i++)
        {
            for (int j = 0; j < ExploreMap[i].Count; j++)
            {
                ExploreMap[i][j].CloseNodeCoverTexture();
            }
        }

        //���� ���� ����
        GameManager.instance.currentIncountNode = ExploreMap[0][0];
        ExploreMap[0][0].IncountNodeCurrentHighlightCircleObject.SetActive(true);

        //�� ��峢�� �����ϴ� Arrow����
        StartCoroutine(CreateArrowUI());
    }
   
    public void OpenExploreMapNodesBasedOnFloorLength()
    {
        int currentFloor = GameManager.instance.currentExploreMapFloor;
        int openNodeLength = currentFloor + GameManager.instance.checkMapNodeFloorLength;
        openNodeLength = openNodeLength > ExploreMap.Count ? ExploreMap.Count : openNodeLength;

        for (int i = currentFloor; i < openNodeLength; i++)
        {
            for (int j = 0; j < ExploreMap[i].Count; j++)
            {
                ExploreMap[i][j].OpenNodeCoverTexture();
            }
        }
    }

    public void OpenRandomExploreMapNode()
    {
        int x = Random.Range(0, ExploreMap.Count);
        int y = Random.Range(0, ExploreMap[x].Count);

        ExploreMap[x][y].OpenNodeCoverTexture();
    }

    IEnumerator CreateArrowUI()
    {
        yield return new WaitForEndOfFrame();

        //���̾ƿ��� ������ ���� ���� ���� �����ӿ� ��ġ ��� ����
        SetNodePosition();

        //�� ��� ���� ���� ���
        SetNextIncountNode();

        //���ῡ �°� ȭ��ǥ ����
        for (int i = 0; i < ExploreMap.Count; i++)
        {
            for (int j = 0; j < ExploreMap[i].Count; j++)
            {
                foreach (GameObject nextNode in ExploreMap[i][j].nextIncountNode)
                {
                    MakeArrowUI(ExploreMap[i][j].arrowRelativePos, nextNode.transform.GetComponent<IncountNode>().arrowRelativePos);
                }
            }
        }
    }

    void MakeArrowUI(Vector2 startPos, Vector2 endPos)
    {
        Vector2 offset = (endPos - startPos).normalized * 50;
        Vector2 dir = endPos - startPos;
        float dist = dir.magnitude - 120;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

       // Debug.Log("StartPos : " + startPos + ", EndPos : " + endPos + ", Position : " + ((startPos + endPos) / 2f).ToString());

        if (ArrowLinePrefab != null)
        {
            RectTransform arrowLine = GameObject.Instantiate(ArrowLinePrefab, ArrowObjects.transform).GetComponent<RectTransform>();

            arrowLine.anchoredPosition = ((startPos + endPos) / 2f);
            arrowLine.sizeDelta = new Vector2(dist, 5); //�� �β� 5
            arrowLine.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (ArrowHeadPrefab != null)
        {
            RectTransform arrowHead = GameObject.Instantiate(ArrowHeadPrefab, ArrowObjects.transform).GetComponent<RectTransform>();
            arrowHead.anchoredPosition = endPos - (offset * 1.4f);
            arrowHead.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    void SetNodePosition()
    {
        RectTransform parentRect = ArrowObjects.GetComponent<RectTransform>();

        for (int i = 0; i < ExploreMap.Count; i++)
        {
            for (int j = 0; j < ExploreMap[i].Count; j++)
            {
                Vector2 relativeVector = (Vector2)parentRect.InverseTransformPoint(ExploreMap[i][j].transform.GetComponent<RectTransform>().position);
                ExploreMap[i][j].arrowRelativePos = relativeVector;
            }
        }
    }

    /// <summary>
    /// IncountNode�� nextIncountNode�� ���� ���� ���
    /// </summary>
    void SetNextIncountNode()
    {
        for (int i = 0; i < ExploreMap.Count - 1; i++)
        {
            if (ExploreMap[i].Count == 1)
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                while (nextNodeCount < ExploreMap[i + 1].Count)
                {
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                    nextNodeCount++;
                }
            }
            else if(ExploreMap[i + 1].Count == 1)
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                while (currentNodeCount < ExploreMap[i].Count)
                {
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                    currentNodeCount++;
                }
            }
            else if(ExploreMap[i].Count < ExploreMap[i + 1].Count)  //���� ��尡 ������ ��庸�� ���� ���
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                bool recentlyIgnoreNode = false;
                //�ټ��� ��忡�� ���õ� ��� �� ���� �������� ����
                int randomNodeNumber = Random.Range(0, ExploreMap[i].Count);

                while (currentNodeCount < ExploreMap[i].Count && nextNodeCount < ExploreMap[i + 1].Count)
                {
                    if(currentNodeCount == randomNodeNumber)
                    {
                        //���� ����� ���� �� + 1 = ������ ����� ���� ���� �� �� ���� ���� ��带 �������� ���
                        while (ExploreMap[i].Count - currentNodeCount + 1 < ExploreMap[i + 1].Count - nextNodeCount)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                            nextNodeCount++;
                        }
                    }

                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                    //�� ó�� ���� ������ ��尡 �ƴ� ��� 50%�� Ȯ���� �Ʒ� ���� �����
                    //������ �Ʒ� ���� ������ �ȉ��� ���� ������ �����
                    if (nextNodeCount + 1 < ExploreMap[i + 1].Count)
                    {
                        if (Random.Range(0, 11) % 2 == 0 || recentlyIgnoreNode || currentNodeCount == 0 || currentNodeCount == ExploreMap[i].Count - 1)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount + 1].gameObject);
                            nextNodeCount++;
                            recentlyIgnoreNode = false;
                        }
                        else
                        {
                            nextNodeCount++;
                            recentlyIgnoreNode = true;
                        }
                    }

                    //���� ���������� ������ �ȵ� ��尡 ���� �� ������ ��忡 ������ ��������
                    if (currentNodeCount == ExploreMap[i].Count - 1 && nextNodeCount < ExploreMap[i + 1].Count - 1)
                    {
                        while (nextNodeCount < ExploreMap[i + 1].Count)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                            nextNodeCount++;
                        }
                    }

                    currentNodeCount++;
                }
            }
            else  //���� ��尡 ������ ��庸�� Ŭ ���
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                bool recentlyIgnoreNode = false;
                //�ټ��� ��忡�� ���õ� ��� �� ���� �������� ����
                int randomNodeNumber = Random.Range(0, ExploreMap[i+1].Count);

                while (currentNodeCount < ExploreMap[i].Count && nextNodeCount < ExploreMap[i + 1].Count)
                {
                    //�ϴ� ���� ���õ� ���� ��忡 ���õ� ������ ��带 ���
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                    //���� ������ ��尡 ���õ� ����� ��
                    if (nextNodeCount == randomNodeNumber)
                    {
                        //���� ����� ���� �� = ������ ����� ���� �� - 1�� �� �� ���� ���� ��带 �������� ���
                        while (ExploreMap[i].Count - currentNodeCount > ExploreMap[i + 1].Count - nextNodeCount)
                        {
                            if (currentNodeCount + 1 < ExploreMap[i].Count)
                            {
                                currentNodeCount++;
                                ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (currentNodeCount + 1 < ExploreMap[i].Count)
                        {
                            currentNodeCount++;
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                        }
                    }

                    //�� ó�� ���� ������ ��尡 �ƴ� ��� 50%�� Ȯ���� �Ʒ� ���� �����
                    //������ �Ʒ� ���� ������ �ȉ��� ���� ������ �����
                    if (nextNodeCount + 1 < ExploreMap[i + 1].Count)
                    {
                        if (Random.Range(0, 11) % 2 == 0 || recentlyIgnoreNode || currentNodeCount == 0 || currentNodeCount == ExploreMap[i].Count - 1)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount + 1].gameObject);
                            nextNodeCount++;
                            recentlyIgnoreNode = false;
                        }
                        else
                        {
                            recentlyIgnoreNode = true;
                        }
                    }

                    //���� ���������� ������ �ȵ� ��尡 ���� �� ������ ��忡 ������ ��������
                    if (currentNodeCount == ExploreMap[i].Count - 1 && nextNodeCount < ExploreMap[i + 1].Count - 1)
                    {
                        while (nextNodeCount < ExploreMap[i + 1].Count)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                            nextNodeCount++;
                        }
                    }

                    currentNodeCount++;
                }
            }
        }
    }
}
