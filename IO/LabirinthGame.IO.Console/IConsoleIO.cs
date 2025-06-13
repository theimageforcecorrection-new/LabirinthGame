namespace LabirinthGame.IO.Console
{
    public interface IConsoleIO
    {
        string ReadLine();

        void Write(string line);

        void Write(char ch);

        void WriteLine();
    }
}
