using Game.Battle.Units;

namespace Game.Battle.SubModules
{
    public interface IUnitTurnStartedSubModule
    {
        void OnUnitTurnStarted(BattleUnitModel unit);
    }
}