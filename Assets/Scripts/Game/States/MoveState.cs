using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.States
{
    public class MoveState : State
    {
        private const float speed = 2.0f;
        
        protected override string AnimKey => "move";

        private Vector3 _currentInputVector;
        private Vector3 _currentVelocity;
        
        public MoveState(StateMachine stateMachine, PlayerCharacter character) : base(stateMachine, character) { }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            var x = _inputActions.CharacterInputs.Horizontal.ReadValue<float>();
            var y = _inputActions.CharacterInputs.Vertical.ReadValue<float>();

            var direction = new Vector3(x, 0, y);
            
            _currentInputVector = Vector3.SmoothDamp(_currentInputVector, direction, ref _currentVelocity, .2f);
            
            float velocityHorizontal = Vector3.Dot(Character.RigidBody.velocity, Character.transform.right) / speed;
            float velocityVertical = Vector3.Dot(Character.RigidBody.velocity, Character.transform.forward) / speed;
            
            Character.Animator.SetFloat("movementX", _currentInputVector.x);
            Character.Animator.SetFloat("movementY", _currentInputVector.z);
            
            if (direction.magnitude == 0)
            {
                StateMachine.SetState<IdleState>();
            }
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            
            Character.RigidBody.velocity = new Vector3(
                _currentInputVector.x * speed,
                Character.RigidBody.velocity.y, 
                _currentInputVector.z * speed);
        }
    }
}