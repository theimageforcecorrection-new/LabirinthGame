using LabirinthGame.IO.FileSystem;
using LabirinthGame.IO.Parsing.Implementation.ParserData;

namespace LabirinthGame.IO.Parsing.Implementation
{
    internal interface IParser<TParserResult> where TParserResult : IParserData
    {
        IParser<IParserDataTree<TParserResult, TPartResult>> AddPart<TPartResult>(Func<TParserResult, ILevelInfoFileLinesProvider, TPartResult> parserPart) where TPartResult : IParserData;

        IParser<IParserDataTree<TParserResult, IParserDataList<TPartResult>>> AddRepeatingPart<TPartResult>(Func<TParserResult, int> repetitionsNumberFunc, Func<TParserResult, ILevelInfoFileLinesProvider, TPartResult> parserPart) where TPartResult : IParserData;

        public IParser<IParserDataTree<TParserResult, TPartResult>> AddComplexPart<TPartResult>(IParser<TPartResult> parserPart) where TPartResult : IParserData;

        public IParser<IParserDataTree<TParserResult, IParserDataList<TPartResult>>> AddRepeatingComplexPart<TPartResult>(Func<TParserResult, int> repetitionsNumberFunc, IParser<TPartResult> parserPart) where TPartResult : IParserData;

        IParser<TJoinedType> Join<TJoinedType>(Func<TParserResult, TJoinedType> joiningFunc) where TJoinedType : IParserData;

        TParserResult Parse(ILevelInfoFileLinesProvider fileLinesProvider);
    }
}
