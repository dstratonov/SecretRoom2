using Game.Battle.Abilities.Mechanics.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(AbilityConfig), menuName = "Battle/Configs/Ability")]
    public class AbilityConfig : SerializedScriptableObject
    {
        [AbilityId] public string id;
        public int energyCost;
        public AbilitySelection selection;
        [OdinSerialize] public MechanicData[] mechanics;
    }
}