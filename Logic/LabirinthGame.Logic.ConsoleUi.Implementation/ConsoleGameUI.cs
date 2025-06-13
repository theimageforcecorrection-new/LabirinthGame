using LabirinthGame.IO.Console;

namespace LabirinthGame.Logic.ConsoleUi.Implementation
{
    public sealed class ConsoleGameUI : IConsoleGameUI
    {
        private IConsoleIO _consoleIO;

        public ConsoleGameUI(IConsoleIO consoleIO)
        {
            _consoleIO = consoleIO;
        }

        public string ReadLine()
        {
            return _consoleIO.ReadLine();
        }

        public void Write(string line)
        {
            _consoleIO.Write(line);
        }

        public void Write(char ch)
        {
            _consoleIO.Write(ch);
        }

        public void WriteLine()
        {
            _consoleIO.WriteLine();
        }
    }
}
