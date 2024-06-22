using Zenject;

namespace Game.Battle.SubModules.BattleCineMachine
{
    public class BattleCineMachineSubModuleInstaller : Installer<BattleCineMachineSubModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleCineMachineSubModule>().AsSingle();
        }
    }
}