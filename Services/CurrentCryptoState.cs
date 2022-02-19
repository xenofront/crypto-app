namespace CryptoApi.Services;

public class CurrentCryptoState
{
    public string Name;
    public string CurrentPrice;
    public string Investment;
    public string Difference;
    public double PercentageDifference;
#nullable enable
    public string? SymbolPrice;
#nullable enable
    public string? InitialSymbolPrice;
}