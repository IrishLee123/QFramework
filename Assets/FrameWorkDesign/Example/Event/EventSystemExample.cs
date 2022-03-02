using FrameworkDesign;
using UnityEngine;

public class EventSystemExample : MonoBehaviour
{

    public struct EventA
    {

    }

    public struct EventB
    {
        public int ParamB;
    }

    public interface IEventGroup { }

    public struct EventC : IEventGroup
    {

    }
    public struct EventD : IEventGroup
    {

    }



    private EventSystem mEventSystem = new EventSystem();

    private void Start()
    {
        mEventSystem.Register<EventA>(OneventA);

        mEventSystem.Register<EventB>(b =>
        {
            Debug.Log("OnEventB, param: " + b.ParamB);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        mEventSystem.Register<IEventGroup>(e =>
        {
            Debug.Log(e.GetType());
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mEventSystem.Send<EventA>();
        }

        if (Input.GetMouseButtonDown(1))
        {
            mEventSystem.Send(new EventB()
            {
                ParamB = 123
            });
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mEventSystem.Send<IEventGroup>(new EventC());
            mEventSystem.Send<IEventGroup>(new EventD());
        }
    }

    private void OneventA(EventA obj)
    {
        Debug.Log("OnEventA");
    }

    private void OnDestroy()
    {
        mEventSystem.UnRegister<EventA>(OneventA);

        mEventSystem = null;
    }
}
