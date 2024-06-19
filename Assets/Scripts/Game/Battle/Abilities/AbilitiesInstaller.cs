using Game.Battle.Abilities.Mechanics;
using Zenject;

namespace Game.Battle.Abilities
{
    public class AbilitiesInstaller : Installer<AbilitiesInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<AbilityExecutionService>()
                .FromSubContainerResolve()
                .ByMethod(InstallService)
                .AsSingle();
        }

        private void InstallService(DiContainer subContainer)
        {
            subContainer.Bind<AbilityExecutionService>().AsSingle();
            subContainer.Bind<MechanicsFactory>().AsSingle();
        }
    }
}