using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElement
{
    public sealed class WallGameFieldElement : IGameFieldElement
    {
        public char ElementChar => '#';

        public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return false;
        }
    }
}
