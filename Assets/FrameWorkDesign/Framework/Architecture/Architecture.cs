using System;
using System.Collections.Generic;

namespace FrameworkDesign
{
    /// <summary>
    /// Architecture接口
    /// </summary>
    public interface IArchitecture
    {
        void RegisterSystem<T>(T system) where T : ISystem;

        void RegisterModel<T>(T model) where T : IModel;

        void RegisterUtility<T>(T utility) where T : IUtility;

        T GetSystem<T>() where T : class, ISystem;

        T GetModel<T>() where T : class, IModel;

        T GetUtility<T>() where T : class, IUtility;

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        void SendEvent<T>() where T : new();
        void SendEvent<T>(T e);

        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        void UnRegisterEvent<T>(Action<T> onEvent);
    }

    /// <summary>
    /// Architecture 基类
    /// 对IOC容器进行了一层封装
    /// </summary>
    /// <typeparam name="TA"></typeparam>
    public abstract class Architecture<TA> : IArchitecture where TA : Architecture<TA>, new()
    {
        // architecture 静态单例
        private static TA _mArchitecture;

        /// <summary>
        /// model是否初始化完毕
        /// </summary>
        private bool _mInited = false;

        private List<IModel> mModels = new List<IModel>();

        private List<ISystem> mSystems = new List<ISystem>();

        public static Action<TA> OnRegisterPatch = architecture => { };

        public static IArchitecture Interface
        {
            get
            {
                if (_mArchitecture == null)
                {
                    MakeSureArchitecture();
                }

                return _mArchitecture;
            }
        }

        private static void MakeSureArchitecture()
        {
            if (_mArchitecture == null)
            {
                _mArchitecture = new TA();

                _mArchitecture.IocInjection(); // 执行依赖注入

                OnRegisterPatch?.Invoke(_mArchitecture);

                // 执行model的初始化方法
                foreach (var model in _mArchitecture.mModels)
                {
                    model.Init();
                }

                _mArchitecture.mModels.Clear();

                // 执行system的初始化方法
                foreach (var system in _mArchitecture.mSystems)
                {
                    system.Init();
                }

                _mArchitecture.mSystems.Clear();

                _mArchitecture._mInited = true;
            }
        }

        // IOC容器对象
        private IOCContainer mContainer = new IOCContainer();

        /// <summary>
        /// 子类向IOC容器注入对象
        /// </summary>
        protected abstract void IocInjection();

        /// <summary>
        /// 注入模块对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void RegisterModel<T>(T model) where T : IModel
        {
            MakeSureArchitecture();

            // 指定模块所属架构
            model.SetArchitecture(this);

            // 注入IOC容器
            _mArchitecture.mContainer.Register(model);

            if (!_mInited)
            {
                mModels.Add(model);
            }
            else
            {
                model.Init();
            }
        }

        /// <summary>
        /// 注入System对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="system"></param>
        public void RegisterSystem<T1>(T1 system) where T1 : ISystem
        {
            MakeSureArchitecture();

            // 指定模块所属架构
            system.SetArchitecture(this);

            // 注入IOC容器
            _mArchitecture.mContainer.Register(system);

            if (!_mInited)
            {
                mSystems.Add(system);
            }
            else
            {
                system.Init();
            }
        }

        /// <summary>
        /// 注入工具类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="utility"></param>
        public void RegisterUtility<T>(T utility) where T : IUtility
        {
            MakeSureArchitecture();

            _mArchitecture.mContainer.Register(utility);
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return mContainer.Get<T>();
        }

        /// <summary>
        /// 获取工具类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUtility<T>() where T : class, IUtility
        {
            return mContainer.Get<T>();
        }

        /// <summary>
        /// 获取Model对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public T1 GetModel<T1>() where T1 : class, IModel
        {
            return mContainer.Get<T1>();
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
        }


        private IEventSystem mEventSystem = new EventSystem();

        public void SendEvent<T>() where T : new()
        {
            mEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            mEventSystem.Send<T>(e);
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return mEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            mEventSystem.UnRegister<T>(onEvent);
        }
    }
}