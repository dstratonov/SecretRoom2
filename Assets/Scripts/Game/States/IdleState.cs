using UnityEngine.InputSystem;

namespace Game.States
{
    public class IdleState : State
    {
        protected override string AnimKey => "idle";

        public IdleState(StateMachine stateMachine, PlayerCharacter character) : base(stateMachine, character) { }

        protected override void OnEnter()
        {
            base.OnEnter();

            _inputActions.CharacterInputs.Move.performed += MoveOnPerformed;
            _inputActions.CharacterInputs.Jump.performed += JumpOnPerformed;
        }

        protected override void OnExit()
        {
            base.OnExit();

            _inputActions.CharacterInputs.Move.performed -= MoveOnPerformed;
            _inputActions.CharacterInputs.Jump.performed -= JumpOnPerformed;
        }

        private void JumpOnPerformed(InputAction.CallbackContext context)
        {
            StateMachine.SetState<JumpState>();
        }

        private void MoveOnPerformed(InputAction.CallbackContext context)
        {
            StateMachine.SetState<MoveState>();
        }
    }
}