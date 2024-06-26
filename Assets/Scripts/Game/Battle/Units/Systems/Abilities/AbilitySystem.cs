using System.Collections.Generic;
using System.Linq;
using Game.Battle.Abilities;

namespace Game.Battle.Units.Systems.Abilities
{
    public class AbilitySystem : UnitSystem
    {
        private readonly Dictionary<string, AbilityModel> _abilities = new();

        public IReadOnlyList<string> AbilityIds => _abilities.Keys.ToList();

        public void AddAbility(AbilityModel abilityModel)
        {
            _abilities.Add(abilityModel.Id, abilityModel);
        }

        public int Count() =>
            _abilities.Count;

        public AbilityModel GetAbilityModel(string id) =>
            _abilities.GetValueOrDefault(id);

        public IReadOnlyList<AbilityModel> GetAbilityModels() =>
            _abilities.Values.ToList();

        public void RemoveAbility(string id)
        {
            _abilities.Remove(id);
        }
    }
}