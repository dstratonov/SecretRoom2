using System.Collections.Generic;
using Common.Events;
using Game.Battle.Abilities;
using Game.Battle.Abilities.Mechanics;
using Game.Battle.Abilities.Mechanics.Core;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Models;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;
using Zenject;

namespace Game.Battle.SubModules.AbilityExecution
{
    public class AbilityExecutionSubModule : IBattleStartedSubModule
    {
        private readonly IInstantiator _instantiator;
        
        private AbilityExecutor _animExecutor;

        private BattleModel _model;

        public AbilityExecutionSubModule(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IAbilityExecutor GetAbilityExecutor(string id, BattleUnitModel caster, BattleUnitModel target)
        {
            AbilityModel ability = caster.GetSystem<AbilitySystem>().GetAbilityModel(id);

            if (ability == null)
            {
                return null;
            }

            // ReactiveValue casterEnergy = caster
            //     .GetSystem<StatsSystem>()
            //     .GetStat(Stat.EN);

            // if (!ability.CanUse(casterEnergy.Current))
            // {
            //     return;
            // }

            // caster.GetSystem<PawnSystem>().Pawn.AnimationFinished +=

            var executor = _instantiator.Instantiate<AbilityExecutor>();
            
            executor.SetExecutionData(ability, caster, target, _model);

            return executor;
        }

        public void OnBattleStarted(BattleModel model)
        {
            _model = model;
        }

       

        
    }
}