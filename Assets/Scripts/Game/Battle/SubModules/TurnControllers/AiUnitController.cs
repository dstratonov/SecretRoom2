namespace Game.Battle.SubModules.TurnControllers
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