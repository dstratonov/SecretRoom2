using Common.Events;
using Game.Battle.Factories;
using UnityEngine;
using Zenject;

namespace Game.Battle
{
    public class BattleInstaller : MonoInstaller<BattleInstaller>
    {
        [SerializeField] private BattleField _battleField;
        [SerializeField] private BattleInitializer _battleInitializer;

        public override void InstallBindings()
        {
            Container.BindInstance(_battleField);
            Container.BindInstance(_battleInitializer);

            Container.Bind<UnitFactory>().AsSingle();
            Container.Bind<Battle>().AsSingle();
            Container.Bind<EventBus>().AsSingle();
        }
    }
}