using System.Collections.Generic;
using UnityEngine;

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
    public BattleMapManager ActionCard_EffectArea;
    public ExploreUI currentExploreUI;

    public LoadMapHandler loadMapHandler;

    //맵 이동 시 제거할 오브젝트 모음
    public List<GameObject> currentSpawnEnemyList;
    public List<GameObject> currentSpawnNPCList;
    public List<GameObject> currentSpawnUIList;

    void Start()
    {
        loadMapHandler = GetComponent<LoadMapHandler>();
        currentMapName = "LostTemple";
        playerStat = new PlayerStat();
        playerStat.inGame_Currency = 0;
        playerStat.mapFloorCheck_Length = 3;
        AddInGame_Currency(0);
    }

    public PlayerStat GetPlayerStat() {  return playerStat; }

    public void AddInGame_Currency(int amount)
    {
        playerStat.inGame_Currency += amount;
        GameUIManager.instance.UpdateInGameCurrencyText(playerStat.inGame_Currency.ToString());
    }
}
