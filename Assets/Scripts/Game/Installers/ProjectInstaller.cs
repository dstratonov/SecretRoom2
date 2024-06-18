using Common.Events;
using Game.Input;
using Zenject;

namespace Game.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle();
            Container.Bind<InputService>().AsSingle();
        }
    }
}