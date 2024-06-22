using Zenject;

namespace Game.Battle.TurnControllers
{
    public class TurnControllerInstaller : Installer<TurnControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TurnController>()
                .FromSubContainerResolve()
                .ByMethod(InstallModule)
                .AsSingle();
        }

        private void InstallModule(DiContainer subContainer)
        {
            subContainer.Bind<TurnController>().AsSingle();
            subContainer.Bind<UnitControllerFactory>().AsSingle();
        }
    }
}