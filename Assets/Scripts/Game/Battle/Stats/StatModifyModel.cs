using System;

namespace Game.Battle.Stats
{
    [Serializable]
    public struct StatModifyModel
    {
        public Stat stat;
        public float multiplier;
    }
}