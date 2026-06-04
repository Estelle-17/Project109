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
    public MapManager currentMap;
    public ExploreUI currentExploreUI;

    public LoadMapHandler loadMapHandler;

    [Header("Temp")]
    public PlayerStat testPlayerStat;

    [Header("Lobby Settings")]
    [SerializeField] private string _selectedStarterKitId = "warrior_starter";

    public void SetTempStarterKit(string kitId)
    {
        _selectedStarterKitId = kitId;
    }

    public void EnterDungeonRun()
    {
        if (player == null) return;

        // 1. 기존 플레이어 덱 및 유물 상태 완전 청소
        if (player.deck != null) player.deck.Clear();
        if (player.relicManager != null) player.relicManager.ClearRelics();

        // 2. 임시 보관되어 있던 로드아웃 ID로 시작 장비 지급
        ApplyStarterKit(_selectedStarterKitId);

        // 3. 던전 씬 진입 처리
        currentStageLevel = 1;
        currentExploreMapFloor = 1;

        if (loadMapHandler != null)
        {
            loadMapHandler.StartFadeInOut(true);
        }
    }

    public void ApplyStarterKit(string loadoutId)
    {
        if (player == null) return;

        // 기존 덱과 유물 초기화
        if (player.deck != null) player.deck.Clear();
        if (player.relicManager != null) player.relicManager.ClearRelics();

        // 캐릭터 에셋 기본 스탯 로드
        CharacterData characterData;
        AssetCacheManager.instance.TryGetCharacter("전투광", out characterData);

        if (ModLoader.Instance.StarterKitDatabase.TryGetValue(loadoutId, out StarterKitData kitData))
        {
            player.playerStat.inGame_Currency_Gold = kitData.startGold;

            if (kitData.characterStat != null && playerBattleController != null && playerBattleController.controlledCharacter != null)
            {
                playerBattleController.controlledCharacter.InitializeStat(kitData.characterStat);
            }
            else
            {
                Debug.LogError("[RunManager] 시작 키트에 'characterStat' 스탯 정보가 정의되어 있지 않거나 캐릭터 컨트롤러가 없습니다.");
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

            player.deck.RequestAllCardRefresh();
            Debug.Log($"[RunManager] '{kitData.displayName}'({loadoutId}) 로드아웃이 플레이어에게 성공적으로 적용되었습니다.");
        }
        else
        {
            Debug.LogError($"[RunManager] '{loadoutId}' 시작 키트를 StarterKitDatabase에서 찾을 수 없습니다.");
        }
    }

    void Start()
    {
        loadMapHandler = GetComponent<LoadMapHandler>();
        currentMapName = "LostTemple";

        player = new Player(_startingCharacter);
        playerBattleController = new PlayerBattleController(player);
        playerExploreController = new PlayerExploreController(player);
        _activePlayerController = playerExploreController; // 기본적으로 탐색 컨트롤러가 액티브

        // 폴백 시작 장비 적용 (로비를 거치지 않고 바로 시작하는 씬 진입용)
        ApplyStarterKit(_selectedStarterKitId);

        player.playerStat.inGame_Currency_Gold = 100;
        player.playerStat.inGame_Currency_MemorySharp = 1;

        if (GameItemRewardManager.instance != null)
        {
            GameItemRewardManager.instance.SubscribeToPlayerEvents();
        }

        //불러온 아이템들 세분화 진행
        GameItemRewardManager.instance.UpdateItemList();
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
        if (playerBattleController != null)
        {
            playerBattleController.OnTurnStart();
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadRun()
    {
        RunSaveData data = SaveSystem.LoadGameData();
        if (data == null)
        {
            Debug.LogWarning("[RunManager] 로드할 게임 데이터가 존재하지 않습니다.");
            return;
        }

        // 1. 기본 게임 메타 복구
        currentMapName = data.currentMapName;
        currentStageLevel = data.currentStageLevel;
        currentExploreMapFloor = data.currentExploreMapFloor;

        if (player == null)
        {
            player = new Player(_startingCharacter);
        }

        if (player.playerStat == null)
        {
            player.playerStat = new PlayerStat();
        }

        player.playerStat.inGame_Currency_Gold = data.playerGold;
        player.playerStat.inGame_Currency_MemorySharp = data.playerMemorySharp;

        if (player.character != null)
        {
            player.character.curHealth = data.playerCurHealth;
        }

        // 2. 플레이어 덱 복구
        if (player.deck != null)
        {
            player.deck.Clear(); // 기존 덱 카드 초기화

            // 이제 세이브로부터 카드 생성 및 속성 복구
            foreach (var cardEntry in data.playerDeckCards)
            {
                if (ModLoader.Instance.CardDatabase.TryGetValue(cardEntry.cardName, out CardData cardData))
                {
                    Card newCard = player.deck.AddCard(cardData);
                    if (newCard != null)
                    {
                        // AddMastery(upgrade.masteryId)를 count 번만큼 호출하여 순차적으로 마스터리 복구
                        if (cardEntry.masteryUpgrades != null)
                        {
                            foreach (var upgrade in cardEntry.masteryUpgrades)
                            {
                                for (int i = 0; i < upgrade.count; i++)
                                {
                                    newCard.AddMastery(upgrade.masteryId);
                                }
                            }
                        }
                        // AddMastery 수행 시 XP 차감 연산이 들어가므로, 최종 마스터리 XP를 세이브값으로 명확히 덮어씌워 줍니다.
                        newCard.currentMasteryXP = cardEntry.currentMasteryXP;
                    }
                }
            }
            player.deck.RequestAllCardRefresh();
        }

        // 3. 유물 복구
        if (player.relicManager != null)
        {
            player.relicManager.ClearRelics();
            foreach (var relicName in data.playerRelicNames)
            {
                player.AddRelic(relicName);
            }
        }

        Debug.Log("[RunManager] 세이브 파일로부터 이전 세션 데이터를 완벽히 복구했습니다.");

        if (loadMapHandler != null)
        {
            loadMapHandler.StartFadeInOut(true);
        }
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

            // 전투 매니저 시작!
            List<ICharacterController> players = new List<ICharacterController> { playerBattleController };
            List<ICharacterController> enemies = new List<ICharacterController>();
            if (currentMap != null && currentMap.currentEnemyControllers != null)
            {
                enemies.AddRange(currentMap.currentEnemyControllers);
            }

            if (battleManager != null)
            {
                battleManager.InitBattle(players, enemies);
            }

            if (playerBattleController != null)
            {
                playerBattleController.OnBattleStart();
            }

            foreach (var enemy in enemies)
            {
                if (enemy is NPCUnitController enemyCtrl)
                {
                    enemyCtrl.EvaluateNextIntent();
                }
            }
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
