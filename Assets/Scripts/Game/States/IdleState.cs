using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.States
{
    public class IdleState : State
    {
        protected override string AnimKey => "idle";

        protected override void OnEnter()
        {
            base.OnEnter();
            
            _inputActions.CharacterInputs.Move.performed += MoveOnperformed;
        }

        protected override void OnExit()
        {
            base.OnExit();
            
            _inputActions.CharacterInputs.Move.performed -= MoveOnperformed;
        }

        private void MoveOnperformed(InputAction.CallbackContext obj)
        {
            StateMachine.SetState<MoveState>();
        }

        public IdleState(StateMachine stateMachine, Animator animator) : base(stateMachine, animator)
        {
            
        }
    }
}