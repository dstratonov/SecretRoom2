using Game.Battle.Abilities;
using Game.Battle.Factories;
using Game.Battle.SubModules.TurnControllers;
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
            Container.Bind<BattleBuilder>().AsSingle();

            
            BindSubModules();
        }

        private void BindSubModules()
        {
            AbilityExecutionServiceInstaller.Install(Container);
            TurnControllerInstaller.Install(Container);
        }
    }
}