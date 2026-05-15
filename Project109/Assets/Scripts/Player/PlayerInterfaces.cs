using EventStructs;

public interface IPlayerEvent {}

#region ?좊Ъ 愿???명꽣?섏씠??
public interface IOnAddRelic : IPlayerEvent { void OnAddRelic(RelicData relicData); }
public interface IOnRemoveRelic : IPlayerEvent { void OnRemoveRelic(RelicData relicData); }
#endregion

#region 移대뱶 愿???명꽣?섏씠??
public interface IOnAddCard : IPlayerEvent { void OnAddCard(ActionCardData cardData); }
public interface IOnRemoveCard : IPlayerEvent { void OnRemoveCard(ActionCardData cardData); }
#endregion

#region ?ы솕 愿???명꽣?섏씠??
public interface IOnAddGold : IPlayerEvent { void OnAddGold(int gold); }
public interface IOnRemoveGold : IPlayerEvent { void OnRemoveGold(int gold); }

public interface IOnAddMemorySharp : IPlayerEvent { void OnAddMemorySharp(int memorySharp); }
public interface IOnRemoveMemorySharp : IPlayerEvent { void OnRemoveMemorySharp(int memorySharp); }
#endregion

