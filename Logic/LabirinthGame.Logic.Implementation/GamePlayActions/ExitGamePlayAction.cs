using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic.Implementation.GamePlayActions
{
    public sealed class ExitGamePlayAction : IGamePlayAction
    {
        public string ActionString => "q";

        public void Invoke(ILabirinthGameLogic gameLogic, IGamePlayGameStateManager gameStateManager)
        {
            gameStateManager.SetMenuState();
        }
    }
}
