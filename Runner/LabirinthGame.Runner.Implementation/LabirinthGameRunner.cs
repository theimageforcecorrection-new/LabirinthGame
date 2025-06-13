using LabirinthGame.Logic;
using LabirinthGame.Logic.Implementation.GameStates;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Ui.MenuNavigation;

namespace LabirinthGame.Runner.Implementation
{
    public sealed class LabirinthGameRunner : ILabirinthGameRunner
    {
        private readonly Thread _gameThread;
        private readonly ILabirinthGame _game;


        public LabirinthGameRunner(IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI)
        {
            _game = new Logic.Implementation.LabirinthGame(new MenuLabirinthGameState(menuActionApplier, consoleGameUI));

            _gameThread = new Thread(Start);
            _gameThread.Start();
        }

        public void Wait()
        {
            _gameThread.Join();
        }

        private void Start()
        {
            while (_game.IsRunning)
            {
                _game.GetDisplayingInfo();
                _game.ProcessInput();
            }
        }
    }
}
