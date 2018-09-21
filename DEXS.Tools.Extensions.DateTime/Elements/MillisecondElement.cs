namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class MillisecondElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddMilliseconds(amount);
        }
    }
}