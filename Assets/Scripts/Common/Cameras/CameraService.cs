using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Cameras
{
    public class CameraService
    {
        private readonly Stack<Camera> _cameraStack = new();
        
        public event Action ActiveCameraChanged;

        public Camera ActiveCamera { get; private set; }

        public void RemoveTopCamera()
        {
            Camera camera = _cameraStack.Pop();

            if (camera != null)
            {
                camera.gameObject.SetActive(false);
            }

            if (_cameraStack.Count > 0)
            {
                Camera nextCamera = _cameraStack.Peek();

                if (nextCamera != null)
                {
                    ChangeActiveCamera(nextCamera);
                }
            }
        }

        public void SetActiveCamera(Camera camera)
        {
            if (ActiveCamera != null)
            {
                ActiveCamera.gameObject.SetActive(false);
            }

            _cameraStack.Push(camera);

            ChangeActiveCamera(camera);
        }

        private void ChangeActiveCamera(Camera camera)
        {
            ActiveCamera = camera;
            ActiveCamera.gameObject.SetActive(true);
            ActiveCameraChanged?.Invoke();
        }
    }
}