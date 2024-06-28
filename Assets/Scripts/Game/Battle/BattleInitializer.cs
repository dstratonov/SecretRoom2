using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.AssetManagement;
using Common.Loggers;
using Game.Battle.Configs;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Stats;
using Game.Models;
using Game.Units;
using UnityEngine;
using Zenject;

namespace Game.Battle
{
    public class BattleInitializer : MonoBehaviour
    {
        [SerializeField] private BattleConfig _battleConfig;
        [SerializeField] private UnitConfig _playerUnitConfig;
        
        [Inject] private GameModel _gameModel;
        [Inject] private Battle _battle;
        [Inject] private AssetsProvider _assetsProvider;
        
        private void Start()
        {
            _assetsProvider.Initialize();
            
            var playerStats = new List<StatModel>();

            foreach (StatModel statModel in _playerUnitConfig.battleData.rawStats)
            {
                playerStats.Add(new StatModel
                {
                    stat = statModel.stat,
                    value = statModel.value,
                });
            }
            
            _gameModel.player = new PlayerModel
            {
                playerView = _playerUnitConfig.viewData,
                playerStats = playerStats,
                abilities = _playerUnitConfig.battleData.abilities.ToList(),
            };
            
            _battle.Initialize(_battleConfig);
            
            LogUnits();
            
            _battle.Start();
        }

        private void LogUnits()
        {
            this.Log($"Battle Initialized");
            
            foreach (BattleUnitModel unit in _battle.Model.GetAllUnits())
            {
                this.Log($"Unit {unit.Id} in {unit.Team} team");
                this.Log(unit.GetSystem<StatsSystem>().ToString());
            }
        }
    }
}