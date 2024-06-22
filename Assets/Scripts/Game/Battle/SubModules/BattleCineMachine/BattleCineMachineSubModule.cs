using System.Collections.Generic;
using Cinemachine;
using Game.Battle.Models;
using Game.Battle.TurnControllers;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Pawn;

namespace Game.Battle.SubModules.BattleCineMachine
{
    public class BattleCineMachineSubModule : IBattleStartedSubModule, IUnitTurnStartedSubModule
    {
        private readonly Dictionary<BattleUnitModel, CinemachineVirtualCamera> _cameras = new();

        private CinemachineVirtualCamera _lastCamera;
        private TeamController _teamController;

        public void OnBattleStarted(BattleModel model)
        {
            foreach (BattleUnitModel unit in model.PlayerTeam.GetUnits())
            {
                CinemachineVirtualCamera camera = unit.GetSystem<PawnSystem>().Pawn.VirtualCamera;
                camera.Priority = 0;

                _cameras.Add(unit, camera);
            }

            foreach (BattleUnitModel unit in model.EnemyTeam.GetUnits())
            {
                CinemachineVirtualCamera camera = unit.GetSystem<PawnSystem>().Pawn.VirtualCamera;
                camera.Priority = 0;

                _cameras.Add(unit, camera);
            }
        }

        public void OnUnitTurnStarted(BattleUnitModel unit)
        {
            if (_lastCamera != null)
            {
                _lastCamera.Priority = 0;
            }

            CinemachineVirtualCamera camera = _cameras[unit];

            camera.Priority = 1;

            _lastCamera = camera;
        }
    }
}