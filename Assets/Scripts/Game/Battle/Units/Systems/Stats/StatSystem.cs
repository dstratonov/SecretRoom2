using System.Collections.Generic;
using System.Text;
using Common.Reactive;
using Game.Battle.Stats;

namespace Game.Battle.Units.Systems.Stats
{
    public class StatsSystem : UnitSystem
    {
        private readonly Dictionary<Stat, ReactiveValue> _stats;

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
            GetStat(stat).Max;

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Stat {Stat.HP} = {GetStat(Stat.HP).Current} / {GetStat(Stat.HP).Max}");
            sb.AppendLine($"Stat {Stat.EN} = {GetStat(Stat.EN).Current} / {GetStat(Stat.EN).Max}");
            sb.AppendLine($"Stat {Stat.AD} = {GetStat(Stat.AD).Max} ");
            sb.AppendLine($"Stat {Stat.AP} = {GetStat(Stat.AP).Max} ");
            sb.AppendLine($"Stat {Stat.DEF} = {GetStat(Stat.DEF).Max}");
            sb.AppendLine($"Stat {Stat.ENR} = {GetStat(Stat.ENR).Max}");

            return sb.ToString();
        }
    }
}