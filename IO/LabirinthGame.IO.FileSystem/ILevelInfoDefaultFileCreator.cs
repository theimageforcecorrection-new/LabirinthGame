namespace LabirinthGame.IO.FileSystem
{
    public interface ILevelInfoDefaultFileCreator
    {
        public bool CheckIsFileExist();

        public void CreateFile();
    }
}
