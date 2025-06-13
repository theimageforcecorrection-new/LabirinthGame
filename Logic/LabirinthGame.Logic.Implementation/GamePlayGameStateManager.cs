using LabirinthGame.Logic.Levels;

namespace LabirinthGame.Logic.Implementation
{
    public sealed class GamePlayGameStateManager : IGamePlayGameStateManager
    {
        private readonly Action _setWinState;
        private readonly Action _setLoseState;
        private readonly Action _setMenuState;

        public GamePlayGameStateManager(Action setWinState, Action setLoseState, Action setMenuState)
        {
            _setWinState = setWinState;
            _setLoseState = setLoseState;
            _setMenuState = setMenuState;
        }

        public void SetWinState()
        {
            _setWinState();
        }

        public void SetLoseState()
        {
            _setLoseState();
        }

        public void SetMenuState()
        {
            _setMenuState();
        }
    }
}
