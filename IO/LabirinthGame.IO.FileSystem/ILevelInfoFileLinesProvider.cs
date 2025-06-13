namespace LabirinthGame.IO.FileSystem
{
    public interface ILevelInfoFileLinesProvider : IDisposable
    {
        public string GetNextLine();
    }
}
