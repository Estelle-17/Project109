#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using Codice.CM.Common;

public class YAMLImporter
{
    [MenuItem("Tools/Import Card YAML")]
    //[System.Obsolete]
    public static void ImportCardYAML()
    {
        string yamlPath = "Assets/Data/ActionCards.yaml";
        string schemaPath = "Assets/Data/ActionCardSchema.yaml";

        string yamlText = File.ReadAllText(yamlPath);
        string schemaYamlText = File.ReadAllText(schemaPath);

        //YAML -> Json으로 변환
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
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
        var rawData = deserializer.Deserialize<RootData>(yamlText);

        foreach (var card in rawData.@cardCollection)
        {
            var asset = ScriptableObject.CreateInstance<ActionCardData>();
            asset.className = card.className;
            asset.cardName = card.cardName;
            asset.texturePath = card.texturePath;
            asset.level = card.level;
            asset.useStamina = card.useStamina;
            asset.effectArea = card.effectArea;
            asset.defaultEffects = card.defaultEffects;
            asset.upgradeEffects = card.upgradeEffects;

            asset.cardTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(asset.texturePath, typeof(Texture2D));
            asset.upgradeCount = 0;

            var path = $"Assets/SO/Cards/{card.cardName}.asset";
            Directory.CreateDirectory("Assets/SO/Cards");
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
                CreateOrFindAddressablesGroup(settings, "Cards")
            );
            entry.address = card.cardName;
            entry.SetLabel("Card", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

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
            asset.level = relic.level;
            asset.description = relic.description;
            asset.effects = relic.effects;
            asset.conditions = relic.conditions;

            asset.relicTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(asset.texturePath, typeof(Texture2D));

            var path = $"Assets/SO/Relics/{relic.relicName}.asset";
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
            entry.address = relic.relicName;
            entry.SetLabel("Relic", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

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
            asset.eventAppearLevel = eventData.eventAppearLevel;
            asset.eventAppearCondition = eventData.eventAppearCondition;
            asset.eventName = eventData.eventName;
            asset.eventDescription = eventData.eventDescription;
            asset.choices = eventData.choices;

            var path = $"Assets/SO/Events/{eventData.eventName}.asset";
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
            entry.address = eventData.eventName;
            entry.SetLabel("Event", true);
            Debug.Log("Addressables에 등록 완료: " + entry.address);
        }

        Debug.Log("YAML import complete.");
    }

    [MenuItem("Tools/Import BattleNode YAML")]
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
        var rawData = deserializer.Deserialize<RootBattleNodeData>(yamlText);

        foreach (var battleNodeData in rawData.battleMonsterCollection)
        {
            var asset = ScriptableObject.CreateInstance<BattleNodeData>();
            asset.battleNodeName = battleNodeData.battleNodeName;
            asset.battleAppearLevel = battleNodeData.battleAppearLevel;
            asset.monsterAppearInformation = battleNodeData.monsterAppearInformation;

            var path = $"Assets/SO/BattleNodes/{battleNodeData.battleNodeName}.asset";
            Directory.CreateDirectory("Assets/SO/BattleNodes");
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
                CreateOrFindAddressablesGroup(settings, "BattleNodes")
            );
            entry.address = battleNodeData.battleNodeName;
            entry.SetLabel("BattleNode", true);
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
            asset.appearLevel = monsterData.appearLevel;
            asset.hp = monsterData.hp;
            asset.stamina = monsterData.stamina;
            asset.staminaRegen = monsterData.staminaRegen;
            asset.strength = monsterData.strength;
            asset.armor = monsterData.armor;

            asset.monsterPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(monsterData.objectPath, typeof(GameObject));

            var path = $"Assets/SO/Monsters/{monsterData.monsterName}.asset";
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
                CreateOrFindAddressablesGroup(settings, "Monsters")
            );
            entry.address = monsterData.monsterName;
            entry.SetLabel("Monster", true);
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
            asset.modelingPath = characterData.modelingPath;
            asset.level = characterData.level;
            asset.description = characterData.description;
            asset.hp = characterData.hp;
            asset.stamina = characterData.stamina;
            asset.staminaRegen = characterData.staminaRegen;
            asset.strength = characterData.strength;
            asset.armor = characterData.armor;
            asset.startRelic = characterData.startRelic;
            asset.startCards = characterData.startCards;

            //asset.characterObject = (GameObject)AssetDatabase.LoadAssetAtPath(CharacterData.modelingPath, typeof(GameObject));

            var path = $"Assets/SO/Characters/{characterData.characterName}.asset";
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
                CreateOrFindAddressablesGroup(settings, "Characters")
            );
            entry.address = characterData.characterName;
            entry.SetLabel("Character", true);
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
    public class RootData
    {
        public List<CardEntry> @cardCollection { get; set; }
    }

    public class CardEntry
    {
        public string className { get; set; }
        public string cardName { get; set; }
        public string texturePath { get; set; }
        public int level { get; set; }
        public int useStamina { get; set; }
        public List<EffectArea> effectArea { get; set; }
        public CardEffect defaultEffects { get; set; }
        public CardEffect upgradeEffects { get; set; }
}

    public class RootRelicData
    {
        public List<RelicEntry> @relicCollection { get; set; }
    }

    public class RelicEntry
    {
        public string classType { get; set; }
        public string relicName { get; set; }
        public string texturePath { get; set; }
        public int level { get; set; }
        public string description { get; set; }
        public List<RelicCondition> conditions { get; set; }
        public List<RelicEffect> effects { get; set; }
    }

    public class RootEventData
    {
        public List<EventEntry> @eventCollection { get; set; }
    }

    public class EventEntry
    {
        public int eventAppearLevel { get; set; }
        public List<AppearCondition> eventAppearCondition { get; set; }
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public List<Choice_RelicAndCard> choices { get; set; }
    }

    public class RootBattleNodeData
    {
        public List<BattleNodeEntry> @battleMonsterCollection { get; set; }
    }

    public class BattleNodeEntry
    {
        public string battleNodeName { get; set; }
        public int battleAppearLevel { get; set; }
        public List<MonsterSpawnInfo> monsterAppearInformation { get; set; }
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
        public int appearLevel { get; set; }
        public float hp { get; set; }
        public float stamina { get; set; }
        public float staminaRegen { get; set; }
        public int strength { get; set; }
        public int armor { get; set; }
    }

    public class RootCharacterData
    {
        public List<CharacterEntry> characterCollection { get; set; }
    }

    public class CharacterEntry
    {
        public string classType { get; set; }
        public string characterName { get; set; }
        public string modelingPath { get; set; }
        public int level { get; set; }
        public string description { get; set; }
        public float hp { get; set; }
        public float stamina { get; set; }
        public float staminaRegen { get; set; }
        public int strength { get; set; }
        public int armor { get; set; }
        public List<string> startRelic { get; set; }
        public List<StartCard> startCards { get; set; }
    }
}
#endif