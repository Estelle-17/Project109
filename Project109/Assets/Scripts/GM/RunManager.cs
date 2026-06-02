using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoadMapHandler))]
public class RunManager : MonoBehaviour
{
    public static RunManager instance { get; private set; }

    public BattleManager battleManager { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // DontDestroyOnLoad(this.gameObject);
            battleManager = new();
            currentMap = new MapManager();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    private Character _startingCharacter;

    [Header("Player")]
    //플레이어 관리
    public Player player;
    public PlayerBattleController playerBattleController;
    public PlayerExploreController playerExploreController;

    private ICharacterController _activePlayerController;
    public ICharacterController activePlayerController => _activePlayerController;

    [Header("Map Settings")]
    [SerializeField] private MapPrefabs mapPrefabs;
    public MapPrefabs MapPrefabs => mapPrefabs;

    [Header("Map")]
    //맵 관련 변수
    public string currentMapName;
    public int currentStageLevel = 0;
    public int currentExploreMapFloor = 0;
    public IncountNode currentIncountNode;
    public IncountNode beforeIncountNode;
    public MapManager currentMap = new();
    public ExploreUI currentExploreUI;

    public LoadMapHandler loadMapHandler;

    [Header("Temp")]
    public PlayerStat testPlayerStat;

    void Start()
    {
        loadMapHandler = GetComponent<LoadMapHandler>();
        currentMapName = "Temple";

        player = new Player(_startingCharacter);
        playerBattleController = new PlayerBattleController(player);
        playerExploreController = new PlayerExploreController(player);
        _activePlayerController = playerExploreController; // 기본적으로 탐색 컨트롤러가 액티브

        // 임시코드: 새 StarterKit 시스템으로 캐릭터 및 시작 카드/유물/효과 초기화
        CharacterData characterData;

        AssetCacheManager.instance.TryGetCharacter("전투광", out characterData);
        if (characterData != null)
        {
            // StarterKitDatabase에서 warrior_starter를 찾아와 적용
            if (ModLoader.Instance.StarterKitDatabase.TryGetValue("warrior_starter", out StarterKitData kitData))
            {
                player.playerStat.inGame_Currency_Gold = kitData.startGold;

                // 시작 키트(무기)의 스탯으로 플레이어 캐릭터 스탯 최종 초기화
                if (kitData.characterStat != null)
                {
                    playerBattleController.controlledCharacter.InitializeStat(kitData.characterStat);
                }
                else
                {
                    Debug.LogError("[RunManager] 시작 키트에 'characterStat' 스탯 정보가 정의되어 있지 않습니다.");
                }

                foreach (string cardId in kitData.startCards)
                {
                    if (ModLoader.Instance.CardDatabase.TryGetValue(cardId, out CardData cardData))
                    {
                        player.deck.AddCard(cardData);
                    }
                    else
                    {
                        Debug.LogWarning($"[RunManager] 시작 키트의 '{cardId}' 카드가 CardDatabase에 존재하지 않습니다.");
                    }
                }

                foreach (string relicId in kitData.startRelics)
                {
                    player.AddRelic(relicId);
                }

                foreach (var effInfo in kitData.startEffects)
                {
                    if (ModLoader.Instance.EffectDatabase.TryGetValue(effInfo.effectName, out EffectData effectData))
                    {
                        EventStructs.EffectInfo info = new EventStructs.EffectInfo(
                            player.character,
                            player.character,
                            new Effect(effectData, effectData.luaPrototype),
                            effInfo.stack,
                            effInfo.duration
                        );
                        player.character.TakeEffect(info);
                    }
                }
            }
            else
            {
                Debug.LogError("[RunManager] 'warrior_starter' 시작 키트를 StarterKitDatabase에서 찾을 수 없습니다.");
            }

            player.deck.RequestAllCardRefresh();
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

        // TopHUDPanel에 플레이어 데이터 바인딩 시도 (UIManager 로딩 시점 대비)
        if (UIManager.instance != null)
        {
            UIManager.instance.BindPlayerToHUD(player);
        }
    }

    private void Update()
    {
        if (currentMap.currentMapState == MapState.Battle)
        {
            battleManager?.Update(Time.deltaTime);
        }
    }

    public void InitRun()
    {
        playerBattleController.OnTurnStart();

        if (UIManager.instance != null)
        {
            UIManager.instance.DestroyExploreMap();
            UIManager.instance.GetOrSpawnExploreMap();
        }
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

    public void OnMapStateChanged(MapState newState)
    {
        // 이전 탐색 컨트롤러 해제
        if (_activePlayerController == playerExploreController && playerExploreController != null)
        {
            playerExploreController.Deactivate();
        }

        if (newState == MapState.Battle)
        {
            _activePlayerController = playerBattleController;
        }
        else
        {
            _activePlayerController = playerExploreController;
            if (playerExploreController != null)
            {
                playerExploreController.Activate();
            }
        }
    }

    void OnDestroy()
    {
        if (playerExploreController != null)
        {
            playerExploreController.Deactivate();
        }

        if (instance != null)
        {
            instance = null;
        }
    }
}
