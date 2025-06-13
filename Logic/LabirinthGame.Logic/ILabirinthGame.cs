using LabirinthGame.Logic.GameStates;

namespace LabirinthGame.Logic
{
    public interface ILabirinthGame
    {
        public ILabirinthGameState State { get; set; }

        public bool IsRunning { get; set; }

        public string GetDisplayingInfo();

        public void ProcessInput();
    }
}
