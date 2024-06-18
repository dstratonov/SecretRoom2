using System.Collections.Generic;
using Common.Events;
using Game.Battle.Configs;
using Game.Battle.Models;
using Game.Battle.SubModules;

namespace Game.Battle
{
    public class Battle
    {
        private readonly BattleBuilder _battleBuilder;
        private readonly EventBus _eventBus;
        private readonly List<BattleSubModule> _subModules;

        public BattleModel Model { get; private set; }

        public Battle(BattleBuilder battleBuilder, EventBus eventBus, List<BattleSubModule> subModules)
        {
            _battleBuilder = battleBuilder;
            _eventBus = eventBus;
            _subModules = subModules;
        }

        public void Initialize(BattleConfig battleConfig)
        {
            Model = _battleBuilder.Build(battleConfig);

            foreach (BattleSubModule gameplaySubService in _subModules)
            {
                gameplaySubService.SetLevelModel(Model);
                gameplaySubService.Initialize();
            }
        }

        public void Start()
        {
            _eventBus.Fire<BattleStartedEvent>();
        }
    }
}