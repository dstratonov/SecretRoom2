using System;
using System.Collections.Generic;
using Game.Battle.Abilities;
using Game.Models;

namespace Game.Battle.Configs
{
    [Serializable]
    public class UnitBattleData
    {
        [AbilityId]
        public string[] abilities;

        public List<StatModel> rawStats;
        public List<StatModel> statMultipliers;
    }
}