namespace LabirinthGame.Logic.GameStates
{
    public interface ILabirinthGameState
    {
        string GetDisplayingInfo(ILabirinthGame game);

        public void ProcessInput(ILabirinthGame game);
    }
}
