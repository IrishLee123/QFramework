using System;

namespace FrameworkDesign
{
    public interface ICanRegisterEvent : IBelongToArchitecture { }

    public static class CanRegisterEventExtension
    {
        public static IUnRegister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) where T : new()
        {
            return self.GetArchitecture().RegisterEvent<T>(onEvent);
        }
        public static void UnRegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) where T : new()
        {
            self.GetArchitecture().UnRegisterEvent<T>(onEvent);
        }
    }
}
