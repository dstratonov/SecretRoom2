using System;
using System.Collections.Generic;
using Game.Battle.Units;

namespace Game.Battle.Models
{
    [Serializable]
    public class TeamModel
    {
        private readonly List<BattleUnitModel> _units = new();
        
        public Team Team { get; }

        public TeamModel(Team team)
        {
            Team = team;
        }

        public void AddUnit(BattleUnitModel model)
        {
            model.SetTeam(Team);
            
            _units.Add(model);
        }

        public int GetCharactersCount() =>
            _units.Count;

        public IReadOnlyList<BattleUnitModel> GetUnits() =>
            _units;
    }
}