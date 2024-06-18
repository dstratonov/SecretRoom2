using System.Collections.Generic;
using Game.Battle.Configs;
using Game.Battle.Factories;
using Game.Battle.Models;

namespace Game.Battle
{
    public class Battle
    {
        private readonly BattleField _battleField;
        private readonly UnitFactory _unitFactory;

        public BattleModel Model { get; private set; }

        public Battle(UnitFactory unitFactory, BattleField battleField)
        {
            _unitFactory = unitFactory;
            _battleField = battleField;
        }

        public void Initialize(BattleConfig battleConfig)
        {
            Model = new BattleModel(
                CreateTeam(Team.Player, battleConfig.playerUnits),
                CreateTeam(Team.Enemy, battleConfig.enemyUnits));
        }

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