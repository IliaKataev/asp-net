namespace BlazorApp2.Service
{
    public class CurrencyService
    {
        private string currency = "RUB";

        private readonly Dictionary<string, decimal> exchangeRate = new()
        {
            { "RUB", 1m },
            { "USD", 77.86m },
            { "EUR", 90.84m }
        };

        public string GetCurrecy() => currency;

        public void SetCurrency(string currency)
        {
            if(exchangeRate.ContainsKey(currency))
                this.currency = currency;
        }

        public decimal ConvertPrices(decimal rubPrice)
        {
            var rate = exchangeRate[currency];
            return Math.Round(rubPrice / rate, 2);
        }
    }
}
