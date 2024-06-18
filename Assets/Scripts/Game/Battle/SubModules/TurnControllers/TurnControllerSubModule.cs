using System.Collections.Generic;
using System.Linq;
using Common.Events;
using Game.Battle.Models;

namespace Game.Battle.SubModules.TurnControllers
{
    public class TurnControllerSubModule : BattleSubModule
    {
        private readonly Queue<BattleUnitModel> _actingQueue = new();
        private readonly TurnControllerFactory _turnControllerFactory;

        private UnitController _currentController;

        public TurnControllerSubModule(TurnControllerFactory turnControllerFactory)
        {
            _turnControllerFactory = turnControllerFactory;
        }

        protected override void OnBattleStarted(BattleStartedEvent args)
        {
            base.OnBattleStarted(args);

            Next();
        }

        private void FillActionQueue()
        {
            IOrderedEnumerable<BattleUnitModel> sortedUnits = Model
                .GetAllUnits()
                .OrderBy(x => x.Team);

            foreach (BattleUnitModel unitModel in sortedUnits)
            {
                _actingQueue.Enqueue(unitModel);
            }
        }

        private UnitController GetNextUnitController() =>
            _turnControllerFactory.GetController(_actingQueue.Peek().Team);

        private void Next()
        {
            if (_actingQueue.Count == 0)
            {
                FillActionQueue();
            }

            UnitController controller = GetNextUnitController();

            _currentController = controller;

            controller.SetUnit(_actingQueue.Dequeue());
            controller.PrepareForTurn();

            controller.TurnFinished += OnTurnFinished;
            controller.Activate();
        }

        private void OnTurnFinished()
        {
            _currentController.Deactivate();
            _currentController.TurnFinished -= OnTurnFinished;
            _currentController = null;

            Next();
        }
    }
}