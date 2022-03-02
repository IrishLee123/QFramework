using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void IocInjection()
        {
            RegisterUtility<IStorage>(new PlayerPrefsStorage());

            RegisterModel<IGameModel>(new GameModel());

            RegisterSystem<IScoreSystem>(new ScoreSystem());    
            RegisterSystem<ICountDownSystem>(new CountDownSystem());    
            RegisterSystem<IAchievementSystem>(new AchievementSystem());    
        }
    }
}
