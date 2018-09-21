namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class HourElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddHours(amount);
        }
    }
}