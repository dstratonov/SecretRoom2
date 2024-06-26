using UnityEngine;

namespace Common.UI
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

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
            Deactivate();

            //on closed event
            
            OnClose();
        }

        protected virtual void OnClose() { }
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
}