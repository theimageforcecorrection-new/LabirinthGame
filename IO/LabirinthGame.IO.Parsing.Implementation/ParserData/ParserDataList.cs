using System.Collections;

namespace LabirinthGame.IO.Parsing.Implementation.ParserData
{
    internal sealed class ParserDataList<TParserData> : IParserDataList<TParserData> where TParserData : IParserData
    {
        private readonly List<TParserData> _data = new List<TParserData>();

        public TParserData this[int index] { get => _data[index]; set => _data[index] = value; }

        public IReadOnlyList<TParserData> Data => _data;

        public int Count => _data.Count;

        public bool IsReadOnly => false;

        public void Add(TParserData item)
        {
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(TParserData item)
        {
            return _data.Contains(item);
        }

        public void CopyTo(TParserData[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TParserData> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int IndexOf(TParserData item)
        {
            return _data.IndexOf(item);
        }

        public void Insert(int index, TParserData item)
        {
            _data.Insert(index, item);
        }

        public bool Remove(TParserData item)
        {
            return _data.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
