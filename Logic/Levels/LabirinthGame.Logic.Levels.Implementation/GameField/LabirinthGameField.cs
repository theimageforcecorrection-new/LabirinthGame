using LabirinthGame.Logic.Levels.GameField;
using LabirinthGame.Logic.Levels.GameFieldElement;
using LabirinthGame.Logic.Levels.Implementation.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameField
{
    public sealed class LabirinthGameField : ILabirinthGameField
    {
        private readonly IGameFieldElement[,] _field;

        private int _playerX, _playerY;

        public LabirinthGameField(IGameFieldElement[,] field, int playerX, int playerY)
        {
            _field = field;
            _playerX = playerX;
            _playerY = playerY;
        }

        public int MaxX => _field.GetLength(0);

        public int MaxY => _field.GetLength(1);

        public IGameFieldElement this[int x, int y] { get { return _field[x, y]; } private set { _field[x, y] = value; } }

        public bool TryMovePlayerUp(Func<IGameFieldElement[,], int, int, bool> moveCheck)
        {
            if (_playerY <= 0)
            {
                return false;
            }

            return TryMovePlayer(_playerX, _playerY, _playerX, _playerY - 1, moveCheck);
        }

        public bool TryMovePlayerDown(Func<IGameFieldElement[,], int, int, bool> moveCheck)
        {
            if (_playerY >= _field.GetLength(1) - 1)
            {
                return false;
            }

            return TryMovePlayer(_playerX, _playerY, _playerX, _playerY + 1, moveCheck);
        }


        public bool TryMovePlayerLeft(Func<IGameFieldElement[,], int, int, bool> moveCheck)
        {
            if (_playerX <= 0)
            {
                return false;
            }

            return TryMovePlayer(_playerX, _playerY, _playerX - 1, _playerY, moveCheck);
        }

        public bool TryMovePlayerRight(Func<IGameFieldElement[,], int, int, bool> moveCheck)
        {
            if (_playerX >= _field.GetLength(0) - 1)
            {
                return false;
            }

            return TryMovePlayer(_playerX, _playerY, _playerX + 1, _playerY, moveCheck);
        }

        private bool TryMovePlayer(int oldX, int oldY, int newX, int newY, Func<IGameFieldElement[,], int, int, bool> moveCheck)
        {
            if (moveCheck(_field, newX, newY))
            {
                _field[oldX, oldY] = new EmptyGameFieldElement();
                _field[newX, newY] = new PlayerGameFieldElement();

                _playerX = newX;
                _playerY = newY;

                return true;
            }

            return false;
        }
    }
}
