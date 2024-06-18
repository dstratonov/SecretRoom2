namespace Game.Input
{
    public class InputService
    {
        private readonly InputActions _inputActions = new();

        public InputActions.BattleActions BattleActions => _inputActions.Battle;
    }
}