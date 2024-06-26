using System;
using System.Collections.Generic;
using Game.Battle.Abilities.Mechanics.Data;

namespace Game.Battle.Abilities
{
    [Serializable]
    public class AbilityModel
    {
        public string Id => _data.id;
        public AbilityBattleConfig Data => _data;
        
        private readonly AbilityBattleConfig _data;
        
        public AbilityModel(AbilityBattleConfig data)
        {
            _data = data;
        }
        
        public bool CanUse(int energyCount) =>
            _data.energyCost <= energyCount;

        public int GetCost() =>
            _data.energyCost;

        public IEnumerable<MechanicData> GetMechanics() =>
            _data.mechanics;
    }
}