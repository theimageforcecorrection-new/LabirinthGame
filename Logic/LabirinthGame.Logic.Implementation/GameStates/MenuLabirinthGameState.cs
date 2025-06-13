using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Logic.GameStates;
using LabirinthGame.Ui.MenuNavigation;

namespace LabirinthGame.Logic.Implementation.GameStates
{
    public sealed class MenuLabirinthGameState : ILabirinthGameState
    {
        private readonly IMenuActionApplier _menuActionApplier;
        private readonly IConsoleGameUI _consoleGameUI;

        public MenuLabirinthGameState(IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI)
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

            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write(" ####    **     #   #  #####  ####     #          \n");
            _consoleGameUI.Write("#       ****    #  ##  #      #   #   # #         \n");
            _consoleGameUI.Write(" ###   ******   # # #  #      ####   #####        \n");
            _consoleGameUI.Write("    #   ****    ##  #  #      #       # #         \n");
            _consoleGameUI.Write("####     **     #   #  #      #      #   #        \n");
            _consoleGameUI.Write("                                                        \n");


            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write("                                                        \n");



            _consoleGameUI.Write("                                                        \n");
            _consoleGameUI.Write(" ## #    **     ####   #      #  #   #   ###     #           \n");
            _consoleGameUI.Write("#  ##   ****    #   #  #      #   # #   #   #   # #         \n");
            _consoleGameUI.Write("#  ##  ******   ####   ####   #    #    #   #   # #        \n");
            _consoleGameUI.Write(" ## #   ****    #   #  #   #  #   # #   #   #  #####         \n");
            _consoleGameUI.Write("    #    **     ####   ####   #  #   #   ###   #   #        \n");
            _consoleGameUI.Write("                                                        \n");

            return string.Empty;
        }

        public void ProcessInput(ILabirinthGame game)
        {
            var input = _consoleGameUI.ReadLine();

            _menuActionApplier.SelectAndApplyActionByString(input, game, _consoleGameUI);
        }
    }
}
