using System;
using Game.Battle.Stats;

namespace Game.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class HealMechanicData : MechanicData
    {
        public int flatAmount;
        public StatBasedModel statModifiers;
    }
}