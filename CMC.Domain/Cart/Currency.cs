namespace CMC.Domain.Cart
{
    public class Currency
    {
        public int CurrencyId { get; internal set; }
        public string Name { get; internal set; }
        public string Country { get; internal set; }
        public double CurrencyToAudRatio { get; internal set; }
        public double AudToCurrencyRatio => 1 / CurrencyToAudRatio;

        internal Currency() { }

        public static Currency Create(int currencyId, string name, string country, double audRatio)
        {
            return new Currency
            {
                CurrencyId = currencyId,
                Name = name,
                Country = country,
                CurrencyToAudRatio = audRatio
            };
        }
    }
}
