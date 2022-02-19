namespace CryptoApi.Commands;

public class CryptoStatsCmd : IBotCommand
{
    public async void Execute(Update update, TelegramBotClient telegramClient)
    {
        long chatId = update.Message.Chat.Id;

        List<CurrentCryptoState> result = await new CoinGecko().Get();

        string html = "";
        foreach (CurrentCryptoState res in result)
        {
            html += $@"{char.ConvertFromUtf32(0x1F449)} <b>{res.Name}</b>
CS {res.CurrentPrice}
II {res.Investment}{Environment.NewLine}";

            if (res?.SymbolPrice != null && res?.InitialSymbolPrice != null)
            {
                html += @$"CP {res.SymbolPrice}
IP {res.InitialSymbolPrice}{Environment.NewLine}";
            }
            html += @$"D {res.Difference}
D% {res.PercentageDifference}{Environment.NewLine}{Environment.NewLine}";
        }


        await telegramClient.SendTextMessageAsync(chatId, html, parseMode: ParseMode.Html);
    }
}