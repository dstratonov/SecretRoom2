using UnityEngine;

namespace Common.UI
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private bool _isTransparent;
        [SerializeField] private bool _isReturnable;
        
        public bool IsTransparent => _isTransparent;
        public bool IsReturnable => _isReturnable;
        public bool IsContentEnabled => _content.activeSelf;

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
        
        public void Close()
        {
            OnClose();
        }

        protected virtual void OnClose() { }
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
}