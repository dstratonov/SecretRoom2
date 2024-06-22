using System;
using System.Collections.Generic;
using Game.Battle.Abilities;
using Game.Battle.Abilities.Mechanics;
using Game.Battle.Abilities.Mechanics.Core;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Models;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Abilities;

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

        private void Invoke(AbilityModel model, BattleUnitModel caster, BattleUnitModel target)
        {
            if (!model.CanUse())
            {
                return;
            }

            List<BattleUnitModel> mechanicTargets = new();
            
            foreach (MechanicData data in model.GetMechanics())
            {
                mechanicTargets.Clear();
                
                Mechanic mechanic = _mechanicsFactory.Create(data);

                SetMechanicTargets(target, caster, data.selection, mechanicTargets);
                
                mechanic.SetCaster(caster);
                mechanic.Invoke(mechanicTargets);
            }

            //todo Remove mana from caster
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