using System;
using Game.Battle.Units;
using Zenject;

namespace Game.Battle.TurnControllers
{
    public abstract class UnitController
    {
        public event Action TurnFinished;
        
        protected BattleUnitModel UnitModel { get; private set; }

        //TODO OLEG DO SOMETHING
        [Inject] public Battle battle;
        
        public void SetUnit(BattleUnitModel unitModel)
        {
            UnitModel = unitModel;
        }
        
        public void PrepareForTurn()
        {
            
        }
        
        protected void FinishTurn()
        {
            TurnFinished?.Invoke();
        }

        public void Activate()
        {
            OnActivate();
        }

        public void Deactivate()
        {
            OnDeactivate();
        }

        public void ForceFinish()
        {
            FinishTurn();
        }
        
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
}