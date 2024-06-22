using Game.Battle.Models;
using Game.Battle.Units;

namespace Game.Battle.Abilities
{
    public struct AbilityInvokeArgs
    {
        public AbilityModel ability;
        public BattleUnitModel caster;
        public BattleUnitModel target;
    }
}