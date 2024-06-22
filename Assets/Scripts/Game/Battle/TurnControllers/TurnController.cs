using System;
using Common.Loggers;
using Game.Battle.Models;
using Game.Battle.Units;

namespace Game.Battle.TurnControllers
{
    public class TurnController
    {
        private readonly UnitControllerFactory _unitControllerFactory;

        private TeamController _currentTeamController;
        private TeamController _enemyTeamController;
        private TeamController _playerTeamController;
        
        public event Action<Team> TeamTurnFinished;
        public event Action<Team> TeamTurnStarted;
        public event Action<BattleUnitModel> UnitTurnFinished;
        public event Action<BattleUnitModel> UnitTurnStarted;

        public TurnController(UnitControllerFactory unitControllerFactory)
        {
            _unitControllerFactory = unitControllerFactory;
        }

        public void Initialize(BattleModel model)
        {
            _playerTeamController = new TeamController(model.PlayerTeam, _unitControllerFactory);
            _enemyTeamController = new TeamController(model.EnemyTeam, _unitControllerFactory);

            _currentTeamController = _playerTeamController;
        }

        public void PerformTeamTurn()
        {
            this.Log("Team turn: " + _currentTeamController.Team);

            _currentTeamController.TeamTurnStarted += OnTeamTurnStarted;
            _currentTeamController.TeamTurnFinished += OnTeamTurnFinished;
            _currentTeamController.UnitTurnStarted += OnUnitTurnStarted;
            _currentTeamController.UnitTurnFinished += OnUnitTurnFinished;

            _currentTeamController.StartTurn();
        }

        private void OnTeamTurnFinished()
        {
            _currentTeamController.TeamTurnStarted -= OnTeamTurnStarted;
            _currentTeamController.TeamTurnFinished -= OnTeamTurnFinished;
            _currentTeamController.UnitTurnStarted -= OnUnitTurnStarted;
            _currentTeamController.UnitTurnFinished -= OnUnitTurnFinished;

            TeamTurnFinished?.Invoke(_currentTeamController.Team);

            SwapTeam();
            PerformTeamTurn();
        }

        private void OnTeamTurnStarted()
        {
            TeamTurnStarted?.Invoke(_currentTeamController.Team);
        }

        private void OnUnitTurnFinished(BattleUnitModel unit)
        {
            UnitTurnFinished?.Invoke(unit);
        }

        private void OnUnitTurnStarted(BattleUnitModel unit)
        {
            UnitTurnStarted?.Invoke(unit);
        }

        private void SwapTeam()
        {
            switch (_currentTeamController.Team)
            {
                case Team.Player:
                    _currentTeamController = _enemyTeamController;
                    break;
                case Team.Enemy:
                    _currentTeamController = _playerTeamController;
                    break;
            }
        }
    }
}