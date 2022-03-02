##对象间交互的方式
**方法调用**
>上层调用下层

**委托注册**
>下层通知上层

**发送事件**
>下层通知上层

##BindableProperty-可绑定的属性

>在数据层与表现层分离的时候，数据更新往往需要通知表现层。这里的通知方式可以使用注册的方式，即UI层注册数据变化事件。s
通过BindableProperty把基础数据抽象成对象，并赋予数值变化时的事件注册器。通过这种方式减少重复的代码。

**代码：**
```
public class BindableProperty<T> where T : IEquatable<T>
{
    private T mValue = default(T);

    public T Value
    {
        get => mValue;

        set
        {
            if (!value.Equals(mValue))
            {
                mValue = value;
                // 数值变化时触发注册器
                OnValueChanged?.Invoke(value);
            }
        }
    }

    public Action<T> OnValueChanged;
}
```

##Command命令模式

>通过命令模式可以将一系列复杂的逻辑独立到一个命令对象内部。
一来可以拆分外部逻辑控制类中的逻辑，使代码整洁；二来可以在开发时专注于这一部分的业务逻辑实现，便与开发。

**命令接口：**
```
namespace FrameworkDesign
{
    // 命令接口
    public interface ICommand
    {
        void Execute();
    }
}
```
**命令类实现：**
```
namespace FrameworkDesign.Example
{
    // 消灭敌人命令
    public class KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            // 消灭敌人数量增加
            PointGame.Get<GameModel>().KillCount.Value++;

            if (PointGame.Get<GameModel>().KillCount.Value >= 10)
            {
                // 达成胜利条件
                GamePassEvent.Triger();
            }
        }
    }
}
```
**命令执行：**
```
public class Enemy : MonoBehaviour
{
    private void OnMouseDown()
    {
        // 鼠标点到Enemy时销毁Enemy
        GameObject.Destroy(gameObject);

        // 执行命令
        new KillEnemyCommand().Execute();
    }
}
```
##单例与IOC容器

##贫血模型与充血模型

**贫血模型** 表现层查询用户数据时，返回的是一个完整的用户对象，这种数据流向模型成为贫血模型

**充血模型** 额外定义一个数据结构，只包含表现层所需的数据，查询时返回这个数据结构称为充血模型。
可以通过引入System或Command+Event组合来实现