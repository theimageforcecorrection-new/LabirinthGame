using LabirinthGame.Logic.Levels.Common;
using LabirinthGame.Logic.Levels.GameFieldElement;
using LabirinthGame.Logic.Levels.GameFieldElementCreator;
using LabirinthGame.Logic.Levels.Implementation.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElementCreator
{
    public sealed class EmptyGameFieldElementCreator : IGameFieldElementCreator
    {
        public int ElementCode => GameFieldElementCodes.EmptyFieldCode;

        public IGameFieldElement Create()
        {
            return new EmptyGameFieldElement();
        }
    }
}
