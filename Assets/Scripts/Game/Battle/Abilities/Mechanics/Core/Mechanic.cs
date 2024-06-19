using Common.Loggers;
using Game.Battle.Abilities.Mechanics.Data;
using Game.Battle.Models;
using Zenject;

namespace Game.Battle.Abilities.Mechanics.Core
{
    public abstract class Mechanic<TData> : Mechanic where TData : MechanicData
    {
        [Inject] protected TData Data { get; private set; }

        protected override BattleUnitModel GetMechanicTarget(BattleUnitModel enemy)
        {
            return enemy;
        }
    }

    public abstract class Mechanic
    {
        public void Invoke(BattleUnitModel enemy)
        {
            BattleUnitModel target = GetMechanicTarget(enemy);

            LogOnInvoke(target);

            OnInvoke(target);
        }
        
        protected virtual void LogOnInvoke(BattleUnitModel target)
        {
            // this.Log($"Actor <color=yellow>{Caster.Id}</color> applies <color=orange>{GetType().Name}</color> " +
                     // $"to actor <color=yellow>{target.Id}</color>");
        }
        
        protected abstract void OnInvoke(BattleUnitModel target);
        
        
        protected abstract BattleUnitModel GetMechanicTarget(BattleUnitModel enemy);
    }
}