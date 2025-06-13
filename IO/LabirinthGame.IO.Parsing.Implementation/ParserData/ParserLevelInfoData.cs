namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal sealed class ParserLevelInfoData : IParserData<(int, int, int[,])>
    {
        public ParserLevelInfoData((int, int, int[,]) data)
        {
            Data = data;
        }

        public object AsObject()
        {
            return Data;
        }

        public Type DataType => typeof((int, int, int[,]));

        public (int, int, int[,]) Data { get; }
    }
}
