using LabirinthGame.Logic;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Ui.MenuNavigation.GameMenuActions;

namespace LabirinthGame.Ui.MenuNavigation.Implementation
{
    public sealed class MenuActionApplier : IMenuActionApplier
    {
        private IReadOnlyDictionary<string, IGameMenuAction> _gameMenuActions;

        public MenuActionApplier(IReadOnlyList<IGameMenuAction> gameMenuActions)
        {
            _gameMenuActions = gameMenuActions.ToDictionary(a => a.ActionString, a => a);
        }

        public void SelectAndApplyActionByString(string actionString, ILabirinthGame game, IConsoleGameUI consoleGameUI)
        {
            if (_gameMenuActions.TryGetValue(actionString, out var action))
            {
                action.Invoke(game, this, consoleGameUI);
            }
        }
    }
}
