using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using XLua;
using System.Collections.Generic;

[CSharpCallLua]
public delegate bool CheckSelectable(
    Choice_UseItem item,
    PlayerStat player,
    CharacterStat character,
    List<ActionCardData> card,
    List<RelicData> relic
    );

public class EventHandler : MonoBehaviour
{
    [SerializeField]
    EventData eventData;

    public GameObject eventUICanvasPrefab;
    public EventDescriptionScript eventDescription;

    private LuaEnv luaEnv;
    private CheckSelectable isChoiceCanSelectable;
    public string luaChoiceSelectableScript;

    ChoiceDescriptionHandler makeEventDescription;
    ChoiceHandler choiceHandler;

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

        makeEventDescription = GetComponent<ChoiceDescriptionHandler>();
        choiceHandler = GetComponent<ChoiceHandler>();

        luaEnv = new LuaEnv();

        //알맞은 스크립트 주소 입력
        luaChoiceSelectableScript = "Assets/Scripts/Lua/IsChoiceCanSelectable.lua";
        string luaString = File.ReadAllText(luaChoiceSelectableScript);
        luaEnv.DoString(luaString);

        isChoiceCanSelectable = luaEnv.Global.Get<CheckSelectable>("IsChoiceCanSelectable");
        if(isChoiceCanSelectable == null)
        {
            Debug.LogError("XLua : 'IsChoiceCanSelectable' function not found!");
        }
    }
    public void SetEventData(EventData newEventData)
    {
        eventData = newEventData;

        //새로운 이벤트 데이터 등록 시 알맞은 Npc오브젝트 생성
        InstantiateEventNpc();
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
            //선택 시 랜덤으로 사용될 카드, 유물 선택
            if (CardDeckManager.instance.GetCardDeckList().Count > 0)
            {
                choice.randomLoseCard = CardDeckManager.instance.GetRandomCard();
            }
            if (RelicManager.instance.GetRelicList().Count > 0)
            {
                choice.randomLoseRelic = RelicManager.instance.GetRandomRelic();
            }

            Button button = eventDescription.CreateChoiceButton(choice.description + "\n" + makeEventDescription.MakeChoiceDescription(choice));

            //현재 선택지를 선택할 수 있는지 확인


            //각 선택지에서 소비되는 스탯들을 비교
            foreach (Choice_UseItem item in choice.useItems)
            {
                bool result = isChoiceCanSelectable(item,
                                                    GameManager.instance.playerStat,
                                                    GameManager.instance.currentCharacter.GetCharacterStat(),
                                                    CardDeckManager.instance.GetCardDeckList(),
                                                    RelicManager.instance.GetRelicList());
                if (!result)
                {
                    button.interactable = false;
                }
            }

            int capturedIndex = buttonIndex;    //버튼 순서 캡쳐
            if (button != null)
            {
                button.onClick.AddListener(() =>    //선택지 클릭 시 특정 결과 진행
                {
                    Debug.Log($"Button Index {capturedIndex} clicked");
                    CheckChoiceResult(capturedIndex, choice);
                });
            }
            buttonIndex++;
        }

        //UI 설정 후 오브젝트 비활성화
        eventDescription.gameObject.SetActive(false);
    }

    public void InstantiateEventNpc()
    {
        if(AssetCacheManager.instance.TryGetModel(eventData.eventObjectPath, out GameObject npcPrefab))
        {
            Debug.Log($"Found Model from Addressable: {eventData.eventObjectPath}");
            GameObject model = Instantiate(npcPrefab, this.transform);
            ChangeAllLayer(model, LayerMask.NameToLayer("EventNPC"));
        }
        else
        {
            Debug.Log($"Found Failed from Addressable: {eventData.eventObjectPath}");
        }
    }
    
    //특정 오브젝트의 모든 layer 변경
    void ChangeAllLayer(GameObject model, int layer)
    {
        model.layer = layer;

        foreach(Transform child in model.transform)
        {
            ChangeAllLayer(child.gameObject, layer);
        }
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

    void CheckChoiceResult(int index, Choice_Data choice_Data)
    {
        Debug.Log($"Button Index {index} clicked");
        Debug.Log($"UseItems : {eventData.choices[index].useItems.Count}, GetItems : {eventData.choices[index].getItems.Count}");

        choiceHandler.CheckChoiceData(choice_Data);

        eventDescription.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if(luaEnv != null)
        {
            //Lua GC를 강제로 여러 번 실행하여 모든 C# 참조 해제
            for(int i = 0; i < 3; i++)
            {
                luaEnv.Tick();
            }

            //제거 시 LuaEnv 해제
            luaEnv.Dispose();
            luaEnv = null;
        }
    }
}
