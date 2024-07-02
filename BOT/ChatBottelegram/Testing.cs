using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class Testing
{
    // Состояния пользователей: количество правильных и неправильных ответов, список неправильных вопросов
    private static Dictionary<long, (int correctAnswers, int incorrectAnswers, List<int> incorrectQuestions)> userStates = new Dictionary<long, (int, int, List<int>)>();

    public static async Task Start(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        string initialMessage = "Тут ты можаш сябе праверыць";
        var initialKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("Пачні тэсціраванне", "startTesting") }
        });

        await botClient.SendTextMessageAsync(chatId, initialMessage, replyMarkup: initialKeyboard);
    }

    public static async Task HandleCallbackQuery(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {

        var chatId = callbackQuery.Message.Chat.Id;
        switch (callbackQuery.Data)
        {
            case "startTesting":
                long chatIdStart = callbackQuery.Message.Chat.Id;
                string chooseTestMessage = "Выберы нумар тэсту";
                var testKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Тэст 1", "test1") }
                });

                // Инициализация счетчиков
                userStates[chatIdStart] = (0, 0, new List<int>());

                await botClient.SendTextMessageAsync(chatIdStart, chooseTestMessage, replyMarkup: testKeyboard);
                break;

            case "test1":
                await SendTest1Question1(botClient, callbackQuery.Message.Chat.Id);
                break;

            case "test1_q1_1":
            case "test1_q1_2":
                await HandleTest1Question1(botClient, callbackQuery);
                break;

            case "test1_q2_1":
            case "test1_q2_2":
            case "test1_q2_3":
            case "test1_q2_4":
                await HandleTest1Question2(botClient, callbackQuery);
                break;

            case "test1_q3_1":
            case "test1_q3_2":
                await HandleTest1Question3(botClient, callbackQuery);
                break;

            case "test1_q4_1":
            case "test1_q4_2":
                await HandleTest1Question4(botClient, callbackQuery);
                break;

            case "test1_q5_1":
            case "test1_q5_2":
                await HandleTest1Question5(botClient, callbackQuery);
                break;
        }
    }

    private static async Task SendTest1Question1(TelegramBotClient botClient, long chatId)
    {
        string test1Question1 = @"1) Адзначце словы, у якіх на месцы пропуску трэба пісаць літару а:
1) сакр_тарка;
2) выс_каякасны;
3) гл_ток;
4) шніц_ль;
5) в_страсюжэтны.";

        var test1Keyboard1 = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4", "test1_q1_1") }, // Правильный ответ
            new[] { InlineKeyboardButton.WithCallbackData("3, 5", "test1_q1_2") }
        });

        await botClient.SendTextMessageAsync(chatId, test1Question1, replyMarkup: test1Keyboard1);
    }

    private static async Task HandleTest1Question1(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        long chatId = callbackQuery.Message.Chat.Id;
        if (callbackQuery.Data == "test1_q1_1") // Правильный ответ
        {
            userStates[chatId] = (userStates[chatId].correctAnswers + 1, userStates[chatId].incorrectAnswers, userStates[chatId].incorrectQuestions);
        }
        else
        {
            userStates[chatId] = (userStates[chatId].correctAnswers, userStates[chatId].incorrectAnswers + 1, userStates[chatId].incorrectQuestions);
            userStates[chatId].incorrectQuestions.Add(1); // Неправильный ответ на первый вопрос
        }

        string test1Question2 = @"2) Адзначце словы, у якіх на месцы пропуску пішацца літара я
1) см_шнаваты;
2) закашл_цца;
3) дз_сяты;
4) ген_ральны;
5) вым_няць.";

        var test1Keyboard2 = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("1, 4", "test1_q2_1") },
            new[] { InlineKeyboardButton.WithCallbackData("2, 3", "test1_q2_2") },
            new[] { InlineKeyboardButton.WithCallbackData("1, 5", "test1_q2_3") },
            new[] { InlineKeyboardButton.WithCallbackData("1, 4, 5", "test1_q2_4") } // Правильный ответ
        });

        await botClient.SendTextMessageAsync(chatId, test1Question2, replyMarkup: test1Keyboard2);
    }

    private static async Task HandleTest1Question2(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        long chatId = callbackQuery.Message.Chat.Id;
        if (callbackQuery.Data == "test1_q2_4") // Правильный ответ
        {
            userStates[chatId] = (userStates[chatId].correctAnswers + 1, userStates[chatId].incorrectAnswers, userStates[chatId].incorrectQuestions);
        }
        else
        {
            userStates[chatId] = (userStates[chatId].correctAnswers, userStates[chatId].incorrectAnswers + 1, userStates[chatId].incorrectQuestions);
            userStates[chatId].incorrectQuestions.Add(2); // Неправильный ответ на второй вопрос
        }

        string test1Question3 = @"3) Адзначце словы, у якіх на месцы пропуску трэба пісаць літару ў:
1) індывіду_м;
2) ка_чукавы;
3) Ніна _льянаўна;
4) ва _нівермагу;
5) урачыста-_знёслы.";

        var test1Keyboard3 = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("1, 3", "test1_q3_1") },
            new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5", "test1_q3_2") } // Правильный ответ
        });

        await botClient.SendTextMessageAsync(chatId, test1Question3, replyMarkup: test1Keyboard3);
    }

    private static async Task HandleTest1Question3(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        long chatId = callbackQuery.Message.Chat.Id;
        if (callbackQuery.Data == "test1_q3_2") // Правильный ответ
        {
            userStates[chatId] = (userStates[chatId].correctAnswers + 1, userStates[chatId].incorrectAnswers, userStates[chatId].incorrectQuestions);
        }
        else
        {
            userStates[chatId] = (userStates[chatId].correctAnswers, userStates[chatId].incorrectAnswers + 1, userStates[chatId].incorrectQuestions);
            userStates[chatId].incorrectQuestions.Add(3); // Неправильный ответ на третий вопрос
        }

        string test1Question4 = @"4) Адзначце словы, у якіх на месцы пропуску трэба пісаць мяккі знак:
1) снежан_скі;
2) якіс_ці;
3) восем_сот;
4) рэл_еф;
5) вераб_і.";

        var test1Keyboard4 = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4", "test1_q4_1") },
            new[] { InlineKeyboardButton.WithCallbackData("3, 5", "test1_q4_2") } // Правильный ответ
        });

        await botClient.SendTextMessageAsync(chatId, test1Question4, replyMarkup: test1Keyboard4);
    }

    private static async Task HandleTest1Question4(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        long chatId = callbackQuery.Message.Chat.Id;
        if (callbackQuery.Data == "test1_q4_2") // Правильный ответ
        {
            userStates[chatId] = (userStates[chatId].correctAnswers + 1, userStates[chatId].incorrectAnswers, userStates[chatId].incorrectQuestions);
        }
        else
        {
            userStates[chatId] = (userStates[chatId].correctAnswers, userStates[chatId].incorrectAnswers + 1, userStates[chatId].incorrectQuestions);
            userStates[chatId].incorrectQuestions.Add(4); // Неправильный ответ на четвертый вопрос
        }

        string test1Question5 = @"5) Адзначце правільна напісаныя словы:
1) массіўны;
2) палоззе;
3) ванначка;
4) паддашак;
5) паветранны.";

        var test1Keyboard5 = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("1, 5", "test1_q5_1") },
            new[] { InlineKeyboardButton.WithCallbackData("2, 3, 4", "test1_q5_2") } // Правильный ответ
        });

        await botClient.SendTextMessageAsync(chatId, test1Question5, replyMarkup: test1Keyboard5);
    }

    private static async Task HandleTest1Question5(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        long chatId = callbackQuery.Message.Chat.Id;
        if (callbackQuery.Data == "test1_q5_2") // Правильный ответ
        {
            userStates[chatId] = (userStates[chatId].correctAnswers + 1, userStates[chatId].incorrectAnswers, userStates[chatId].incorrectQuestions);
        }
        else
        {
            userStates[chatId] = (userStates[chatId].correctAnswers, userStates[chatId].incorrectAnswers + 1, userStates[chatId].incorrectQuestions);
            userStates[chatId].incorrectQuestions.Add(5); // Неправильный ответ на пятый вопрос
        }

        // Отображение результата
        string resultMessage = $"Тэст завершаны! Правільных адказаў: {userStates[chatId].correctAnswers}, Неправільных адказаў: {userStates[chatId].incorrectAnswers}.";

        if (userStates[chatId].incorrectAnswers > 0)
        {
            resultMessage += "\nПытанні з памылкамі: " + string.Join(", ", userStates[chatId].incorrectQuestions);
        }

        await botClient.SendTextMessageAsync(chatId, resultMessage);
    }
}
