using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Logic.GameStates;
using LabirinthGame.Ui.MenuNavigation;

namespace LabirinthGame.Logic.Implementation.GameStates
{
    public sealed class LoseLabirinthGameState : ILabirinthGameState
    {
        private readonly IMenuActionApplier _menuActionApplier;
        private readonly IConsoleGameUI _consoleGameUI;

        public LoseLabirinthGameState(IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI)
        {
            _menuActionApplier = menuActionApplier;
            _consoleGameUI = consoleGameUI;
        }

        public string GetDisplayingInfo(ILabirinthGame game)
        {
            for (int i = 0; i < 60; i++)
            {
                _consoleGameUI.WriteLine();
            }

            _consoleGameUI.Write("     **********************************     \n");
            _consoleGameUI.Write("     *                                *     \n");
            _consoleGameUI.Write("     *   #   #   # #   #####  ####    *     \n");
            _consoleGameUI.Write("     *    # #   # # #  #      #   #   *     \n");
            _consoleGameUI.Write("     *     #    # # #  #####  ####    *     \n");
            _consoleGameUI.Write("     *    #     # # #  #      #       *     \n");
            _consoleGameUI.Write("     *   #      # # #  #####  #       *     \n");
            _consoleGameUI.Write("     *                                *     \n");
            _consoleGameUI.Write("     **********************************     \n");

            return string.Empty;
        }

        public void ProcessInput(ILabirinthGame game)
        {
            _consoleGameUI.ReadLine();

            game.State = new MenuLabirinthGameState(_menuActionApplier, _consoleGameUI);
        }
    }
}
