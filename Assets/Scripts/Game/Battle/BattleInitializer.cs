using System.Collections.Generic;
using System.Linq;
using Game.Battle.Configs;
using Game.Models;
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

        private void Start()
        {
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
            _battle.Start();
        }
    }
}