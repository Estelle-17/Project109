using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[CSharpCallLua]
public delegate bool CheckSelectable(
    Choice_UseItem item,
    Player player,
    Character character,
    List<Card> card,
    RelicManager relicManager
    );

public class EventHandler : MonoBehaviour, IInteractable
{
    [SerializeField]
    EventData eventData;
    private Dictionary<string, EventStageData> eventStages = new Dictionary<string, EventStageData>();  //스테이지 데이터 저장

    public GameObject eventUICanvasPrefab;
    public EventDescriptionScript eventDescription;

    private LuaEnv luaEnv;
    private CheckSelectable isChoiceCanSelectable;
    private string luaChoiceSelectableScript;

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
        if (isChoiceCanSelectable == null)
        {
            Debug.LogError("XLua : 'IsChoiceCanSelectable' function not found!");
        }
    }
    public void SetEventData(EventData newEventData)
    {
        eventData = newEventData;

        //새로운 이벤트 데이터 등록 시 알맞은 Npc오브젝트 생성
        InstantiateEventNpc();

        //빠른 데이터 탐색을 위해 Dictionary에 저장
        foreach (EventStageData stageData in eventData.stages)
        {
            eventStages.Add(stageData.stageID, stageData);
        }
    }

    public void UpdateEventDescription(string currentStageID)
    {
        if (eventData == null || eventDescription == null)
        {
            Debug.LogWarning("Failed to load EventData or EventDescription!");
            return;
        }
        if (eventStages.Count == 0)
        {
            Debug.LogWarning("Failed to load EventStages!");
            return;
        }

        EventStageData stageData = eventStages[currentStageID];

        eventDescription.SetDescription(stageData.stageDescription);

        //이전에 만들어진 선택지들 제거
        eventDescription.ClearChoiceButton();

        //이벤트에 맞는 선택지 추가
        int buttonIndex = 0;
        foreach (Choice_Data choice in stageData.choices)
        {
            //선택 시 랜덤으로 사용될 카드, 유물 선택
            if (RunManager.instance != null && RunManager.instance.player != null)
            {
                if (RunManager.instance.player.deck.GetCards().Count > 0)
                {
                    choice.randomLoseCard.Add(RunManager.instance.player.deck.GetRandomCard());
                }
                if (RunManager.instance.player.relicManager.GetRelics().Count > 0)
                {
                    choice.randomLoseRelic.Add(RunManager.instance.player.GetRandomRelic().Data);
                }
            }

            Button button = eventDescription.CreateChoiceButton(choice.description + "\n" + makeEventDescription.MakeChoiceDescription(choice));

            //각 선택지에서 소비되는 스탯들을 비교하여 현재 선택지를 선택할 수 있는지 확인
            foreach (Choice_UseItem item in choice.useItems)
            {
                if (RunManager.instance != null && RunManager.instance.player != null)
                {
                    bool result = isChoiceCanSelectable(item,
                                                        RunManager.instance.player,
                                                        RunManager.instance.player.character,
                                                        RunManager.instance.player.deck.GetCards(),
                                                        RunManager.instance.player.relicManager);
                    if (!result)
                    {
                        button.interactable = false;
                    }
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

        //처음으로 UI 설정이 진행되었다면 오브젝트 비활성화
        if (stageData.stageID == "START")
        {
            eventDescription.gameObject.SetActive(false);
        }
    }

    public void InstantiateEventNpc()
    {
        if (AssetCacheManager.instance.TryGetModel(eventData.eventObjectPath, out GameObject npcPrefab))
        {
            Debug.Log($"Found Model from Addressable: {eventData.eventObjectPath}");
            GameObject model = Instantiate(npcPrefab, this.transform);
            if (model == null)
            {
                Debug.LogWarning("Failed to Instantiate Event NPC Model!");
                return;
            }
            model.tag = "EventNPC";
            ChangeAllLayer(model, LayerMask.NameToLayer("NPC"));
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

        foreach (Transform child in model.transform)
        {
            ChangeAllLayer(child.gameObject, layer);
        }
    }

    public void EnableEventDescriptionUI()
    {
        if (eventDescription != null)
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

        choiceHandler.CheckChoiceData(choice_Data, this);
    }

    private void OnDestroy()
    {
        if (luaEnv != null)
        {
            if (isChoiceCanSelectable != null)
            {
                isChoiceCanSelectable = null;
            }

            //System.GC.Collect();

            //Lua GC를 강제로 여러 번 실행하여 정리
            for (int i = 0; i < 1; i++)
            {
                //luaEnv.Tick();
            }

            //제거 시 LuaEnv 해제
            //luaEnv.Dispose();
            luaEnv = null;
        }
    }

    public bool RequiresCameraFocus => true;

    public void OnInteract()
    {
        EnableEventDescriptionUI();
    }
}
