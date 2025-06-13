using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Logic
{
    public interface IGamePlayActionProvider
    {
        IGamePlayAction Provide(string actionString);
    }
}
