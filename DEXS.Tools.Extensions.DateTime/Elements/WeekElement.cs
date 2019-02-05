using System;

namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class WeekElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return amount == 0 ? date : date.AddDays(amount*7);
        }
    }
}