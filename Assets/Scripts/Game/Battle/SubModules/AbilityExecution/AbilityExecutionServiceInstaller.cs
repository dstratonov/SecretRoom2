using Common.Extensions;
using Game.Battle.Abilities.Mechanics;
using Zenject;

namespace Game.Battle.Abilities
{
    public class AbilityExecutionServiceInstaller : Installer<AbilityExecutionServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindBattleSubModuleFromSubContainer<AbilityExecutionSubModule>(InstallService);
        }

        private void InstallService(DiContainer subContainer)
        {
            subContainer.BindBattleSubModule<AbilityExecutionSubModule>();
            
            subContainer.Bind<MechanicsFactory>().AsSingle();
        }
    }
}