using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Common.UI
{
    [CreateAssetMenu(fileName = nameof(ViewConfig), menuName = "Configs/UI/Views")]
    public class ViewConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<ViewEntry> _views;

        private Dictionary<string, AssetReference> _viewsDictionary;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _viewsDictionary = _views.ToDictionary(x => x.id, x => x.viewReference);
        }

        public AssetReference GetViewReference(string id) =>
            _viewsDictionary.GetValueOrDefault(id);

        [Button]
        private void ValidateAssets()
        {
            foreach (ViewEntry viewEntry in _views)
            {
                Object viewObject = viewEntry.viewReference.editorAsset;

                if (viewObject is GameObject viewGameObject && viewGameObject.GetComponent<BaseView>() != null)
                {
                    continue;
                }

                Debug.LogError($"View validation error for ID: {viewEntry.id}");
            }
        }

        [Serializable]
        private class ViewEntry
        {
            [ViewId]
            public string id;
            public AssetReference viewReference;
        }
    }
}