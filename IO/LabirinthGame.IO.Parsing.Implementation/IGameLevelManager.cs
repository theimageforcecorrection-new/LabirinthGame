namespace LabirinthGame.IO.Parsing.Implementation
{
    internal interface IGameLevelManager
    {
        bool IsLevelExist(int levelNumber);

        (int PlayerX, int PlayerY, int[,] Field) GetLevelInfo(int levelNumber);
    }
}
