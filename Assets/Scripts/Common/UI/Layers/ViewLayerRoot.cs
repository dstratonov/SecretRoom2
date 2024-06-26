using System;
using System.Collections.Generic;
using Common.Loggers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Common.UI.Layers
{
   [RequireComponent(typeof(Canvas))]
    public class ViewLayerRoot : MonoBehaviour
    {
        [SerializeField] private UILayer _layer;
        [SerializeField] private Canvas _root;
        private readonly List<BaseView> _views = new();

        public event Action<BaseView> ViewOpened;
        public Canvas Canvas => _root;

        public UILayer Layer => _layer;

        public void CloseView(BaseView view = null)
        {
            if (view == null)
            {
                view = _views[^1];
            }

            view.Deactivate();
            _views.Remove(view);
            Destroy(view.gameObject);
        }

        public async UniTask<TView> InstantiateView<TView, TViewModel>(TViewModel model)
            where TView : BaseModelView<TViewModel>
            where TViewModel : ViewModel
        {
            // var view = await _viewFactory.CreateView<TView>();

            // if (view == null)
            // {
                // this.Error($"[ViewLayerRoot]: Incorrect view for instantiating {nameof(TView)}");
                // return await UniTask.FromResult<TView>(null);
            // }

            TView view = default;
            return ShowModelView(view, model);
        }

        public void MatchWithCamera(Camera cam)
        {
            if (_root.renderMode != RenderMode.WorldSpace)
            {
                return;
            }

            var rect = _root.GetComponent<RectTransform>();

            float height = cam.orthographicSize * 2;
            Vector2 sizeDelta = rect.sizeDelta;

            float x = height * cam.aspect / sizeDelta.x;
            float y = height / sizeDelta.y;

            transform.localScale = new Vector3(x, y, 1);
        }

        public TView ShowModelView<TView, TViewModel>(TView view, TViewModel model)
            where TView : BaseModelView<TViewModel>
            where TViewModel : ViewModel
        {
            model.root = this;
            view.Initialize(model);
            view.Activate();

            if (_views[^1] != null)
            {
                //todo check if view should hide content?
                _views[^1].SetContentEnabled(false);
            }

            _views.Add(view);

            ViewOpened?.Invoke(view);

            return view;
        }
    }
}