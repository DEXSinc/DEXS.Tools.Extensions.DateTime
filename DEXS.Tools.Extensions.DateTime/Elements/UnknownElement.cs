namespace DEXS.Tools.Extensions.DateTime.Elements
{
    public class UnknownElement : IDateElement
    {
        public System.DateTime Add(System.DateTime date, int amount)
        {
            return date;
        }
    }
}