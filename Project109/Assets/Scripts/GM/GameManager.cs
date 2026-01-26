using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    None,
    Battle,
    Event
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    //현재 맵 진행 상태
    public MapState currentMapState;

    //플레이어 관련 스탯
    private PlayerStat playerStat;
    public CharacterBase currentCharacter;

    //맵 관련 변수
    public string currentMapName;
    public int currentStageLevel = 0;
    public int currentExploreMapFloor = 0;
    public IncountNode currentIncountNode;
    public IncountNode beforeIncountNode;
    public BattleMapManager currentMap;
    public ExploreUI currentExploreUI;

    public LoadMapHandler loadMapHandler;

    //맵 이동 시 제거할 오브젝트 모음
    public List<GameObject> currentSpawnEnemyList;
    public List<GameObject> currentSpawnNPCList;
    public List<GameObject> currentSpawnUIList;

    void Start()
    {
        currentMapState = MapState.None;

        loadMapHandler = GetComponent<LoadMapHandler>();
        currentMapName = "LostTemple";
        playerStat = new PlayerStat();
        playerStat.inGame_Currency_Gold = 0;
        playerStat.mapFloorCheck_Length = 3;
        playerStat.Upgrade_MasteryPoint_Value = 500;
        AddInGame_Currency(CurrencyType.Gold, 0);
        AddInGame_Currency(CurrencyType.MemorySharp, 0);

        //불러온 아이템들 세분화 진행
        GameItemRewardManager.instance.UpdateItemList();
    }

    public PlayerStat GetPlayerStat() {  return playerStat; }

    public void AddInGame_Currency(CurrencyType type, int amount)
    {
        switch (type)
        {
            case CurrencyType.Gold:
                playerStat.inGame_Currency_Gold += amount;
                GameUIManager.instance.UpdateGoldText(playerStat.inGame_Currency_Gold.ToString());
                break;
            case CurrencyType.MemorySharp:
                playerStat.inGame_Currency_MemorySharp += amount;
                GameUIManager.instance.UpdateMemorySharpText(playerStat.inGame_Currency_MemorySharp.ToString());
                break;
            default:
                break;
        }
    }
}
