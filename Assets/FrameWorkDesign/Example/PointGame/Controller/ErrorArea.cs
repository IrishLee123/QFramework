using UnityEngine;

namespace FrameworkDesign.Example
{
    public class ErrorArea : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Interface;
        }

        private void OnMouseDown()
        {
            Debug.Log("点错了");
            this.SendCommand<MissCommand>();
        }
    }
}
