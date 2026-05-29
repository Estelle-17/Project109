using UnityEngine;

namespace GameItem.Types
{
    public enum CardRarity
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Unique = 4
    }

    public enum RelicRarity
    {
        Common = 1,
        Rare = 2,
        Unique = 3,
        Boss = 4
    }

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
        Uncommon,
        Rare,
        Unique,
        CommonToUncommon,
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
}

namespace CardTypes
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



    public enum TargetType  //공격 대상 타입
    {
        Target,
        Area,
        Self,
        All
    }

    public enum CardMasteryType
    {
        UseCard,
        DealDamage,
        GuardDamage,
        KillEnemy,
        Heal,
        DrawCard,
        UpgradeCard
    }
}

public class Enums
{

}
