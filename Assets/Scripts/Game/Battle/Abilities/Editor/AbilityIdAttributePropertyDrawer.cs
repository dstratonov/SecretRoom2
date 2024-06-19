using System;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game.Battle.Abilities.Editor
{
    [UsedImplicitly]
    public class AbilityIdAttributePropertyDrawer : OdinAttributeDrawer<AbilityIdAttribute>
    {
        private static GUIContent[] guiContents;
        
        public override bool CanDrawTypeFilter(Type type) =>
            type == typeof(string);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            string[] abilityIdsConfig = AssetDatabase.FindAssets("t:AbilityIdsConfig");

            if (abilityIdsConfig.Length == 0)
            {
                return;
            }

            string configPath = AssetDatabase.GUIDToAssetPath(abilityIdsConfig[0]);
            var config = AssetDatabase.LoadAssetAtPath<AbilityIdsConfig>(configPath);

            guiContents ??= new[] { new GUIContent("None") }
                .Union(config.ids.Select(name => new GUIContent(name))).ToArray();

            EditorGUILayout.BeginHorizontal();

            if (label != null)
            {
                EditorGUILayout.PrefixLabel(label);
            }

            int index = Array.IndexOf(config.ids, Property.ValueEntry.WeakSmartValue as string) + 1;
            index = EditorGUILayout.Popup(index, guiContents) - 1;
            Property.ValueEntry.WeakSmartValue = index == -1 ? string.Empty : config.ids[index];
            EditorGUILayout.EndHorizontal();
        }
    }
}