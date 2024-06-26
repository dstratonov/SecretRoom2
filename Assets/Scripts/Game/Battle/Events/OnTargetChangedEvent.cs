using Game.Battle.Units;
using Game.Battle.Units.Systems.Pawn;

namespace Game.Battle.Events
{
    public struct OnTargetChangedEvent 
    {
        public UnitPawn target;

        public UnitPawn caster;

        public OnTargetChangedEvent(UnitPawn target, UnitPawn caster)
        {
            this.target = target;
            this.caster = caster;
        }
    }
}
