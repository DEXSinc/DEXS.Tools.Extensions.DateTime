using System;
using System.Collections.Generic;
using System.Linq;
using DEXS.Tools.Extensions.DateTime.Elements;

namespace DEXS.Tools.Extensions.DateTime.Enums
{
    public class DateElement : EnumBaseType<DateElement>, IEnumType
    {
        public static readonly DateElement Unknown = new DateElement(0, "?", "Unknown", "Unknown", "Unknown", new[] { "?" }, new UnknownElement());
        public static readonly DateElement Milliseconds = new DateElement(1, "f", "Milliseconds", "Millisecond", "Milliseconds", new [] {"f", "F"}, new MillisecondElement());
        public static readonly DateElement Seconds = new DateElement(2, "s", "Seconds", "Second", "Seconds", new[] { "s", "S" }, new SecondElement());
        public static readonly DateElement Minutes = new DateElement(3, "m", "Minutes", "Minute", "Minutes", new[] { "m" }, new MinuteElement());
        public static readonly DateElement Hours = new DateElement(3, "H", "Hours", "Hour", "Hours", new[] { "h", "H" }, new HourElement());
        public static readonly DateElement Days = new DateElement(4, "d", "Days", "Day", "Days", new[] { "d", "D" }, new DayElement());
        public static readonly DateElement Weeks = new DateElement(5, "w", "Weeks", "Week", "Weeks", new[] { "w", "W" }, new WeekElement());
        public static readonly DateElement Months = new DateElement(6, "M", "Months", "Month", "Months", new[] { "M" }, new MonthElement());
        public static readonly DateElement Years = new DateElement(7, "y", "Years", "Year", "Years", new[] { "y", "Y" }, new YearElement());

        static DateElement()
        {
            EnumValues = new List<DateElement>
            {
                Unknown,
                Milliseconds,   // f, F
                Seconds,        // s, S
                Minutes,        // m
                Hours,          // h, H
                Days,           // d, D
                Weeks,          // w, W
                Months,         // M
                Years           // y, Y
            };
        }

        public string NameSingular { get; set; }
        public string NamePlural { get; set; }
        public string[] Values { get; }
        public IDateElement Element { get; }

        public DateElement(int key, string value, string description, string nameSingular, string namePlural, string[] values, IDateElement dateElement)
            : base(key, value, description)
        {
            NameSingular = nameSingular;
            NamePlural = namePlural;
            Values = values;
            Element = dateElement;
        }

        public static DateElement Parse(string str)
        {
            return EnumValues.FirstOrDefault(t => t.Values.Contains(str, StringComparer.CurrentCulture)) ?? Unknown;
        }

    }
}