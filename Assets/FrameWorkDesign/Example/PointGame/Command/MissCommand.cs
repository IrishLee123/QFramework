﻿namespace FrameworkDesign.Example
{
    public class MissCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();

            if (gameModel.Life.Value > 0)
            {
                gameModel.Life.Value--;
            }
            else
            {
                this.SendEvent<OnMissEnemyEvent>();
            }
        }
    }
}