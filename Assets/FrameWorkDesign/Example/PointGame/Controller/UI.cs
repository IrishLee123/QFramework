using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;

public class UI : MonoBehaviour, IController
{
    private void Start()
    {
        this.RegisterEvent<GameStartEvent>(OnGameStart)
            .UnRegisterWhenGameObjectDestroyed(gameObject);

        this.RegisterEvent<GamePassEvent>(OnGamePass)
            .UnRegisterWhenGameObjectDestroyed(gameObject);
        
        this.RegisterEvent<OnCountDownEndEvent>(OnCountDownEnd)
            .UnRegisterWhenGameObjectDestroyed(gameObject);
        
        this.RegisterEvent<OnRestartGameEvent>(OnRestartGame)
            .UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnGameStart(GameStartEvent e)
    {
        transform.Find("GamePanel")?.gameObject.SetActive(true);
    }
    
    private void OnGamePass(GamePassEvent e)
    {
        transform.Find("GamePanel")?.gameObject.SetActive(false);
        transform.Find("GamePassPanel")?.gameObject.SetActive(true);
    }
    
    private void OnCountDownEnd(OnCountDownEndEvent e)
    {
        transform.Find("GamePanel")?.gameObject.SetActive(false);
        transform.Find("GameOverPanel")?.gameObject.SetActive(true);
    }

    private void OnRestartGame(OnRestartGameEvent e)
    {
        transform.Find("GameStartPanel")?.gameObject.SetActive(true);
    }


    public IArchitecture GetArchitecture()
    {
        return PointGame.Interface;
    }
}