using FrameworkDesign;

namespace CounterApp
{
    public class SubCountCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            this.GetModel<ICounterModel>().Count.Value--;
        }
    }
}
