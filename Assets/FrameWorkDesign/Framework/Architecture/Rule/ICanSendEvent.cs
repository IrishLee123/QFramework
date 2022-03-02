namespace FrameworkDesign
{
    public interface ICanSendEvent : IBelongToArchitecture { }

    public static class CanSendEventExtension
    {
        public static void SendEvent<T>(this ICanSendEvent self ) where T : new()
        {
            self.GetArchitecture().SendEvent<T>();
        }
    }

}
