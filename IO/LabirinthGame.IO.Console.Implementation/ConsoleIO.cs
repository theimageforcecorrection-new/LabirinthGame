namespace LabirinthGame.IO.Console.Implementation
{
    public sealed class ConsoleIO : IConsoleIO
    {
        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void Write(string line)
        {
            System.Console.Write(line);
        }

        public void Write(char ch)
        {
            System.Console.Write(ch);
        }

        public void WriteLine()
        {
            System.Console.WriteLine();
        }
    }
}
