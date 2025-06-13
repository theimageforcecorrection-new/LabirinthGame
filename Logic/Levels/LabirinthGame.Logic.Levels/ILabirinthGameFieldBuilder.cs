using LabirinthGame.Logic.Levels.GameField;

namespace LabirinthGame.Logic.Levels
{
    public interface ILabirinthGameFieldBuilder
    {
        public ILabirinthGameField Build(int levelNumber);
    }
}
