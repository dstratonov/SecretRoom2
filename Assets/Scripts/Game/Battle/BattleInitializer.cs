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
                StringBuilder sb = new();
                
                var stats = unit.GetSystem<StatsSystem>();
                
                sb.AppendLine($"Unit {unit.Id} in {unit.Team} team");
                sb.AppendLine($"Stat {Stat.HP} = {stats.GetStat(Stat.HP).Current} / {stats.GetStat(Stat.HP).Max}");
                sb.AppendLine($"Stat {Stat.EN} = {stats.GetStat(Stat.EN).Current} / {stats.GetStat(Stat.EN).Max}");
                sb.AppendLine($"Stat {Stat.AD} = {stats.GetStat(Stat.AD).Max} ");
                sb.AppendLine($"Stat {Stat.AP} = {stats.GetStat(Stat.AP).Max} ");
                sb.AppendLine($"Stat {Stat.DEF} = {stats.GetStat(Stat.DEF).Max}");
                sb.AppendLine($"Stat {Stat.ENR} = {stats.GetStat(Stat.ENR).Max}");
                
                this.Log(sb.ToString());
            }
        }
    }
}