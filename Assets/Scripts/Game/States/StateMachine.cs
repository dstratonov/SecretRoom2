using UnityEngine;
using Zenject;

namespace Game.States
{
    public class StateMachine
    {
        private readonly IInstantiator _instantiator;
        private readonly Animator _animator;

        private State _currentState;

        public StateMachine(IInstantiator instantiator, Animator animator)
        {
            _instantiator = instantiator;
            _animator = animator;
        }
        
        public void SetState<TState>() where TState : State
        {
            _currentState?.Exit();

            var state = CreateState<TState>();
            _currentState = state;
            
            _currentState.Enter();
        }

        private TState CreateState<TState>() where TState : State
        {
            return _instantiator.Instantiate<TState>(new object[] { this, _animator });
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}