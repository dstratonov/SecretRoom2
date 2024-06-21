using System;
using System.Collections.Generic;

namespace Game.Battle.Models
{
    [Serializable]
    public class BattleModel
    {
        public TeamModel EnemyTeam {get; }
        public TeamModel PlayerTeam {get; }

        public BattleModel(TeamModel playerTeam, TeamModel enemyTeam)
        {
            PlayerTeam = playerTeam;
            EnemyTeam = enemyTeam;
        }
    }
}