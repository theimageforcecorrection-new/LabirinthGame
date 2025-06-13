namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal sealed class ParserIntArrayData : IParserData<int[]>
    {
        public ParserIntArrayData(int[] data)
        {
            Data = data;
        }

        public object AsObject()
        {
            return Data;
        }

        public Type DataType => typeof(int[]);

        public int[] Data { get; }
    }
}
