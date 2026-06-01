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
        loadMapHandler = GetComponent<LoadMapHandler>();
        currentMapName = "LostTemple";

        player = new Player(_startingCharacter);
        playerCharacterController = new PlayerCharacterController(player);

        // 임시코드: 새 StarterKit 시스템으로 캐릭터 및 시작 카드/유물/효과 초기화
        CharacterData characterData;

        AssetCacheManager.instance.TryGetCharacter("전투광", out characterData);
        if (characterData != null)
        {
            playerCharacterController.controlledCharacter.InitializeStat(characterData.characterStat);

            // StarterKitDatabase에서 warrior_starter를 찾아와 적용
            if (ModLoader.Instance.StarterKitDatabase.TryGetValue("warrior_starter", out StarterKitData kitData))
            {
                player.playerStat.inGame_Currency_Gold = kitData.startGold;

                if (kitData.startMaxHpBonus > 0 && player.character.curCharacterStat != null)
                {
                    player.character.curCharacterStat.maxHealth += kitData.startMaxHpBonus;
                    player.character.curHealth = player.character.curCharacterStat.maxHealth;
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
                Debug.LogWarning("[RunManager] 'warrior_starter' 시작 키트를 StarterKitDatabase에서 찾을 수 없습니다.");
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
