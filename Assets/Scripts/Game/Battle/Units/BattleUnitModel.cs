using System;
using System.Collections.Generic;
using Game.Battle.Configs;
using Game.Battle.Units.Systems;

namespace Game.Battle.Models
{
    [Serializable]
    public class BattleUnitModel
    {
        private readonly UnitSystemContainer _systems = new();
        private UnitConfig _unitConfig;

        public string Id => _unitConfig.id;
        public Team Team { get; private set; }

        public BattleUnitModel(UnitConfig config)
        {
            _unitConfig = config;
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