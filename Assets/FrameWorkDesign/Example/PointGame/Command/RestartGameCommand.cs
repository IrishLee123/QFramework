namespace FrameworkDesign.Example
{
    public class RestartGameCommand:AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<OnRestartGameEvent>();
        }
    }
}