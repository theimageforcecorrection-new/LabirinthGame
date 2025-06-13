using LabirinthGame.Common;
using LabirinthGame.IO.Common;
using LabirinthGame.IO.FileSystem;
using LabirinthGame.IO.Parsing.Implementation.ParserData;

namespace LabirinthGame.IO.Parsing.Implementation
{
    internal sealed class ParserBuilder : IParserBuilder
    {
        public IParser<IParserDataList<ParserLevelInfoData>> Build()
        {
            IParser<EmptyData> parser = Parser<EmptyData>.Create();

            var parser2 = AddLevelNumberParsingStep(parser);

            var parser3 = AddLevelInfosComplexParsingStep(parser2);

            return parser3;
        }

        private IParser<IParserDataTree<EmptyData, ParserIntData>> AddLevelNumberParsingStep(IParser<EmptyData> oldParser)
        {
            var newParser = oldParser.AddPart((_, p) =>
            {
                var levelNumberHeaderString = p.GetNextLine();

                if (!int.TryParse(levelNumberHeaderString, out var levelNumber))
                {
                    throw new LevelFieldParsingException();
                }

                return new ParserIntData(levelNumber);
            });

            return newParser;
        }

        private IParser<IParserDataList<ParserLevelInfoData>> AddLevelInfosComplexParsingStep(IParser<IParserDataTree<EmptyData, ParserIntData>> oldParser)
        {
            var parserComplexPart = CreateLevelInfoDataParserComplexPart();

            var newParser = oldParser.AddRepeatingComplexPart(d => d.Right.Data, parserComplexPart);
            return newParser.Join(d => d.Right);
        }

        private IParser<ParserLevelInfoData> CreateLevelInfoDataParserComplexPart()
        {
            IParser<EmptyData> parserComplexPart1 = Parser<EmptyData>.Create();

            var parserComplexPart2 = AddFieldXAndYSizesParsingStep(parserComplexPart1);

            var parserComplexPart3 = AddPlayerXAndYCoordinatesParsingStep(parserComplexPart2);

            var parserComplexPart4 = AddLevelFieldParsingStep(parserComplexPart3);

            var parserComplexPart5 = JoinData(parserComplexPart4);

            return parserComplexPart5;
        }

        private IParser<IParserDataTree<EmptyData, ParserFieldSizesData>> AddFieldXAndYSizesParsingStep(IParser<EmptyData> oldParser)
        {
            var newParser = oldParser.AddPart((_, p) => ParseXAndYCoordinatesLine(p));

            return newParser;
        }

        private IParser<IParserDataTree<IParserDataTree<EmptyData, ParserFieldSizesData>, ParserFieldSizesData>> AddPlayerXAndYCoordinatesParsingStep(IParser<IParserDataTree<EmptyData, ParserFieldSizesData>> oldParser)
        {
            var newParser = oldParser.AddPart((_, p) => ParseXAndYCoordinatesLine(p));

            return newParser;
        }

        private ParserFieldSizesData ParseXAndYCoordinatesLine(ILevelInfoFileLinesProvider fileLinesProvider)
        {
            var levelSizesHeaderString = fileLinesProvider.GetNextLine();
            var sizes = levelSizesHeaderString.Split(' ');
            if (sizes.Length != 2)
            {
                throw new LevelFieldParsingException();
            }

            var xSizeString = sizes[0];
            var ySizeString = sizes[1];

            if (!int.TryParse(xSizeString, out var xSize))
            {
                throw new LevelFieldParsingException();

            }

            if (!int.TryParse(ySizeString, out var ySize))
            {
                throw new LevelFieldParsingException();
            }

            return new ParserFieldSizesData((xSize, ySize));

        }

        private IParser<IParserDataTree<IParserDataTree<IParserDataTree<EmptyData, ParserFieldSizesData>, ParserFieldSizesData>, IParserDataList<ParserIntArrayData>>>
            AddLevelFieldParsingStep(IParser<IParserDataTree<IParserDataTree<EmptyData, ParserFieldSizesData>, ParserFieldSizesData>> oldParser)
        {
            var newParser = oldParser.AddRepeatingPart(
                (d) =>
                {
                    return d.Left.Right.Data.Item2;
                },
                (d, p) =>
                {
                    var xSize = d.Left.Right.Data.Item1;
                    var levelFieldLine = new int[xSize];

                    var levelFieldString = p.GetNextLine();

                    if (levelFieldString.Length != xSize)
                    {
                        throw new LevelFieldParsingException();
                    }

                    for (int x = 0; x < xSize; x++)
                    {
                        if (!CharToIntParser.TryParse(levelFieldString[x], out var levelFieldElement))
                        {
                            throw new LevelFieldParsingException();
                        }

                        levelFieldLine[x] = levelFieldElement;
                    }

                    return new ParserIntArrayData(levelFieldLine);
                });

            return newParser;
        }

        private IParser<ParserLevelInfoData> JoinData(IParser<IParserDataTree<IParserDataTree<IParserDataTree<EmptyData, ParserFieldSizesData>, ParserFieldSizesData>, IParserDataList<ParserIntArrayData>>> oldParser)
        {
            return oldParser.Join(p =>
            {
                var dataList = p.Right;

                var xSize = p.Left.Left.Right.Data.Item1;
                var ySize = p.Left.Left.Right.Data.Item2;

                var playerX = p.Left.Right.Data.Item1;
                var playerY = p.Left.Right.Data.Item2;

                if (xSize != dataList.Count)
                {
                    throw new LevelFieldParsingException();
                }

                if (ySize != dataList[0].Data.Length)
                {
                    throw new LevelFieldParsingException();
                }

                var field = new int[xSize, ySize];

                for (int y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        var fieldElement = dataList[y].Data[x];
                        field[x, y] = fieldElement;
                    }
                }

                return new ParserLevelInfoData((playerX, playerY, field));
            });
        }
    }
}
