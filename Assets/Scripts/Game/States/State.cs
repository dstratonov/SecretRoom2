using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.States
{
    public abstract class State
    {
        protected abstract string AnimKey { get; }
        
        private readonly StateMachine _stateMachine;
        private readonly Animator _animator;

        protected InputActions _inputActions = new();
        
        protected StateMachine StateMachine => _stateMachine;

        protected State(StateMachine stateMachine, Animator animator)
        {
            _stateMachine = stateMachine;
            _animator = animator;
        }
        
        public void Enter()
        {
            _inputActions.CharacterInputs.Enable();
            _animator.SetBool(AnimKey, true);
            OnEnter();
        }

        public void Exit()
        {
            _inputActions.CharacterInputs.Disable();
            _animator.SetBool(AnimKey, false);
            OnExit();
        }

        public void Update()
        {
            
        }

        protected virtual void OnEnter()
        {
            
        }

        protected virtual void OnExit()
        {
            
        }
        
        protected virtual void OnUpdate()
        {
            
        }
    }
}