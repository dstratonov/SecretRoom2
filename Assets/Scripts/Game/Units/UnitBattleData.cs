using System;
using Game.Battle.Abilities;

namespace Game.Battle.Configs
{
    [Serializable]
    public class UnitBattleData
    {
        [AbilityId]
        public string[] abilities;

        public int energy;
        public int health;
    }
}