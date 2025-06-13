using LabirinthGame.Logic;
using LabirinthGame.Logic.GamePlayActions;

namespace LabirinthGame.Ui.TextUi
{
    public interface ILabirinthGamePlayIO
    {
        void ShowGameField(ILabirinthGameLogic gameLogic);

        IGamePlayAction ReadNextAction();
    }
}
