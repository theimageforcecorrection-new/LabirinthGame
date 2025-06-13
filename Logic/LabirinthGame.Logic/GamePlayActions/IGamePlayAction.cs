using LabirinthGame.Logic.Levels;

namespace LabirinthGame.Logic.GamePlayActions
{
    public interface IGamePlayAction
    {
        string ActionString { get; }

        void Invoke(ILabirinthGameLogic gameLogic, IGamePlayGameStateManager gameStateManager);
    }
}
