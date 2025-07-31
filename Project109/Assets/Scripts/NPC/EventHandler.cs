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
            Debug.LogWarning("Prefab이 존재하지 않습니다.");
        }
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
        foreach (Choice_RelicAndCard choice in eventData.choices)
        {
            Button button = eventDescription.CreateChoiceButton(choice.choiceDescription);

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
            //eventDescription.gameObject.SetActive(true);
        }
    }

    public void DisableEventDescriptionUI()
    {
        if (eventDescription != null)
        {
            eventDescription.UIDeactive();
            //eventDescription.gameObject.SetActive(false);
        }
    }

    void CheckChoiceResult(int index)
    {
        Debug.Log($"Button Index {index} clicked");
        Debug.Log($"EffectType : {eventData.choices[index].effectType}, RelicName : {eventData.choices[index].getRelicName}");
    }
}
