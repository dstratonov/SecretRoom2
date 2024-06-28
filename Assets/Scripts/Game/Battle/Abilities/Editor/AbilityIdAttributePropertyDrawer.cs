using Common.Configs.Editor;
using JetBrains.Annotations;
using UnityEditor;

namespace Game.Battle.Abilities.Editor
{
    [UsedImplicitly]
    public class AbilityIdAttributePropertyDrawer : IdAttributePropertyDrawer<AbilityIdAttribute>
    {
        protected override string[] GetIds()
        {
            string[] abilityGuids = AssetDatabase.FindAssets($"t:{nameof(AbilityBattleConfig)}");
            var ids = new string[abilityGuids.Length];

            for (var i = 0; i < abilityGuids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(abilityGuids[i]);
                var abilityConfig = AssetDatabase.LoadAssetAtPath<AbilityBattleConfig>(path);
                ids[i] = abilityConfig.name;
            }

            return ids;
        }
    }
}