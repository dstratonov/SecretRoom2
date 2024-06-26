using Game.Battle.Abilities;
using Game.Battle.Units;

namespace Game.Battle.Events
{
    public struct AbilityCastEndEvent
    {
        public AbilityModel ability;
        public BattleUnitModel caster;
        public BattleUnitModel target;
    }
}