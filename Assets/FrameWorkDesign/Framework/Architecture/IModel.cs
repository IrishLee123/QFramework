namespace FrameworkDesign
{
    public interface IModel : IBelongToArchitecture, ICanSetArchitecture,
        ICanGetUtility, ICanSendEvent
    {
        /// <summary>
        /// Model初始化接口，代替构造函数
        /// </summary>
        void Init();
    }

    public abstract class AbstractModel : IModel
    {

        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        void IModel.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }
}
