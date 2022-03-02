using UnityEngine;

namespace FrameworkDesign.Example
{
    // 消灭敌人命令
    public class KillEnemyCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            
            // 消灭敌人数量增加
            gameModel.KillCount.Value++;

            // random add gold
            if (Random.Range(0, 10) < 3)
            {
                gameModel.Gold.Value += Random.Range(1, 3);
            }
            
            this.SendEvent<KillOneEnemyEvent>();

            // check if game end
            if (gameModel.KillCount.Value >= 10)
            {
                // 达成胜利条件
                this.SendEvent<GamePassEvent>();
            }
        }
    }
}
