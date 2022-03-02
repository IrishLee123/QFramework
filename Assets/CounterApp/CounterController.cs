using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return global::CounterApp.CounterApp.Interface;
        }

        private void Start()
        {
            transform.Find("Add").GetComponent<Button>().onClick.AddListener(this.SendCommand<AddCountCommand>);

            transform.Find("Sub").GetComponent<Button>().onClick.AddListener(this.SendCommand<SubCountCommand>);

             this.GetModel<ICounterModel>().Count.RegisterOnValueChanged(UpdateView).
                 UnRegisterWhenGameObjectDestroyed(gameObject);

            UpdateView(this.GetModel<ICounterModel>().Count.Value);
        }

        private void UpdateView(int count)
        {
            transform.Find("Count").GetComponent<Text>().text = count.ToString();
        }
    }



    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel : AbstractModel, ICounterModel
    {
        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();

            Count.Value = storage.LoadInt("COUNTER_COUNT", 0);

            Count.RegisterOnValueChanged(count => { storage.SaveInt("COUNTER_COUNT", count); });
        }

        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
    }
}