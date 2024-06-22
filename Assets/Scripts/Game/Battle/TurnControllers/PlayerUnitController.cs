using System.Collections.Generic;
using Game.Battle.Units.Systems.Abilities;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Battle.TurnControllers
{
    public class PlayerUnitController : UnitController
    {
        private readonly InputService _inputService;

        private bool _hasAbilities = true;
        private int _selectedAbilityIndex;
        private IReadOnlyList<string> _unitAbilities;

        private InputActions.BattleActions BattleActions => _inputService.BattleActions;

        public PlayerUnitController(InputService inputService)
        {
            _inputService = inputService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            BattleActions.Enable();

            BattleActions.SelectNext.performed += OnSelectNextPerformed;
            BattleActions.SelectPrevious.performed += OnSelectPreviousPerformed;
            BattleActions.PerformAbility.performed += OnAbilityPerformed;

            var abilitySystem = UnitModel.GetSystem<AbilitySystem>();

            _hasAbilities = abilitySystem.Count() != 0;

            if (!_hasAbilities)
            {
                return;
            }

            _unitAbilities = abilitySystem.AbilityIds;

            SelectAbility();
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

            _selectedAbilityIndex = Mathf.Clamp(_selectedAbilityIndex + 1, 0, _unitAbilities.Count - 1);

            SelectAbility();
        }

        private void OnSelectPreviousPerformed(InputAction.CallbackContext context)
        {
            if (!_hasAbilities)
            {
                return;
            }

            _selectedAbilityIndex = Mathf.Clamp(_selectedAbilityIndex - 1, 0, _unitAbilities.Count - 1);

            SelectAbility();
        }

        private void SelectAbility()
        {
        }

        private void OnAbilityPerformed(InputAction.CallbackContext context)
        {
            FinishTurn();
        }
    }
}