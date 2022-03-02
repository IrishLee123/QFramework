using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface ICountDownSystem : ISystem
    {
        int CurrentRemainSeconds { get; }
        void Update();
    }

    public class CountDownSystem : AbstractSystem, ICountDownSystem
    {
        public int CurrentRemainSeconds => 10 - (int) (DateTime.Now - _mStartTime).TotalSeconds;

        private Boolean _mStarted;

        private DateTime _mStartTime;

        protected override void OnInit()
        {
            this.RegisterEvent<GameStartEvent>(e =>
            {
                _mStarted = true;
                _mStartTime = DateTime.Now;
            });

            this.RegisterEvent<GamePassEvent>(e => { _mStarted = false; });
        }

        public void Update()
        {
            if (_mStarted)
            {
                if (DateTime.Now - _mStartTime > TimeSpan.FromSeconds(10))
                {
                    this.SendEvent<OnCountDownEndEvent>();
                    _mStarted = false;
                }
            }
        }
    }
}