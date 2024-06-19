using System;
using System.Collections.Generic;

namespace Game.Battle.Units.Systems
{
    public class UnitSystemContainer
    {
        private readonly Dictionary<Type, UnitSystem> _registeredSystems = new();

        public void AddSystem(UnitSystem system)
        {
            _registeredSystems.TryAdd(system.GetType(), system);
        }

        public IEnumerable<UnitSystem> GetSystems()
        {
            return _registeredSystems.Values;
        }

        public TActorSystem GetSystem<TActorSystem>() where TActorSystem : UnitSystem
        {
            _registeredSystems.TryGetValue(typeof(TActorSystem), out UnitSystem system);
            return (TActorSystem)system;
        }
    }
}