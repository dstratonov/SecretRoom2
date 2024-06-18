using System;
using Game.Battle.Configs;
using Game.Battle.Units;

namespace Game.Battle.Models
{
    [Serializable]
    public class BattleUnitModel
    {
        private UnitConfig _unitConfig;
        
        public BattleUnit Unit { get; }
        public Team Team { get; private set; }
        
        public BattleUnitModel(UnitConfig config, BattleUnit unit)
        {
            _unitConfig = config;
            Unit = unit;
        }

        public void SetTeam(Team team)
        {
            Team = team;
        }
    }
}