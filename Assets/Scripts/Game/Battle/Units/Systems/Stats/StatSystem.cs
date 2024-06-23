using System.Collections.Generic;
using Common.Reactive;
using Game.Battle.Stats;

namespace Game.Battle.Units.Systems.Stats
{
    public class StatsSystem : UnitSystem
    {  
        private readonly Dictionary<Stat, ReactiveValue> _stats = new();

        public StatsSystem(Dictionary<Stat, ReactiveValue> stats)
        {
            _stats = stats;
        }

        public ReactiveValue GetStat(Stat stat)
        {
            _stats.TryAdd(stat, new ReactiveValue());
            return _stats[stat];
        }

        public float GetStatValue(Stat stat) =>
            GetStat(stat).Current;
    }
}