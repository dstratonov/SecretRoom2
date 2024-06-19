using Game.Battle.Abilities;
using Game.Battle.Units;
using UnityEngine;

namespace Game.Battle.Configs
{
    [CreateAssetMenu(fileName = nameof(UnitConfig), menuName = "Configs/Battle/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public string id;
        public Sprite unitIcon;
        public UnitPawn unitPawn;

        public int health;
        public int energy;

        [AbilityId]
        public string[] abilities;
    }
}