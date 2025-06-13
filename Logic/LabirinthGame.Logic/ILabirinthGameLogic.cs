using LabirinthGame.Logic.Levels;
using LabirinthGame.Logic.Levels.GameField;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic
{
    public interface ILabirinthGameLogic
    {
        IReadOnlyLabirinthGameField GetReadOnlyGameField();

        void ApplyAction(IGamePlayAction input, Action setWinStateAction, Action setLoseStateAction, Action setMenuStateAction);

        bool TryMovePlayerUp(IGamePlayGameStateManager gamePlayGameStateManager);

        bool TryMovePlayerDown(IGamePlayGameStateManager gamePlayGameStateManager);

        bool TryMovePlayerLeft(IGamePlayGameStateManager gamePlayGameStateManager);

        bool TryMovePlayerRight(IGamePlayGameStateManager gamePlayGameStateManager);
    }
}
