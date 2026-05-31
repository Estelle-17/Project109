#if UNITY_EDITOR
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class YAMLImporter
{

    /* 
    [MenuItem("Tools/Import Relic YAML")]
    //[System.Obsolete]
    public static void ImportRelicYAML()
    {
        string yamlPath = "Assets/Data/Relics.yaml";
        string schemaPath = "Assets/Data/RelicSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootRelicData>(yamlText);

        foreach (var relic in rawData.@relicCollection)
        {
            var asset = ScriptableObject.CreateInstance<RelicData>();
            asset.classType = relic.classType;
            asset.relicName = relic.relicName;
            asset.texturePath = relic.texturePath;
            asset.dataPath = relic.dataPath;
            asset.rarity = relic.rarity;
            asset.canUpgrade = relic.canUpgrade;
            asset.description = relic.description;
            asset.upgradeDescription = relic.upgradeDescription;

            var path = $"Assets/SO/Relics/{relic.dataPath}.asset";
            Directory.CreateDirectory("Assets/SO/Relics");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "Relics")
            );
            entry.address = relic.dataPath;
            entry.SetLabel("Relic", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }
    */

    [MenuItem("Tools/Import Event YAML")]
    //[System.Obsolete]
    public static void ImportEventYAML()
    {
        string yamlPath = "Assets/Data/Events.yaml";
        string schemaPath = "Assets/Data/EventSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootEventData>(yamlText);

        foreach (var eventData in rawData.@eventCollection)
        {
            var asset = ScriptableObject.CreateInstance<EventData>();
            asset.eventID = eventData.eventID;
            asset.eventAppearLevel = eventData.eventAppearLevel;
            asset.eventAppearCondition = eventData.eventAppearCondition;
            asset.eventObjectPath = eventData.eventObjectPath;
            asset.stages = eventData.stages;

            var path = $"Assets/SO/Events/{eventData.eventID}.asset";
            Directory.CreateDirectory("Assets/SO/Events");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "Events")
            );
            entry.address = eventData.eventID;
            entry.SetLabel("Event", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import Battle YAML")]
    //[System.Obsolete]
    public static void ImportBattleNodeYAML()
    {
        string yamlPath = "Assets/Data/BattleData.yaml";
        string schemaPath = "Assets/Data/BattleDataSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootBattleData>(yamlText);

        foreach (var battleNodeData in rawData.battleMonsterCollection)
        {
            var asset = ScriptableObject.CreateInstance<BattleData>();
            asset.battleDataName = battleNodeData.battleDataName;
            asset.dataPath = battleNodeData.dataPath;
            asset.battleAppearLevel = battleNodeData.battleAppearLevel;
            asset.battleLocation = battleNodeData.battleLocation;
            asset.monsterNames = battleNodeData.monsterNames;
            asset.rewards = battleNodeData.rewards;

            var path = $"Assets/SO/BattleNodes/{battleNodeData.dataPath}.asset";
            Directory.CreateDirectory("Assets/SO/Battles");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "BattleData")
            );
            entry.address = battleNodeData.dataPath;
            entry.SetLabel("Battle", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import Monster YAML")]
    //[System.Obsolete]
    public static void ImportMonsterYAML()
    {
        string yamlPath = "Assets/Data/Monster.yaml";
        string schemaPath = "Assets/Data/MonsterSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootMonsterData>(yamlText);

        foreach (var monsterData in rawData.MonsterCollection)
        {
            var asset = ScriptableObject.CreateInstance<MonsterData>();
            asset.monsterType = monsterData.monsterType;
            asset.monsterName = monsterData.monsterName;
            asset.objectPath = monsterData.objectPath;
            asset.dataPath = monsterData.dataPath;
            asset.appearLevel = monsterData.appearLevel;
            asset.hp = monsterData.hp;
            asset.stamina = monsterData.stamina;
            asset.staminaRegen = monsterData.staminaRegen;
            asset.strength = monsterData.strength;
            asset.armor = monsterData.armor;

            //asset.monsterPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(monsterData.objectPath, typeof(GameObject));

            var path = $"Assets/SO/Monsters/{monsterData.dataPath}.asset";
            Directory.CreateDirectory("Assets/SO/Monsters");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "Enemys")
            );
            entry.address = monsterData.dataPath;
            entry.SetLabel("Monster", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import MonsterReward YAML")]
    //[System.Obsolete]
    public static void ImportMonsterRewardYAML()
    {
        string yamlPath = "Assets/Data/MonsterReward.yaml";
        string schemaPath = "Assets/Data/MonsterRewardSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootMonsterRewardData>(yamlText);

        foreach (var monsterRewardData in rawData.monsterRewardCollection)
        {
            var asset = ScriptableObject.CreateInstance<MonsterRewardData>();
            asset.monsterName = monsterRewardData.monsterName;
            asset.minDropGoldAmount = monsterRewardData.minDropGoldAmount;
            asset.maxDropGoldAmount = monsterRewardData.maxDropGoldAmount;

            var path = $"Assets/SO/Rewards/{monsterRewardData.monsterName}.asset";
            Directory.CreateDirectory("Assets/SO/Rewards");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "Reward")
            );
            entry.address = monsterRewardData.monsterName + "_Reward_Data";
            entry.SetLabel("Reward", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import Character YAML")]
    //[System.Obsolete]
    public static void ImportCharacterYAML()
    {
        string yamlPath = "Assets/Data/Character.yaml";
        string schemaPath = "Assets/Data/CharacterSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootCharacterData>(yamlText);

        foreach (var characterData in rawData.characterCollection)
        {
            var asset = ScriptableObject.CreateInstance<CharacterData>();
            asset.classType = characterData.classType;
            asset.characterName = characterData.characterName;
            asset.assetPath = characterData.assetPath;
            asset.level = characterData.level;
            asset.description = characterData.description;
            asset.characterStat = characterData.characterStat;
            asset.startRelic = characterData.startRelic;

            //asset.characterObject = (GameObject)AssetDatabase.LoadAssetAtPath(CharacterData.modelingPath, typeof(GameObject));

            var path = $"Assets/SO/Characters/{characterData.assetPath}.asset";
            Directory.CreateDirectory("Assets/SO/Characters");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "GameData")
            );
            entry.address = characterData.assetPath;
            entry.SetLabel("Character", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import MapInfo YAML")]
    //[System.Obsolete]
    public static void ImportMapInfoYAML()
    {
        string yamlPath = "Assets/Data/MapInfo.yaml";
        string schemaPath = "Assets/Data/MapInfoSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootMapInfoData>(yamlText);

        foreach (var mapData in rawData.mapInfoCollection)
        {
            var asset = ScriptableObject.CreateInstance<MapDataInfo>();
            asset.mapName = mapData.mapName;
            asset.dataPath = mapData.dataPath;
            asset.appearMonstersDataPath = mapData.appearMonstersDataPath;
            asset.appearObstaclesDataPath = mapData.appearObstaclesDataPath;
            asset.appearTrapsDataPath = mapData.appearTrapsDataPath;

            var path = $"Assets/SO/Maps/Info/{mapData.dataPath}.asset";
            Directory.CreateDirectory("Assets/SO/Maps/Info");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "GameData")
            );
            entry.address = mapData.dataPath;
            entry.SetLabel("MapInfo", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }



    [MenuItem("Tools/Import Obstacle YAML")]
    //[System.Obsolete]
    public static void ImportObstacleYAML()
    {
        string yamlPath = "Assets/Data/Obstacle.yaml";
        string schemaPath = "Assets/Data/ObstacleSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootObstacleData>(yamlText);

        foreach (var obstacle in rawData.obstacleCollection)
        {
            var asset = ScriptableObject.CreateInstance<ObstacleData>();
            asset.obstacleName = obstacle.obstacleName;
            asset.objectPath = obstacle.objectPath;
            asset.dataPath = obstacle.dataPath;
            asset.hp = obstacle.hp;

            var path = $"Assets/SO/Obstacle/{obstacle.dataPath}.asset";
            Directory.CreateDirectory("Assets/SO/Obstacle");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "Obstacle")
            );
            entry.address = obstacle.dataPath;
            entry.SetLabel("Obstacle", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import Trap YAML")]
    //[System.Obsolete]
    public static void ImportTrapYAML()
    {
        string yamlPath = "Assets/Data/Trap.yaml";
        string schemaPath = "Assets/Data/TrapSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithNodeTypeResolver(new NumericTypeResolver())
            .Build();
        var yamlObject = deserializer.Deserialize(new StringReader(yamlText));

        var jsonSerializer = new SerializerBuilder()
            .JsonCompatible()
            .Build();
        string jsonText = jsonSerializer.Serialize(yamlObject);

        //YAML Schema -> Json Schema로 변환
        var schemaYamlObject = deserializer.Deserialize(new StringReader(schemaYamlText));
        string schemaJsonText = jsonSerializer.Serialize(schemaYamlObject);

        JSchema schema = JSchema.Parse(schemaJsonText);

        JObject jsonObj = JObject.Parse(jsonText);
        if (!jsonObj.IsValid(schema, out IList<string> errorMessages))
        {
            Debug.LogError("❌ YAML validation failed:");
            foreach (var error in errorMessages)
                Debug.LogError(error);
            return;
        }

        Debug.Log("YAML validation Success:");

        //검증에 성공하면 ScriptableObject 생성
        var rawData = deserializer.Deserialize<RootTrapData>(yamlText);

        foreach (var trap in rawData.trapCollection)
        {
            var asset = ScriptableObject.CreateInstance<TrapData>();
            asset.trapName = trap.trapName;
            asset.objectPath = trap.objectPath;
            asset.dataPath = trap.dataPath;
            asset.hp = trap.hp;
            asset.damage = trap.damage;
            asset.attackCount = trap.attackCount;

            var path = $"Assets/SO/Trap/{trap.dataPath}.asset";
            Directory.CreateDirectory("Assets/SO/Trap");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Addressable에 등록
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogError("Addressables 설정이 존재하지 않습니다.");
                return;
            }

            AddressableAssetEntry entry = settings.CreateOrMoveEntry(
                AssetDatabase.AssetPathToGUID(path),
                CreateOrFindAddressablesGroup(settings, "Trap")
            );
            entry.address = trap.dataPath;
            entry.SetLabel("Trap", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    public static AddressableAssetGroup CreateOrFindAddressablesGroup(AddressableAssetSettings settings, string groupName)
    {
        AddressableAssetGroup group = settings.FindGroup(groupName);
        if (group == null)
        {
            group = settings.CreateGroup(groupName, false, false, false, null, typeof(BundledAssetGroupSchema));
        }

        return group;
    }

    // Matching C# classes

    /*
    public class RootRelicData
    {
        public List<RelicEntry> @relicCollection { get; set; }
    }

    public class RelicEntry
    {
        public string classType { get; set; }
        public string relicName { get; set; }
        public string texturePath { get; set; }
        public string dataPath { get; set; }
        public int rarity { get; set; }
        public bool canUpgrade { get; set; }
        public string description { get; set; }
        public string upgradeDescription { get; set; }
    }
    */

    public class RootEventData
    {
        public List<EventEntry> @eventCollection { get; set; }
    }

    public class EventEntry
    {
        public string eventID { get; set; }
        public int eventAppearLevel { get; set; }
        public List<AppearCondition> eventAppearCondition { get; set; }
        public string eventObjectPath { get; set; }
        public string eventName { get; set; }
        public List<EventStageData> stages { get; set; }
    }

    public class RootBattleData
    {
        public List<BattleEntry> @battleMonsterCollection { get; set; }
    }

    public class BattleEntry
    {
        public string battleDataName { get; set; }
        public string dataPath { get; set; }
        public int battleAppearLevel { get; set; }
        public string battleLocation { get; set; }
        public List<string> monsterNames { get; set; }
        public List<RewardItem> rewards { get; set; }
    }

    public class RootMonsterData
    {
        public List<MonsterEntry> @MonsterCollection { get; set; }
    }

    public class MonsterEntry
    {
        public GameObject monsterPrefab { get; set; }
        public string monsterType { get; set; }
        public string monsterName { get; set; }
        public string objectPath { get; set; }
        public string dataPath { get; set; }
        public int appearLevel { get; set; }
        public float hp { get; set; }
        public float stamina { get; set; }
        public float staminaRegen { get; set; }
        public int strength { get; set; }
        public int armor { get; set; }
    }

    public class RootMonsterRewardData
    {
        public List<MonsterRewardEntry> @monsterRewardCollection { get; set; }
    }

    public class MonsterRewardEntry
    {
        public string monsterName { get; set; }
        public int minDropGoldAmount { get; set; }
        public int maxDropGoldAmount { get; set; }
    }

    public class RootCharacterData
    {
        public List<CharacterEntry> characterCollection { get; set; }
    }

    public class CharacterEntry
    {
        public string classType { get; set; }
        public string characterName { get; set; }
        public string assetPath { get; set; }
        public int level { get; set; }
        public string description { get; set; }
        public CharacterStat characterStat { get; set; }
        public List<string> startRelic { get; set; }
        // startCardIds는 CharacterData(ScriptableObject)에서 직접 편집합니다.
        public List<string> startCardIds { get; set; }
    }

    public class RootMapInfoData
    {
        public List<MapInfoEntry> mapInfoCollection { get; set; }
    }

    public class MapInfoEntry
    {
        public string mapName { get; set; }
        public string dataPath { get; set; }
        public List<string> appearMonstersDataPath { get; set; }
        public List<string> appearObstaclesDataPath { get; set; }
        public List<string> appearTrapsDataPath { get; set; }
    }



    public class RootObstacleData
    {
        public List<ObstacleEntry> obstacleCollection { get; set; }
    }

    public class ObstacleEntry
    {
        public string obstacleName { get; set; }
        public string objectPath { get; set; }
        public string dataPath { get; set; }
        public float hp { get; set; }
    }

    public class RootTrapData
    {
        public List<TrapEntry> trapCollection { get; set; }
    }

    public class TrapEntry
    {
        public string trapName { get; set; }
        public string objectPath { get; set; }
        public string dataPath { get; set; }
        public float hp { get; set; }
        public float damage { get; set; }
        public int attackCount { get; set; }
    }
}
#endif