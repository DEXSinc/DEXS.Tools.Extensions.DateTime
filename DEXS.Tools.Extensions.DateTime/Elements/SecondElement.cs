namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class SecondElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date.AddSeconds(amount);
        }
    }
}