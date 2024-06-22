using System;
using System.Collections.Generic;
using System.Linq;
using Common.Events;
using Common.Loggers;
using Game.Battle.Models;

namespace Game.Battle.SubModules.TurnControllers
{
    public class TurnControllerSubModule : BattleSubModule
    {
        private readonly UnitControllerFactory _unitControllerFactory;

        public event Action onPlayerTurnStarted;
        public event Action onPlayerTurnFinished;

        public event Action onEnemyTurnStarted;

        public event Action onEnemyTurnFinished;

        public event Action onBattleEnded;

        private TeamController _currentTeamController;

        public TeamController _playerTeamController;
        public TeamController _enemyTeamController;

        private Team _currentTeamInTurn;


        public TurnControllerSubModule(UnitControllerFactory unitControllerFactory)
        {
            _unitControllerFactory = unitControllerFactory;
            _playerTeamController = new TeamController(Model.PlayerTeam, _unitControllerFactory);
            _enemyTeamController = new TeamController(Model.EnemyTeam, _unitControllerFactory);
        }

        protected override void OnBattleStarted(BattleStartedEvent args)
        {
            base.OnBattleStarted(args);
            _currentTeamInTurn = Team.Player;

            PerformTeamTurn();
        }

        private TeamController GetCurrentTeamModel()
        {
            switch(_currentTeamInTurn)
            {
                case Team.Player:
                    return _playerTeamController;
                case Team.Enemy:
                    return _enemyTeamController;
            }

            return null;
        }

        private void InvokeStartEvents()
        {
            switch(_currentTeamInTurn)
            {
                case Team.Player:
                    onPlayerTurnStarted?.Invoke();
                    break;
                case Team.Enemy:
                    onEnemyTurnStarted?.Invoke();
                    break;
            }
        }

        private void InvokeEndEvents()
        {
            switch(_currentTeamInTurn)
            {
                case Team.Player:
                    onPlayerTurnFinished?.Invoke();
                    break;
                case Team.Enemy:
                    onEnemyTurnFinished?.Invoke();
                    break;
            }
        }

        private void PerformTeamTurn()
        {
            InvokeStartEvents();

            this.Log("Team turn: " + _currentTeamInTurn);

            _currentTeamController = GetCurrentTeamModel();
            _currentTeamController._teamTurnFinished += OnTeamTurnFinished;
            _currentTeamController.StartTurn();
        }

        private void CheckOnBattleEnded()
        {
            // todo add somehow end of battle maybe not in this class
            onBattleEnded?.Invoke();
        }

        private void SwapTeam()
        {
            switch(_currentTeamInTurn)
            {
                case Team.Player:
                    _currentTeamInTurn = Team.Enemy;
                    break;
                case Team.Enemy:
                    _currentTeamInTurn = Team.Player;
                    break;
            }
        }


        private void OnTeamTurnFinished()
        {
            _currentTeamController._teamTurnFinished -= OnTeamTurnFinished;
            _currentTeamController = null;

            InvokeEndEvents();

            CheckOnBattleEnded();

            SwapTeam();

            PerformTeamTurn();
        }
    }
}