using Game.Battle.Units.Systems;

namespace Game.Battle.Units
{
    public class PawnSystem : UnitSystem
    {
        public UnitPawn Pawn { get; }

        public PawnSystem(UnitPawn pawn)
        {
            Pawn = pawn;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            // Pawn.EnableComponents();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            // Pawn.DisableComponents();
        }
    }
}