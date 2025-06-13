using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElement
{
    public abstract class LosingGameFieldElementBase : IGameFieldElement
    {
        public abstract char ElementChar { get; }

        public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            gamePlayGameStateManager.SetLoseState();
            return false;
        }
    }
}
