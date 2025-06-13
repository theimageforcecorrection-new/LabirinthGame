using LabirinthGame.Logic.Levels.Common;

namespace LabirinthGame.IO.FileSystem.Implementation
{
    public sealed class LevelInfoDefaultFileCreator : ILevelInfoDefaultFileCreator
    {
        public bool CheckIsFileExist()
        {
            return File.Exists(LevelInfoFileConstants.FileName);
        }

        public void CreateFile()
        {
            //TODO: Сделать свою обёртку над StreamWriter
            using (var writer = new StreamWriter(LevelInfoFileConstants.FileName))
            {
                const int levelNumber = 1;

                writer.WriteLine(levelNumber);

                const int xSize = 10;
                const int ySize = 10;

                writer.WriteLine($"{xSize} {ySize}");

                const int xPlayerCoordinate = 3;
                const int yPlayerCoordinate = 1;

                writer.WriteLine($"{xPlayerCoordinate} {yPlayerCoordinate}");

                var field = new int[xSize, ySize];

                field[1, 4] = GameFieldElementCodes.TrapCode;
                field[0, 4] = GameFieldElementCodes.EnemyCode;

                field[xPlayerCoordinate, yPlayerCoordinate] = GameFieldElementCodes.PlayerCode;
                field[2, 4] = GameFieldElementCodes.ExitCode;

                field[0, 5] = GameFieldElementCodes.WallCode;
                field[1, 5] = GameFieldElementCodes.WallCode;
                field[2, 5] = GameFieldElementCodes.WallCode;
                field[3, 5] = GameFieldElementCodes.WallCode;
                field[4, 5] = GameFieldElementCodes.WallCode;
                field[5, 5] = GameFieldElementCodes.WallCode;
                field[6, 5] = GameFieldElementCodes.WallCode;
                field[7, 5] = GameFieldElementCodes.WallCode;
                field[8, 5] = GameFieldElementCodes.WallCode;
                field[9, 5] = GameFieldElementCodes.WallCode;

                for (int y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        writer.Write(field[x, y]);
                    }

                    writer.WriteLine();
                }
            }
        }
    }
}
