using UnityEngine;

namespace GameItem.Types
{
    public enum RewardItemType
    {
        Card,
        Relic
    }

    public enum ItemRewardUIType
    {
        Gold,
        MemorySharp,
        Card,
        Relic
    }

    public enum RandomCardPickupType
    {
        Common,
        Rare,
        Unique,
        CommonToUnique,
        CommonToRare,
        RareToUnique
    }

    public enum RandomRelicPickupType
    {
        Common,
        Rare,
        Unique,
        CommonToUnique,
        CommonToRare,
        RareToUnique,
        Boss
    }

    public enum EvolveType
    {
        Cost,
        Amount,
        Custom
    }
}

namespace Card.Types
{
    public enum EffectType  //효과 타입
    {
        None,
        Damage,
        Heal,
        Shield,
        TemporaryShield,
        Buff,
        Debuff,
        Move
    }

    public enum BuffType    //버프 타입
    {
        None,
        Strength,       //힘
        Armor,          //방어
        Frenzy          //광분
    }

    public enum DebuffType    //디버프 타입
    {
        None,
        Bleeding,       //출혈
        Poison,         //독
        DeadlyPoison,   //맹독
        Debilitate,     //쇠약
        Weaken,         //약화
    }

    public enum FeatureType //카드 특징 타입
    {
        None,
        Start_Action,    //전투 시작 시 처음으로 뽑는 카드에 포함
        Vanguard,        //턴의 가장 처음으로 사용되면 보너스 효과
        Finale,          //턴이 종료되었을 때 손에 있을 경우 남은 스태미너를 전부 소모하여 스태미너 X당 한 번 씩 사용됨
        Echo,            //이번 턴에 카드 사용 시 연속으로 사용 가능하지만 사용 할 때마다 비용 증가
        Single_use,      //이번 전투에서 한 번만 사용 가능
        Destroy,         //카드를 사용한 후 덱에서 없어짐
        Divide,          //카드를 사용하면 두 장으로 늘어난 후 카드 무덤으로 이동
        Chain,           //스태미너를 N만큼 더 소모한다면 추가로 효과가 발동함
        Unavailable      //카드를 직접 사용할 수 없음
    }

    public enum TargetType  //공격 대상 타입
    {
        All,
        Target,
        Random
    }
}

public class Enums
{
    
}
