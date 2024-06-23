using System.Collections.Generic;
using Game.Units;
using UnityEngine;

namespace Game.Battle.Configs
{
    [CreateAssetMenu(fileName = nameof(BattleConfig), menuName = "Configs/Battle")]
    public class BattleConfig : ScriptableObject
    {
        public List<UnitConfig> playerUnits;
        public List<UnitConfig> enemyUnits;
    }
}