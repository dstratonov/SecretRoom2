using System.Collections.Generic;
using Common.Events;
using Game.Battle.Configs;
using Game.Battle.Factories;
using Game.Battle.Models;

namespace Game.Battle
{
    public class Battle
    {
        private readonly BattleField _battleField;
        private readonly EventBus _eventBus;
        private readonly UnitFactory _unitFactory;

        public BattleModel Model { get; private set; }

        public Battle(UnitFactory unitFactory, BattleField battleField, EventBus eventBus)
        {
            _unitFactory = unitFactory;
            _battleField = battleField;
            _eventBus = eventBus;
        }

        public void Initialize(BattleConfig battleConfig)
        {
            Model = new BattleModel(
                CreateTeam(Team.Player, battleConfig.playerUnits),
                CreateTeam(Team.Enemy, battleConfig.enemyUnits));
            
            _eventBus.Fire<BattleStartedEvent>();
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