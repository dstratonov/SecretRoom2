using Cysharp.Threading.Tasks;

namespace Game.Battle.SubModules.AbilityExecution
{
    public interface IAbilityExecutor
    {
        UniTask Execute();
    }
}