using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElement
{
    public abstract class MovableGameFieldElementBase : IGameFieldElement
    {
        public abstract char ElementChar { get; }

        public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return true;
        }
    }
}
