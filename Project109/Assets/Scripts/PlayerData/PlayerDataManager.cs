using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance { get; private set; }

    public PlayerStat playerStat;

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


}
