using System.Collections.Generic;
using Common.Loggers;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Units;
using Zenject;

namespace Game.Battle.Abilities.Mechanics.Core
{
    public abstract class Mechanic<TData> : Mechanic where TData : MechanicData
    {
        [Inject] protected TData Data { get; private set; }
    }

    public abstract class Mechanic
    {
        public BattleUnitModel Caster { get; private set; }
        
        public void SetCaster(BattleUnitModel caster)
        {
            Caster = caster;
        }
        
        public void Invoke(IEnumerable<BattleUnitModel> targets)
        {
            foreach (BattleUnitModel target in targets)
            {
                LogOnInvoke(target);

                OnInvoke(target);
            }
        }
        
        protected virtual void LogOnInvoke(BattleUnitModel target)
        {
            this.Log($"Actor <color=yellow>{Caster.Id}</color> applies <color=orange>{GetType().Name}</color> " +
                     $"to actor <color=yellow>{target.Id}</color>");
        }
        
        protected abstract void OnInvoke(BattleUnitModel target);
    }
}