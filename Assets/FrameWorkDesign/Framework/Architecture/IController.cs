namespace FrameworkDesign
{
    public interface IController : IBelongToArchitecture,
        ICanSendCommand, ICanGetSystem, ICanGetModel, ICanRegisterEvent
    {
    }
}
