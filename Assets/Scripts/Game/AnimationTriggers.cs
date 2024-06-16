using System;
using System.Collections.Generic;
using Game.States;
using UnityEngine;

namespace Game
{
    public class AnimationTriggers : MonoBehaviour
    {
        private Dictionary<string, Action> _actionSubscribers = new();

        public void Trigger(string key)
        {
            if (_actionSubscribers.TryGetValue(key, out Action actions))
            {
                actions?.Invoke();
            }
        }

        public void Subscribe(string key, Action action)
        {
            _actionSubscribers.TryAdd(key, null);
            _actionSubscribers[key] += action;
        }

        public void Unsubscribe(string key, Action action)
        {
            if (_actionSubscribers.ContainsKey(key))
            {
                _actionSubscribers[key] -= action;
            }
        }

        public void Clear(string key)
        {
            if (_actionSubscribers.ContainsKey(key))
            {
                _actionSubscribers[key] = null;
            }
        }
    }
}