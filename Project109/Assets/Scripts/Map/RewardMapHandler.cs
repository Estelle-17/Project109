using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardMapHandler : MonoBehaviour
{
    [SerializeField] private Transform rewardSpawnPosition;
    [SerializeField] private Transform entranceTransform;
    [SerializeField] private List<Transform> nextEntrancePositions;

    [SerializeField] private GameObject battleEntrancePrefab;
    [SerializeField] private GameObject eliteBattleEntrancePrefab;
    [SerializeField] private GameObject shopEntrancePrefab;
    [SerializeField] private GameObject secretEntrancePrefab;

    void Start()
    {
        foreach (Transform child in entranceTransform)
        {
            nextEntrancePositions.Add(child);
        }
    }

    public void ProcessEntranceSetting()
    {
        if(nextEntrancePositions.Count == 0)
        {
            foreach (Transform child in entranceTransform)
            {
                nextEntrancePositions.Add(child);
            }
        }

        IncountNode node = GameManager.instance.currentIncountNode;

        int entranceCount = 0;

        //다음 노드에 따른 입구 생성
        foreach (GameObject nextNode in node.nextIncountNode)
        {
            NextEntranceHandler nextEntranceHandler = null;

            if(nextNode.GetComponent<IncountNode>().incountType == IncountType.Battle)
            {
                nextEntranceHandler = Instantiate(battleEntrancePrefab, nextEntrancePositions[entranceCount].position, Quaternion.identity).
                                      GetComponent<NextEntranceHandler>();
            }
            else if(nextNode.GetComponent<IncountNode>().incountType == IncountType.Elite)
            {
                nextEntranceHandler = Instantiate(eliteBattleEntrancePrefab, nextEntrancePositions[entranceCount].position, Quaternion.identity).
                                      GetComponent<NextEntranceHandler>();
            }
            else if(nextNode.GetComponent<IncountNode>().incountType == IncountType.Store)
            {
                nextEntranceHandler = Instantiate(shopEntrancePrefab, nextEntrancePositions[entranceCount].position, Quaternion.identity).
                                      GetComponent<NextEntranceHandler>();
            }
            else if(nextNode.GetComponent<IncountNode>().incountType == IncountType.Secret)
            {
                nextEntranceHandler = Instantiate(secretEntrancePrefab, nextEntrancePositions[entranceCount].position, Quaternion.identity).
                                      GetComponent<NextEntranceHandler>();
            }

            if (nextEntranceHandler != null)
            {
                nextEntranceHandler.SetCurrentNode(nextNode.GetComponent<IncountNode>());
            }

            entranceCount++;
            if(entranceCount >= nextEntrancePositions.Count)
            {
                break;
            }
        }

        //보상 생성
        InstantiateRewardBox();
    }

    void InstantiateRewardBox()
    {
        GameItemRewardManager.instance.SpawnRewardBox();
    }
}
