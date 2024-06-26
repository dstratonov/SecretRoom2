using Common.UI.Layers;
using Common.UI.Root;
using Cysharp.Threading.Tasks;

namespace Common.UI
{
    public class ViewService
    {
        private readonly CanvasProvider _canvasProvider;

        public ViewService(CanvasProvider canvasProvider)
        {
            _canvasProvider = canvasProvider;
        }
        
        public async UniTask<TView> CreateView<TView, TViewModel>(TViewModel model, UILayer layer)
            where TView : BaseModelView<TViewModel>
            where TViewModel : ViewModel
        {
            CanvasRoot activeRoot = _canvasProvider.GetActiveCanvas();
            ViewLayerRoot layerRoot = activeRoot.GetLayer(layer);
            
            TView instance = await layerRoot.InstantiateView<TView, TViewModel>(model);
            
            return instance;
        }

        public async UniTask<TView> CreateView<TView>(UILayer layer) where TView : BaseModelView => 
            await CreateView<TView, ViewModel>(ViewModel.Empty, layer);
    }
}