using System;
using System.Collections.Generic;
using Zenject;

namespace Common.Factories
{
    public abstract class TypedFactory<T1, T2>
    {
        private readonly IInstantiator _instantiator;

        protected abstract Dictionary<Type, Type> Types { get; }

        protected TypedFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public abstract T2 Create(T1 data);

        protected T2 CreateRaw(T1 data)
        {
            Type t = data.GetType();

            if (!Types.TryGetValue(t, out Type type))
            {
                return default;
            }
            
            var instance = (T2)_instantiator.Instantiate(type);
            
            return instance;
        }
    }
}