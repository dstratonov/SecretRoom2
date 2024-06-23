using System;
using System.Collections.Generic;
using Common.Factories;
using Game.Battle.Abilities.Mechanics.Core;
using Game.Battle.Abilities.Mechanics.Data;
using Zenject;

namespace Game.Battle.Abilities.Mechanics
{
    public class MechanicsFactory : TypedFactory<MechanicData, Mechanic>
    {
        protected override bool IsPoolable => true;

        protected override Dictionary<Type, Type> Types =>
            new()
            {
                { typeof(HealMechanicData), typeof(HealMechanic) },
                { typeof(DamageMechanicData), typeof(DamageMechanic) },
            };

        public MechanicsFactory(DiContainer container) : base(container) { }

        public override Mechanic Create(MechanicData data)
        {
            Mechanic mechanic = CreateRaw(data);

            return mechanic;
        }
    }
}