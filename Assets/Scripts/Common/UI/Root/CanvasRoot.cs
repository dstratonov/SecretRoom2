using System.Collections.Generic;
using Common.UI.Layers;
using UnityEngine;
using Zenject;

namespace Common.UI.Root
{
    public class CanvasRoot : MonoBehaviour
    {
        private readonly Dictionary<UILayer, ViewLayerRoot> _layerRoots = new();

        private CanvasProvider _canvasProvider;

        [Inject]
        private void Construct(CanvasProvider canvasProvider)
        {
            _canvasProvider = canvasProvider;
        }

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                bool hasRoot = child.TryGetComponent(out ViewLayerRoot layerRoot);

                if (!hasRoot)
                {
                    continue;
                }

                _layerRoots.Add(layerRoot.Layer, layerRoot);
            }

            _canvasProvider.AddRoot(this);
        }

        private void OnDestroy()
        {
            _canvasProvider.RemoveTopRoot();
        }

        public ViewLayerRoot GetLayer(UILayer layer) =>
            _layerRoots[layer];
    }
}