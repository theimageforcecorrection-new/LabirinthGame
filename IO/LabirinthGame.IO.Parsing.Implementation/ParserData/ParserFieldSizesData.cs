namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal sealed class ParserFieldSizesData : IParserData<(int, int)>
    {
        public ParserFieldSizesData((int, int) data)
        {
            Data = data;
        }

        public object AsObject()
        {
            return Data;
        }

        public Type DataType => typeof(int);

        public (int, int) Data { get; }
    }
}
