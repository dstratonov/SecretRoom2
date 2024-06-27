using System.Collections.Generic;

namespace Game.Battle.Stats
{
    public static class StatsUtils
    {
        public static List<Stat> GetPossibleEnemyStats()
        {
            return new List<Stat>
            {
                Stat.HP,
                Stat.AD,
                Stat.AP,
                Stat.DEF,
            };
        }
    }
}