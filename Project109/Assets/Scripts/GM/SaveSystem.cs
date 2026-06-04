using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasteryUpgradeSaveEntry
{
    public string masteryId;
    public int count;
}

[Serializable]
public class CardSaveEntry
{
    public string cardName;
    public int masteryLevel;
    public List<MasteryUpgradeSaveEntry> masteryUpgrades = new List<MasteryUpgradeSaveEntry>();
    public float currentMasteryXP;
}

[Serializable]
public class RunSaveData
{
    public string currentMapName;
    public int currentStageLevel;
    public int currentExploreMapFloor;
    public int playerGold;
    public int playerMemorySharp;
    
    public float playerCurHealth;
    public float playerMaxHealth;

    public List<CardSaveEntry> playerDeckCards = new List<CardSaveEntry>();
    public List<string> playerRelicNames = new List<string>();
}

/// <summary>
/// JSON 파일을 기반으로 게임 세션을 로컬 저장소에 저장하고 불러오는 시스템입니다.
/// </summary>
public static class SaveSystem
{
    private static readonly string SaveFileName = "save.json";
    private static string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    /// <summary>
    /// 현재의 RunManager 세션 상태를 save.json 파일로 저장합니다.
    /// </summary>
    public static void SaveGame(RunManager runManager)
    {
        if (runManager == null || runManager.player == null)
        {
            Debug.LogError("[SaveSystem] RunManager 또는 플레이어 정보가 없어 저장할 수 없습니다.");
            return;
        }

        try
        {
            RunSaveData data = new RunSaveData();
            data.currentMapName = runManager.currentMapName;
            data.currentStageLevel = runManager.currentStageLevel;
            data.currentExploreMapFloor = runManager.currentExploreMapFloor;
            
            data.playerGold = runManager.player.playerStat != null ? runManager.player.playerStat.inGame_Currency_Gold : 0;
            data.playerMemorySharp = runManager.player.playerStat != null ? runManager.player.playerStat.inGame_Currency_MemorySharp : 0;

            if (runManager.player.character != null)
            {
                data.playerCurHealth = runManager.player.character.curHealth;
                data.playerMaxHealth = runManager.player.character.curCharacterStat != null ? runManager.player.character.curCharacterStat.maxHealth : 100f;
            }

            // 플레이어 덱 저장
            if (runManager.player.deck != null)
            {
                foreach (var card in runManager.player.deck.GetCards())
                {
                    if (card == null || card.cardData == null) continue;
                    
                    CardSaveEntry entry = new CardSaveEntry();
                    entry.cardName = card.cardData.cardName;
                    entry.masteryLevel = card.masteryLevel;
                    entry.currentMasteryXP = card.currentMasteryXP;
                    
                    entry.masteryUpgrades = new List<MasteryUpgradeSaveEntry>();
                    if (card.masteryUpgrades != null)
                    {
                        foreach (var kvp in card.masteryUpgrades)
                        {
                            entry.masteryUpgrades.Add(new MasteryUpgradeSaveEntry { masteryId = kvp.Key, count = kvp.Value });
                        }
                    }
                    data.playerDeckCards.Add(entry);
                }
            }

            // 플레이어 유물 저장
            if (runManager.player.relicManager != null)
            {
                foreach (var relic in runManager.player.relicManager.GetRelics())
                {
                    if (relic == null || relic.Data == null) continue;
                    data.playerRelicNames.Add(relic.Data.relicName);
                }
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json, System.Text.Encoding.UTF8);
            Debug.Log($"[SaveSystem] 게임 상태가 '{SavePath}'에 정상적으로 저장되었습니다.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] 저장 중 예외 발생: {e.Message}");
        }
    }

    /// <summary>
    /// save.json 파일이 존재하는지 검사합니다.
    /// </summary>
    public static bool HasSaveData()
    {
        return File.Exists(SavePath);
    }

    /// <summary>
    /// 저장소로부터 save.json 데이터를 읽어 RunSaveData 객체로 역직렬화합니다.
    /// </summary>
    public static RunSaveData LoadGameData()
    {
        if (!HasSaveData())
        {
            Debug.LogWarning("[SaveSystem] 로드할 세이브 파일이 존재하지 않습니다.");
            return null;
        }

        try
        {
            string json = File.ReadAllText(SavePath, System.Text.Encoding.UTF8);
            RunSaveData data = JsonUtility.FromJson<RunSaveData>(json);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] 로드 중 예외 발생: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// 세이브 파일을 영구적으로 삭제합니다.
    /// </summary>
    public static void DeleteSaveFile()
    {
        if (HasSaveData())
        {
            try
            {
                File.Delete(SavePath);
                Debug.Log("[SaveSystem] 세이브 파일이 성공적으로 삭제되었습니다.");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] 세이브 파일 삭제 중 예외 발생: {e.Message}");
            }
        }
    }
}
