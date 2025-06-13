namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal interface IParserDataTree<TLeftData, TRightData> : IParserData
        where TLeftData : IParserData
        where TRightData : IParserData
    {
        TLeftData Left { get; }

        TRightData Right { get; }
    }
}
