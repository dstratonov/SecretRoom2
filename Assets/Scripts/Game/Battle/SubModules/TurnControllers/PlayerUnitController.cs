using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Battle.SubModules.TurnControllers
{
    public class PlayerUnitController : UnitController
    {
        private readonly InputService _inputService;

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
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();

            BattleActions.SelectNext.performed -= OnSelectNextPerformed;
            BattleActions.SelectPrevious.performed -= OnSelectPreviousPerformed;
        }

        private void OnSelectNextPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Select Next");
        }

        private void OnSelectPreviousPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Select Previous");
        }
    }
}