namespace LabirinthGame.IO.Parsing.Implementation
{
    internal sealed class GameLevelManager : IGameLevelManager
    {
        private readonly IReadOnlyList<(int PlayerX, int PlayerY, int[,] Field)> _levelGameFields;

        public GameLevelManager(ILevelInfosInitializer fieldInitializer)
        {
            _levelGameFields = fieldInitializer.Initialize();
        }

        public bool IsLevelExist(int levelNumber)
        {
            return levelNumber < _levelGameFields.Count && levelNumber >= 0;
        }

        public (int PlayerX, int PlayerY, int[,] Field) GetLevelInfo(int levelNumber)
        {
            return _levelGameFields[levelNumber];
        }
    }
}
