using LabirinthGame.Common;
using LabirinthGame.IO.FileSystem;
using LabirinthGame.IO.Parsing.Implementation.ParserData;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LabirinthGame.EntryPoint.Container")]

namespace LabirinthGame.IO.Parsing.Implementation
{
    internal sealed class LevelInfosInitializer : ILevelInfosInitializer
    {
        private readonly ILevelInfoDefaultFileCreator _levelInfoDefaultFileCreator;

        private readonly ILevelInfoFileLinesProviderCreator _levelInfoFileLinesProviderCreator;

        public LevelInfosInitializer(ILevelInfoDefaultFileCreator levelInfoDefaultFileCreator, ILevelInfoFileLinesProviderCreator levelInfoFileLinesProviderCreator)
        {
            _levelInfoDefaultFileCreator = levelInfoDefaultFileCreator;
            _levelInfoFileLinesProviderCreator = levelInfoFileLinesProviderCreator;
        }

        public IReadOnlyList<(int PlayerX, int PlayerY, int[,] Field)> Initialize()
        {
            if (!_levelInfoDefaultFileCreator.CheckIsFileExist())
            {
                _levelInfoDefaultFileCreator.CreateFile();
            }
            
            var parser = BuildParser();
            return Parse(parser);
        }

        private IParser<IParserDataList<ParserLevelInfoData>> BuildParser()
        {
            var parserBuilder = new ParserBuilder();

            var parser = parserBuilder.Build();

            return parser;
        }


        private IReadOnlyList<(int PlayerX, int PlayerY, int[,] Field)> Parse(IParser<IParserDataList<ParserLevelInfoData>> parser)
        {
            using (var levelInfoFileLinesProvider = _levelInfoFileLinesProviderCreator.Create())
            {
                try
                {
                    var parsingResult = parser.Parse(levelInfoFileLinesProvider);

                    return parsingResult.Select(d => ((IParserData<(int PlayerX, int PlayerY, int[,] Field)>)d).Data).ToList();
                }
                catch (LabirinthGameException)
                {
                    return new List<(int PlayerX, int PlayerY, int[,] Field)>();
                }
            }
        }
    }
}
