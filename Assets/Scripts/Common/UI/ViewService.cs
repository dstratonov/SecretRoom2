using System;
using Common.UI.Layers;
using Common.UI.Root;
using Cysharp.Threading.Tasks;

namespace Common.UI
{
    public class ViewService
    {
        private readonly CanvasProvider _canvasProvider;
        private readonly ViewFactory _viewFactory;

        public ViewService(CanvasProvider canvasProvider, ViewFactory viewFactory)
        {
            _canvasProvider = canvasProvider;
            _viewFactory = viewFactory;
        }
        
        public async UniTask<TView> CreateView<TView, TViewModel>(TViewModel model, UILayer layer)
            where TView : BaseModelView<TViewModel>
            where TViewModel : ViewModel
        {
            CanvasRoot activeRoot = _canvasProvider.GetActiveCanvas();
            ViewLayerRoot layerRoot = activeRoot.GetLayer(layer);

            var view = await _viewFactory.CreateView<TView>(layerRoot.Canvas.transform);
            
            layerRoot.ShowView(view, model);
            
            return view;
        }

        public async UniTask<TView> CreateView<TView>(UILayer layer) where TView : BaseModelView => 
            await CreateView<TView, ViewModel>(ViewModel.Empty, layer);
    }
}