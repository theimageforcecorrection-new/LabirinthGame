namespace LabirinthGame.Logic.ConsoleUi
{
    public interface IConsoleGameUI
    {
        string ReadLine();

        void Write(string line);

        void Write(char ch);

        void WriteLine();
    }
}
