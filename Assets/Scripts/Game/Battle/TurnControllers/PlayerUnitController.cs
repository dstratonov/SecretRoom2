using System.Collections.Generic;
using Common.Events;
using Game.Battle.Abilities;
using Game.Battle.Events;
using Game.Battle.Models;
using Game.Battle.SubModules.AbilityExecution;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;
using Game.Battle.Units.Systems.Pawn;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Battle.TurnControllers
{
    public class PlayerUnitController : UnitController
    {
        private readonly InputService _inputService;
        private readonly AbilityExecutionSubModule _abilityExecutionSubModule;
        private bool _hasAbilities = true;
        private int _selectedAbilityIndex;
        private IReadOnlyList<string> _unitAbilities;

        private InputActions.BattleActions BattleActions => _inputService.BattleActions;
        
        private bool _isSelectionStage = false;
        private int _targetIndex;
        private AbilityModel _selectedAbility;

        private IReadOnlyList<BattleUnitModel> _activeTeam;
        private AbilitySystem _abilitySystem;

        private EventBus _eventBus;



        public PlayerUnitController(InputService inputService, AbilityExecutionSubModule abilityExecutionSubModule, EventBus eventBus)
        {
            _eventBus = eventBus;
            _inputService = inputService;
            _abilityExecutionSubModule = abilityExecutionSubModule;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            BattleActions.Enable();

            BattleActions.SelectNext.performed += OnSelectNextPerformed;
            BattleActions.SelectPrevious.performed += OnSelectPreviousPerformed;
            BattleActions.PerformAbility.performed += OnAbilityPerformed;

            _abilitySystem = UnitModel.GetSystem<AbilitySystem>();

            _hasAbilities = _abilitySystem.Count() != 0;

            if (!_hasAbilities)
            {
                return;
            }

            _unitAbilities = _abilitySystem.AbilityIds;
            _selectedAbilityIndex = _abilitySystem.Count() / 2;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();

            BattleActions.Disable();
            
            BattleActions.SelectNext.performed -= OnSelectNextPerformed;
            BattleActions.SelectPrevious.performed -= OnSelectPreviousPerformed;
            BattleActions.PerformAbility.performed -= OnAbilityPerformed;
        }

        private void OnSelectNextPerformed(InputAction.CallbackContext context)
        {
            if (!_hasAbilities)
            {
                return;
            }
            if (_isSelectionStage)
            {
                _activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn.Deselect();
                _targetIndex = Mathf.Clamp(_targetIndex + 1, 0, _activeTeam.Count - 1);
                _activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn.Select();
                OnTargetChanged(_activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn);
            }
            else
            {
                _selectedAbilityIndex = Mathf.Clamp(_selectedAbilityIndex + 1, 0, _unitAbilities.Count - 1);
            }
        }

        private void OnTargetChanged(UnitPawn target)
        {
            _eventBus.Fire(new OnTargetChangedEvent(target, UnitModel.GetSystem<PawnSystem>().Pawn));
        }

        private void OnSelectPreviousPerformed(InputAction.CallbackContext context)
        {
            if (!_hasAbilities)
            {
                return;
            }
            if (_isSelectionStage)
            {
                _activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn.Deselect();
                _targetIndex = Mathf.Clamp(_targetIndex - 1, 0, _activeTeam.Count - 1);
                _activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn.Select();
                OnTargetChanged(_activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn);
            }
            else
            {
                _selectedAbilityIndex = Mathf.Clamp(_selectedAbilityIndex - 1, 0, _unitAbilities.Count - 1);
            }
        }

        private void FinishTurnWithArgs(AbilityInvokeArgs args)
        {
            _abilityExecutionSubModule.OnCastEnded -= FinishTurnWithArgs;
            FinishTurn();
        }

        private void OnAbilityPerformed(InputAction.CallbackContext context)
        {
            if (_isSelectionStage)
            {
                _isSelectionStage = false;
                _activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn.Deselect();
                BattleActions.Disable();
                _abilityExecutionSubModule.OnCastEnded += FinishTurnWithArgs;
                _abilityExecutionSubModule.CastAbility(_unitAbilities[_selectedAbilityIndex], UnitModel, _activeTeam[_targetIndex]);
            }
            else
            {
                _selectedAbility = _abilitySystem.GetAbilityModel(_unitAbilities[_selectedAbilityIndex]);
                switch(_selectedAbility._data.selection)
                {
                    case AbilitySelection.Enemy:
                        _activeTeam = battle.Model.EnemyTeam.GetUnits();
                        break;
                    case AbilitySelection.Ally:
                        _activeTeam = battle.Model.PlayerTeam.GetUnits();
                        break;
                }
                _targetIndex = _activeTeam.Count / 2;
                _activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn.Select();
                OnTargetChanged(_activeTeam[_targetIndex].GetSystem<PawnSystem>().Pawn);
                _isSelectionStage = true;
            }
        }
    }
}