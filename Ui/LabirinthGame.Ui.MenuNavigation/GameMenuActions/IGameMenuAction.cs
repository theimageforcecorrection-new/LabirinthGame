using LabirinthGame.Logic;
using LabirinthGame.Logic.ConsoleUi;

namespace LabirinthGame.Ui.MenuNavigation.GameMenuActions
{
    public interface IGameMenuAction
    {
        string ActionString { get; }

        void Invoke(ILabirinthGame game, IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI);
    }
}
