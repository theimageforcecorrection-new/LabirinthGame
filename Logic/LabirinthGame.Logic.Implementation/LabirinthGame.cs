using LabirinthGame.Logic.GameStates;

namespace LabirinthGame.Logic.Implementation
{
    public sealed class LabirinthGame : ILabirinthGame
    {
        public LabirinthGame(ILabirinthGameState state)
        {
            State = state;
            IsRunning = true;
        }

        public ILabirinthGameState State { get; set; }

        public bool IsRunning { get; set; }

        public string GetDisplayingInfo()
        {
            return State.GetDisplayingInfo(this);
        }

        public void ProcessInput()
        {
            State.ProcessInput(this);
        }
    }
}
