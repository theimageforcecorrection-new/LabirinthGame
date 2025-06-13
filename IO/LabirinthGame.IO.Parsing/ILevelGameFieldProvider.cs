namespace LabirinthGame.IO.Parsing
{
    public interface ILevelGameFieldProvider
    {
        (int PlayerX, int PlayerY, int[,] Field) Provide(int levelNumber);
    }
}
