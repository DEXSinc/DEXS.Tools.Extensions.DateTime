using System;
using System.Collections.Generic;
using System.Linq;
using DEXS.Tools.Extensions.DateTime.Elements;

namespace DEXS.Tools.Extensions.DateTime.Enums
{
    public class DateElement : EnumBaseType<DateElement>, IEnumType
    {
        public static readonly DateElement Unknown = new DateElement(0, "?", "Unknown", new[] { "?" }, new UnknownElement());
        public static readonly DateElement MilliSeconds = new DateElement(1, "f", "MilliSeconds", new [] {"f", "F"}, new MillisecondElement());
        public static readonly DateElement Seconds = new DateElement(2, "s", "Seconds", new[] { "s", "S" }, new SecondElement());
        public static readonly DateElement Minutes = new DateElement(3, "m", "Minutes", new[] { "m" }, new MinuteElement());
        public static readonly DateElement Hours = new DateElement(3, "H", "Hours", new[] { "h", "H" }, new HourElement());
        public static readonly DateElement Days = new DateElement(4, "d", "Days", new[] { "d", "D" }, new DayElement());
        public static readonly DateElement Weeks = new DateElement(5, "w", "Weeks", new[] { "w", "W" }, new WeekElement());
        public static readonly DateElement Months = new DateElement(6, "M", "Months", new[] { "M" }, new MonthElement());
        public static readonly DateElement Years = new DateElement(7, "y", "Years", new[] { "y", "Y" }, new YearElement());

        static DateElement()
        {
            EnumValues = new List<DateElement>
            {
                Unknown,
                MilliSeconds,
                Seconds,
                Minutes,
                Hours,
                Days,
                Weeks,
                Months,
                Years
            };
        }

        public string[] Values { get; }
        public IDateElement Element { get; }

        public DateElement(int key, string value, string description, string[] values, IDateElement dateElement)
            : base(key, value, description)
        {
            Values = values;
            Element = dateElement;
        }

        public static DateElement Parse(string elementStr)
        {
            return EnumValues.FirstOrDefault(t => t.Values.Contains(elementStr, StringComparer.OrdinalIgnoreCase)) ?? Unknown;
        }

    }
}