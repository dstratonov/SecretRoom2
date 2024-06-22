using System.Collections.Generic;

namespace Game.Battle.SubModules
{
    public class BattleSubModulesHolder
    {
        public IEnumerable<IBattleStartedSubModule> BattleStartedSubModules { get; }
        public IEnumerable<IBattleFinishedSubModule> BattleFinishedSubModules { get; }
        public IEnumerable<ITeamTurnStartedSubModule> TeamTurnStartedSubModules { get; }
        public IEnumerable<ITeamTurnFinishedSubModule> TeamTurnFinishedSubModules { get; }
        public IEnumerable<IUnitTurnStartedSubModule> UnitTurnStartedSubModules { get; }
        public IEnumerable<IUnitTurnFinishedSubModule> UnitTurnFinishedSubModules { get; }

        public BattleSubModulesHolder(
            IEnumerable<IBattleStartedSubModule> battleStartedSubModules,
            IEnumerable<IBattleFinishedSubModule> battleFinishedSubModules,
            IEnumerable<ITeamTurnStartedSubModule> teamTurnStartedSubModules,
            IEnumerable<ITeamTurnFinishedSubModule> teamTurnFinishedSubModules,
            IEnumerable<IUnitTurnStartedSubModule> unitTurnStartedSubModules,
            IEnumerable<IUnitTurnFinishedSubModule> unitTurnFinishedSubModules)
        {
            BattleStartedSubModules = battleStartedSubModules;
            BattleFinishedSubModules = battleFinishedSubModules;
            TeamTurnStartedSubModules = teamTurnStartedSubModules;
            TeamTurnFinishedSubModules = teamTurnFinishedSubModules;
            UnitTurnStartedSubModules = unitTurnStartedSubModules;
            UnitTurnFinishedSubModules = unitTurnFinishedSubModules;
        }
    }
}