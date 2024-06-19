using Game.Battle.Models;

namespace Game.Battle.Units.Systems
{
    public abstract class UnitSystem
    {
        protected BattleUnitModel Owner { get; private set; }

        public void Dispose()
        {
            OnDispose();
        }

        public void EndTurnUpdate()
        {
            OnTurnEnded();
        }

        public void Initialize(BattleUnitModel owner)
        {
            Owner = owner;

            OnInitialize();
        }

        public void StartTurnUpdate()
        {
            OnTurnStarted();
        }

        protected virtual void OnDispose() { }

        protected virtual void OnInitialize() { }

        protected virtual void OnTurnEnded() { }

        protected virtual void OnTurnStarted() { }
    }
}