namespace Game.Battle.Units.Systems.Pawn
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