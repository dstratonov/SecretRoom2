using Game.Battle.Models;

namespace Game.Battle.SubModules
{
    public interface IBattleStartedSubModule
    {
        void OnBattleStarted(BattleModel model);
    }
}