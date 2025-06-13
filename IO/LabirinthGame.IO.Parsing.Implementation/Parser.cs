using LabirinthGame.IO.FileSystem;
using LabirinthGame.IO.Parsing.Implementation.ParserData;

namespace LabirinthGame.IO.Parsing.Implementation
{
    internal sealed class Parser<TParserResult> : IParser<TParserResult> where TParserResult : IParserData
    {
        private readonly Func<ILevelInfoFileLinesProvider, TParserResult> _parsingFunc;

        private Parser(Func<ILevelInfoFileLinesProvider, TParserResult> parsingFunc)
        {
            _parsingFunc = parsingFunc;
        }

        public static IParser<EmptyData> Create()
        {
            return new Parser<EmptyData>((_) => new EmptyData());
        }

        public IParser<IParserDataTree<TParserResult, TPartResult>> AddPart<TPartResult>(Func<TParserResult, ILevelInfoFileLinesProvider, TPartResult> parserPart) where TPartResult : IParserData
        {
            return new Parser<IParserDataTree<TParserResult, TPartResult>>((p) =>
            {
                var oldParserResult = _parsingFunc(p);
                return new ParserDataTree<TParserResult, TPartResult>(oldParserResult, parserPart(oldParserResult, p));
            });
        }

        public IParser<IParserDataTree<TParserResult, IParserDataList<TPartResult>>> AddRepeatingPart<TPartResult>(Func<TParserResult, int> repetitionsNumberFunc, Func<TParserResult, ILevelInfoFileLinesProvider, TPartResult> parserPart) where TPartResult : IParserData
        {
            return new Parser<IParserDataTree<TParserResult, IParserDataList<TPartResult>>>((p) =>
            {
                var oldParserResult = _parsingFunc(p);

                var result = new ParserDataList<TPartResult>();

                var partNumber = repetitionsNumberFunc(oldParserResult);

                for (int i = 0; i < partNumber; i++)
                {
                    result.Add(parserPart(oldParserResult, p));
                }

                return new ParserDataTree<TParserResult, IParserDataList<TPartResult>>(oldParserResult, result);
            });
        }

        public IParser<IParserDataTree<TParserResult, TPartResult>> AddComplexPart<TPartResult>(IParser<TPartResult> parserPart) where TPartResult : IParserData
        {
            return new Parser<IParserDataTree<TParserResult, TPartResult>>((p) =>
            {
                var oldParserResult = _parsingFunc(p);
                return new ParserDataTree<TParserResult, TPartResult>(oldParserResult, parserPart.Parse(p));
            });
        }

        public IParser<IParserDataTree<TParserResult, IParserDataList<TPartResult>>> AddRepeatingComplexPart<TPartResult>(Func<TParserResult, int> repetitionsNumberFunc, IParser<TPartResult> parserPart) where TPartResult : IParserData
        {
            return new Parser<IParserDataTree<TParserResult, IParserDataList<TPartResult>>>((p) =>
            {

                var oldParserResult = _parsingFunc(p);

                var result = new ParserDataList<TPartResult>();

                var partNumber = repetitionsNumberFunc(oldParserResult);

                for (int i = 0; i < partNumber; i++)
                {
                    result.Add(parserPart.Parse(p));
                }

                return new ParserDataTree<TParserResult, IParserDataList<TPartResult>>(oldParserResult, result);
            });

        }

        public IParser<TJoinedType> Join<TJoinedType>(Func<TParserResult, TJoinedType> joiningFunc) where TJoinedType : IParserData
        {
            return new Parser<TJoinedType>((p) =>
            {
                var oldParserResult = _parsingFunc(p);
                return joiningFunc(oldParserResult);
            });
        }

        public TParserResult Parse(ILevelInfoFileLinesProvider fileLinesProvider)
        {
            return _parsingFunc(fileLinesProvider);
        }
    }
}
