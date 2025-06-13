using LabirinthGame.Logic;
using LabirinthGame.Logic.ConsoleUi;
using LabirinthGame.Logic.GamePlayActions;
using LabirinthGame.Logic.Levels.Implementation.GameFieldElement;

namespace LabirinthGame.Ui.TextUi.Implementation
{
    public sealed class LabirinthGamePlayIO : ILabirinthGamePlayIO
    {
        private readonly IGamePlayActionProvider _gamePlayActionProvider;
        private readonly IConsoleGameUI _consoleGameUI;

        public LabirinthGamePlayIO(IGamePlayActionProvider gamePlayActionProvider, IConsoleGameUI consoleGameUI)
        {
            _gamePlayActionProvider = gamePlayActionProvider;
            _consoleGameUI = consoleGameUI;
        }

        public void ShowGameField(ILabirinthGameLogic gameLogic)
        {

            var field = gameLogic.GetReadOnlyGameField();

            for (int i = 0; i < 60; i++)
            {
                _consoleGameUI.WriteLine();
            }

            for (int i = 0; i < field.MaxX + 2; i++)
            {
                //TODO: Переделать здесь, чтобы при прочтении из файла игровое поле инициализировалось дополнительными элементами стены
                _consoleGameUI.Write(new WallGameFieldElement().ElementChar);
            }

            _consoleGameUI.WriteLine();


            for (int i = 0; i < field.MaxY; i++)
            {
                _consoleGameUI.Write(new WallGameFieldElement().ElementChar);

                for (int j = 0; j < field.MaxX; j++)
                {
                    _consoleGameUI.Write(field[j, i].ElementChar);

                }
                _consoleGameUI.Write(new WallGameFieldElement().ElementChar);

                _consoleGameUI.WriteLine();
            }

            for (int i = 0; i < field.MaxX + 2; i++)
            {
                _consoleGameUI.Write(new WallGameFieldElement().ElementChar);
            }

            _consoleGameUI.WriteLine();
        }


        public IGamePlayAction ReadNextAction()
        {
            var input = _consoleGameUI.ReadLine();

            return _gamePlayActionProvider.Provide(input);
        }
    }
}
