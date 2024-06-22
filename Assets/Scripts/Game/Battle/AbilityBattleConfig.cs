using Game.Battle.Abilities;
using Game.Battle.Abilities.Mechanics.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Battle
{
    [CreateAssetMenu(fileName = nameof(AbilityBattleConfig), menuName = "Configs/Battle/Ability")]
    public class AbilityBattleConfig : SerializedScriptableObject
    {
        [AbilityId] public string id;
        public int energyCost;
        public AbilitySelection selection;
        [OdinSerialize] public MechanicData[] mechanics;
    }
}