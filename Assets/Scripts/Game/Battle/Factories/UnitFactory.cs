using System.Collections.Generic;
using System.Linq;
using Common.Reactive;
using Game.Abilities;
using Game.Battle.Abilities;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;
using Game.Battle.Units.Systems.Pawn;
using Game.Battle.Units.Systems.Stats;
using Game.Units;
using UnityEngine;
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

        public BattleUnitModel Create(string id, UnitViewData viewData, Dictionary<Stat, ReactiveValue> stats,
            string[] abilities)
        {
            var pawn = _instantiator.InstantiatePrefabForComponent<UnitPawn>(viewData.unitPawn);

            var unitModel = new BattleUnitModel(id, viewData);

            unitModel.AddSystem(new PawnSystem(pawn));
            unitModel.AddSystem(new StatsSystem(stats));
            unitModel.AddSystem(new AbilitySystem());

            AddAbilities(unitModel, abilities);

            return unitModel;
        }

        public BattleUnitModel Create(string id, UnitViewData viewData, IReadOnlyDictionary<Stat, int> stats,
            string[] abilities) =>
            Create(
                id,
                viewData,
                stats.ToDictionary(x => x.Key, x => new ReactiveValue(x.Value)),
                abilities);

        public BattleUnitModel Create(UnitConfig config) =>
            Create(
                config.id,
                config.viewData,
                config.battleData.rawStats.ToDictionary(x => x.stat, x => Mathf.FloorToInt(x.value)),
                config.battleData.abilities);

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