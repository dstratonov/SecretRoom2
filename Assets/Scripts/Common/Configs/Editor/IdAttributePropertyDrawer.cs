using System;
using System.Linq;
using Common.Configs;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
    [UsedImplicitly]
    public abstract class IdAttributePropertyDrawer<TAttribute, TConfig> : OdinAttributeDrawer<TAttribute> 
        where TAttribute : Attribute
        where TConfig : IdsConfig
    {
        private GUIContent[] _guiContents;
        
        public override bool CanDrawTypeFilter(Type type) =>
            type == typeof(string);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            string configName = typeof(TConfig).Name;
            string[] idsConfig = AssetDatabase.FindAssets($"t:{configName}");

            if (idsConfig.Length == 0)
            {
                return;
            }

            string configPath = AssetDatabase.GUIDToAssetPath(idsConfig[0]);
            var config = AssetDatabase.LoadAssetAtPath<TConfig>(configPath);

            _guiContents ??= new[] { new GUIContent("None") }
                .Union(config.ids.Select(name => new GUIContent(name))).ToArray();

            EditorGUILayout.BeginHorizontal();

            if (label != null)
            {
                EditorGUILayout.PrefixLabel(label);
            }

            int index = Array.IndexOf(config.ids, Property.ValueEntry.WeakSmartValue as string) + 1;
            index = EditorGUILayout.Popup(index, _guiContents) - 1;
            Property.ValueEntry.WeakSmartValue = index == -1 ? string.Empty : config.ids[index];
            EditorGUILayout.EndHorizontal();
        }
    }
}