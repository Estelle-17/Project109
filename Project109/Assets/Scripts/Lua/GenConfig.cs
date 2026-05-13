using UnityEngine;
using System;
using System.Collections.Generic;
using XLua;

public static class GenConfig
{
    //XLua가 Lua에서 c#을 호출할 때 필요한 바인딩 코드를 생성할 타입 목록
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>()
    {
        typeof(PlayerStat),
        typeof(CharacterStat),
        typeof(ActionCardData),
        typeof(RelicData),
        typeof(Choice_UseItem),
        typeof(EffectBase),
        typeof(EventStructs.DamageInfo),
        typeof(EventStructs.HealInfo),
        typeof(EventStructs.StaminaInfo),
        typeof(EventStructs.ShieldInfo),
        typeof(EventStructs.CardInfo),
        typeof(EventStructs.MoveInfo),
        typeof(EventStructs.EffectInfo),
        typeof(EventStructs.DamageFlag),
        typeof(EventStructs.HealFlag),
        typeof(EventStructs.StaminaFlag),
        typeof(EventStructs.ShieldFlag),
        typeof(EventStructs.CardFlag),
        typeof(EventStructs.MoveFlag),
        typeof(EventStructs.EffectFlag),

        typeof(List<ActionCardData>),
        typeof(List<RelicData>),

        typeof(List<int>),
        typeof(List<float>),
        typeof(List<string>),
    };
}
