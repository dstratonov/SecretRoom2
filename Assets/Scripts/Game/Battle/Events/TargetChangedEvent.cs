using Game.Battle.Units.Systems.Pawn;

namespace Game.Battle.Events
{
    public struct TargetChangedEvent
    {
        public readonly UnitPawn caster;
        public readonly UnitPawn target;

        public TargetChangedEvent(UnitPawn target, UnitPawn caster)
        {
            this.target = target;
            this.caster = caster;
        }
    }
}