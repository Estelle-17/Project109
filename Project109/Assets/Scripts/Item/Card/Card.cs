using System;
using System.Collections.Generic;
using XLua;

public class CardBase : IDescribable
{
    // 정적 카드 데이터 템플릿
    public CardData cardData { get; private set; }

    // 이 카드를 소유한 캐릭터

    public Character owner { get; private set; }

    #region Runtime State Fields (런타임 상태 데이터)

    // 런타임 고유 식별 ID
    public int runtimeID { get; private set; }

    // 전투 중 실시간 변동이 반영되는 카드 비용 (기본값: Data.stamina)
    public int currentCost { get; set; }

    // 전투 중 임시로 생성된 카드인지 여부
    public bool isTemporary { get; set; }

    // 현재 누적 숙련도 포인트
    public float currentMasteryPoint { get; set; }

    // 현재 마스터리 레벨
    public int masteryLevel { get; set; }

    // 이 카드 인스턴스가 획득한 세부 마스터리 업그레이드 현황 (마스터리ID -> 강화횟수)
    public Dictionary<string, int> activeMasteryUpgrades { get; private set; } = new Dictionary<string, int>();

    #endregion

    private readonly LuaTable luaTable;
    private readonly List<object> activeProxies = new List<object>();

    public CardBase(CardData data, Character owner, LuaTable luaLogic, int runtimeID)
    {
        this.cardData = data;
        this.owner = owner;
        this.luaTable = luaLogic;
        this.runtimeID = runtimeID;

        // 초기 런타임 비용 설정

        this.currentCost = data != null ? data.stamina : 0;

        // 1. Lua 측 OnInit 함수 호출 (초기화)
        var luaOnInit = luaTable?.Get<Action<LuaTable, CardBase, Character>>("OnInit");
        luaOnInit?.Invoke(luaTable, this, owner);

        // 2. 캐릭터 이벤트 자동 바인딩
        if (owner != null && owner.eventBus != null)
        {
            LuaEventBinder.BindCharacterEvents(luaTable, owner.eventBus, activeProxies);
        }
    }

    public void Dispose()
    {
        // 1. Lua 측 OnRemoved 함수 호출
        var luaOnRemoved = luaTable?.Get<Action<LuaTable, CardBase, Character>>("OnRemoved");
        luaOnRemoved?.Invoke(luaTable, this, owner);

        // 2. 캐릭터 이벤트 버스 구독 해제
        if (owner != null && owner.eventBus != null)
        {
            LuaEventBinder.UnbindCharacterEvents(owner.eventBus, activeProxies);
        }

        // 3. 리소스 정리
        activeProxies.Clear();
        luaTable?.Dispose();
    }

    /// <summary>
    /// 카드 인스턴스 자체에 기록된 마스터리 업그레이드 횟수를 기반으로 최종 수치를 계산합니다.
    /// </summary>
    public float GetEffectiveValue(string valueKey)
    {
        if (cardData == null) return 0f;

        float val = 0f;
        if (cardData.baseValues != null && cardData.baseValues.TryGetValue(valueKey, out float baseVal))
        {
            val = baseVal;
        }

        if (activeMasteryUpgrades != null)
        {
            foreach (var upgrade in activeMasteryUpgrades)
            {
                string masteryId = upgrade.Key;
                int level = upgrade.Value;

                if (cardData.masteryUpgrades != null && cardData.masteryUpgrades.TryGetValue(masteryId, out var modifierDict))
                {
                    if (modifierDict != null && modifierDict.TryGetValue(valueKey, out float modifier))
                    {
                        val += modifier * level;
                    }
                }
            }
        }
        return val;
    }

    /// <summary>
    /// 이 카드 인스턴스가 특정 마스터리 업그레이드를 몇 번 획득했는지 조회합니다.
    /// </summary>
    public int GetMasteryLevel(string masteryId)
    {
        if (activeMasteryUpgrades != null && activeMasteryUpgrades.TryGetValue(masteryId, out int level))
        {
            return level;
        }
        return 0;
    }

    /// <summary>
    /// YAML description 템플릿을 Lua에 넘겨 토큰 치환을 위임합니다.
    /// </summary>
    public string GetDescription()
    {
        string template = cardData?.description ?? string.Empty;

        var luaFunc = luaTable?.Get<Func<LuaTable, CardBase, string, string>>("GetDescription");
        if (luaFunc != null)
            return luaFunc(luaTable, this, template);

        return template;
    }
}
