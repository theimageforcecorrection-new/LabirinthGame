using LabirinthGame.Logic.Levels.GameFieldElement;

namespace LabirinthGame.Logic.Levels.GameFieldElementCreator
{
    public interface IGameFieldElementCreator
    {
        public int ElementCode { get; }

        public IGameFieldElement Create();
    }
}
