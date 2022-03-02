using UnityEngine;

namespace FrameworkDesign.Example
{
    public class  Game : MonoBehaviour, IController
    {
        private void Start()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGameStart(GameStartEvent e)
        {
            // reset game when start game
            var enemyRoot = transform.Find("Enemies").gameObject;
            enemyRoot.SetActive(true);
            for (int i = 0; i < enemyRoot.transform.childCount; i++)
            {
                var child = enemyRoot.transform.GetChild(i);
                if (child.gameObject != null) child.gameObject.SetActive(true);
            }

            transform.Find("Enemies").gameObject.SetActive(true);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}