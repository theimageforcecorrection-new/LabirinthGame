using LabirinthGame.Logic.Levels.GameFieldElementCreator;

namespace LabirinthGame.Logic.Levels
{
    public interface IGameFieldElementCreatorProvider
    {
        IGameFieldElementCreator Provide(int elementCode);
    }
}
