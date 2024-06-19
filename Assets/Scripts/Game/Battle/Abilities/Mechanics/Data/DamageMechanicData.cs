using System;
using Game.Battle.Stats;

namespace Game.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class DamageMechanicData : MechanicData
    {
        public int flatAmount;
        public StatBasedModel statModifiers;
    }
}