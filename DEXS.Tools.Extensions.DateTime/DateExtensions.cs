using System;
using System.Linq;
using System.Text.RegularExpressions;
using DEXS.Tools.Extensions.DateTime.Enums;

namespace DEXS.Tools.Extensions.DateTime
{
    public static class DateExtensions
    {
        public static string DefaultDirection = "-";
        public static string[] ValidDirections = {"-", "+"};

        public static string GetDirection(string directionStr)
        {
            return ValidDirections.Contains(directionStr.Trim()) ? directionStr : DefaultDirection;
        }

        public static System.DateTime Calculate(this System.DateTime date, string lookbackstr)
        {
            var result = date;
            // [direction]<amount><type>[[direction]<amount><type>...]
            const string regexMatchStr = "([+-]?)([0-9]+)([yYmMdDwWhHsSfF]{1})";
            var matches = Regex.Matches(lookbackstr, regexMatchStr);

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
