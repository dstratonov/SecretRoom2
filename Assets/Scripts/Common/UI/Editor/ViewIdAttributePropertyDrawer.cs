using System;
using System.Linq;
using System.Reflection;
using Common.Configs.Editor;
using UnityEngine;

namespace Common.UI.Editor
{
    public class ViewIdAttributePropertyDrawer : IdAttributePropertyDrawer<ViewIdAttribute>
    {
        private GUIContent[] _guiContents;

        public override bool CanDrawTypeFilter(Type type) =>
            type == typeof(string);

        protected override string[] GetIds()
        {
            return Assembly
                .GetAssembly(typeof(BaseView))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseView)) && !t.IsAbstract)
                .Select(x => x.Name)
                .ToArray();
        }
    }
}