using System;
using UnityEngine;

namespace Common.Reactive
{
    public class ReactiveValue
    {
        private readonly float _min;

        public event Action<ReactiveValueUpdatedEventArgs> Updated;

        public float Current { get; private set; }
        public float Max { get; private set; }

        public ReactiveValue(float max = 0)
        {
            _min = 0;
            Max = max;
        }

        public void Add(float amount)
        {
            if (Current >= Max)
            {
                return;
            }

            float before = Current;
            float newValue = Current + amount;

            Current = newValue > Max ? Max : newValue;

            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }

        public bool AtMax() =>
            Mathf.FloorToInt(Current) == Mathf.FloorToInt(Max);

        public void Remove(float amount)
        {
            if (Current <= _min)
            {
                return;
            }

            float before = Current;
            float newValue = Current - amount;

            Current = newValue > _min ? newValue : _min;

            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }

        public void Set(float value)
        {
            float before = Current;

            Current = Mathf.Clamp(value, _min, Max);
            var result = new ReactiveValueUpdatedEventArgs(before, Current, Max);
            Updated?.Invoke(result);
        }

        public void UpdateMax(float newMaxValue, bool fixCurrent = true)
        {
            float diff = newMaxValue - Max;
            Max = newMaxValue;

            if (!fixCurrent)
            {
                return;
            }

            float before = Current;
            float next = Current + diff;

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