using Game.Battle.Models;

namespace Game.Battle.Abilities
{
    public struct AbilityInvokeArgs
    {
        public AbilityModel ability;
        public BattleUnitModel caster;
        public BattleUnitModel target;
    }
}