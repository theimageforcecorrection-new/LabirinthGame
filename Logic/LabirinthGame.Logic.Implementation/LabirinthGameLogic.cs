using LabirinthGame.Logic.Levels.GameField;
using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.GamePlayActions;
using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Implementation
{
    public sealed class LabirinthGameLogic : ILabirinthGameLogic
    {
        private readonly ILabirinthGameField _gameField;

        public LabirinthGameLogic(ILabirinthGameField field)
        {
            _gameField = field;
        }

        public IReadOnlyLabirinthGameField GetReadOnlyGameField()
        {
            return _gameField;
        }

        public void ApplyAction(IGamePlayAction input, Action setWinStateAction, Action setLoseStateAction, Action setMenuStateAction)
        {
            input.Invoke(this, new GamePlayGameStateManager(setWinStateAction, setLoseStateAction, setMenuStateAction));
        }

        public bool TryMovePlayerUp(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return _gameField.TryMovePlayerUp((field, newX, newY) => CheckMove(field, newX, newY, gamePlayGameStateManager));
        }

        public bool TryMovePlayerDown(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return _gameField.TryMovePlayerDown((field, newX, newY) => CheckMove(field, newX, newY, gamePlayGameStateManager));
        }

        public bool TryMovePlayerLeft(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return _gameField.TryMovePlayerLeft((field, newX, newY) => CheckMove(field, newX, newY, gamePlayGameStateManager));
        }

        public bool TryMovePlayerRight(IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return _gameField.TryMovePlayerRight((field, newX, newY) => CheckMove(field, newX, newY, gamePlayGameStateManager));
        }

        private bool CheckMove(IGameFieldElement[,] field, int newX, int newY, IGamePlayGameStateManager gamePlayGameStateManager)
        {
            return field[newX, newY].CheckMoveOrFinishLevel(gamePlayGameStateManager);
        }
    }
}
