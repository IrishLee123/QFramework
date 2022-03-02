using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrameworkDesign.Example;
using FrameworkDesign;

public class GameStartPanel : MonoBehaviour, IController
{
    public GameObject enemys;

    private void Start()
    {
        transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        this.SendCommand<StartGameCommand>();

        gameObject.SetActive(false);
    }

    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return PointGame.Interface;
    }
}