namespace LabirinthGame.Common
{
    public static class CharToIntParser
    {
        public static bool TryParse(char ch, out int result)
        {
            if (!char.IsDigit(ch))
            {
                result = default;
                return false;
            }

            result = ch - '0';
            return true;
        }
    }
}
