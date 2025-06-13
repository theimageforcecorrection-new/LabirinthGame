using LabirinthGame.Logic;
using LabirinthGame.Logic.ConsoleUi;

namespace LabirinthGame.Ui.MenuNavigation
{
    public interface IMenuActionApplier
    {
        void SelectAndApplyActionByString(string actionString, ILabirinthGame game, IConsoleGameUI consoleGameUI);
    }
}
