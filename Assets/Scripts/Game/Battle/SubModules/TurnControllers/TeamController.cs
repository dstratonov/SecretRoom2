using System;
using System.Collections.Generic;
using Common.Events;
using Common.Loggers;
using Game.Battle.Events;
using Game.Battle.Models;
using Game.Battle.SubModules.TurnControllers;

public class TeamController
{
    private readonly EventBus _eventBus;
    private readonly TeamModel _teamModel;
    private readonly UnitControllerFactory _unitControllerFactory;

    private BattleUnitModel _currentUnit;

    private UnitController _currentUnitController;
    private int _currentUnitId;

    private IReadOnlyList<BattleUnitModel> _turnUnitsQueue;
    
    public event Action _teamTurnFinished;
    public event Action _teamTurnStarted;

    public TeamController(TeamModel teamModel, UnitControllerFactory unitControllerFactory, EventBus eventBus)
    {
        _teamModel = teamModel;
        _unitControllerFactory = unitControllerFactory;
        _eventBus = eventBus;
    }

    public void StartTurn()
    {
        _teamTurnStarted?.Invoke();
        _turnUnitsQueue = _teamModel.GetUnits();

        _currentUnitId = 0;
        _currentUnit = null;

        UnitTurn();
    }

    private UnitController GetUnitController(BattleUnitModel unitModel) =>
        _unitControllerFactory.GetController(unitModel.Team);

    private void OnUnitTurnFinished()
    {
        _eventBus.Fire(new UnitEndTurnEvent
        {
            uniModel = _currentUnit,
        });

        _currentUnitController.Deactivate();
        _currentUnitController.TurnFinished -= OnUnitTurnFinished;

        _currentUnitId++;

        _currentUnitController = null;
        _currentUnit = null;

        if (_currentUnitId >= _turnUnitsQueue.Count)
        {
            _teamTurnFinished?.Invoke();
            return;
        }

        UnitTurn();
    }

    private void UnitTurn()
    {
        _currentUnit = _turnUnitsQueue[_currentUnitId];

        this.Log("Unit turn: " + _currentUnit.Id);

        _currentUnitController = GetUnitController(_currentUnit);

        _currentUnitController.SetUnit(_currentUnit);
        _currentUnitController.PrepareForTurn();

        _currentUnitController.TurnFinished += OnUnitTurnFinished;
        _currentUnitController.Activate();

        _eventBus.Fire(new UnitStartTurnEvent
        {
            uniModel = _currentUnit,
        });
    }
}