using Zenject;

namespace Game.Battle.SubModules.BattleCinemachine
{
    public class BattleCineMachineSubModuleInstaller : Installer<BattleCineMachineSubModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleCineMachineSubModule>().AsSingle();
        }
    }
}