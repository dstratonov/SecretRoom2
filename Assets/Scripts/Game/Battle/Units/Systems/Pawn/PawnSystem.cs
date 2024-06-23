namespace Game.Battle.Units.Systems.Pawn
{
    public class PawnSystem : UnitSystem
    {
        public UnitPawn Pawn { get; }

        public PawnSystem(UnitPawn pawn)
        {
            Pawn = pawn;
        }
    }
}