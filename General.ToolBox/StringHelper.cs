using System.Text.RegularExpressions;

namespace General.ToolBox
{
    /// <summary>
    /// Provide Tools to work with strings
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Check if <paramref name="value"/> is Alphanumeric
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAlphaNum(string value)
        {
            Regex r = new Regex("[a-zA-Z0-9]+$");

            if (value is null || !r.IsMatch(value) )
                return false;
            return true;
        }
    }
}
