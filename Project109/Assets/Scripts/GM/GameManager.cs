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

    public int currentStageLevel = 0;
    public int currentExploreMapFloor = 0;
    public int checkMapNodeFloorLength = 3;
    public IncountNode currentIncountNode;

    void Start()
    {

    }

}
