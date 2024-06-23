using System;
using System.Collections.Generic;
using Common.Reactive;
using Game.Battle.Abilities;
using Game.Battle.Abilities.Mechanics;
using Game.Battle.Abilities.Mechanics.Core;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Models;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;
using Game.Battle.Units.Systems.Stats;

namespace Game.Battle.SubModules.AbilityExecution
{
    public class AbilityExecutionSubModule : IBattleStartedSubModule 
    {
        private readonly MechanicsFactory _mechanicsFactory;
        
        public event Action<AbilityInvokeArgs> OnCastEnded;
        public event Action<AbilityInvokeArgs> OnCastStarted;

        private BattleModel _model;
        
        public AbilityExecutionSubModule(MechanicsFactory mechanicsFactory)
        {
            _mechanicsFactory = mechanicsFactory;
        }
        
        public void OnBattleStarted(BattleModel model)
        {
            _model = model;
        }

        public void CastAbility(string id, BattleUnitModel caster, BattleUnitModel target)
        {
            AbilityModel ability = caster.GetSystem<AbilitySystem>().GetAbilityModel(id);

            if (ability == null)
            {
                return;
            }

            var castArgs = new AbilityInvokeArgs
            {
                ability = ability,
                caster = caster,
                target = target,
            };
            
            OnCastStarted?.Invoke(castArgs);

            Invoke(ability, caster, target);

            OnCastEnded?.Invoke(castArgs);
        }

        private void Invoke(AbilityModel ability, BattleUnitModel caster, BattleUnitModel target)
        {
            ReactiveValue casterEnergy = caster
                .GetSystem<StatsSystem>()
                .GetStat(Stat.EN);
            
            if (!ability.CanUse(casterEnergy.Current))
            {
                return;
            }

            List<BattleUnitModel> mechanicTargets = new();
            
            foreach (MechanicData data in ability.GetMechanics())
            {
                mechanicTargets.Clear();
                
                Mechanic mechanic = _mechanicsFactory.Create(data);

                SetMechanicTargets(target, caster, data.selection, mechanicTargets);
                
                mechanic.SetCaster(caster);
                mechanic.Invoke(mechanicTargets);
            }

            casterEnergy.Remove(ability.GetCost());
        }
        
        private void SetMechanicTargets(BattleUnitModel selectedUnit, BattleUnitModel caster, MechanicSelection selection,
            List<BattleUnitModel> targets)
        {
            switch (selection)
            {
                case MechanicSelection.Keep:
                {
                    targets.Add(selectedUnit);
                    break;
                }

                case MechanicSelection.Self:
                {
                    targets.Add(caster);
                    break;
                }

                case MechanicSelection.Allies:
                {
                    IReadOnlyList<BattleUnitModel> units = _model.GetAllyTeamBySide(caster.Team).GetUnits();
                    
                    targets.AddRange(units);
                    break;
                }

                case MechanicSelection.Enemies:
                {
                    IReadOnlyList<BattleUnitModel> units = _model.GetOpponentTeamBySide(caster.Team).GetUnits();
                    
                    targets.AddRange(units);
                    break;
                }
            }
        }

    }
}