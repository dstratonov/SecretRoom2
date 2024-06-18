using System;

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
    }
}