using Game.Battle.Models;

namespace Game.Battle.SubModules
{
    public interface ITeamTurnStartedSubModule
    {
        void OnTeamTurnStarted(Team team);
    }
}