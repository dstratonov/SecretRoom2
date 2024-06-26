using Common.UI.Root;
using UnityEngine;
using Zenject;

namespace Common.UI
{
    public class ViewServiceInstaller : MonoInstaller<ViewServiceInstaller>
    {
        [SerializeField] private ViewConfig _viewConfig;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ViewService>()
                .FromSubContainerResolve()
                .ByMethod(InitializeService)
                .AsSingle();
        }

        private void InitializeService(DiContainer subContainer)
        {
            subContainer.Bind<ViewService>().AsSingle();
            subContainer.Bind<CanvasProvider>().AsSingle();
            subContainer.Bind<ViewProvider>().AsSingle();
            subContainer.Bind<ViewFactory>().AsSingle();
            
            subContainer.BindInstance(_viewConfig);
        }
    }
}