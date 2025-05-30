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
            asset.level = card.level;
            asset.effectArea = card.effectArea;
            asset.useStamina = card.useStamina;
            asset.cardEffects = card.cardEffects;
            asset.effects = card.effects;
            asset.upgradeEffects = card.upgradeEffects;
            asset.conditions = card.conditions;

            var path = $"Assets/Data/Cards/{card.cardName}.asset";
            Directory.CreateDirectory("Assets/Data/Cards");
            AssetDatabase.CreateAsset(asset, path);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
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
            asset.level = relic.level;
            asset.effects = relic.effects;
            asset.conditions = relic.conditions;

            var path = $"Assets/Data/Relics/{relic.relicName}.asset";
            Directory.CreateDirectory("Assets/Data/Relics");
            AssetDatabase.CreateAsset(asset, path);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("YAML import complete.");
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
        public int level { get; set; }
        public List<EffectArea> effectArea { get; set; }
        public int useStamina { get; set; }
        public List<CardEffect> cardEffects { get; set; }
        public List<SkillEffect> effects { get; set; }
        public List<SkillEffect> upgradeEffects { get; set; }
        public List<SkillCondition> conditions { get; set; }
    }

    // Matching C# classes
    public class RootRelicData
    {
        public List<RelicEntry> @relicCollection { get; set; }
    }

    public class RelicEntry
    {
        public string classType { get; set; }
        public string relicName { get; set; }
        public int level { get; set; }
        public List<RelicCondition> conditions { get; set; }
        public List<RelicEffect> effects { get; set; }
    }
}
#endif