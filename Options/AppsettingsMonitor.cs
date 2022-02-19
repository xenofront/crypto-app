namespace CryptoApi.Options;

public class AppsettingsMonitor
{
    public long MyId { get; set; }
    public string TelegramToken { get; set; }
    public string Ids { get; set; }
    public List<Crypto> Coins { get; set; }
    public string CoinGeckoUri { get; set; }
}
