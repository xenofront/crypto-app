namespace CryptoApi.Interfaces;

public interface IBotCommand
{
    void Execute(Update update, TelegramBotClient telegramClient);
}