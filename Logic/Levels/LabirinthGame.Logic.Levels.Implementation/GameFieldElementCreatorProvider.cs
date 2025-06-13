using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameFieldElementCreator;

namespace LabirinthGame.Logic.Levels.Implementation
{
    public sealed class GameFieldElementCreatorProvider : IGameFieldElementCreatorProvider
    {
        private readonly IReadOnlyDictionary<int, IGameFieldElementCreator> _gameFieldElementCreators;

        public GameFieldElementCreatorProvider(IReadOnlyList<IGameFieldElementCreator> gameFieldElementCreators)
        {
            _gameFieldElementCreators = gameFieldElementCreators.ToDictionary(c => c.ElementCode, c => c);
        }

        public IGameFieldElementCreator Provide(int elementCode)
        {
            if (_gameFieldElementCreators.TryGetValue(elementCode, out var creator))
            {
                return creator;
            }

            return _gameFieldElementCreators[0];
        }
    }
}
