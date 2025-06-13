using LabirinthGame.IO.Common;
using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Logic.GameStates;
using LabirinthGame.Ui.MenuNavigation;
using LabirinthGame.Ui.TextUi;

namespace LabirinthGame.Logic.Implementation.GameStates
{
    public sealed class WinLabirinthGameState : ILabirinthGameState
    {
        private readonly ILabirinthGamePlayIO _gamePlayIo;
        private readonly ILabirinthGameFieldBuilder _fieldBuilder;
        private readonly IMenuActionApplier _menuActionApplier;
        private readonly IConsoleGameUI _consoleGameUI;

        private readonly int _levelNumber;

        public WinLabirinthGameState(ILabirinthGamePlayIO gamePlayIo, ILabirinthGameFieldBuilder fieldBuilder, IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI, int levelNumber = 0)
        {
            _fieldBuilder = fieldBuilder;
            _gamePlayIo = gamePlayIo;
            _levelNumber = levelNumber;
            _menuActionApplier = menuActionApplier;
            _consoleGameUI = consoleGameUI;
        }

        public string GetDisplayingInfo(ILabirinthGame game)
        {
            for (int i = 0; i < 60; i++)
            {
                _consoleGameUI.WriteLine();
            }

            _consoleGameUI.Write("     ************************************************     \n");
            _consoleGameUI.Write("     *                                              *     \n");
            _consoleGameUI.Write("     *   #####   ###   #####  #####    #      #     *     \n");
            _consoleGameUI.Write("     *   #   #  #   #  #      #       # #    # #    *     \n");
            _consoleGameUI.Write("     *   #   #  #   #  ####   #####   # #   #####   *     \n");
            _consoleGameUI.Write("     *   #   #  #   #  #   #  #      #####   # #    *     \n");
            _consoleGameUI.Write("     *   #   #   ###   ####   #####  #   #  #   #   *     \n");
            _consoleGameUI.Write("     *                                              *     \n");
            _consoleGameUI.Write("     ************************************************     \n");

            return string.Empty;
        }

        public void ProcessInput(ILabirinthGame game)
        {
            _consoleGameUI.ReadLine();

            //TODO: Здесь сделать по-другому проверку на закончившиеся уровни!!!
            try
            {
                game.State = new LevelRunningLabirinthGameState(_gamePlayIo, _fieldBuilder, _menuActionApplier, _consoleGameUI, _levelNumber + 1);
            }
            catch (LevelNotExistException)
            {
                game.State = new MenuLabirinthGameState(_menuActionApplier, _consoleGameUI);
            }
        }
    }
}
