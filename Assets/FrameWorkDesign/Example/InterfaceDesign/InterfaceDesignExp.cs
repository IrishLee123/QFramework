using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface ICanSayHello
    {
        void SayHello();
        void SayOther();
    }

    public class InterfaceDesignExp : MonoBehaviour, ICanSayHello
    {
        /// <summary>
        /// 接口的隐式实现
        /// </summary>
        public void SayHello()
        {
            Debug.Log("Hello");
        }

        /// <summary>
        /// 接口的显式实现
        /// </summary>
        void ICanSayHello.SayOther()
        {
            Debug.Log("Other");
        }

        private void Start()
        {

            this.SayHello();

            // 显示实现的接口增加了子类调用的难度，以此来限制子类对某些关键方法的调用
            (this as ICanSayHello).SayOther();
        }
    }
}
