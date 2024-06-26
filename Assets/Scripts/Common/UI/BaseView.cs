using Common.UI.Layers;
using UnityEngine;
using Zenject;

namespace Common.UI
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private bool _isTransparent;
        [SerializeField] private bool _isReturnable;
        
        public bool IsTransparent => _isTransparent;
        public bool IsReturnable => _isReturnable;
        public bool IsContentEnabled => _content.activeSelf;

        public abstract UILayer Layer { get; }
        
        protected ViewService ViewService { get; private set; }
        
        [Inject]
        public void Construct(ViewService viewService)
        {
            ViewService = viewService;
        }
        
        public void Activate()
        {
            SetContentEnabled();
            OnActivate();
        }

        public void Deactivate()
        {
            SetContentEnabled(false);
            OnDeactivate();
        }
        
        public void SetContentEnabled(bool value = true)
        {
            if (_content != null)
            {
                _content.SetActive(value);
            }
        }
        
        public void PreClose()
        {
            OnPreClose();
        }

        public void Close()
        {
            ViewService.CloseView(this);
        }

        protected virtual void OnPreClose() { }
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
}