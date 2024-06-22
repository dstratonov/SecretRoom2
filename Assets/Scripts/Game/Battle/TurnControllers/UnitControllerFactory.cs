using System.Collections.Generic;
using Game.Battle.Models;
using Zenject;

namespace Game.Battle.TurnControllers
{
    public class UnitControllerFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Team, UnitController> _unitControllers = new();

        public UnitControllerFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public UnitController GetController(Team team)
        {
            if (_unitControllers.TryGetValue(team, out UnitController controller))
            {
                return controller;
            }

            controller = CreateControllerInternal(team);
            _unitControllers.Add(team, controller);

            return controller;
        }

        private UnitController CreateControllerInternal(Team team)
        {
            switch (team)
            {
                case Team.Player:
                    return _instantiator.Instantiate<PlayerUnitController>();
                default:
                    return _instantiator.Instantiate<AiUnitController>();
            }
        }
    }
}