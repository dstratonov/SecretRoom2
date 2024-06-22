using Game.Battle.Units;

namespace Game.Battle.SubModules
{
    public interface IUnitTurnFinishedSubModule
    {
        void OnUnitTurnFinished(BattleUnitModel unit);
    }
}