using LabirinthGame.Logic.Levels.Common;
using LabirinthGame.Logic.Levels.GameFieldElement;
using LabirinthGame.Logic.Levels.GameFieldElementCreator;
using LabirinthGame.Logic.Levels.Implementation.GameFieldElement;

namespace LabirinthGame.Logic.Levels.Implementation.GameFieldElementCreator
{
    public sealed class TrapGameFieldElementCreator : IGameFieldElementCreator
    {
        public int ElementCode => GameFieldElementCodes.TrapCode;

        public IGameFieldElement Create()
        {
            return new TrapGameFieldElement();
        }
    }
}
