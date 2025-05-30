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
    [SerializeField] private int StoreNumber = 1;  //맵에 등장하는 상점 갯수
    [SerializeField] private int RestoreNumber = 1;  //맵에 등장하는 휴식 갯수
    [SerializeField] private int SecretNumber = 6;  //맵에 등장하는 시크릿 갯수
    [SerializeField] private int BoxNumber = 4;  //맵에 등장하는 상자 갯수
    [SerializeField] private int EliteNumber = 3;  //맵에 등장하는 엘리트 갯수

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

        //노드들을 담아두는 Vertical Leyout들을 미리 담아두기
        ExploreVerticalObjects = new List<VerticalLayoutGroup>();
        for(int i = 0; i < mapLength; i++)
        {
            ExploreVerticalObjects.Add(GameObject.Instantiate(ExploreMapVerticalLayoutPrefab, ViewLayout.transform).GetComponent<VerticalLayoutGroup>());
            ExploreVerticalObjects[i].spacing = VerticalLayoutSpacing;
        }
    }
    /// <summary>
    /// Vertical Layout의 padding시 노드가 2개 이상일 경우 390 - (노드의 갯수 * 65)만큼 top에 더해주면 중심이 맞게 정렬됨
    /// 1개일 경우는 325로 고정
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

            if(index == 0)  //처음 노드는 무조건 None으로 생성
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.exploreUI = this;
                node.SetIncountNode(IncountType.None);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 325;
            }
            else if(index == mapLength - 1) //마지막 노드는 무조건 Boss로 생성
            {
                IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                node.exploreUI = this;
                node.SetIncountNode(IncountType.Boss);
                ExploreMap[index].Add(node);
                ExploreVerticalObjects[index].padding.top = 325;
            }
            else if(index == mapLength / 2) //맵 중간에 회복 및 상점 위치 생성
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

                //다음 섹션의 노드 저장을 위해 List추가
                incountNodeListInSection.Add(new List<IncountNode>());
                sectionIndex++;
            }
            else //나머지 노드는 랜덤 갯수에 인카운트 노드 생성
            {
                createNodeCount = Random.Range(3, 7);
                //정해진 수 만큼 랜덤한 인카운터 생성
                for (int mapIndex = 0; mapIndex < createNodeCount; mapIndex++)
                {
                    IncountNode node = GameObject.Instantiate(NodePrefab, ExploreVerticalObjects[index].transform).GetComponent<IncountNode>();
                    node.exploreUI = this;
                    node.SetIncountNode(IncountType.Battle);
                    ExploreMap[index].Add(node);
                    ExploreVerticalObjects[index].padding.top = 390 - (createNodeCount * 65);

                    //섹션 내의 노드들 저장
                    incountNodeListInSection[sectionIndex].Add(node);
                }
            }
        }

        //보스 전에는 무조견 휴식 존재
        foreach (IncountNode node in ExploreMap[mapLength - 2])
        {
            node.SetIncountNode(IncountType.Restore);
            node.DisableExtraType();
        }

        /// <summary>
        /// 섹션 내의 노드들 중에서 특정 노드들로 변경
        /// 추가되는 노드 종류는 엘리트, 시크릿, 박스, 휴식, 상점으로 총 5가지
        /// </summary>
        for (int sectionIdx = 0; sectionIdx < incountNodeListInSection.Count; sectionIdx++)
        {
            IncountNode currentNode;

            //엘리트 적 생성
            for (int index = 0; index < EliteNumber; index++)
            {
                //일반 전투인 노드들 중 랜덤으로 한 개 선택
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Elite);
                currentNode.DisableExtraType();
            }

            //시크릿 생성
            for (int index = 0; index < SecretNumber; index++)
            {
                //일반 전투인 노드들 중 랜덤으로 한 개 선택
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Secret);
                currentNode.DisableExtraType();
            }

            //상자 생성
            for (int index = 0; index < BoxNumber; index++)
            {
                //일반 전투인 노드들 중 랜덤으로 한 개 선택
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.SecretBox);
                currentNode.DisableExtraType();
            }

            //휴식 생성
            for (int index = 0; index < RestoreNumber; index++)
            {
                //일반 전투인 노드들 중 랜덤으로 한 개 선택
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Restore);
                currentNode.DisableExtraType();
            }

            //상점 생성
            for (int index = 0; index < StoreNumber; index++)
            {
                //일반 전투인 노드들 중 랜덤으로 한 개 선택
                do
                {
                    currentNode = incountNodeListInSection[sectionIdx][Random.Range(0, incountNodeListInSection[sectionIdx].Count)];
                }
                while (currentNode.incountType != IncountType.Battle);

                currentNode.SetIncountNode(IncountType.Store);
                currentNode.DisableExtraType();
            }
        }

        //생성 시 필요한 만큼 노드 가리기
        for (int i = GameManager.instance.checkMapNodeFloorLength; i < ExploreMap.Count; i++)
        {
            for (int j = 0; j < ExploreMap[i].Count; j++)
            {
                ExploreMap[i][j].CloseNodeCoverTexture();
            }
        }

        //시작 지점 저장
        GameManager.instance.currentIncountNode = ExploreMap[0][0];
        ExploreMap[0][0].IncountNodeCurrentHighlightCircleObject.SetActive(true);

        //각 노드끼리 연결하는 Arrow생성
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

        //레이아웃의 정렬이 끝난 후인 다음 프레임에 위치 계산 실행
        SetNodePosition();

        //각 노드 간의 연결 계산
        SetNextIncountNode();

        //연결에 맞게 화살표 생성
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
            arrowLine.sizeDelta = new Vector2(dist, 5); //선 두께 5
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
    /// IncountNode의 nextIncountNode에 다음 노드들 등록
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
            else if(ExploreMap[i].Count < ExploreMap[i + 1].Count)  //왼쪽 노드가 오른쪽 노드보다 작을 경우
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                bool recentlyIgnoreNode = false;
                //다수의 노드에게 선택될 노드 한 개를 랜덤으로 선택
                int randomNodeNumber = Random.Range(0, ExploreMap[i].Count);

                while (currentNodeCount < ExploreMap[i].Count && nextNodeCount < ExploreMap[i + 1].Count)
                {
                    if(currentNodeCount == randomNodeNumber)
                    {
                        //왼쪽 노드의 남은 수 + 1 = 오른쪽 노드의 남은 수가 될 때 까지 왼쪽 노드를 내려가며 등록
                        while (ExploreMap[i].Count - currentNodeCount + 1 < ExploreMap[i + 1].Count - nextNodeCount)
                        {
                            ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                            nextNodeCount++;
                        }
                    }

                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                    //맨 처음 노드와 마지막 노드가 아닐 경우 50%의 확률로 아래 노드와 연결됨
                    //이전에 아래 노드와 연결이 안됬을 경우는 무조건 연결됨
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

                    //만약 마지막까지 연결이 안된 노드가 존재 시 마지막 노드에 강제로 연결해줌
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
            else  //왼쪽 노드가 오른쪽 노드보다 클 경우
            {
                int currentNodeCount = 0;
                int nextNodeCount = 0;
                bool recentlyIgnoreNode = false;
                //다수의 노드에게 선택될 노드 한 개를 랜덤으로 선택
                int randomNodeNumber = Random.Range(0, ExploreMap[i+1].Count);

                while (currentNodeCount < ExploreMap[i].Count && nextNodeCount < ExploreMap[i + 1].Count)
                {
                    //일단 현재 선택된 왼쪽 노드에 선택된 오른쪽 노드를 등록
                    ExploreMap[i][currentNodeCount].nextIncountNode.Add(ExploreMap[i + 1][nextNodeCount].gameObject);
                    //만약 오른쪽 노드가 선택된 노드일 시
                    if (nextNodeCount == randomNodeNumber)
                    {
                        //왼쪽 노드의 남은 수 = 오른쪽 노드의 남은 수 - 1이 될 때 까지 왼쪽 노드를 내려가며 등록
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

                    //맨 처음 노드와 마지막 노드가 아닐 경우 50%의 확률로 아래 노드와 연결됨
                    //이전에 아래 노드와 연결이 안됬을 경우는 무조건 연결됨
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

                    //만약 마지막까지 연결이 안된 노드가 존재 시 마지막 노드에 강제로 연결해줌
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
