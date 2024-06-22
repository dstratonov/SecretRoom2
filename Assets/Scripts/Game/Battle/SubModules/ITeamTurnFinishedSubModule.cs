using Game.Battle.Models;

namespace Game.Battle.SubModules
{
    public interface ITeamTurnFinishedSubModule
    {
        void OnTeamTurnFinished(Team team);
    }
}