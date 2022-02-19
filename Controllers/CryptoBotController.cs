namespace CryptoApi.Controllers;

[ApiController]
[Route("bot")]
public class CryptoBotController : ControllerBase
{
    private readonly ICommandFactory _commandFactory;

    public CryptoBotController(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }

    [HttpPost]
    public void Post([FromBody] Update update)
    {
        var _config = Appsettings.OptionsMonitor;

        string token = _config.CurrentValue.TelegramToken;
        long myId = _config.CurrentValue.MyId;

        if (update.Message.Chat.Id != myId)
            return;

        TelegramBotClient telegramClient = new(token);

        _commandFactory.CreateCommand(update).Execute(update, telegramClient);
    }


    [HttpGet]
    [Route("test")]
    public string Test()
    {
        return "Api works";
    }
}