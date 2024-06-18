using Game.Battle.Units;
using UnityEngine;

namespace Game.Battle.Configs
{
    [CreateAssetMenu(fileName = nameof(UnitConfig), menuName = "Battle/Configs/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public Sprite unitIcon;
        public BattleUnit unit;
    }
}