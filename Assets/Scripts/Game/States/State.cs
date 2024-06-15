namespace Game.States
{
    public abstract class State
    {
        protected InputActions _inputActions = new();
        private readonly PlayerCharacter _character;

        protected abstract string AnimKey { get; }

        protected StateMachine StateMachine { get; }
        protected PlayerCharacter Character => _character;

        protected State(StateMachine stateMachine, PlayerCharacter character)
        {
            StateMachine = stateMachine;
            _character = character;
        }

        public void AnimationFinishTrigger()
        {
            OnAnimationFinishTrigger();
        }

        public void Enter()
        {
            _inputActions.CharacterInputs.Enable();
            _character.Animator.SetBool(AnimKey, true);
            OnEnter();
        }

        public void Exit()
        {
            _inputActions.CharacterInputs.Disable();
            _character.Animator.SetBool(AnimKey, false);
            OnExit();
        }

        public void Update() { }

        protected virtual void OnAnimationFinishTrigger() { }

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        protected virtual void OnUpdate() { }
    }
}