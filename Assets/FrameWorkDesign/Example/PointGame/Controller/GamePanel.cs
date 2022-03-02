using System;
using System.Collections;
using System.Collections.Generic;
using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GamePanel : MonoBehaviour, IController
    {
        private Text mBestScoreLab;
        private Text mLifeLab;
        private Text mGoldLab;
        private Text mCountDownLab;

        private void Start()
        {
            transform.Find("BuyLife").GetComponent<Button>().onClick.AddListener(OnBuyLife);

            mBestScoreLab = transform.Find("BestScore").GetComponent<Text>();
            mLifeLab = transform.Find("Life").GetComponent<Text>();
            mGoldLab = transform.Find("Gold").GetComponent<Text>();
            mCountDownLab = transform.Find("CountDown").GetComponent<Text>();
            
            mBestScoreLab.text = "Best: " + this.GetModel<IGameModel>().BestScore.Value;
            mLifeLab.text = "Life: " + this.GetModel<IGameModel>().Life.Value;
            mGoldLab.text = "Gold: " + this.GetModel<IGameModel>().Gold.Value;

            this.GetModel<IGameModel>().BestScore
                .RegisterOnValueChanged(value => { mBestScoreLab.text = "Best: " + value; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            this.GetModel<IGameModel>().Life
                .RegisterOnValueChanged(value => { mLifeLab.text = "Life: " + value; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            this.GetModel<IGameModel>().Gold
                .RegisterOnValueChanged(value => { mGoldLab.text = "Gold: " + value; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<GamePassEvent>(OnGamePassEvent);
        }

        private void Update()
        {
            if (Time.frameCount % 20 == 0)
            {
                this.GetSystem<ICountDownSystem>().Update();
                
                
                mCountDownLab.text = this.GetSystem<ICountDownSystem>().CurrentRemainSeconds + "s";
            }
        }

        private void OnBuyLife()
        {
            this.SendCommand<BuyLifeCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }

        private void OnGamePassEvent(GamePassEvent e)
        {
            gameObject.SetActive(false);
        }
    }
}