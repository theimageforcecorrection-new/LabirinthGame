using LabirinthGame.Logic.GameStates;

namespace LabirinthGame.Logic.Implementation.GameStates
{
    public sealed class ExitLabirinthGameState : ILabirinthGameState
    {
        public string GetDisplayingInfo(ILabirinthGame game)
        {
            game.IsRunning = false;

            return string.Empty;
        }

        public void ProcessInput(ILabirinthGame game)
        {

        }
    }
}
