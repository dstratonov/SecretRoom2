using Common.Extensions;
using Zenject;

namespace Game.Battle.SubModules.TurnControllers
{
    public class TurnControllerInstaller : Installer<TurnControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindBattleSubModuleFromSubContainer<TurnControllerSubModule>(InstallSubModule);
        }
        
        private void InstallSubModule(DiContainer subContainer)
        {
            subContainer.BindBattleSubModule<TurnControllerSubModule>();

            subContainer.Bind<TurnControllerFactory>().AsSingle();
        }
    }
}