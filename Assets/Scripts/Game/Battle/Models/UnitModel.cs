using System;
using Game.Battle.Configs;
using Game.Battle.Units;

namespace Game.Battle.Models
{
    [Serializable]
    public class UnitModel
    {
        private UnitConfig _unitConfig;
        
        public BattleUnit Unit { get; }

        public UnitModel(UnitConfig config, BattleUnit unit)
        {
            _unitConfig = config;
            Unit = unit;
        }
    }
}