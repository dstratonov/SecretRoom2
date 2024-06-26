using System;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Common.Configs.Editor
{
    [UsedImplicitly]
    public abstract class IdAttributePropertyDrawer<TAttribute> : OdinAttributeDrawer<TAttribute> 
        where TAttribute : Attribute
    {
        private GUIContent[] _guiContents;
        
        public override bool CanDrawTypeFilter(Type type) =>
            type == typeof(string);

        protected abstract string[] GetIds();
        
        protected override void DrawPropertyLayout(GUIContent label)
        {
            string[] ids = GetIds();

            _guiContents ??= new[] { new GUIContent("None") }
                .Union(ids.Select(name => new GUIContent(name))).ToArray();

            EditorGUILayout.BeginHorizontal();

            if (label != null)
            {
                EditorGUILayout.PrefixLabel(label);
            }

            int index = Array.IndexOf(ids, Property.ValueEntry.WeakSmartValue as string) + 1;
            index = EditorGUILayout.Popup(index, _guiContents) - 1;
            Property.ValueEntry.WeakSmartValue = index == -1 ? string.Empty : ids[index];
            EditorGUILayout.EndHorizontal();
        }
    }
}