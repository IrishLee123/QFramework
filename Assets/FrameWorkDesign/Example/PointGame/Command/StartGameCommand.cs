namespace FrameworkDesign.Example
{
    public class StartGameCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            // reset game data
            var gameModel = this.GetModel<IGameModel>();

            gameModel.KillCount.Value = 0;
            gameModel.Score.Value = 0;

            this.SendEvent<GameStartEvent>();
        }
    }
}