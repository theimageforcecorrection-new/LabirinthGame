namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal interface IParserDataList<TParserData> : IParserData, IList<TParserData> where TParserData : IParserData
    {

    }
}
