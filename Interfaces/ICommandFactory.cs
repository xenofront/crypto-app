namespace CryptoApi.Interfaces;

public interface ICommandFactory
{
    IBotCommand CreateCommand(Update update);
}