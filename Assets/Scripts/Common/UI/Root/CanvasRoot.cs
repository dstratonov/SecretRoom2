using System.Collections.Generic;
using Common.Cameras;
using Common.UI.Layers;
using UnityEngine;
using Zenject;

namespace Common.UI.Root
{
    public class CanvasRoot : MonoBehaviour
    {
        private readonly Dictionary<UILayer, ViewLayerRoot> _layerRoots = new();

        private CameraService _cameraService;
        private CanvasProvider _canvasProvider;
        
        [Inject]
        private void Construct(CameraService cameraService, CanvasProvider canvasProvider)
        {
            _cameraService = cameraService;
            _canvasProvider = canvasProvider;
        }

        public ViewLayerRoot GetLayer(UILayer layer)
        {
            return _layerRoots[layer];
        }
        
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                bool hasRoot = child.TryGetComponent<ViewLayerRoot>(out ViewLayerRoot layerRoot);

                if (!hasRoot) continue;
                
                _layerRoots.Add(layerRoot.Layer, layerRoot);
            }           
            
            _canvasProvider.AddRoot(this);
            
        }

        private void OnEnable()
        {
            _cameraService.ActiveCameraChanged += HandleCameraChanged;
        }

        private void OnDisable()
        {
            _cameraService.ActiveCameraChanged -= HandleCameraChanged;
        }

        private void OnDestroy()
        {
            _canvasProvider.RemoveTopRoot();
        }

        private void HandleCameraChanged()
        {
            Camera newCamera = _cameraService.ActiveCamera;
            
            if (newCamera == null)
            {
                return;
            }
            
            foreach (ViewLayerRoot layer in _layerRoots.Values)
            {
                layer.Canvas.worldCamera = newCamera;
                layer.MatchWithCamera(newCamera);
            }
        }
    }
}