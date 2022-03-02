using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class IOCExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var container = new IOCContainer();

            container.Register<IBluetoothManager>(new BluetoothManager());

            var bluetoothMgr = container.Get<IBluetoothManager>();

            bluetoothMgr.Connect();
        }
    }

    // 依赖倒置原则
    public interface IBluetoothManager
    {
        void Connect();
    }

    public class BluetoothManager : IBluetoothManager
    {
        public void Connect()
        {
            Debug.Log("蓝牙连接成功");
        }
    }
}
