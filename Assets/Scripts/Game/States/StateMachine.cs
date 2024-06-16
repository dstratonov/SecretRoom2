using Zenject;

namespace Game.States
{
    public class StateMachine
    {
        public State CurrentState => _currentState;
        
        private readonly IInstantiator _instantiator;
        private readonly PlayerCharacter _character;

        private State _currentState;

        public StateMachine(IInstantiator instantiator, PlayerCharacter character)
        {
            _instantiator = instantiator;
            _character = character;
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
            return _instantiator.Instantiate<TState>(new object[] { this, _character });
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}