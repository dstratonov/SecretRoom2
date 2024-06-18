using Game.Battle.Configs;
using Game.Battle.Models;
using Game.Battle.Units;
using Zenject;

namespace Game.Battle.Factories
{
    public class UnitFactory
    {
        private readonly IInstantiator _instantiator;

        public UnitFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public UnitModel Create(UnitConfig config)
        {
            var unitInstance = _instantiator.InstantiatePrefabForComponent<BattleUnit>(config.unit);

            //create stats
            //create abilities

            return new UnitModel(config, unitInstance);
        }
    }
}