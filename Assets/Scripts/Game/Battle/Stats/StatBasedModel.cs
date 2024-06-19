using System;
using Sirenix.OdinInspector;

namespace Game.Battle.Stats
{
    [Serializable]
    public struct StatBasedModel
    {
        public bool statBased;
        [ShowIf(nameof(statBased))] public StatModifyModel[] statModifiers;
    }
}