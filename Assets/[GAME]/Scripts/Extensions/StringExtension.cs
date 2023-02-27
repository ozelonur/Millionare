using System.Text.RegularExpressions;

namespace _GAME_.Scripts.Extensions
{
    public static class StringExtension
    {
        private static readonly Regex Regex = new Regex(@"\d+");    
        public static int GetNumberInString(this string value)
        {        
            Match match = Regex.Match(value);
            
            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            else
            {
                return -1;
            }
        }

        public static string MoneyWithComma(this int value)
        {
            return value.ToString("N");
        }
    }
}