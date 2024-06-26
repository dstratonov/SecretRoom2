using Common.Events;
using Cysharp.Threading.Tasks;
using Game.Battle.Configs;
using Game.Battle.Events;
using Game.Battle.Models;
using Game.Battle.SubModules;
using Game.Battle.TurnControllers;
using Game.Battle.Units;

namespace Game.Battle
{
    public class Battle
    {
        private readonly BattleBuilder _battleBuilder;
        private readonly EventBus _eventBus;
        private readonly BattleSubModulesHolder _subModulesHolder;
        private readonly TurnController _turnController;

        public BattleModel Model { get; private set; }

        public Battle(BattleBuilder battleBuilder, TurnController turnController, EventBus eventBus,
            BattleSubModulesHolder subModulesHolder)
        {
            _battleBuilder = battleBuilder;
            _turnController = turnController;
            _eventBus = eventBus;
            _subModulesHolder = subModulesHolder;
        }

        public void Initialize(BattleConfig battleConfig)
        {
            Model = _battleBuilder.Build(battleConfig);

            _turnController.Initialize(Model);
        }

        public void Start()
        {
            _eventBus.Fire(new BattleStartedEvent());

            StartBattle().Forget();
        }

        private void OnTeamTurnFinished(Team team)
        {
            foreach (ITeamTurnFinishedSubModule teamTurnFinishedSubModule in _subModulesHolder
                         .TeamTurnFinishedSubModules)
            {
                teamTurnFinishedSubModule.OnTeamTurnFinished(team);
            }
        }

        private void OnTeamTurnStarted(Team team)
        {
            foreach (ITeamTurnStartedSubModule teamTurnStartedSubModule in _subModulesHolder.TeamTurnStartedSubModules)
            {
                teamTurnStartedSubModule.OnTeamTurnStarted(team);
            }
        }

        private void OnUnitTurnFinished(BattleUnitModel unit)
        {
            foreach (IUnitTurnFinishedSubModule turnFinishedSubModule in _subModulesHolder.UnitTurnFinishedSubModules)
            {
                turnFinishedSubModule.OnUnitTurnFinished(unit);
            }
        }

        private void OnUnitTurnStarted(BattleUnitModel unit)
        {
            foreach (IUnitTurnStartedSubModule unitTurnStartedSubModule in _subModulesHolder.UnitTurnStartedSubModules)
            {
                unitTurnStartedSubModule.OnUnitTurnStarted(unit);
            }
        }

        private async UniTaskVoid StartBattle()
        {
            foreach (IBattleStartedSubModule battleStartedSubModule in _subModulesHolder.BattleStartedSubModules)
            {
                await battleStartedSubModule.OnBattleStarted(Model);
            }

            _turnController.TeamTurnStarted += OnTeamTurnStarted;
            _turnController.TeamTurnFinished += OnTeamTurnFinished;
            _turnController.UnitTurnStarted += OnUnitTurnStarted;
            _turnController.UnitTurnFinished += OnUnitTurnFinished;

            _turnController.PerformTeamTurn();
        }
    }
}