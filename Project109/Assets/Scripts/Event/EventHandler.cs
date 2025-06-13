using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    public EventData eventData;

    public GameObject eventUIPrefab;
    public EventDescriptionScript eventDescription;

    void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        if(canvas != null)
        {
            eventDescription = GameObject.Instantiate(eventUIPrefab, canvas.transform).GetComponent<EventDescriptionScript>();
        }
        else
        {
            Debug.LogWarning("MainCanvas가 탐색되지 않았습니다.");
        }
        

        if (eventData != null && eventDescription != null)
        {
            eventDescription.SetDescription(eventData.eventDescription);

            //이벤트에 맞는 선택지 추가
            int buttonIndex = 0;
            foreach(Choice_RelicAndCard choice in eventData.choices)
            {
                Button button = eventDescription.CreateChoiceButton(choice.choiceDescription);

                int capturedIndex = buttonIndex;    //버튼 순서 캡쳐
                if (button != null)
                {
                    button.onClick.AddListener(() =>
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
    }

    public void EnableEventDescriptionUI()
    {
        if(eventDescription != null)
        {
            eventDescription.gameObject.SetActive(true);
        }
    }

    public void DisableEventDescriptionUI()
    {
        if (eventDescription != null)
        {
            eventDescription.gameObject.SetActive(false);
        }
    }

    void CheckChoiceResult(int index)
    {
        Debug.Log($"Button Index {index} clicked");
        Debug.Log($"EffectType : {eventData.choices[index].effectType}, RelicName : {eventData.choices[index].getRelicName}");
    }
}
