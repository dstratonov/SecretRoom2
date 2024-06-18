using System;
using System.Collections.Generic;

namespace Game.Battle.Models
{
    [Serializable]
    public class BattleModel
    {
        private TeamModel _enemyTeam;
        private TeamModel _playerTeam;

        public BattleModel(TeamModel playerTeam, TeamModel enemyTeam)
        {
            _playerTeam = playerTeam;
            _enemyTeam = enemyTeam;
        }

        public IEnumerable<BattleUnitModel> GetAllUnits()
        {
            List<BattleUnitModel> units = new();
            
            units.AddRange(_playerTeam.GetUnits());
            units.AddRange(_enemyTeam.GetUnits());

            return units;
        }
    }
}