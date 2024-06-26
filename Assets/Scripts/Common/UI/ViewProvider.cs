using System;
using System.Collections.Generic;
using Common.AssetManagement;
using Common.Loggers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Common.UI
{
    public class ViewProvider
    {
        private readonly AssetsProvider _assetsProvider;
        private readonly Dictionary<Type, BaseView> _cachedViews = new();
        private readonly Dictionary<Type, UniTask> _preloadingTasks = new();
        private readonly ViewConfig _viewConfig;

        private UniTask _currentPreloadTask;

        public ViewProvider(ViewConfig viewConfig, AssetsProvider assetsProvider)
        {
            _viewConfig = viewConfig;
            _assetsProvider = assetsProvider;
        }

        public async UniTask<TView> PreloadViewAsync<TView>() where TView : BaseView
        {
            Type viewType = typeof(TView);

            this.Log($"PreloadViewAsync: preload {viewType.Name} START");

            bool foundTask = _preloadingTasks.TryGetValue(viewType, out UniTask preloadTask);

            if (!foundTask)
            {
                this.Log($"PreloadViewAsync: no task for {viewType.Name}, creating...");
                preloadTask = CreatePreloadViewTask<TView>();
                _preloadingTasks.Add(viewType, preloadTask);
            }

            if (_currentPreloadTask.Status != UniTaskStatus.Pending)
            {
                this.Log($"PreloadViewAsync: {viewType.Name}: _currentPreloadTask is clear, skip scheduling");
                _currentPreloadTask = preloadTask;
            }
            else
            {
                this.Log($"PreloadViewAsync: _currentPreloadTask isn't clear, scheduling {viewType.Name}...");
                _currentPreloadTask = _currentPreloadTask.ContinueWith(() => preloadTask);
            }

            this.Log($"PreloadViewAsync: waiting for {viewType.Name} to preload");

            await preloadTask;

            var view = GetCachedView<TView>();

            if (view == null)
            {
                this.Error($"PreloadViewAsync: got null from cache after trying to preload ({viewType.Name})");
            }

            this.Log($"PreloadViewAsync: preload for {viewType.Name} DONE");
            return view;
        }

        private async UniTask CreatePreloadViewTask<TView>() where TView : BaseView =>
            await PreloadViewInternalAsync<TView>();

        private TView GetCachedView<TView>() where TView : BaseView
        {
            Type screenType = typeof(TView);

            _cachedViews.TryGetValue(screenType, out BaseView view);

            if (view == null)
            {
                return null;
            }

            this.Log($"GetCachedScreen: cache for {screenType.Name} FOUND");
            return view as TView;
        }

        private async UniTask<TView> LoadViewAsync<TView>() where TView : BaseView
        {
            Type viewType = typeof(TView);
            string viewId = viewType.Name;

            this.Log($"PreloadScreenInternalAsync: no cache for ID: {viewId}, loading...");

            AssetReference viewReference = _viewConfig.GetViewReference(viewId);

            if (viewReference == null)
            {
                this.Error($"PreloadScreenInternalAsync: can't find ID in config {viewId}");
                return null;
            }

            var viewGameObject = await _assetsProvider.Load<GameObject>(viewReference);

            var view = viewGameObject.GetComponent<TView>();
            
            if (view == null)
            {
                Debug.LogError($"PreloadScreenInternalAsync: can't load view with ID: {viewId}");
                return null;
            }

            _cachedViews.Add(viewType, view);

            this.Log($"PreloadScreenInternalAsync: view {viewId} loaded and added to cache");

            return view;
        }

        private async UniTask<TView> PreloadViewInternalAsync<TView>() where TView : BaseView
        {
            var cachedView = GetCachedView<TView>();

            if (cachedView != null)
            {
                return cachedView;
            }

            return await LoadViewAsync<TView>();
        }
    }
}