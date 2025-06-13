using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.GameField
{
    public interface IReadOnlyLabirinthGameField
    {
        public int MaxX { get; }

        public int MaxY { get; }

        public IGameFieldElement this[int x, int y] { get; }
    }
}
