using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(AbilityContainerConfig), menuName = "Configs/Game/Abilities Container")]
    public class AbilityContainerConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private AbilityInfo[] _abilitiesInfo;

        private Dictionary<string, AbilityInfo> _abilitiesDictionary = new();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _abilitiesDictionary = _abilitiesInfo.ToDictionary(x => x.id, x => x);
        }

        public AbilityInfo GetAbilityInfo(string id)
        {
            if (!_abilitiesDictionary.TryGetValue(id, out AbilityInfo info))
            {
                info = new AbilityInfo();
            }

            return info;
        }
    }

    [Serializable]
    public class AbilityInfo
    {
        public AbilityBattleConfig battleConfig;
        
        [AbilityId]
        public string id;
    }
}