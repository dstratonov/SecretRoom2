using System;
using Common.Loggers;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Stats;
using UnityEngine;

namespace Game.Battle.Damage
{
    public class DamageCalculationService
    {
        public event Action<DamageData> DamageDealt;
        
        public DamageData CreateDamage(BattleUnitModel dealer, BattleUnitModel taker, int damage, DamageType type)
        {
            var takerStats = taker.GetSystem<StatsSystem>();
            
            int calculatedDamage =  CalculateRawAttack(takerStats, damage, type);

            takerStats.GetStat(Stat.HP).Remove(calculatedDamage);

            this.Log($"Unit {taker.Id} from {taker.Team} takes {calculatedDamage} damage");
            
            var damageData = new DamageData
            {
                dealer = dealer,
                taker = taker,
                damage = calculatedDamage,
                type = type,
            };
            
            DamageDealt?.Invoke(damageData);
            
            return damageData;
        }
        
        private int CalculateRawAttack(StatsSystem takerStats, int damage, DamageType damageType)
        {
            float damageReduction = 0;

            if (damageType == DamageType.Attack)
            {
                damageReduction = takerStats.GetStatValue(Stat.DEF);
            }

            int totalDamage = Mathf.CeilToInt(Mathf.Max(0, damage - damageReduction)); 
         
            return totalDamage;
        }
    }
}