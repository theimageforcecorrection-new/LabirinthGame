using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic.Implementation.GamePlayActions
{
    public sealed class LeftGamePlayAction : IGamePlayAction
    {
        public string ActionString => "a";

        public void Invoke(ILabirinthGameLogic gameLogic, IGamePlayGameStateManager gameStateManager)
        {
            gameLogic.TryMovePlayerLeft(gameStateManager);

        }
    }
}
