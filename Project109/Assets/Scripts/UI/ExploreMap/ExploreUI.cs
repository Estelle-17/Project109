using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExploreUI : MonoBehaviour
{
    public List<List<IncountNode>> ExploreMap;
    public VerticalLayoutGroup[] ExploreVerticalObjects;

    int mapLength = 8;
    [SerializeField] private int VerticalLayoutSpacing = 30;

    [Header ("Prefab")]
    [SerializeField] private GameObject NodePrefab;
    [SerializeField] private GameObject ArrowLinePrefab;
    [SerializeField] private GameObject ArrowHeadPrefab;
    
    void Start()
    {
        ExploreMap = new List<List<IncountNode>>();

        //������ ��Ƶδ� Vertical Leyout���� �̸� ��Ƶα�
        ExploreVerticalObjects = new VerticalLayoutGroup[9];
        for(int i = 0; i < mapLength; i++)
        {
            ExploreVerticalObjects[i] = transform.GetChild(0).GetChild(i).GetComponent<VerticalLayoutGroup>();
            ExploreVerticalObjects[i].spacing = VerticalLayoutSpacing;
        }
    }
    /// <summary>
    /// Vertical Layout�� padding�� ��尡 2�� �̻��� ��� 390 - (����� ���� * 65)��ŭ top�� �����ָ� �߽��� �°� ���ĵ�
    /// 1���� ���� 325�� ����
    /// </summary>
    public void CreateExploreMap()
    {
        int createNodeCount = 0;

        for(int index = 0; index < mapLength; index++)
        {
            ExploreMap.Add(new List<IncountNode>());

            if(index == 0)
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.SetIncountNode(IncountType.None);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 325;

                //Debug.Log("0 : 1 ");
            }
            else if(index == mapLength - 1)
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.SetIncountNode(IncountType.Boss);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 325;

                //Debug.Log("8 : 1");
            }
            else
            {
                createNodeCount = Random.Range(3, 7);
                //������ �� ��ŭ ������ ��ī���� ����
                for (int mapIndex = 0; mapIndex < createNodeCount; mapIndex++)
                {
                    IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                    node.SetRandomIncountNode();
                    ExploreMap[index].Add(node);
                    ExploreVerticalObjects[index].padding.top = 390 - (createNodeCount * 65);
                }

                //Debug.Log(index + " : " + createNodeCount);
            }
        }

        //�� ��峢�� �����ϴ� Arrow����
        StartCoroutine(CreateArrowUI());
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
                foreach (IncountNode nextNode in ExploreMap[i][j].nextIncountNode)
                {
                    MakeArrowUI(ExploreMap[i][j].arrowRelativePos, nextNode.arrowRelativePos);
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

        Debug.Log("StartPos : " + startPos + ", EndPos : " + endPos + ", Position : " + ((startPos + endPos) / 2f).ToString());

        if (ArrowLinePrefab != null)
        {
            RectTransform arrowLine = GameObject.Instantiate(ArrowLinePrefab, transform.GetChild(1).transform).GetComponent<RectTransform>();

            arrowLine.anchoredPosition = ((startPos + endPos) / 2f);
            arrowLine.sizeDelta = new Vector2(dist, 5); //�� �β� 5
            arrowLine.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (ArrowHeadPrefab != null)
        {
            RectTransform arrowHead = GameObject.Instantiate(ArrowHeadPrefab, transform.GetChild(1).transform).GetComponent<RectTransform>();
            arrowHead.anchoredPosition = endPos - (offset * 1.4f);
            arrowHead.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    void SetNodePosition()
    {
        RectTransform parentRect = transform.GetChild(1).GetComponent<RectTransform>();

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
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                    nextNodeCount++;
                }
            }
            else if(ExploreMap[i + 1].Count == 1)
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                while (currentNodeCount < ExploreMap[i].Count)
                {
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
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
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                            nextNodeCount++;
                        }
                    }

                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                    //�� ó�� ���� ������ ��尡 �ƴ� ��� 50%�� Ȯ���� �Ʒ� ���� �����
                    //������ �Ʒ� ���� ������ �ȉ��� ���� ������ �����
                    if (nextNodeCount + 1 < ExploreMap[i + 1].Count)
                    {
                        if (Random.Range(0, 11) % 2 == 0 || recentlyIgnoreNode || currentNodeCount == 0 || currentNodeCount == ExploreMap[i].Count - 1)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount + 1]);
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
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
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
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                    //���� ������ ��尡 ���õ� ����� ��
                    if (nextNodeCount == randomNodeNumber)
                    {
                        //���� ����� ���� �� = ������ ����� ���� �� - 1�� �� �� ���� ���� ��带 �������� ���
                        while (ExploreMap[i].Count - currentNodeCount > ExploreMap[i + 1].Count - nextNodeCount)
                        {
                            if (currentNodeCount + 1 < ExploreMap[i].Count)
                            {
                                currentNodeCount++;
                                ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (currentNodeCount + 1 < ExploreMap[i].Count)
                        {
                            currentNodeCount++;
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                        }
                    }

                    //�� ó�� ���� ������ ��尡 �ƴ� ��� 50%�� Ȯ���� �Ʒ� ���� �����
                    //������ �Ʒ� ���� ������ �ȉ��� ���� ������ �����
                    if (nextNodeCount + 1 < ExploreMap[i + 1].Count)
                    {
                        if (Random.Range(0, 11) % 2 == 0 || recentlyIgnoreNode || currentNodeCount == 0 || currentNodeCount == ExploreMap[i].Count - 1)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount + 1]);
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
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount]);
                            nextNodeCount++;
                        }
                    }

                    currentNodeCount++;
                }
            }
        }
    }
}
