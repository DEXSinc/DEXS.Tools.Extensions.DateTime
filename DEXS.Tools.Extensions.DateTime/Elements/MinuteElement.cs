using System;

namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class MinuteElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddMinutes(amount);
        }
    }
}