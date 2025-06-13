using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElement
{
    public abstract class WinningGameFieldElementBase : IGameFieldElement
    {
        public abstract char ElementChar { get; }

        public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            gamePlayGameStateManager.SetWinState();
            return false;
        }
    }
}
