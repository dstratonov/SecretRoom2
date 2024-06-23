using System;
using System.Collections.Generic;
using Zenject;

namespace Common.Factories
{
    public abstract class TypedFactory<T1, T2>
    {
        private readonly DiContainer _container;

        private readonly Dictionary<Type, T2> _pooledInstances = new();
        protected abstract bool IsPoolable { get; }

        protected abstract Dictionary<Type, Type> Types { get; }

        protected TypedFactory(DiContainer container)
        {
            _container = container;
        }

        public abstract T2 Create(T1 data);

        protected T2 CreateRaw(T1 data)
        {
            Type t = data.GetType();

            if (!Types.TryGetValue(t, out Type type))
            {
                return default;
            }

            T2 instance;

            if (IsPoolable && _pooledInstances.ContainsKey(type))
            {
                instance = _pooledInstances[type];
                _container.Inject(instance, new object[] { data });

                return instance;
            }

            instance = (T2)_container.Instantiate(type, new object[] { data });

            if (IsPoolable)
            {
                _pooledInstances.Add(type, instance);
            }

            return instance;
        }
    }
}