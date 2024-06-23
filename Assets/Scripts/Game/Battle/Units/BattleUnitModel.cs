using System;
using System.Collections.Generic;
using Game.Battle.Configs;
using Game.Battle.Models;
using Game.Battle.Units.Systems;

namespace Game.Battle.Units
{
    [Serializable]
    public class BattleUnitModel
    {
        private readonly UnitSystemContainer _systems = new();

        public string Id { get; }
        public Team Team { get; private set; }

        public BattleUnitModel(string id)
        {
            Id = id;
        }

        public void AddSystem(UnitSystem systemModel) =>
            _systems.AddSystem(systemModel);

        public TActorSystem GetSystem<TActorSystem>() where TActorSystem : UnitSystem =>
            _systems.GetSystem<TActorSystem>();

        public IEnumerable<UnitSystem> GetSystems() =>
            _systems.GetSystems();

        public void SetTeam(Team team)
        {
            Team = team;
        }
    }
}