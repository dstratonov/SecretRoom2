using System.Collections.Generic;
using Cinemachine;
using Common.Events;
using Game.Battle.Models;
using Game.Battle.SubModules;
using Game.Battle.Units;

public class CameraControllerSubModule : BattleSubModule
{
    private readonly CinemachineBrain _cameraBrain;
    private readonly Dictionary<BattleUnitModel, CinemachineVirtualCamera> _cameras = new();

    private CinemachineVirtualCamera _lastCamera;
    private TeamController _teamController;


    public CameraControllerSubModule(CinemachineBrain cameraBrain)
    {
        _cameraBrain = cameraBrain;
    }
    
    protected override void OnBattleStarted(BattleStartedEvent args)
    {
        base.OnBattleStarted(args);

        foreach (BattleUnitModel unit in Model.PlayerTeam.GetUnits())
        {
            CinemachineVirtualCamera camera = unit.GetSystem<PawnSystem>().Pawn.VirtualCamera;
            camera.Priority = 0;
            
            _cameras.Add(unit, camera);
        }
        
        foreach (BattleUnitModel unit in Model.EnemyTeam.GetUnits())
        {
            CinemachineVirtualCamera camera = unit.GetSystem<PawnSystem>().Pawn.VirtualCamera;
            camera.Priority = 0;
            
            _cameras.Add(unit, camera);
        }
        
        EventBus.Subscribe<UnitStartTurnEvent>(OnUnitTurnStarted);
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        
        EventBus.Unsubscribe<UnitStartTurnEvent>(OnUnitTurnStarted);
    }

    private void OnUnitTurnStarted(UnitStartTurnEvent args)
    {
        if (_lastCamera != null)
        {
            _lastCamera.Priority = 0;
        }
        
        CinemachineVirtualCamera camera = _cameras[args.uniModel];

        camera.Priority = 1;

        _lastCamera = camera;
    }
}
