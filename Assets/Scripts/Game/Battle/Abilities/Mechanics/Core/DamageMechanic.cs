using Common.Loggers;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Damage;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Stats;
using UnityEngine;
using Zenject;

namespace Game.Battle.Abilities.Mechanics.Core
{
    public class DamageMechanic : Mechanic<DamageMechanicData>
    {
        private DamageCalculationService _damageCalculationService;
        
        [Inject]
        public void Construct(DamageCalculationService damageCalculationService)
        {
            _damageCalculationService = damageCalculationService;
        }
        
        protected override void OnInvoke(BattleUnitModel target)
        {
            int damage = Data.baseDamage;

            if (Data.statModifiers.statBased)
            {
                var casterStats = Caster.GetSystem<StatsSystem>();
                    
                foreach (StatModifyModel modifier in Data.statModifiers.statModifiers)
                {
                    damage += Mathf.CeilToInt(modifier.multiplier * casterStats.GetStatValue(modifier.stat));
                }
            }

            _damageCalculationService.CreateDamage(Caster, target, damage, Data.damageType);
        }
    }
}