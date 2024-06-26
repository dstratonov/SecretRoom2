using Common.Loggers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Common.UI
{
    public class ViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly ViewProvider _viewProvider;

        public ViewFactory(IInstantiator instantiator, ViewProvider viewProvider)
        {
            _instantiator = instantiator;
            _viewProvider = viewProvider;
        }
        
        public async UniTask<TView> CreateView<TView>(Transform parent) 
            where TView : BaseView
        {
            var viewPrefab = await _viewProvider.PreloadViewAsync<TView>();
            
            if (viewPrefab != null)
            {
                var view = _instantiator.InstantiatePrefabForComponent<TView>(viewPrefab.gameObject, parent);
                return view;
            }
           
            this.Error($"CreateView: can't create view {typeof(TView)}");
            return null;
        }
    }
}