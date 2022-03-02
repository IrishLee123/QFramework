using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameOverPanel : MonoBehaviour, IController
    {
        private void Start()
        {
            transform.Find("ReturnBtn").GetComponent<Button>().onClick.AddListener(OnBtnReturn);
        }

        private void OnBtnReturn()
        {
            this.SendCommand<RestartGameCommand>();

            gameObject.SetActive(false);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}