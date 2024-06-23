using Game.Abilities;
using Game.Battle.Abilities;
using Game.Battle.Configs;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;
using Game.Battle.Units.Systems.Pawn;
using Game.Battle.Units.Systems.Stats;
using Zenject;

namespace Game.Battle.Factories
{
    public class UnitFactory
    {
        private readonly AbilityContainerConfig _abilityContainer;
        private readonly IInstantiator _instantiator;

        public UnitFactory(IInstantiator instantiator, AbilityContainerConfig abilityContainer)
        {
            _instantiator = instantiator;
            _abilityContainer = abilityContainer;
        }

        public BattleUnitModel Create(UnitConfig config)
        {
            var pawn = _instantiator.InstantiatePrefabForComponent<UnitPawn>(config.viewData.unitPawn);

            var unitModel = new BattleUnitModel(config);

            unitModel.AddSystem(new PawnSystem(pawn));
            unitModel.AddSystem(new HealthStatSystem(config.battleData.health));
            unitModel.AddSystem(new EnergyStatSystem(config.battleData.energy));
            unitModel.AddSystem(new AbilitySystem());

            AddAbilities(unitModel, config.battleData.abilities);

            return unitModel;
        }

        private void AddAbilities(BattleUnitModel unitModel, string[] abilityIds)
        {
            foreach (string abilityId in abilityIds)
            {
                AbilityInfo abilityInfo = _abilityContainer.GetAbilityInfo(abilityId);

                if (abilityInfo.battleConfig != null)
                {
                    unitModel
                        .GetSystem<AbilitySystem>()
                        .AddAbility(new AbilityModel(abilityInfo.battleConfig));
                }
            }
        }
    }
}