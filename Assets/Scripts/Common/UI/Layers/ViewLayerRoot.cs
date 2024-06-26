using System;
using System.Collections.Generic;
using Common.Loggers;
using UnityEngine;

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
        
        public void ShowView<TView, TViewModel>(TView view, TViewModel model) 
            where TView : BaseModelView<TViewModel>
            where TViewModel : ViewModel
        {
            BaseView previousView = _views[^1];
            
            if (previousView != null)
            {
                previousView.Deactivate();
                
                if (view.IsTransparent)
                {
                    previousView.SetContentEnabled(false);
                }
            }
            
            view.SetContentEnabled(false);
            model.root = this;
            view.Initialize(model);
            view.SetContentEnabled();
            view.Activate();
            
            _views.Add(view);
            
            TryCloseViewsAbove(view);
        }
        
        // public bool CloseView(BaseView screen)
        // {
        //     if (_views[^1] == screen)
        //     {
        //         if (_views.Count > 1)
        //         {
        //             // back for last screen
        //             ScreenDeActivateCurrent();
        //             ScreenRemove();
        //             ScreenShow(_screens.Last());
        //             return true;
        //         }
        //         
        //         if (_views.Count == 1)
        //         {
        //             ScreenDeActivateCurrent();
        //             ScreenRemove();
        //             return true;
        //         }
        //     }
        //     
        //     return false;
        // }
        
        private void TryCloseViewsAbove(BaseView screen)
        {
            if (!screen.IsReturnable && !screen.IsTransparent)
            {
                int screensToRemove = _views.Count - 1;

                for (var i = 0; i < screensToRemove; i++)
                {
                    RemoveView(_views[0]);
                }
            }
        }
        
        private void RemoveView(BaseView view = null)
        {
            if (_views.Count > 0)
            {
                if (view == null)
                {
                    view = _views[^1];
                }
                
                if (view == null)
                {
                    return;
                }

                if (view.IsContentEnabled)
                {
                    view.SetContentEnabled(false);
                }

                view.Close();
                _views.Remove(view);
                Destroy(view.gameObject);
            }
            else
            {
                this.Error($"RemoveView: no instantiated views in layer {_layer}");
            }
        }
    }
}