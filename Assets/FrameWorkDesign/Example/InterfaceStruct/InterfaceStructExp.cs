using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface ICustomScript
    {
        void Start();
        void update();
        void Destroy();
    }

    /// <summary>
    /// Start，Update，Destroy可以理解为父类关键方法
    /// 仅提供给底层框架使用
    /// 通过接口限制方法
    /// 防止子类不小心调用父类关键方法
    /// 这种写法在框架内部常见
    /// </summary>
    public abstract class CustomScript : ICustomScript
    {
        /// <summary>
        /// 接口限制方法实现
        /// </summary>
        void ICustomScript.Start()
        {
            OnStart();
        }

        protected abstract void OnStart();

        void ICustomScript.update()
        {
            OnUpdate();
        }

        protected abstract void OnUpdate();

        void ICustomScript.Destroy()
        {
            OnDestroy();
        }

        protected abstract void OnDestroy();

    }

    public class MyScript : CustomScript
    {
        protected override void OnDestroy()
        {
            Debug.Log("on destroy");
        }

        protected override void OnStart()
        {
            Debug.Log("on start");
        }

        protected override void OnUpdate()
        {
            Debug.Log("on update");
        }
    }

    public class InterfaceStructExp : MonoBehaviour
    {
        private void Start()
        {
            ICustomScript script = new MyScript();
            script.Start();
            script.update();
            script.Destroy();
        }

    }
}
