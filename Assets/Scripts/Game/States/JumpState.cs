using UnityEngine;

namespace Game.States
{
    public class JumpState : State
    {
        protected override string AnimKey => "jump";

        private bool _isJumping = false;
     
        public JumpState(StateMachine stateMachine, PlayerCharacter character) : base(stateMachine, character) { }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            // Debug.Log("State info");
            // Debug.Log(_isJumping);
            // Debug.Log(IsGrounded());
            // Debug.Log("State info");

            if (IsGrounded() && _isJumping && Character.Rigidbody.velocity.y < 0.0f)
            {
                Debug.Log("Off jump");
                _isJumping = false;
                StateMachine.SetState<IdleState>();
            }
            
        }

        protected override void OnAnimationFinishTrigger()
        {
            base.OnAnimationFinishTrigger();
            Debug.Log("FinishTrigger");
            Character.Rigidbody.velocity += new Vector3(0.0f, 2.5f, 0.0f) + Character.Rigidbody.velocity * 2.0f;
            _isJumping = true;
        }
        
        private bool IsGrounded()
        {
            RaycastHit hit;
            float dist = 0.4f;
            float radius = 0.3f;  
            
            if (Physics.SphereCast(Character.transform.position + Vector3.up * dist, radius, -Vector3.up, out hit, dist, Character.GroundMask))
            {
                if (Vector3.Distance(hit.point, Character.transform.position) < dist)  return true;
            }
            
            return false;
        }
    }
}