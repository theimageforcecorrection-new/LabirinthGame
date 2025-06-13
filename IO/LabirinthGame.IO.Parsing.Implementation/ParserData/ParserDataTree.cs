namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal sealed class ParserDataTree<TLeftData, TRightData> : IParserDataTree<TLeftData, TRightData>
        where TLeftData : IParserData
        where TRightData : IParserData
    {
        public ParserDataTree(TLeftData left, TRightData right)
        {
            Left = left;
            Right = right;
        }

        public TLeftData Left { get; }

        public TRightData Right { get; }
    }
}
