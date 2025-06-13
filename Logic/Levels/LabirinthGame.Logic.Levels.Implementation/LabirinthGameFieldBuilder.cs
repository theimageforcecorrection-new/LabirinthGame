using LabirinthGame.IO.Parsing;
using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameField;
using LabirinthGame.Logic.Levels.GameFieldElement;
using LabirinthGame.Logic.Levels.Implementation.GameField;

namespace LabirinthGame.Logic.Levels.Implementation
{
    public sealed class LabirinthGameFieldBuilder : ILabirinthGameFieldBuilder
    {
        private readonly IGameFieldElementCreatorProvider _elementCreatorProvider;
        private readonly ILevelGameFieldProvider _levelGameFieldProvider;

        public LabirinthGameFieldBuilder(IGameFieldElementCreatorProvider elementCreatorProvider, ILevelGameFieldProvider levelGameFieldProvider)
        {
            _elementCreatorProvider = elementCreatorProvider;
            _levelGameFieldProvider = levelGameFieldProvider;
        }

        public ILabirinthGameField Build(int levelNumber)
        {
            var (playerX, playerY, providedField) = _levelGameFieldProvider.Provide(levelNumber);

            var newField = new IGameFieldElement[providedField.GetLength(0), providedField.GetLength(1)];

            for (int i = 0; i < providedField.GetLength(0); i++)
            {
                for (int j = 0; j < providedField.GetLength(1); j++)
                {
                    var elementCreator = _elementCreatorProvider.Provide(providedField[i, j]);
                    newField[i, j] = elementCreator.Create();
                }
            }

            var field = new LabirinthGameField(newField, playerX, playerY);

            return field;
        }
    }
}
