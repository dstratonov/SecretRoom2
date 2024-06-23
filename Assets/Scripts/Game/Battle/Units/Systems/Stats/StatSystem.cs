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
        
        public StatsSystem(IReadOnlyDictionary<Stat, int> stats)
        {
            FillStats(stats);
        }
        
        private void FillStats(IReadOnlyDictionary<Stat, int> stats)
        {
            foreach (KeyValuePair<Stat,int> keyValueStat in stats)
            {
                _stats.Add(keyValueStat.Key, new ReactiveValue(keyValueStat.Value));
            }
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