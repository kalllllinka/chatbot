using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class Commands
{
    private readonly ITelegramBotClient botClient;

    public Commands(ITelegramBotClient botClient)
    {
        this.botClient = botClient;
    }

    public async Task ProcessCommand(Update update, CancellationToken cancellationToken)
    {
        if (update.Message == null)
            return;

        var message = update.Message;

        // Проверяем, является ли сообщение текстовым
        if (message.Type != MessageType.Text)
            return;

        string response = string.Empty;

        switch (message.Text.ToLower())
        {
            // Кейсы обработки команд
            case "/start":
            case "/restart":
                response = $"Прывітанне, {message.From.FirstName} 😊";
                response += @"У меню знаходзіцца спіс каманд, якія могуць табе дапамагчы больш даведацца пра мяне і пачаць падрыхтоўку.";
                break;
            case "/developer":
                response = "Можаце звязацца з распрацоўшчыкам 🫠 https://t.me/kalllllinka";
                break;
            case "/information":
                response = "Чат-бот для падрыхтоўкі да ЦТ/ЦЭ па беларускай мове ўяўляе сабой аўтаматызаваную сістэму...";
                break;
            case "/purpose":
                response = "Мэта бота-дапамагчы ў падрыхтоўцы да ЦТ / ЦЭ па беларускай мове з дапамогай заданняў са зборніка а таксама дадатковых заданняў з падручніку \"тэсты\" па беларускай мове. Да часткі А  і раздзелаў есць правілы, якія могуць дапамагчыюПоспехаў у падрыхтоўцы 😉";
                break;
            case "/testing":
                await Testing.Start(botClient, message.Chat.Id, cancellationToken);
                break;
            case "/preparation":
                await Preparation.Start(botClient, message.Chat.Id, cancellationToken);
                break;
            case "/section":
                await Section.Start(botClient, message.Chat.Id, cancellationToken);
                break;
            case "/rules":
                await Rules.Start(botClient, message.Chat.Id, cancellationToken);
                break;
        }

        if (!string.IsNullOrEmpty(response))
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, response, cancellationToken: cancellationToken);
        }
    }

}