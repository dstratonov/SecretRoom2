using Cinemachine;
using Game.Battle.Factories;
using Game.Battle.SubModules;
using Game.Battle.SubModules.AbilityExecution;
using Game.Battle.SubModules.BattleCineMachine;
using Game.Battle.SubModules.HUD;
using Game.Battle.TurnControllers;
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
            Container.Bind<BattleSubModulesHolder>().AsSingle();

            TurnControllerInstaller.Install(Container);

            BindSubModules();
        }

        private void BindSubModules()
        {
            Container.BindInterfacesAndSelfTo<BattleHUDSubModule>().AsSingle();
            AbilityExecutionServiceInstaller.Install(Container);
            BattleCineMachineSubModuleInstaller.Install(Container);
        }
    }
}