using LabirinthGame.Logic;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Ui.MenuNavigation.GameMenuActions;

namespace LabirinthGame.Ui.MenuNavigation.Implementation.GameMenuActions
{
    public sealed class ExitGameMenuAction : IGameMenuAction
    {
        public string ActionString => "q";

        public void Invoke(ILabirinthGame game, IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI)
        {
            game.IsRunning = false;
        }
    }
}
