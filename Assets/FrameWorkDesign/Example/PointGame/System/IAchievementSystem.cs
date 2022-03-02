using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IAchievementSystem : ISystem
    {
    }

    public class AchievementItem
    {
        public string Name { get; set; }

        public Func<bool> CheckComplete { get; set; }

        public bool Unlocked { get; set; }
    }

    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        private List<AchievementItem> _mItems = new List<AchievementItem>();

        private bool _mMissed = false;

        protected override void OnInit()
        {
            this.RegisterEvent<OnMissEnemyEvent>(e => { _mMissed = true; });

            this.RegisterEvent<GameStartEvent>(e => { _mMissed = false; });

            // add achievement items
            _mItems.Add(new AchievementItem()
            {
                Name = "百分王者",
                CheckComplete = () => this.GetModel<IGameModel>().BestScore.Value > 100
            });

            _mItems.Add(new AchievementItem()
            {
                Name = "弹无虚发",
                CheckComplete = () => !_mMissed
            });

            _mItems.Add(new AchievementItem()
            {
                Name = "天残手",
                CheckComplete = () => this.GetModel<IGameModel>().Score.Value < 0
            });

            _mItems.Add(new AchievementItem()
            {
                Name = "你是高玩",
                CheckComplete = () => _mItems.Count(item => item.Unlocked) >= 3
            });

            this.RegisterEvent<GamePassEvent>(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));

                foreach (var achievementItem in _mItems)
                {
                    if (!achievementItem.Unlocked && achievementItem.CheckComplete())
                    {
                        achievementItem.Unlocked = true;

                        Debug.Log("解锁成就：" + achievementItem.Name);
                    }
                }
            });
        }
    }
}