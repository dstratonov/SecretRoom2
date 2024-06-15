using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.States
{
    public class MoveState : State
    {
        protected override string AnimKey => "move";

        protected override void OnEnter()
        {
            base.OnEnter();

            _inputActions.CharacterInputs.Idle.performed += IdlePerformed;
        }

        private void IdlePerformed(InputAction.CallbackContext obj)
        {
            StateMachine.SetState<IdleState>();
        }

        protected override void OnExit()
        {
            base.OnExit();

            _inputActions.CharacterInputs.Idle.performed -= IdlePerformed;
        }

        public MoveState(StateMachine stateMachine, PlayerCharacter character) : base(stateMachine, character)
        {
            
        }
    }
}