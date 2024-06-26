using Common.UI.Layers;

namespace Common.UI
{
    public abstract class BaseModelView: BaseModelView<ViewModel> { }

    public abstract class BaseModelView<TModel> : BaseView 
        where TModel : ViewModel
    {   
        protected TModel ViewModel { get; private set; }

        public sealed override UILayer Layer => ViewModel.root.Layer;

        public void Initialize(TModel model)
        {
            ViewModel = model;
            transform.SetParent(model.root.transform);

            OnOpen();
        }
        
        protected virtual void OnOpen() { }

        protected override void OnPreClose()
        {
            //todo close
            // ViewModel.root.CloseView(this);
        }
    }
}