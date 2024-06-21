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

        public TeamModel GetAllyTeamBySide(Team team)
        {
            switch (team)
            {
                case Team.Player:
                {
                    return PlayerTeam;
                }

                case Team.Enemy:
                {
                    return EnemyTeam;
                }

                default:
                {
                    return PlayerTeam;
                }
            }
        }

        public TeamModel GetOpponentTeamBySide(Team team)
        {
            switch (team)
            {
                case Team.Player:
                {
                    return EnemyTeam;
                }

                case Team.Enemy:
                {
                    return PlayerTeam;
                }

                default:
                {
                    return EnemyTeam;
                }
            }
        }
    }
}