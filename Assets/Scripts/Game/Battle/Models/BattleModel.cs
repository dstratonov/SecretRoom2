using System;
using System.Collections.Generic;
using Game.Battle.Units;

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

        public IEnumerable<BattleUnitModel> GetAllUnits()
        {
            List<BattleUnitModel> units = new();

            units.AddRange(PlayerTeam.GetUnits());
            units.AddRange(EnemyTeam.GetUnits());

            return units;
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