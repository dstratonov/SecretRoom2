using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(AbilityConfig), menuName = "Battle/Configs/Ability")]
    public class AbilityConfig : SerializedScriptableObject
    {
        [AbilityId]
        public string id;
    }
}