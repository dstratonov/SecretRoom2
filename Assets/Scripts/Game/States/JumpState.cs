using UnityEngine;

namespace Game.States
{
    public class JumpState
    {
        // private bool _isJumping;
        //
        // protected override string AnimKey => "jump";
        //
        // public JumpState(StateMachine stateMachine, PlayerCharacter character) : base(stateMachine, character) { }
        //
        // protected override void OnEnter()
        // {
        //     base.OnEnter();
        //
        //     Character.AnimationTriggers.Subscribe("jumpStart", OnJumpStarted);
        // }
        //
        // protected override void OnExit()
        // {
        //     base.OnExit();
        //
        //     Character.AnimationTriggers.Unsubscribe("jumpStart", OnJumpStarted);
        // }
        //
        // protected override void OnUpdate()
        // {
        //     base.OnUpdate();
        //
        //     if (IsGrounded() && _isJumping && Character.RigidBody.velocity.y < 0.0f)
        //     {
        //         _isJumping = false;
        //         StateMachine.SetState<IdleState>();
        //     }
        // }
        //
        // private bool IsGrounded()
        // {
        //     //todo to configs
        //     const float dist = 1.2f;
        //     const float radius = 0.3f;
        //
        //     if (Physics.SphereCast(Character.transform.position, radius, -Vector3.up,
        //             out RaycastHit hit, 50, Character.GroundMask))
        //     {
        //         if (Vector3.Distance(hit.point, Character.transform.position) < dist)
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
        //
        // private void OnJumpStarted()
        // {
        //     //todo to config
        //     Character.RigidBody.velocity += new Vector3(0.0f,  10f, 0.0f) + Character.RigidBody.velocity * 2.0f;
        //     _isJumping = true;
        // }
    }
}