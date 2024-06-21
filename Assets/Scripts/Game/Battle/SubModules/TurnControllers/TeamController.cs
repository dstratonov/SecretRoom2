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

    private UnitController _currentUnitController;

    public event Action _teamTurnStarted;
    public event Action _teamTurnFinished;

    public TeamController(TeamModel teamModel, UnitControllerFactory unitControllerFactory){
        _teamModel = teamModel;
        _unitControllerFactory = unitControllerFactory;
    }

    public void StartTurn()
    {
        _teamTurnStarted?.Invoke();
        _turnUnitsQueue = _teamModel.GetUnits();

        _currentUnitId = 0;

        UnitTurn();
    }
    
    private UnitController GetUnitController(BattleUnitModel unitModel) =>
            _unitControllerFactory.GetController(unitModel.Team);

    private void UnitTurn()
    {
        var currentUnit = _turnUnitsQueue[_currentUnitId];

        this.Log("Unit turn: " + currentUnit.Id);

        _currentUnitController = GetUnitController(currentUnit);

        _currentUnitController.SetUnit(currentUnit);
        _currentUnitController.PrepareForTurn();

        _currentUnitController.TurnFinished += OnUnitTurnFinished;
        _currentUnitController.Activate();


    }

    private void OnUnitTurnFinished()
    {
        _currentUnitController.Deactivate();
        _currentUnitController.TurnFinished -= OnUnitTurnFinished;
        _currentUnitController = null;

        _currentUnitId++;
        if (_currentUnitId >= _turnUnitsQueue.Count)
        {
            _teamTurnFinished?.Invoke();
            return;
        }

        UnitTurn();
    }
}
