namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal sealed class ParserIntData : IParserData<int>
    {
        public ParserIntData(int data)
        {
            Data = data;
        }

        public object AsObject()
        {
            return Data;
        }

        public int Data { get; }
    }
}
