namespace CryptoApi.Factories;

public class CommandFactory : ICommandFactory
{

    public IBotCommand CreateCommand(Update update)
    {
        string message;

        if (update.Type == UpdateType.Message)
        {
            message = update.Message.Text.ToLower();
            return update.Type switch
            {
                _ when message == "/stats" => new CryptoStatsCmd(),
                _ => new MesseageNotFoundCmd(),
            };
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            return new MesseageNotFoundCmd();
        }

        return new MesseageNotFoundCmd();
    }
}