using Game.Battle.Configs;
using UnityEngine;
using Zenject;

namespace Game.Battle
{
    public class BattleInitializer : MonoBehaviour
    {
        [SerializeField] private BattleConfig _battleConfig;
        
        [Inject] private Battle _battle;

        private void Start()
        {
            _battle.Initialize(_battleConfig);
        }
    }
}