using Common.Events;
using Game.Battle.Models;
using Zenject;

namespace Game.Battle.SubModules
{
    public class BattleSubModule
    {
        protected EventBus EventBus { get; private set; }
        protected BattleModel Model { get; private set; }

        [Inject]
        public void Construct(EventBus eventBus)
        {
            EventBus = eventBus;
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<BattleStartedEvent>(BattleStartedCallback);

            OnDispose();
            Model = null;
        }

        public void Initialize()
        {
            EventBus.Subscribe<BattleStartedEvent>(BattleStartedCallback);

            OnInitialize();
        }

        public void SetLevelModel(BattleModel battleModel)
        {
            Model = battleModel;
        }

        protected virtual void OnBattleStarted(BattleStartedEvent args) { }
        protected virtual void OnDispose() { }
        protected virtual void OnInitialize() { }

        private void BattleStartedCallback(BattleStartedEvent args) =>
            OnBattleStarted(args);
    }
}