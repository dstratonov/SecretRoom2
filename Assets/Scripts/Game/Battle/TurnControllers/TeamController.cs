using System;
using System.Collections.Generic;
using Common.Loggers;
using Game.Battle.Models;
using Game.Battle.Units;

namespace Game.Battle.TurnControllers
{
    public class TeamController
    {
        private readonly TeamModel _teamModel;
        private readonly UnitControllerFactory _unitControllerFactory;

        private BattleUnitModel _currentUnit;
        private UnitController _currentUnitController;
        private int _currentUnitIndex;
        private IReadOnlyList<BattleUnitModel> _turnUnitsQueue;

        public event Action TeamTurnFinished;
        public event Action TeamTurnStarted;
        public event Action<BattleUnitModel> UnitTurnFinished;
        public event Action<BattleUnitModel> UnitTurnStarted;

        public Team Team => _teamModel.Team;

        public TeamController(TeamModel teamModel, UnitControllerFactory unitControllerFactory)
        {
            _teamModel = teamModel;
            _unitControllerFactory = unitControllerFactory;
        }

        public void StartTurn()
        {
            TeamTurnStarted?.Invoke();
            _turnUnitsQueue = _teamModel.GetUnits();

            _currentUnitIndex = 0;
            _currentUnit = null;

            UnitTurn();
        }

        private UnitController GetUnitController(BattleUnitModel unitModel) =>
            _unitControllerFactory.GetController(unitModel.Team);

        private void OnUnitTurnFinished()
        {
            _currentUnitController.Deactivate();
            _currentUnitController.TurnFinished -= OnUnitTurnFinished;

            _currentUnitController = null;
            _currentUnit = null;

            UnitTurnFinished?.Invoke(_currentUnit);

            _currentUnitIndex++;

            if (_currentUnitIndex >= _turnUnitsQueue.Count)
            {
                TeamTurnFinished?.Invoke();
                return;
            }

            UnitTurn();
        }

        private void UnitTurn()
        {
            _currentUnit = _turnUnitsQueue[_currentUnitIndex];

            this.Log("Unit turn: " + _currentUnit.Id);

            _currentUnitController = GetUnitController(_currentUnit);

            _currentUnitController.SetUnit(_currentUnit);
            _currentUnitController.PrepareForTurn();
            
            UnitTurnStarted?.Invoke(_currentUnit);

            _currentUnitController.TurnFinished += OnUnitTurnFinished;
            _currentUnitController.Activate();
        }
    }
}