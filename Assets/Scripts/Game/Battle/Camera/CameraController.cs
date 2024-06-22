using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common.Events;
using Game.Battle.SubModules;
using UnityEngine;
using Zenject;

public class CameraSubmodule : MonoBehaviour
{
    readonly public CinemachineBrain cameraBrain;

    [Inject] private EventBus _eventBus;

    private TeamController _teamController;

    void Start()
    {
        _eventBus.Subscribe<BattleStartedEvent>(OnBattleStarted);
    }

    void OnBattleStarted(BattleStartedEvent battleStartedEventArgs){
        
    }

}
