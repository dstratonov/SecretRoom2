using System;
using Game.Battle.SubModules;
using Zenject;

namespace Common.Extensions
{
    public static class ZenjectExtensions
    {
        public static ConcreteIdArgConditionCopyNonLazyBinder BindBattleSubModule<T>(this DiContainer container)
            where T : BattleSubModule
        {
            return container
                .Bind(typeof(BattleSubModule), typeof(T))
                .To<T>()
                .AsSingle();
        }

        public static void BindBattleSubModuleFromSubContainer<T>(this DiContainer container,
            Action<DiContainer> installerMethod) where T : BattleSubModule
        {
            container
                .Bind(typeof(BattleSubModule), typeof(T))
                .To<T>()
                .FromSubContainerResolve()
                .ByMethod(installerMethod)
                .AsSingle();
        }
    }
}