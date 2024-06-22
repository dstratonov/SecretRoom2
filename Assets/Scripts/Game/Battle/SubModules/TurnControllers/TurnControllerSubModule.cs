using Common.Events;
using Common.Loggers;
using Game.Battle.Models;

namespace Game.Battle.SubModules.TurnControllers
{
    public class TurnControllerSubModule : BattleSubModule
    {
        public TeamController _enemyTeamController;

        public TeamController _playerTeamController;
        private readonly UnitControllerFactory _unitControllerFactory;

        private TeamController _currentTeamController;

        private Team _currentTeamInTurn;

        public TurnControllerSubModule(UnitControllerFactory unitControllerFactory)
        {
            _unitControllerFactory = unitControllerFactory;
        }

        protected override void OnBattleStarted(BattleStartedEvent args)
        {
            base.OnBattleStarted(args);
            
            _playerTeamController = new TeamController(Model.PlayerTeam, _unitControllerFactory, EventBus);
            _enemyTeamController = new TeamController(Model.EnemyTeam, _unitControllerFactory, EventBus);
            
            _currentTeamInTurn = Team.Player;

            PerformTeamTurn();
        }

        private TeamController GetCurrentTeamModel()
        {
            switch (_currentTeamInTurn)
            {
                case Team.Player:
                    return _playerTeamController;
                case Team.Enemy:
                    return _enemyTeamController;
            }

            return null;
        }

        private void OnTeamTurnFinished()
        {
            _currentTeamController._teamTurnFinished -= OnTeamTurnFinished;
            _currentTeamController = null;

            SwapTeam();

            PerformTeamTurn();
        }

        private void PerformTeamTurn()
        {
            this.Log("Team turn: " + _currentTeamInTurn);

            _currentTeamController = GetCurrentTeamModel();
            _currentTeamController._teamTurnFinished += OnTeamTurnFinished;
            _currentTeamController.StartTurn();
        }

        private void SwapTeam()
        {
            switch (_currentTeamInTurn)
            {
                case Team.Player:
                    _currentTeamInTurn = Team.Enemy;
                    break;
                case Team.Enemy:
                    _currentTeamInTurn = Team.Player;
                    break;
            }
        }
    }
}