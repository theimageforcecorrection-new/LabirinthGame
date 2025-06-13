using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic.Implementation.GamePlayActions
{
    public sealed class UpGamePlayAction : IGamePlayAction
    {
        public string ActionString => "w";

        public void Invoke(ILabirinthGameLogic gameLogic, IGamePlayGameStateManager gameStateManager)
        {
            gameLogic.TryMovePlayerUp(gameStateManager);
        }
    }
}
