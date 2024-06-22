namespace Game.Battle.TurnControllers
{
    public class AiUnitController : UnitController
    {
        protected override void OnActivate()
        {
            base.OnActivate();
            FinishTurn();
        }
    }
}