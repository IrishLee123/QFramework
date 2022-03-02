using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface IEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnRegister Register<T>(Action<T> onEvent);
        void UnRegister<T>(Action<T> onEvent);
    }

    public interface IUnRegister
    {
        void UnRegister();
    }

    public struct EventSystemUnRegister<T> : IUnRegister
    {
        public IEventSystem EventSystem;

        public Action<T> OnEvent;

        public void UnRegister()
        {
            EventSystem.UnRegister<T>(OnEvent);

            EventSystem = null;

            OnEvent = null;
        }
    }

    public class UnRegisterDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            mUnRegisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in mUnRegisters)
            {
                unRegister.UnRegister();
            }

            mUnRegisters.Clear();
        }
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameobject)
        {
            var trigger = gameobject.GetComponent<UnRegisterDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameobject.AddComponent<UnRegisterDestroyTrigger>();
            }

            trigger.AddUnRegister(unRegister);
        }
    }

    public class EventSystem : IEventSystem
    {

        public interface IRegisterations { }

        public class Registerations<T> : IRegisterations
        {
            public Action<T> OnEvent = e => { };
        }

        Dictionary<Type, IRegisterations> mEventRegisteration = new Dictionary<Type, IRegisterations>();

        public IUnRegister Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegisterations registerations;

            if (mEventRegisteration.TryGetValue(type, out registerations))
            {

            }
            else
            {
                registerations = new Registerations<T>();
                mEventRegisteration.Add(type, registerations);
            }

            (registerations as Registerations<T>).OnEvent += onEvent;

            return new EventSystemUnRegister<T>()
            {
                OnEvent = onEvent,
                EventSystem = this
            };
        }

        public void Send<T>() where T : new()
        {
            var e = new T();
            Send<T>(e);
        }

        public void Send<T>(T e)
        {
            var type = typeof(T);
            IRegisterations registerations;

            if (mEventRegisteration.TryGetValue(type, out registerations))
            {
                (registerations as Registerations<T>).OnEvent(e);
            }
        }

        public void UnRegister<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegisterations registerations;

            if (mEventRegisteration.TryGetValue(type, out registerations))
            {
                (registerations as Registerations<T>).OnEvent -= onEvent;
            }
        }
    }
}