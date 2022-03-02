using System;
using System.Collections;
using System.Collections.Generic;
using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;
using UnityEngine.UI;

public class GamePassPanel : MonoBehaviour, IController
{
    private Text _mRemainTimeLab;
    private Text _mScoreLab;
    private Text _mBestScoreLab;

    private void Start()
    {
        _mRemainTimeLab = transform.Find("RemainTime").GetComponent<Text>();
        _mScoreLab = transform.Find("Score").GetComponent<Text>();
        _mBestScoreLab = transform.Find("BestScore").GetComponent<Text>();

        _mRemainTimeLab.text = "剩余时间: " + this.GetSystem<ICountDownSystem>().CurrentRemainSeconds + "s";
        _mScoreLab.text = "当前分数: " + this.GetModel<IGameModel>().Score.Value;
        _mBestScoreLab.text = "最高分数: " + this.GetModel<IGameModel>().BestScore.Value;
        
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