using System.Collections;
using System.Collections.Generic;
using Game.Battle.Abilities;
using UnityEngine;
namespace Game.Battle.Events
{
    public struct OnAbilityChangedEvent
    {
        public readonly int abilityId;

        public OnAbilityChangedEvent(int abilityId)
        {
            this.abilityId = abilityId;
        }
    }
}
