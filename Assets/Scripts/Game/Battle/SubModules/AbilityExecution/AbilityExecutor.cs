using System.Collections.Generic;
using Common.Events;
using Cysharp.Threading.Tasks;
using Game.Battle.Abilities;
using Game.Battle.Abilities.Mechanics;
using Game.Battle.Abilities.Mechanics.Core;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Events;
using Game.Battle.Models;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Pawn;

namespace Game.Battle.SubModules.AbilityExecution
{
    public class AbilityExecutor : IAbilityExecutor
    {
        private readonly UniTaskCompletionSource _completionSource = new();
        private readonly EventBus _eventBus;
        private readonly MechanicsFactory _mechanicsFactory;

        private AbilityModel _ability;

        private AbilityAnimator _abilityAnimator;
        private BattleModel _battleModel;
        private BattleUnitModel _caster;
        private BattleUnitModel _target;

        public AbilityExecutor(MechanicsFactory mechanicsFactory, EventBus eventBus)
        {
            _mechanicsFactory = mechanicsFactory;
            _eventBus = eventBus;
        }

        public async UniTask Execute()
        {
            _eventBus.Fire(new AbilityCastStartEvent
            {
                ability = _ability,
                caster = _caster,
                target = _target,
            });

            _abilityAnimator.AnimationFinished += EndCast;
            _abilityAnimator.PlayAnimation();

            await _completionSource.Task;
        }

        public void SetExecutionData(AbilityModel ability, BattleUnitModel caster, BattleUnitModel target,
            BattleModel battleModel)
        {
            _ability = ability;
            _caster = caster;
            _target = target;
            _battleModel = battleModel;

            _abilityAnimator = new AbilityAnimator(ability.Data.animTrigger,
                _caster.GetSystem<PawnSystem>().Pawn, _target.GetSystem<PawnSystem>().Pawn);
        }

        private void EndCast()
        {
            _abilityAnimator.AnimationFinished -= EndCast;

            Invoke(_ability, _caster, _target);

            _eventBus.Fire(new AbilityCastEndEvent
            {
                ability = _ability,
                caster = _caster,
                target = _target,
            });

            _completionSource.TrySetResult();
        }

        private void Invoke(AbilityModel ability, BattleUnitModel caster, BattleUnitModel target)
        {
            List<BattleUnitModel> mechanicTargets = new();

            foreach (MechanicData data in ability.GetMechanics())
            {
                mechanicTargets.Clear();

                Mechanic mechanic = _mechanicsFactory.Create(data);

                SetMechanicTargets(target, caster, data.selection, mechanicTargets);

                mechanic.SetCaster(caster);
                mechanic.Invoke(mechanicTargets);
            }

            // casterEnergy.Remove(ability.GetCost());
        }

        private void SetMechanicTargets(BattleUnitModel selectedUnit, BattleUnitModel caster,
            MechanicSelection selection,
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
                    IReadOnlyList<BattleUnitModel> units = _battleModel.GetAllyTeamBySide(caster.Team).GetUnits();

                    targets.AddRange(units);
                    break;
                }

                case MechanicSelection.Enemies:
                {
                    IReadOnlyList<BattleUnitModel> units = _battleModel.GetOpponentTeamBySide(caster.Team).GetUnits();

                    targets.AddRange(units);
                    break;
                }
            }
        }
    }
}