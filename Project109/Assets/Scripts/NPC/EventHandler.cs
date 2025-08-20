using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    [SerializeField]
    EventData eventData;

    public GameObject eventUICanvasPrefab;
    public EventDescriptionScript eventDescription;

    MakeEventDescription makeEventDescription;

    public void SetEventData(EventData newEventData)
    {
        eventData = newEventData;
    }

    private void Awake()
    {
        if (eventUICanvasPrefab != null)
        {
            eventDescription = GameObject.Instantiate(eventUICanvasPrefab).GetComponent<EventDescriptionScript>();
        }
        else
        {
            Debug.LogWarning("EventUICanvasPrefab이 존재하지 않습니다.");
        }

        makeEventDescription = GetComponent<MakeEventDescription>();
    }

    public void UpdateEventDescription()
    {
        if (eventData == null || eventDescription == null)
        {
            Debug.LogWarning("Failed to load EventData or EventDescription!");
            return;
        }

        eventDescription.SetDescription(eventData.eventDescription);

        //이벤트에 맞는 선택지 추가
        int buttonIndex = 0;
        foreach (Choice_Data choice in eventData.choices)
        {
            Button button = eventDescription.CreateChoiceButton(choice.description + "\n" + makeEventDescription.MakeChoiceDescription(choice));

            int capturedIndex = buttonIndex;    //버튼 순서 캡쳐
            if (button != null)
            {
                button.onClick.AddListener(() =>    //선택지 클릭 시 특정 결과 진행
                {
                    Debug.Log($"Button Index {capturedIndex} clicked");
                    CheckChoiceResult(capturedIndex);
                });
            }
            buttonIndex++;
        }

        //UI 설정 후 오브젝트 비활성화
        eventDescription.gameObject.SetActive(false);
    }

    public void EnableEventDescriptionUI()
    {
        if(eventDescription != null)
        {
            eventDescription.UIActive();
        }
    }

    public void DisableEventDescriptionUI()
    {
        if (eventDescription != null)
        {
            eventDescription.UIDeactive();
        }
    }

    void CheckChoiceResult(int index)
    {
        Debug.Log($"Button Index {index} clicked");
        Debug.Log($"UseItems : {eventData.choices[index].useItems.Count}, GetItems : {eventData.choices[index].getItems.Count}");

        eventDescription.gameObject.SetActive(false);
    }
}
