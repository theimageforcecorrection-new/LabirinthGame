using LabirinthGame.Common;
using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Implementation.GameStates;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Ui.MenuNavigation;
using LabirinthGame.Ui.MenuNavigation.GameMenuActions;
using LabirinthGame.Ui.TextUi;

namespace LabirinthGame.Logic.Implementation.GameMenuActions
{
    public sealed class StartNewGameMenuAction : IGameMenuAction
    {
        private readonly ILabirinthGamePlayIO _gamePlayIo;
        private readonly ILabirinthGameFieldBuilder _fieldBuilder;

        public StartNewGameMenuAction(ILabirinthGamePlayIO gamePlayIo, ILabirinthGameFieldBuilder fieldBuilder)
        {
            _gamePlayIo = gamePlayIo;
            _fieldBuilder = fieldBuilder;
        }

        public string ActionString => "s";

        public void Invoke(ILabirinthGame game, IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI)
        {
            //TODO: Здесь сделать вывод ошибки чтения файла!!!
            try
            {
                game.State = new LevelRunningLabirinthGameState(_gamePlayIo, _fieldBuilder, menuActionApplier, consoleGameUI);
            }
            catch (LabirinthGameException)
            {

            }
        }
    }
}
