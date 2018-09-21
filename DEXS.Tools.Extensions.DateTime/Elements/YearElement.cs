namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class YearElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddYears(amount);
        }
    }
}