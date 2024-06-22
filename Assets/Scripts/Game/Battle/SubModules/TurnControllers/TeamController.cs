using System;
using System.Collections;
using System.Collections.Generic;
using Common.Loggers;
using Game.Battle.Models;
using Game.Battle.SubModules.TurnControllers;
using UnityEngine;

public class TeamController
{
    private TeamModel _teamModel;
    private IReadOnlyList<BattleUnitModel> _turnUnitsQueue;
    private readonly UnitControllerFactory _unitControllerFactory;
    private int _currentUnitId;
    
    private BattleUnitModel _currentUnit;

    private UnitController _currentUnitController;

    public event Action _teamTurnStarted;
    public event Action _teamTurnFinished;

    public event Action<UnitTurnInvokeArgs> _unitTurnStarted;
    public event Action<UnitTurnInvokeArgs> _unitTurnFinished;

    public TeamController(TeamModel teamModel, UnitControllerFactory unitControllerFactory){
        _teamModel = teamModel;
        _unitControllerFactory = unitControllerFactory;
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

    private void UnitTurn()
    {
        _currentUnit = _turnUnitsQueue[_currentUnitId];

        this.Log("Unit turn: " + _currentUnit.Id);

        _currentUnitController = GetUnitController(_currentUnit);

        _currentUnitController.SetUnit(_currentUnit);
        _currentUnitController.PrepareForTurn();

        _currentUnitController.TurnFinished += OnUnitTurnFinished;
        _currentUnitController.Activate();
        _unitTurnStarted?.Invoke(new UnitTurnInvokeArgs{uniModel = _currentUnit});
    }

    private void OnUnitTurnFinished()
    {
        _unitTurnFinished?.Invoke(new UnitTurnInvokeArgs{uniModel = _currentUnit});

        _currentUnitController.Deactivate();
        _currentUnitController.TurnFinished -= OnUnitTurnFinished;
        _currentUnitController = null;

        _currentUnitId++;
        _currentUnit = null;
        if (_currentUnitId >= _turnUnitsQueue.Count)
        {
            _teamTurnFinished?.Invoke();
            return;
        }
        UnitTurn();
    }
}
