using UnityEngine;
using System.Collections.Generic;

public class GameItemContainer : MonoBehaviour
{
    public static GameItemContainer instance { get; private set; }

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

    //Cards
    private List<ActionCardData> commonCardList = new List<ActionCardData>();
    private List<ActionCardData> rareCardList = new List<ActionCardData>();
    private List<ActionCardData> uniqueCardList = new List<ActionCardData>();

    //Relics
    private List<RelicData> commonRelicList = new List<RelicData>();
    private List<RelicData> rareRelicList = new List<RelicData>();
    private List<RelicData> uniqueRelicList = new List<RelicData>();


    public void UpdateItemList()
    {
        foreach(ActionCardData data in AssetCacheManager.instance.cardList)
        {
            switch (data.level) //1~3
            {
                case 1:
                    commonCardList.Add(data);
                    break;
                case 2:
                    rareCardList.Add(data);
                    break;
                case 3:
                    uniqueCardList.Add(data);
                    break;
            }
        }

        foreach (RelicData data in AssetCacheManager.instance.relicList)
        {
            switch (data.level) //1~3
            {
                case 1:
                    commonRelicList.Add(data);
                    break;
                case 2:
                    rareRelicList.Add(data);
                    break;
                case 3:
                    uniqueRelicList.Add(data);
                    break;
            }
        }
    }


    public List<ActionCardData> GetCommonCardList() {   return commonCardList;  }
    public List<ActionCardData> GetRareCardList() { return rareCardList; }
    public List<ActionCardData> GetUniqueCardList() { return uniqueCardList; }

    public List<RelicData> GetCommonRelicList() { return commonRelicList; }
    public List<RelicData> GetRareRelicList() { return rareRelicList; }
    public List<RelicData> GetUniqueRelicList() { return uniqueRelicList; }
}
