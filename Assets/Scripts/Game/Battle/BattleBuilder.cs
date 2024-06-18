using System.Collections.Generic;
using Game.Battle.Configs;
using Game.Battle.Factories;
using Game.Battle.Models;

namespace Game.Battle
{
    public class BattleBuilder
    {
        private readonly BattleField _battleField;
        private readonly UnitFactory _unitFactory;

        public BattleBuilder(BattleField battleField, UnitFactory unitFactory)
        {
            _battleField = battleField;
            _unitFactory = unitFactory;
        }

        public BattleModel Build(BattleConfig battleConfig) =>
            new(
                CreateTeam(Team.Player, battleConfig.playerUnits),
                CreateTeam(Team.Enemy, battleConfig.enemyUnits));

        private TeamModel CreateTeam(Team team, IReadOnlyList<UnitConfig> configs)
        {
            var teamModel = new TeamModel(team);

            foreach (UnitConfig unitConfig in configs)
            {
                teamModel.AddUnit(_unitFactory.Create(unitConfig));
            }

            _battleField.SetTeam(teamModel);

            return teamModel;
        }
    }
}