using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Logic.GameStates;
using LabirinthGame.Ui.MenuNavigation;
using LabirinthGame.Ui.TextUi;

namespace LabirinthGame.Logic.Implementation.GameStates
{
    public sealed class LevelRunningLabirinthGameState : ILabirinthGameState
    {
        private readonly ILabirinthGamePlayIO _gamePlayIo;
        private readonly ILabirinthGameLogic _gameLogic;
        private readonly ILabirinthGameFieldBuilder _fieldBuilder;
        private readonly IMenuActionApplier _menuActionApplier;
        private readonly IConsoleGameUI _consoleGameUI;

        private readonly int _levelNumber;

        public LevelRunningLabirinthGameState(ILabirinthGamePlayIO gamePlayIo, ILabirinthGameFieldBuilder fieldBuilder, IMenuActionApplier menuActionApplier, IConsoleGameUI consoleGameUI, int levelNumber = 0)
        {
            _fieldBuilder = fieldBuilder;
            _gamePlayIo = gamePlayIo;
            _levelNumber = levelNumber;
            _menuActionApplier = menuActionApplier;
            _consoleGameUI = consoleGameUI;

            var field = fieldBuilder.Build(levelNumber);

            //TODO: Заинжектить сюда ILabirinthGameLogic через конструктор, а сам field присваивать методом, а не передавать тут через конструктор!!!
            _gameLogic = new LabirinthGameLogic(field);
        }

        public string GetDisplayingInfo(ILabirinthGame game)
        {
            _gamePlayIo.ShowGameField(_gameLogic);

            return string.Empty;
        }

        public void ProcessInput(ILabirinthGame game)
        {
            var inputAction = _gamePlayIo.ReadNextAction();

            _gameLogic.ApplyAction(inputAction, () => SetWinStateAction(game), () => SetLoseStateAction(game), () => SetMenuStateAction(game));

        }

        private void SetWinStateAction(ILabirinthGame game)
        {
            game.State = new WinLabirinthGameState(_gamePlayIo, _fieldBuilder, _menuActionApplier, _consoleGameUI, _levelNumber);
        }

        private void SetLoseStateAction(ILabirinthGame game)
        {
            game.State = new LoseLabirinthGameState(_menuActionApplier, _consoleGameUI);
        }

        private void SetMenuStateAction(ILabirinthGame game)
        {
            game.State = new MenuLabirinthGameState(_menuActionApplier, _consoleGameUI);
        }
    }
}
