using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.GameField
{
    public interface ILabirinthGameField : IReadOnlyLabirinthGameField
    {
        public bool TryMovePlayerUp(Func<IGameFieldElement[,], int, int, bool> moveCheck);

        public bool TryMovePlayerDown(Func<IGameFieldElement[,], int, int, bool> moveCheck);

        public bool TryMovePlayerLeft(Func<IGameFieldElement[,], int, int, bool> moveCheck);

        public bool TryMovePlayerRight(Func<IGameFieldElement[,], int, int, bool> moveCheck);
    }
}
