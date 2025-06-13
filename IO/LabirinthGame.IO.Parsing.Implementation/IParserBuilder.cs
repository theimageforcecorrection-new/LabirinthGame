using LabirinthGame.IO.Parsing.Implementation.ParserData;

namespace LabirinthGame.IO.Parsing.Implementation
{
    internal interface IParserBuilder
    {
        IParser<IParserDataList<ParserLevelInfoData>> Build();
    }
}
