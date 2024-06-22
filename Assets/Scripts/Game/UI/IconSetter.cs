using System.Collections.Generic;
using Common.Events;
using Game.Battle.Configs;
using Game.Battle.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class IconSetter : MonoBehaviour
    {
        [SerializeField] private BattleConfig _battleConfig;
    
        public List<Image> enemySlots = new();
        public List<Image> playerSlots = new();

        [Inject] private EventBus _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe<BattleStartedEvent>(OnBattleStarted);
        }

        private void OnBattleStarted(BattleStartedEvent battleStartedEventArgs)
        {
            SetEnemyTeam(_battleConfig.enemyUnits);
            SetPlayerTeam(_battleConfig.playerUnits);
        }

        private void SetEnemyTeam(List<UnitConfig> enemyUnits)
        {
            for (var i = 0; i < enemyUnits.Count; i++)
            {
                enemySlots[i].sprite = enemyUnits[i].unitIcon;
            }
        }

        private void SetPlayerTeam(List<UnitConfig> playerUnits)
        {
            for (var i = 0; i < playerUnits.Count; i++)
            {
                playerSlots[i].sprite = playerUnits[i].unitIcon;
            }
        }
    }
}
