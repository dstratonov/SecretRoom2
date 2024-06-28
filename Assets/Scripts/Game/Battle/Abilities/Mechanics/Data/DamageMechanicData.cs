using System;
using Game.Battle.Damage;
using Game.Battle.Stats;

namespace Game.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class DamageMechanicData : MechanicData
    {
        public DamageType damageType;
        public int baseDamage;
        public StatBasedModel statModifiers;
    }
}