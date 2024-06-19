using Game.Battle.Abilities.Mechanics;
using Game.Battle.Abilities.Mechanics.Core;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Models;

namespace Game.Battle.Abilities
{
    public class AbilityExecutionService
    {
        private readonly MechanicsFactory _mechanicsFactory;

        public AbilityExecutionService(MechanicsFactory mechanicsFactory)
        {
            _mechanicsFactory = mechanicsFactory;
        }

        public void CastAbility(string id, BattleUnitModel caster, BattleUnitModel target)
        {
            //todo get ability and call invoke
        }
        
        private void Invoke(AbilityModel model, BattleUnitModel caster, BattleUnitModel target)
        {
            if (!model.CanUse())
            {
                return;
            }

            foreach (MechanicData data in model.GetMechanics())
            {
                Mechanic mechanic = _mechanicsFactory.Create(data);
                mechanic.Invoke(target);
            }

            //todo Remove mana from caster
        }
    }
}