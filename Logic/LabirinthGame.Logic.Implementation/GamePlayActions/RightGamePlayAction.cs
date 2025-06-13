using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic.Implementation.GamePlayActions
{
    public sealed class RightGamePlayAction : IGamePlayAction
    {
        public string ActionString => "d";

        public void Invoke(ILabirinthGameLogic gameLogic, IGamePlayGameStateManager gameStateManager)
        {
            gameLogic.TryMovePlayerRight(gameStateManager);

        }
    }
}
