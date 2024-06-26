using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Common.AssetManagement
{
    public class AssetsProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }

            _completedCache.Clear();
            _handles.Clear();
        }

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async UniTask<GameObject> Instantiate(string address) =>
            await Addressables.InstantiateAsync(address);

        public async UniTask<GameObject> Instantiate(string address, Vector3 at) =>
            await Addressables.InstantiateAsync(address, at, Quaternion.identity);

        public async UniTask<GameObject> Instantiate(string address, Transform parent) =>
            await Addressables.InstantiateAsync(address, parent);

        public async UniTask<T> Load<T>(AssetReference reference) where T : class =>
            await Load<T>(reference.AssetGUID);

        public async UniTask<T> Load<T>(string assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
            handle.Completed += h => { _completedCache[assetReference] = h; };

            AddHandle(assetReference, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}