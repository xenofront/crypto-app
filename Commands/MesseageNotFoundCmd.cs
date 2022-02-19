namespace CryptoApi.Commands;

public class MesseageNotFoundCmd : IBotCommand
{
    public async void Execute(Update update, TelegramBotClient telegramClient)
    {
        long chatId = update.Message.Chat.Id;
        Message message = update.Message;

        await telegramClient.SendTextMessageAsync(chatId, $"{message.Chat.FirstName} Message not found");
    }
}