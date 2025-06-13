using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableDataLoader : MonoBehaviour
{
    public string cardKey = "Card";
    public string relicKey = "Relic";
    public string eventKey = "Event";

    public IList<ActionCardData> cardList;
    public IList<RelicData> relicList;
    public IList<EventData> eventList;

    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        Addressables.LoadAssetsAsync<ActionCardData>(cardKey, null).Completed += OnCardDataLoaded;
        Addressables.LoadAssetsAsync<RelicData>(relicKey, null).Completed += OnRelicDataLoaded;
        Addressables.LoadAssetsAsync<EventData>(eventKey, null).Completed += OnEventDataLoaded;
    }

    void OnEventDataLoaded(AsyncOperationHandle<IList<EventData>> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            eventList = handle.Result;
            foreach(EventData data in eventList) 
            {
                Debug.Log($"이름: {data.eventName}, 값: {data.eventDescription}");
            }
        }
        else
        {
            Debug.LogError("Addressables 로드 실패!");
        }
    }

    void OnRelicDataLoaded(AsyncOperationHandle<IList<RelicData>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            relicList = handle.Result;
            foreach (RelicData data in relicList)
            {
                Debug.Log($"이름: {data.relicName}");
            }
        }
        else
        {
            Debug.LogError("Addressables 로드 실패!");
        }
    }

    void OnCardDataLoaded(AsyncOperationHandle<IList<ActionCardData>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardList = handle.Result;
            foreach (ActionCardData data in cardList)
            {
                Debug.Log($"이름: {data.cardName}, 값: {data.className}");
            }
        }
        else
        {
            Debug.LogError("Addressables 로드 실패!");
        }
    }
}
