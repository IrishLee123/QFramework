using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{

    public class CanDoEverything
    {
        public void Eat()
        {
            Debug.Log("Eat");
        }

        public void Sleep()
        {
            Debug.Log("Sleep");
        }

        public void KickAss()
        {
            Debug.Log("KickAss");
        }
    }

    public interface IHasEverything
    {
        /// <summary>
        /// 持有一个可以调用所有方法的对象
        /// </summary>
        CanDoEverything CanDoEverything { get; }
    }

    /// <summary>
    /// 接口限定规则
    /// </summary>
    public interface ICanEat : IHasEverything { }

    /// <summary>
    /// 接口静态扩展
    /// 限定接口需要实现哪些方法
    /// </summary>
    public static class ICanEatExtension
    {
        public static void DoEat(this ICanEat self)
        {
            self.CanDoEverything.Eat();
        }
    }

    public interface ICanSleep : IHasEverything { }

    public static class ICanSleepExtension
    {
        public static void DoSleep(this ICanSleep self)
        {
            self.CanDoEverything.Sleep();
        }
    }

    public interface ICanKickAss : IHasEverything { }

    public static class ICanKickAssExtension
    {
        public static void DoKickAss(this ICanKickAss self)
        {
            self.CanDoEverything.KickAss();
        }
    }


    public class InterfaceRuleExp : MonoBehaviour
    {
        /// <summary>
        /// 实现对象实例
        /// 通过接口规则选择所需功能
        /// </summary>
        public class OnlyCanEat : ICanEat
        {
            CanDoEverything IHasEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        /// <summary>
        /// 实现对象实例
        /// 通过接口规则选择所需功能
        /// </summary>
        public class CanEatAndSleep : ICanEat, ICanSleep
        {
            CanDoEverything IHasEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        private void Start()
        {
            var onlyCanEat = new OnlyCanEat();
            onlyCanEat.DoEat();

            var eatAndSleep = new CanEatAndSleep();
            eatAndSleep.DoEat();
            eatAndSleep.DoSleep();
        }
    }

}