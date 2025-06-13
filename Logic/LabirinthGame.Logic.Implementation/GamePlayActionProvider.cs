using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic.Implementation
{
    public sealed class GamePlayActionProvider : IGamePlayActionProvider
    {
        private readonly IReadOnlyDictionary<string, IGamePlayAction> _gamePlayActions;

        public GamePlayActionProvider(IReadOnlyList<IGamePlayAction> gamePlayActions)
        {
            _gamePlayActions = gamePlayActions.ToDictionary(a => a.ActionString, a => a);
        }

        public IGamePlayAction Provide(string actionString)
        {
            if (_gamePlayActions.TryGetValue(actionString, out var value))
            {
                return value;
            }

            return _gamePlayActions[string.Empty];
        }
    }
}
