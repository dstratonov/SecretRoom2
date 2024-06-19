using System;
using Common.Reactive;

namespace Game.Battle.Units.Systems.Stats
{
    public abstract class StatSystem : UnitSystem
    {
        public event Action<ReactiveValueUpdatedEventArgs> StatChanged;

        public float Max => Value.Max;

        protected ReactiveValue Value { get; }

        protected StatSystem(float initialValue)
        {
            Value = new ReactiveValue(initialValue);
            Value.Set(initialValue);
        }

        public float GetPercentage() =>
            Value.Current / Value.Max;

        public void Increase(float amount) =>
            Value.Add(amount);

        public void Reduce(float amount) =>
            Value.Remove(amount);

        protected override void OnDispose()
        {
            base.OnDispose();

            Value.Updated -= OnStatUpdated;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Value.Updated += OnStatUpdated;
        }

        private void OnStatUpdated(ReactiveValueUpdatedEventArgs args)
        {
            StatChanged?.Invoke(args);
        }
    }
}