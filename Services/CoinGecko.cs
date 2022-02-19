namespace CryptoApi.Services;

public class CoinGecko
{
    private const string _coinGecko = "coinGecko";

    private static readonly AppsettingsMonitor _config = Appsettings.OptionsMonitor.CurrentValue;

    private static readonly IHttpClientBuilder _httpClientBuilder = new ServiceCollection()
        .AddHttpClient(_coinGecko, c =>
    {
        c.BaseAddress = new Uri(_config.CoinGeckoUri);
    });

    private static readonly IHttpClientFactory _httpClientFactory = _httpClientBuilder
        .Services
        .BuildServiceProvider()
        .GetRequiredService<IHttpClientFactory>();

    public async Task<List<CurrentCryptoState>> Get()
    {
        var coins = _config.Coins;

        List<Crypto> _assets = new();
        foreach (Crypto coin in coins)
        {
            _assets.Add(new Crypto
            {
                Name = coin.Name,
                Symbol = coin.Symbol,
                CoinSum = coin.CoinSum,
                Investment = coin.Investment
            });
        }

        string ids = _config.Ids;
        HttpClient client = _httpClientFactory.CreateClient(_coinGecko);

        try
        {
            var result = await client.GetAsync($"price?ids={ids}&vs_currencies=usd");

            var coinsDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>
                (result.Content.ReadAsStringAsync().Result);

            List<CurrentCryptoState> currentCryptoState = new();

            double sumCurrentPrice = 0;

            foreach (var coin in _assets)
            {
                string price = coinsDict[coin.Name.ToLower()]["usd"];
                double currPrice = double.Parse(price, CultureInfo.InvariantCulture) * coin.CoinSum;

                currentCryptoState.Add(new CurrentCryptoState
                {
                    Name = coin.Symbol.ToUpper(),
                    CurrentPrice = Math.Round(currPrice).ToString("N0") + " $",
                    Investment = Math.Round(coin.Investment).ToString("N0") + " $",
                    Difference = Math.Round(currPrice - coin.Investment).ToString("N0") + " $",
                    PercentageDifference = Math.Round((currPrice - coin.Investment) / coin.Investment * 100, 2),
                    SymbolPrice = double.Parse(price, CultureInfo.InvariantCulture).ToString("0.0000"),
                    InitialSymbolPrice = (coin.Investment / coin.CoinSum).ToString("0.0000")
                });

                sumCurrentPrice += currPrice;
            }

            double sumInvestment = _assets.Sum(x => x.Investment);
            currentCryptoState.Add(new CurrentCryptoState
            {
                Name = "SUMMARY",
                Investment = Math.Round(sumInvestment).ToString("N0") + " $",
                CurrentPrice = sumCurrentPrice.ToString("N0") + " $",
                Difference = Math.Round(sumCurrentPrice - _assets.Sum(x => x.Investment)).ToString("N0") + " $",
                PercentageDifference = Math.Round((sumCurrentPrice - sumInvestment) / sumInvestment * 100, 2)
            });

            return currentCryptoState;
        }
        catch (Exception)
        {
            throw;
        }
    }
}