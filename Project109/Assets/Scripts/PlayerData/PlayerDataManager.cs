using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance { get; private set; }

    private PlayerStat playerStat;

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

    void Start()
    {
        playerStat = new PlayerStat();

        playerStat.reward_Card_Count = 3;
        playerStat.reward_Relic_Count = 3;
    }

    public PlayerStat GetPlayerStat() { return playerStat;}
}
