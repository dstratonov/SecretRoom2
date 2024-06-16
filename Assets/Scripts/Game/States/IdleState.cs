using UnityEngine;
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

            _inputActions.CharacterInputs.Jump.performed += JumpOnPerformed;
        }

        protected override void OnExit()
        {
            base.OnExit();

            _inputActions.CharacterInputs.Jump.performed -= JumpOnPerformed;
        }

        private void JumpOnPerformed(InputAction.CallbackContext context)
        {
            StateMachine.SetState<JumpState>();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            var x = _inputActions.CharacterInputs.Horizontal.ReadValue<float>();
            var y = _inputActions.CharacterInputs.Vertical.ReadValue<float>();

            var direction = new Vector2(x, y);

            if (direction.magnitude != 0)
            {
                StateMachine.SetState<MoveState>();
            }
        }
    }
}