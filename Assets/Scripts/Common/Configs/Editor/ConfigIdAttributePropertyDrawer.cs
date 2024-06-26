using System;
using UnityEditor;

namespace Common.Configs.Editor
{
    public class ConfigIdAttributePropertyDrawer<TAttribute, TConfig> : IdAttributePropertyDrawer<TAttribute>
        where TAttribute : Attribute
        where TConfig : IdsConfig
    {
        protected override string[] GetIds()
        {
            string configName = typeof(TConfig).Name;
            string[] idsConfig = AssetDatabase.FindAssets($"t:{configName}");

            if (idsConfig.Length == 0)
            {
                return Array.Empty<string>();
            }

            string configPath = AssetDatabase.GUIDToAssetPath(idsConfig[0]);
            var config = AssetDatabase.LoadAssetAtPath<TConfig>(configPath);

            return config.ids;
        }
    }
}