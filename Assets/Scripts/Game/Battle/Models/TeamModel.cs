using System;
using System.Collections.Generic;

namespace Game.Battle.Models
{
    [Serializable]
    public class TeamModel
    {
        private readonly List<UnitModel> _units = new();
        
        public Team Team { get; }

        public TeamModel(Team team)
        {
            Team = team;
        }

        public void AddUnit(UnitModel model)
        {
            _units.Add(model);
        }

        public int GetCharactersCount() =>
            _units.Count;

        public IReadOnlyList<UnitModel> GetUnits() =>
            _units;
    }
}