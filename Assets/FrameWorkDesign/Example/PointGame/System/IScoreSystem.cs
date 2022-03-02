using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IScoreSystem : ISystem { }

    public class ScoreSystem : AbstractSystem, IScoreSystem
    {
        protected override void OnInit()
        {
            var gameModel = this.GetModel<IGameModel>();

            // do when miss fire
            this.RegisterEvent<OnMissEnemyEvent>(e =>
            {
                gameModel.Score.Value -= 5;
                Debug.Log("current score: " + gameModel.Score.Value);
            });

            // do when kill enemy
            this.RegisterEvent<KillOneEnemyEvent>(e =>
            {
                // add score
                gameModel.Score.Value += 10;
                Debug.Log("current score: " + gameModel.Score.Value);
            });

            this.RegisterEvent<GamePassEvent>(e =>
            {
                // calculate time score
                var countDownSys = this.GetSystem<ICountDownSystem>();
                var timeScore = countDownSys.CurrentRemainSeconds * 10;
                gameModel.Score.Value += timeScore;
                    
                // check best score
                if (gameModel.Score.Value > gameModel.BestScore.Value)
                {
                    gameModel.BestScore.Value = gameModel.Score.Value;
                    Debug.Log("新纪录");
                }
            });
        }
    }
}
