using System.Collections.Generic;
using Cinemachine;
using Common.Events;
using Game.Battle.Events;
using Game.Battle.Models;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Pawn;

namespace Game.Battle.SubModules.BattleCineMachine
{
    public class BattleCineMachineSubModule : IBattleStartedSubModule, IUnitTurnStartedSubModule
    {
        private readonly BattleField _battleField;
        private readonly Dictionary<BattleUnitModel, CinemachineVirtualCamera> _cameras = new();

        private readonly EventBus _eventBus;

        private CinemachineVirtualCamera _lastCamera;

        public BattleCineMachineSubModule(BattleField battleField, EventBus eventBus)
        {
            _battleField = battleField;
            _eventBus = eventBus;
        }

        public void OnBattleStarted(BattleModel model)
        {
            _eventBus.Subscribe<TargetChangedEvent>(OnTargetSelected);

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
            ChangeCamera(_cameras[unit]);
        }

        private void ChangeCamera(CinemachineVirtualCamera newCamera)
        {
            if (_lastCamera != null)
            {
                _lastCamera.Priority = 0;
            }

            newCamera.Priority = 1;

            _lastCamera = newCamera;
        }

        private void OnTargetSelected(TargetChangedEvent args)
        {
            ChangeCamera(_battleField.TargetCamera);

            _battleField.TargetGroup.m_Targets = new CinemachineTargetGroup.Target[2];

            CinemachineTargetGroup.Target targetCaster;
            targetCaster.target = args.caster.transform;
            targetCaster.weight = 1;
            targetCaster.radius = 0;

            CinemachineTargetGroup.Target targetTarget;
            targetTarget.target = args.target.transform;
            targetTarget.weight = 1;
            targetTarget.radius = 0;

            _battleField.TargetGroup.m_Targets.SetValue(targetCaster, 0);
            _battleField.TargetGroup.m_Targets.SetValue(targetTarget, 1);
        }
    }
}