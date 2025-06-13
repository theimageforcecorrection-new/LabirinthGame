namespace LabirinthGame.Logic.Levels.GameFieldElement
{
    public interface IGameFieldElement
    {
        public char ElementChar { get; }

        public bool CheckMoveOrFinishLevel(IGamePlayGameStateManager gamePlayGameStateManager);
    }
}
