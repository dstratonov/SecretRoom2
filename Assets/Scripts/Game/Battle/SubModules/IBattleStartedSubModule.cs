using Cysharp.Threading.Tasks;
using Game.Battle.Models;

namespace Game.Battle.SubModules
{
    public interface IBattleStartedSubModule
    {
        UniTask OnBattleStarted(BattleModel model);
    }
}