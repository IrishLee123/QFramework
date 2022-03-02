using FrameworkDesign;

namespace CounterApp
{
    public class AddCountCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            this.GetModel<ICounterModel>().Count.Value++;
        }
    }
}
