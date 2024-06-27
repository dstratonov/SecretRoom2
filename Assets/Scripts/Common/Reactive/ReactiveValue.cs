using System;
using UnityEngine;

namespace Common.Reactive
{
    public class ReactiveValue
    {
        private readonly int _min;

        public event Action<ReactiveValueUpdatedEventArgs> Updated;

        public int Current { get; private set; }
        public int Max { get; private set; }

        public ReactiveValue(int max = 0)
        {
            _min = 0;
            Max = max;
        }

        public void Add(int amount)
        {
            if (Current >= Max)
            {
                return;
            }

            int before = Current;
            int newValue = Current + amount;

            Current = newValue > Max ? Max : newValue;

            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }

        public bool AtMax() =>
            Current == Max;

        public void Remove(int amount)
        {
            if (Current <= _min)
            {
                return;
            }

            int before = Current;
            int newValue = Current - amount;

            Current = newValue > _min ? newValue : _min;

            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }

        public void Set(int value)
        {
            int before = Current;

            Current = Mathf.Clamp(value, _min, Max);
            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }

        public void AddMax(int value, bool fixCurrent = true) =>
            UpdateMax(value + Max, fixCurrent);

        public void RemoveMax(int value, bool fixCurrent = true) =>
            UpdateMax(Max - value, fixCurrent);

        public void Maximize() =>
            Set(Max);

        public void UpdateMax(int newMaxValue, bool fixCurrent = true)
        {
            int diff = newMaxValue - Max;
            Max = newMaxValue;

            if (!fixCurrent)
            {
                return;
            }

            int before = Current;
            int next = Current + diff;

            if (next < 0)
            {
                next = 0;
            }

            Current = next;

            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }
    }
}