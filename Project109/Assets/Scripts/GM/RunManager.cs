using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    None,
    Battle,
    Event
}

public class RunManager : MonoBehaviour
{
    public static RunManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //현재 맵 진행 상태
    public MapState currentMapState;

    [SerializeField]
    private Character _startingCharacter;

    [Header("Player")]
    //플레이어 관리
    public Player player;
    public PlayerCharacterController playerCharacterController;

    [Header("Map")]
    //맵 관련 변수
    public string currentMapName;
    public int currentStageLevel = 0;
    public int currentExploreMapFloor = 0;
    public IncountNode currentIncountNode;
    public IncountNode beforeIncountNode;
    public MapManager currentMap;
    public ExploreUI currentExploreUI;

    public LoadMapHandler loadMapHandler;

    //맵 이동 시 제거할 오브젝트 모음
    public List<GameObject> currentSpawnEnemyList;
    public List<GameObject> currentSpawnNPCList;
    public List<GameObject> currentSpawnUIList;

    [Header("Temp")]
    public PlayerStat testPlayerStat;

    void Start()
    {
        currentMapState = MapState.None;

        loadMapHandler = GetComponent<LoadMapHandler>();
        currentMapName = "LostTemple";

        player = new Player(_startingCharacter);
        playerCharacterController = new PlayerCharacterController(player);

        // 임시코드
        CharacterData characterData;

        AssetCacheManager.instance.TryGetCharacter("전투광", out characterData);
        if (characterData != null && CardDeckManager.instance != null)
        {
            playerCharacterController.controlledCharacter.InitializeStat(characterData.characterStat);
            foreach (StartCard cards in characterData.startCards)
            {
                AssetCacheManager.instance.TryGetCard(cards.cardName, out ActionCardData cardData);

                for (int i = 0; i < cards.number; i++)
                {
                    ActionCardData tempCardData = cardData;
                    CardDeckManager.instance.AddCard(tempCardData);
                }
            }
            CardDeckManager.instance.RequestAllCardRefresh();
        }
        else
        {
            Debug.LogWarning("Addressable에서 캐릭터 로드 실패");
        }

        player.playerStat.inGame_Currency_Gold = 100;
        player.playerStat.inGame_Currency_MemorySharp = 1;

        if (GameItemRewardManager.instance != null)
        {
            GameItemRewardManager.instance.SubscribeToPlayerEvents();
        }

        //불러온 아이템들 세분화 진행
        GameItemRewardManager.instance.UpdateItemList();
    }

    public void InitRun()
    {
        playerCharacterController.OnTurnStart();

    }

    public void LoadRun()
    {


    }

    public void AddInGame_Currency(CurrencyType type, int amount)
    {
        if (player == null || player.playerStat == null) return;

        switch (type)
        {
            case CurrencyType.Gold:
                player.playerStat.inGame_Currency_Gold += amount;
                break;
            case CurrencyType.MemorySharp:
                player.playerStat.inGame_Currency_MemorySharp += amount;
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        if (instance != null)
        {
            instance = null;
        }
    }
}
