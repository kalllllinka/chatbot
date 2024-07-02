using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class Program
{
    private static TelegramBotClient botClient = null!;
    private static Commands commands = null!; // Объявляем переменную commands

    public static void Main(string[] args)
    {
        botClient = new TelegramBotClient("6975877753:AAGoDO6RH_Xxoz9GTsXazoFln8GNPLQgGGM");

        commands = new Commands(botClient); // Создаем экземпляр Commands

        using var cts = new CancellationTokenSource();
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token
        );

        Console.WriteLine("Бот запущен. Нажмите любую клавишу для остановки.");
        Console.ReadKey();

        cts.Cancel();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message)
            return;

        var message = update.Message;
        if (message == null || message.Type != MessageType.Text)
            return;

        await commands.ProcessCommand(update, cancellationToken); // Вызываем обработку команд из Commands
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error occurred: {exception.Message}");
        return Task.CompletedTask;
    }
}
