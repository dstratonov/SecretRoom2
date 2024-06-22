using Game.Battle.Abilities.Mechanics;
using Zenject;

namespace Game.Battle.SubModules.AbilityExecution
{
    public class AbilityExecutionServiceInstaller : Installer<AbilityExecutionServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<AbilityExecutionSubModule>()
                .FromSubContainerResolve()
                .ByMethod(InstallModule)
                .AsSingle();
        }

        private void InstallModule(DiContainer subContainer)
        {
            subContainer.BindInterfacesAndSelfTo<AbilityExecutionSubModule>().AsSingle();
            subContainer.Bind<MechanicsFactory>().AsSingle();
        }
    }
}