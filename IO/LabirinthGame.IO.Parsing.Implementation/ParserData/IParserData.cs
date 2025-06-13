namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal interface IParserData
    {

    }

    internal interface IParserData<TData> : IParserData
    {
        TData Data { get; }
    }
}
