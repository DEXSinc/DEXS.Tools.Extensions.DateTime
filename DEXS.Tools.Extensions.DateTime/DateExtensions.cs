using System;
using System.Linq;
using System.Text.RegularExpressions;
using DEXS.Tools.Extensions.DateTime.Enums;

namespace DEXS.Tools.Extensions.DateTime
{
    public static class DateExtensions
    {
        public static string DefaultDirection = "+";
        private static readonly string[] ValidDirections = {"-", "+"};

        public static string GetDirection(string directionStr)
        {
            return ValidDirections.Contains(directionStr.Trim()) ? directionStr : DefaultDirection;
        }

        public static bool IsValidLookback(this string value)
        {
            try
            {
                return value.ParseLookback().Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public static MatchCollection ParseLookback(this string lookback)
        {
            // [direction]<amount><type>[[direction]<amount><type>...]
            const string regexMatchStr = "([+-]?)([0-9]+)([yYmMdDwWhHsSfF]{1})";
            var matches = Regex.Matches(lookback, regexMatchStr);
            return matches;
        }

        public static System.DateTime Calculate(this System.DateTime date, string lookback)
        {
            var result = date;
            var matches = lookback.ParseLookback();
            if (matches.Count <= 0) throw new ArgumentException("Lookback string is not valid", nameof(lookback));
            foreach (Match match in matches)
            {
                var elementDirection = GetDirection(match.Groups[1].Value.Trim());
                var elementValueStr = match.Groups[2].Value.Trim();
                var elementValueType = match.Groups[3].Value.Trim();
                if (!int.TryParse(elementValueStr, out var elementValue)) elementValue = 0;
                if (elementDirection.Equals("-"))
                    elementValue = elementValue * -1;
                result = result.ApplyOffset(elementValueType, elementValue);
            }
            return result;
        }

        public static System.DateTime ApplyOffset(this System.DateTime date, string @type, int amount)
        {
            return DateElement.Parse(type).Element.Add(date, amount);
        }

    }
}
