using LabirinthGame.IO.Common;

namespace LabirinthGame.IO.FileSystem
{
    public sealed class LevelInfoFileLinesProviderCreator : ILevelInfoFileLinesProviderCreator
    {
        public ILevelInfoFileLinesProvider Create()
        {
            return new LevelInfoFileLinesProvider();
        }
    }

    public sealed class LevelInfoFileLinesProvider : ILevelInfoFileLinesProvider
    {
        private readonly StreamReader _streamReader;

        private bool isDisposed = false;

        public LevelInfoFileLinesProvider()
        {
            _streamReader = new StreamReader(LevelInfoFileConstants.FileName);
        }

        public string GetNextLine()
        {
            var nextLine = _streamReader.ReadLine();

            if (nextLine is null)
            {
                throw new LevelInfoFileReadingException();
            }

            return nextLine;
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            _streamReader.Dispose();
            isDisposed = true;
        }
    }

    public interface ILevelInfoFileLinesProviderCreator
    {
        ILevelInfoFileLinesProvider Create();
    }

}
