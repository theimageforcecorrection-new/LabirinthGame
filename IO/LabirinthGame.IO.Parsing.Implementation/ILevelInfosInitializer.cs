namespace LabirinthGame.IO.Parsing.Implementation
{
    internal interface ILevelInfosInitializer
    {
        IReadOnlyList<(int PlayerX, int PlayerY, int[,] Field)> Initialize();
    }
}
