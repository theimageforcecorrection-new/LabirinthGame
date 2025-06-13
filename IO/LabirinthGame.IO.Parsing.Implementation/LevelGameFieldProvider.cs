using LabirinthGame.IO.Common;

namespace LabirinthGame.IO.Parsing.Implementation
{
    internal sealed class LevelGameFieldProvider : ILevelGameFieldProvider
    {
        private readonly IGameLevelManager _gameLevelManager;

        public LevelGameFieldProvider(IGameLevelManager gameLevelManager)
        {
            _gameLevelManager = gameLevelManager;
        }

        public (int PlayerX, int PlayerY, int[,] Field) Provide(int levelNumber)
        {
            if (!_gameLevelManager.IsLevelExist(levelNumber))
            {
                throw new LevelNotExistException();
            }

            return _gameLevelManager.GetLevelInfo(levelNumber);
        }
    }
}
