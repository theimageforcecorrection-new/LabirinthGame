using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic.Implementation.GamePlayActions
{
    public sealed class DownGamePlayAction : IGamePlayAction
    {
        public string ActionString => "s";

        public void Invoke(ILabirinthGameLogic gameLogic, IGamePlayGameStateManager gameStateManager)
        {
            gameLogic.TryMovePlayerDown(gameStateManager);
        }
    }
}
