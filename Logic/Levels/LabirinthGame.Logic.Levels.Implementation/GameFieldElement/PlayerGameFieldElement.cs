using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElement
{
    public sealed class PlayerGameFieldElement : IGameFieldElement
    {
        public char ElementChar => 'o';

        public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return false;
        }
    }
}
