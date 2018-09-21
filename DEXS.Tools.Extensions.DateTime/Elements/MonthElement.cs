namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class MonthElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddMonths(amount);
        }
    }
}