using UnityEngine;

namespace FrameworkDesign.Example
{
    public class BuyLifeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();

            if (gameModel.Gold.Value > 0)
            {
                gameModel.Gold.Value--;
                gameModel.Life.Value++;
            }
            else
            {
                Debug.Log("not enough gold.");
            }
        }
    }
}