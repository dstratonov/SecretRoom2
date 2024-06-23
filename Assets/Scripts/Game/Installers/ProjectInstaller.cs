using Common.Events;
using Game.Abilities;
using Game.Input;
using Game.Models;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private AbilityContainerConfig _abilityContainerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<GameModel>().AsSingle();
            Container.Bind<EventBus>().AsSingle();
            Container.Bind<InputService>().AsSingle();
            
            Container.BindInstance(_abilityContainerConfig);
        }
    }
}