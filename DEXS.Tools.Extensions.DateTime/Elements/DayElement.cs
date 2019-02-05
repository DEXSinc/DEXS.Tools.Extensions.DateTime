using System;

namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class DayElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddDays(amount);
        }
    }
}