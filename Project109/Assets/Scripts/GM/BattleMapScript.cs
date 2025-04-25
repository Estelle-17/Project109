using System.ComponentModel;
using UnityEngine;

public class BattleMapScript : MonoBehaviour
{
    private static BattleMapScript instance = null;

    public int[,] map = new int[5,5];

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static BattleMapScript Instance
    {
        get 
        {
            if(instance == null)
            {
                return null;
            }
            return instance; 
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
