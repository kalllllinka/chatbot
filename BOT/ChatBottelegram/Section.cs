using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public static class Section
{
    public static async Task Start(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        string response = "Вы перешли в режим выполнения заданий по секциям. Начните выполнение заданий здесь...";
        var keyboard = new InlineKeyboardMarkup(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("Фанетыка", "Phonetics") },
            new[] { InlineKeyboardButton.WithCallbackData("Арфаграфія", "Orthography") },
            new[] { InlineKeyboardButton.WithCallbackData("Марфалогія", "Morphology") },
            new[] { InlineKeyboardButton.WithCallbackData("Фразеалогія", "Phraseology") }
        });

        await botClient.SendTextMessageAsync(chatId, "Тэставыя заданні па раздзелам мовы:", replyMarkup: keyboard);
    }

    public static async void HandleCallback(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        string data = callbackQuery.Data;

        switch (data)
        {
            case "Phonetics":
                var startPhoneticsButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Пачаць", "startPhonetics"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Фанетыка. Класіфікацыя гукаў беларускай мовы", replyMarkup: startPhoneticsButton);
                break;

            case "startPhonetics":
                var phoneticsOptions = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("3, 5, 6, 10", "trueP_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4, 7", "P_2") },
                    new[] { InlineKeyboardButton.WithCallbackData("8, 9", "P_3") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце правільныя сцверджанні адносна гукаў:
1) усе ўтвараюцца пры дапамозе голасу і шуму;
2) усе ўтвараюць склады;
3) шумныя зычныя падзяляюцца на звонкія і глухія;
4) санорныя гукі не маюць адпаведных глухіх;
5) гук [ў] не ўтварае пары па мяккасці;
6) з’ява асіміляцыі звязана са слабай фанетычнай пазіцыяй зычных гукаў;
7) для гукаў [д] і [т] пазіцыя перад [ч] або [ц] з’яўляецца моцнай;
8) у канцы слоў назіраецца аглушэнне звонкіх зычных;
9) для свісцячых пазіцыя перад шыпячымі з’яўляецца моцнай;
10) галосныя не маюць слабых пазіцый.", replyMarkup: phoneticsOptions);
                break;

            case "trueP_1":
                var QuestionP2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP2Button);
                break;

            case "P_2":
            case "P_3":
                var retryPhoneticsButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "startPhonetics"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryPhoneticsButton);
                break;

            //P2

            case "QuestionP2":
                var P2Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А4Б3В2Г1", "trueP_4") },
                new[] { InlineKeyboardButton.WithCallbackData("А1Б2В3Г4", "P_5") },
                new[] { InlineKeyboardButton.WithCallbackData("В5А1Б4Г3", "P_6") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж радамі гукаў і назвамі груп зычных, да якіх яны адносяцца.

А. [г], [к], [х]
Б. [л], [м], [н], [р], [в], [ў], [й]
В. [з], [с], [дз], [ц]
Г. [ж], [ш], [дж], [ч]

1. Шыпячыя
2.Свісцячыя
3.Санорныя
4.Заднеязычныя
5.Глухія", replyMarkup: P2Options);
                break;

            case "trueP_4":
                var QuestionP3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP3Button);
                break;

            case "P_5":
            case "P_6":
                var retryQuestionP2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP2Button);
                break;

            //P3

            case "QuestionP3":
                var P3Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("3, 4, 6, 9, 10", "trueP_7") },
                new[] { InlineKeyboardButton.WithCallbackData("1,2, 5, 7, 8", "P_8") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх усе зычныя гукі з’яўляюцца глухімі:

1) грэчка; 
2) коўшык; 
3) падчэпяць;
4) кубкі; 
5) пяеш; 
6) абтаптаць; 
7) пакуюць;
8) кіёчак;
9) шэсць;
10) счышчаць.", replyMarkup: P3Options);
                break;

            case "trueP_7":
                var QuestionP4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP4Button);
                break;

            case "P_8":
                var retryQuestionP3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP3Button);
                break;

            //P4

            case "QuestionP4":
                var P4Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5", "P_9") },
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 6, 7, 9", "trueP_10") },
                new[] { InlineKeyboardButton.WithCallbackData("8, 10", "P_11") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх літар больш, чым гукаў:

1) шчаслівы
2) дождж
3) якасць
4) абуджаюць
5) адзвінеў
6) сцюдзёны
7) ванна
8) дзіўная
9) агеньчык
10) ад’язджаюць", replyMarkup: P4Options);
                break;

            case "trueP_10":
                var QuestionP5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP5Button);
                break;

            case "P_9":
            case "P_11":
                var retryQuestionP4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP4Button);
                break;

            //P5

            case "QuestionP5":
                var P5Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 6", "P_12") },
                new[] { InlineKeyboardButton.WithCallbackData("8, 10", "P_13") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5, 7, 9", "trueP_14") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх гукаў больш, чым літар:

1) апошнія
2) ігліца
3) салаўі
4) здаецца
5) знаёмы
6)  яблынька
7) аб’ява
8) знойдзецца
9) дыягназ
10) батальён", replyMarkup: P5Options);
                break;

            case "trueP_14":
                var QuestionP6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP6Button);
                break;

            case "P_12":
            case "P_13":
                var retryQuestionP5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP5Button);
                break;

            //P6

            case "QuestionP6":
                var P6Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("7, 8, 9", "P_15") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5", "P_16") },
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 6, 10", "trueP_17") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх колькасць гукаў і літар супадае:
1) сям’я
2) надзея
3) абуджаць
4) мяккая
5) блішчыць
6) прыязджаў
7) вяселле
8) агеньчык
9) змагацца
10) брыльянт", replyMarkup: P6Options);
                break;

            case "trueP_17":
                var QuestionP7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP7Button);
                break;

            case "P_15":
            case "P_16":
                var retryQuestionP6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP6Button);
                break;

            //P7

            case "QuestionP7":
                var P7Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 3, 4, 5", "trueP_18") },
                new[] { InlineKeyboardButton.WithCallbackData("1,6, 7", "P_19") },
                new[] { InlineKeyboardButton.WithCallbackData("8, 9, 10", "P_20") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце фанетычныя і арфаэпічныя характарыстыкі, уласцівыя падкрэсленаму ў сказе слову: 

Глянь, на пальчыку кожнай травінкі дыяментны пярсцёнак зіхціць (Н. Маеўская).

1) вымаўленне слова адпавядае яго напісанню; 
2) у слове два санорныя зычныя гукі; 
3) у слове няма звонкіх зычных гукаў;
4) у слове няма шыпячых зычных гукаў; 
5) у слове тры мяккія зычныя гукі. ", replyMarkup: P7Options);
                break;

            case "trueP_18":
                var QuestionP8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP8Button);
                break;

            case "P_19":
            case "P_20":
                var retryQuestionP7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP7Button);
                break;

            //P8

            case "QuestionP8":
                var P8Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 3", "P_21") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 4, 5", "trueP_22") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце фанетычныя характарыстыкі, уласцівыя выдзеленаму ў 4-м сказе слову:

(1)  Плыве туман над скошанаю нівай.
(2) Па росных сцежках верасень брыдзе.
(3) І халадзее сумам жураўліным  Апошняя лілея на вадзе.
(4) Ліст залаты на срэбнай павуціне Гайдаецца над цёмнаю вадой.
(5) І восень у зялёных вершалінах  Вавёркаю мільгае маладой.
(6) Сумненняў час, час роздуму прыходзіць, Час прасвятлення даляў і душы...
(7)  Які спакой у восеньскай прыродзе!..
(8)  Якая таямнічасць у цішы!..М. Башлакоў.

1) у слове колькасць гукаў і літар супадае;  
2) у слове няма санорных зычных гукаў;  
3) у слове няма глухіх зычных гукаў; 
4) у слове няма шыпячых зычных гукаў;  
5) у слове два звонкія зычныя гукі. ", replyMarkup: P8Options);
                break;

            case "trueP_22":
                var QuestionP9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP9Button);
                break;

            case "P_21":
                var retryQuestionP8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP8Button);
                break;

            //P9

            case "QuestionP9":
                var P9Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 5", "trueP_23") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 4", "P_24") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце фанетычныя характарыстыкі, уласцівыя выдзеленаму ў 6-м сказе слову: 

(1)  Плыве туман над скошанаю нівай.
(2) Па росных сцежках верасень брыдзе.
(3) І халадзее сумам жураўліным  Апошняя лілея на вадзе.
(4) Ліст залаты на срэбнай павуціне Гайдаецца над цёмнаю вадой.
(5) І восень у зялёных вершалінах  Вавёркаю мільгае маладой.
(6) Сумненняў час, час роздуму прыходзіць, Час прасвятлення даляў і душы...
(7)  Які спакой у восеньскай прыродзе!..
(8)  Якая таямнічасць у цішы!..М. Башлакоў.

1) у слове колькасць гукаў і літар супадае;    
2) у слове чатыры санорныя зычныя гукі;
3) у слове няма глухіх зычных гукаў;
4) у слове няма свісцячых зычных гукаў;    
5) у слове чатыры мяккія зычныя гукі. ", replyMarkup: P9Options);
                break;

            case "trueP_23":
                var QuestionP10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP10Button);
                break;

            case "P_24":
                var retryQuestionP9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP9Button);
                break;

            //P10

            case "QuestionP10":
                var P10Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4, 5", "trueP_25") },
                new[] { InlineKeyboardButton.WithCallbackData(" 3 ", "P_26") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце фанетычныя і арфаэпічныя характарыстыкі, уласцівыя выдзеленаму ў 8-м сказе слову:

(1)  Плыве туман над скошанаю нівай.
(2) Па росных сцежках верасень брыдзе.
(3) І халадзее сумам жураўліным  Апошняя лілея на вадзе.
(4) Ліст залаты на срэбнай павуціне Гайдаецца над цёмнаю вадой.
(5) І восень у зялёных вершалінах  Вавёркаю мільгае маладой.
(6) Сумненняў час, час роздуму прыходзіць, Час прасвятлення даляў і душы...
(7)  Які спакой у восеньскай прыродзе!..
(8)  Якая таямнічасць у цішы!..М. Башлакоў.

1) у слове колькасць гукаў і літар супадае;    
2) у слове тры санорныя зычныя гукі; 
3) у слове няма глухіх зычных гукаў;
4) у слове ёсць шыпячы зычны гук;   
5) у слове чатыры мяккія зычныя гукі.", replyMarkup: P10Options);
                break;

            case "trueP_25":
                var QuestionP11Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionP11"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionP11Button);
                break;

            case "P_26":
                var retryQuestionP10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionP10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionP10Button);
                break;

            //P11

            case "QuestionP11":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Малайчына😊
Працяг у распрацоўцы🫣");
                break;








            case "Orthography":
                var startOrthographyButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Пачаць", "startOrthography"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Правапіс галосных О, Э — А, Ы ў простых словах", replyMarkup: startOrthographyButton);
                break;

            case "startOrthography":
                var orthographyOptions = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4, 6, 8, 9.", "trueO_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3, 5 , 7, 10 ", "O_2") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару А: 

1) пац_лаваць;
 2) д_таль; 
3) аст_роід;  
4) выкарч_ваць;  
5) ат_ставаць; 
6) выкр_слены; 
7) раскр_шыць; 
8) галант_рэя; 
9) ганд_ль; 
10) Юпіт_р.", replyMarkup: orthographyOptions);
                break;

            case "trueO_1":
                var QuestionO2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO2Button);
                break;

            case "O_2":
                var retryOrthographyButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "startOrthography"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryOrthographyButton);
                break;

            //O2

            case "QuestionO2":
                var O2Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 6, 8", "O_3") },
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5, 7, 9, 10 ", "trueO_4") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару А:
1) д_дэрон; 2) прынт_р; 
3) альт_рнатыва; 
4) шпат_ль; 
5) тр_скатня; 
6) ж_стыкуляцыя; 
7) літ_ратурны; 
8) бяр_знік; 
9) паш_вяліцца; 
10) ч_рніла.", replyMarkup: O2Options);
                break;

            case "trueO_4":
                var QuestionO3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO3Button);
                break;

            case "O_3":
                var retryQuestionO2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO2Button);
                break;


            //O3

            case "QuestionO3":
                var O3Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("3, 4, 6, 7, 8, 10", "trueO_5") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 5, 9 ", "O_6") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару Э:

1) тр_вога
2)  інд_відуальны
3) інгр_дыент
4) р_естр
5) д_летант
6) кат_т
7) орд_н
8) палан_з
9) ш_сцярня
10) пан_ль", replyMarkup: O3Options);
                break;

            case "trueO_5":
                var QuestionO4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO4Button);
                break;

            case "O_6":
                var retryQuestionO3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO3Button);
                break;

            //O4

            case "QuestionO4":
                var O4Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 5, 7, 10", "O_7") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 4, 6, 8, 9", "trueO_8") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару Э:

1) экз_мпляр
2) ар_штаваць
3) сэканд-х_нд
4) тун_ль
5) дыспетч_р
6) д_задарант
7) скут_р
8) ур_гуляваць
9) ас_нсаваць
10) д_сертацыя", replyMarkup: O4Options);
                break;

            case "trueO_8":
                var QuestionO5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO5Button);
                break;

            case "O_7":
                var retryQuestionO4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO4Button);
                break;

            //O5

            case "QuestionO5":
                var O5Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 4, 6, 9", "O_9") },
                new[] { InlineKeyboardButton.WithCallbackData("2, 3, 5, 7, 8, 10", "trueO_10") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару Ы:

1) прац_дура
2) інц_дэнт
3) гор_ч
4) р_актар
5) др_вотня
6) д_дуктыўны
7) зар_ва
8) д_рыжор
9)  задр_маць
10) д_скрымінацыя", replyMarkup: O5Options);
                break;

            case "trueO_10":
                var QuestionO6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO6Button);
                break;

            case "O_9":
                var retryQuestionO5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO5Button);
                break;

            //O6

            case "QuestionO6":
                var O6Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("3,  6, 8, 9", "O_11") },
                new[] { InlineKeyboardButton.WithCallbackData("1 , 2, 4, 5, 7, 10", "trueO_12") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару O:
1) руж_ватвары; 
2) н_ваўтвораны; 
3) ст_метровы;
4) ч_рна-буры;
5) тр_хсоттонны;   
6) п_літэхнікум;  
7) м_вазнаўства;  
8) р_ўнапраўе;
9) в_дацёк;     
10) м_тагонкі.", replyMarkup: O6Options);
                break;

            case "trueO_12":
                var QuestionO7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO7Button);
                break;

            case "O_11":
                var retryQuestionO6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO6Button);
                break;

            //O7

            case "QuestionO7":
                var O7Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5, 7, 8, 10", "trueO_13") },
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 6, 9", "O_14") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару A: 
1) зак_надаўства;  
2) ч_рнавалосы;
3) мн_газначны; 
4) сваб_далюбства;
5) ф_таграфічны; 
6) пр_цівірусны; 
7) ст_рублёвы; 
8) г_рналыжнік;
9) др_васек; 
10) д_брадзей. ", replyMarkup: O7Options);
                break;

            case "trueO_13":
                var QuestionO8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO8Button);
                break;

            case "O_14":
                var retryQuestionO7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO7Button);
                break;

            //O8

            case "QuestionO8":
                var O8Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2 , 3, 4, 7, 10", "O_15") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 5, 6, 8, 9", "trueO_16") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару A:
1) б_мбасховішча; 
2) гр_смайстар; 
3) р_ўназалежны;
4) сваб_далюбны; 
5) д_ўгалецце; 
6) п_ўнакроўны;  
7) прыр_дазнавец; 
8) ж_ўталісце; 
9) ч_рнабровы;   
10) ф_такамера. ", replyMarkup: O8Options);
                break;

            case "trueO_16":
                var QuestionO9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO9Button);
                break;

            case "O_15":
                var retryQuestionO8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO8Button);
                break;

            //O9

            case "QuestionO9":
                var O9Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 3, 5, 6, 8, 9", "trueO_17") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 4, 7, 10", "O_18") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць літару Э:

1) стр_мгалоў;  
2) ш_разём
3) стр_лавідны;
4) сонц_стаянне;
5) ш_сцьсот;  
6) др_ванасаджэнне; 
7) пч_лаводства;  
8) ц_наўтварэнне;
9) р_дканаселены;    
10) ш_сцісоты.  ", replyMarkup: O9Options);
                break;

            case "trueO_17":
                var QuestionO10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionO10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionO10Button);
                break;

            case "O_18":
                var retryQuestionO9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionO9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionO9Button);
                break;

            //O10

            case "QuestionO10":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Малайчына😊
Працяг у распрацоўцы🫣");
                break;






            case "Morphology":
                var startMorphologyButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Пачаць", "startMorphology"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Пытанні пра часціны мовы", replyMarkup: startMorphologyButton);
                break;

            case "startMorphology":
                var morphologyOptions = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3, 6, 7, 9, 10", "trueM_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4, 5, 8", "M_2") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце абстрактныя назоўнікі: 

1) кілаграм; 
2) рухавасць; 
3) аднаўленне; 
4) занятак; 
5) маланка; 
6) існаванне; 
7) хараство; 
8) вецер; 
9) рамантызм; 
10) барацьба.", replyMarkup: morphologyOptions);
                break;

            case "trueM_1":
                var QuestionM2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM2Button);
                break;

            case "M_2":
                var retryMorphologyButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "startMorphology"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryMorphologyButton);
                break;

            //M2


            case "QuestionM2":
                var M2Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 6, 10", "M_3") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5, 7, 8, 9", "trueM_4") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце асабовыя назоўнікі:

1) весялун; 
2) сялянства; 
3) працаўніца; 
4) народ; 
5) патрыёт; 
6) дзетвара; 
7) старшыня; 
8) творца; 
9) муляр; 
10) моладзь..", replyMarkup: M2Options);
                break;

            case "trueM_4":
                var QuestionM3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM3Button);
                break;

            case "M_3":
                var retryQuestionM2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM2Button);
                break;


            //M3


            case "QuestionM3":
                var M3Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5, 7, 9", "trueM_5") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 6, 8, 10", "M_6") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце зборныя назоўнікі: 

1) нафта; 
2) лісце; 
3) морква; 
4) студэнцтва; 
5) клавіятура; 
6) малако;  
7) адміністрацыя; 
8) снег; 
9) сузор’е; 
10) крупы.", replyMarkup: M3Options);
                break;

            case "trueM_5":
                var QuestionM4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM4Button);
                break;

            case "M_6":
                var retryQuestionM3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM3Button);
                break;


            //M4


            case "QuestionM4":
                var M4Options = new InlineKeyboardMarkup(new[]
                {
                            new[] { InlineKeyboardButton.WithCallbackData("2 , 3, 4", "trueM_7") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 5", "M_8") }
            });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Род назоўнікаў");

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словазлучэнні з памылкай у дапасаванні:

1) чорная кава; 
2) высокая насып; 
3) маленькая гусяня; 
4) мужчынскі далонь; 
5) сучасны жывапіс.", replyMarkup: M4Options);
                break;

            case "trueM_7":
                var QuestionM5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM5Button);
                break;

            case "M_8":
                var retryQuestionM4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM4Button);
                break;


            //M5


            case "QuestionM5":
                var M5Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4", "trueM_9") },
                new[] { InlineKeyboardButton.WithCallbackData("3, 5", "M_10") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словазлучэнні з памылкай у дапасаванні: 

1) прыгожая роспіс; 
2) звонкая салоўка; 
3) лёгкі поступ; 
4) шэрая шынель; 
5) узорыстая намаразь.", replyMarkup: M5Options);
                break;

            case "trueM_9":
                var QuestionM6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM6Button);
                break;

            case "M_10":
                var retryQuestionM5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM5Button);
                break;


            //M6


            case "QuestionM6":
                var M6Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4", "trueM_11") },
                new[] { InlineKeyboardButton.WithCallbackData("3, 5", "M_12") }
            });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Лік назоўнікаў");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце назоўнікі, форма множнага ліку назоўнага склону якіх утворана правільна: 

1) каласы; 
2) астрава; 
3) фасоліны; 
4) корані; 
5) азёры; 
6) рага; 
7) каменне; 
8) пашпарта; 
9) імхі; 
10) бровы.", replyMarkup: M6Options);
                break;

            case "trueM_11":
                var QuestionM7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM7Button);
                break;

            case "M_12":
                var retryQuestionM6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM6Button);
                break;


            //M7


            case "QuestionM7":
                var M7Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 8", "M_13") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5, 6, 7, 9, 10", "trueM_14") }
            });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце назоўнікі, форма множнага ліку назоўнага склону якіх утворана няправільна: 

1) дырэктара; 
2) лясы; 
3) вокна; 
4) нябёсы; 
5) воўкі; 
6) торты; 
7) птушані; 
8) рэбры; 
9) лосі; 
10) тыдзені.", replyMarkup: M7Options);
                break;

            case "trueM_14":
                var QuestionM8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM8Button);
                break;

            case "M_13":
                var retryQuestionM7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM7Button);
                break;

            //M8


            case "QuestionM8":
                var M8Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("2, 4, 6, 8, 10", "trueM_16") },
                new[] { InlineKeyboardButton.WithCallbackData("1 , 3,  5, 7, 9 ", "M_17") }
            });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Назоўнікі першага скланення. Правапіс");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце назоўнікі, якія ў форме меснага склону адзіночнага ліку маюць канчатак Е

1) вавёрка; 
2) аздоба; 
3) яблыня; 
4) морква; 
5) нумарацыя; 
6) Паліна; 
7) Лідзія; 
8) Зінаіда; 
9) Таня; 
10) Узда.", replyMarkup: M8Options);
                break;

            case "trueM_16":
                var QuestionM9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM9Button);
                break;

            case "M_17":
                var retryQuestionM8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM8Button);
                break;


            //M9


            case "QuestionM9":
                var M9Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("3, 6, 7", "M_18") },
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4, 5, 8, 9, 10 ", "trueM_19") }
            });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце назоўнікі, якія ў форме меснага склону адзіночнага ліку маюць канчатак і

1) гераіня; 
2) нядзеля; 
3) узнагарода; 
4) свацця; 
5) шпакоўня; 
6) падлога; 
7) ніва; 
8) Азія; 
9) Насця; 
10) Марыя.", replyMarkup: M9Options);
                break;

            case "trueM_19":
                var QuestionM10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM10Button);
                break;

            case "M_18":
                var retryQuestionM9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM9Button);
                break;


            //M10


            case "QuestionM10":
                var M10Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("1, 2, 3, 5, 6, 8, 9", "trueM_20") },
                new[] { InlineKeyboardButton.WithCallbackData("4, 7, 10 ", "M_21") }
            });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце назоўнікі, якія ў форме меснага склону адзіночнага ліку маюць канчатак Ы 

1) пляцоўка; 
2) агароджа; 
3) шаша; 
4) мука; 
5) модніца; 
6) натура; 
7) рука; 
8) Орша; 
9) Варвара; 
10) луска", replyMarkup: M10Options);
                break;

            case "trueM_20":
                var QuestionM11Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionM11"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionM11Button);
                break;

            case "M_21":
                var retryQuestionM10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionM10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionM10Button);
                break;


            //M11


            case "QuestionM11":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Малайчына😊
Працяг у распрацоўцы🫣");
                break;









            case "Phraseology":
                var startPhraseologyButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Пачаць", "startPhraseology"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Значэнне фразеалагізмаў і іх ужыванне", replyMarkup: startPhraseologyButton);
                break;

            case "startPhraseology":
                var phraseologyOptions = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б3В5Г1", "truePH_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б4В5Г1", "PH_2") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж фразеалагізмамі і іх значэннем.

А. Навязнуць у зубах
Б. Прыкласці рукі
В. Ставіць у тупік
Г. Цягнуць за вушы

1. Усяляк дапамагаць няздольнаму, няўмеламу чалавеку
2. Моцна надакучыць
3. Заняцца чым-небудзь
4. Нудна, аднастайна гаварыць
5. Даводзіць да збянтэжанасці", replyMarkup: phraseologyOptions);
                break;

            case "truePH_1":
                var QuestionPH2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH2Button);
                break;

            case "PH_2":
                var retryPhraseologyButton = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "startPhraseology"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryPhraseologyButton);
                break;

            //PH2

            case "QuestionPH2":
                var PH2Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А3Б4В1Г5", "PH_3") },
                new[] { InlineKeyboardButton.WithCallbackData("А4Б5В1Г2", "truePH_4") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж фразеалагізмамі і іх значэннем.

А. Як гром з яснага неба
Б. Як свет светам
В. Як у полі вецер
Г. Як па пісаным

1. Легкадумны, пусты
2. Бойка, гладка, без запінкі
3. Вельмі моцны, дужы
4. Зусім нечакана, раптоўна
5 .Заўсёды, спрадвечна", replyMarkup: PH2Options);
                break;

            case "truePH_4":
                var QuestionPH3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH3Button);
                break;

            case "PH_3":
                var retryQuestionPH2Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH2"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH2Button);
                break;



            //PH3

            case "QuestionPH3":
                var PH3Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А4Б5В1Г3", "truePH_5") },
                new[] { InlineKeyboardButton.WithCallbackData("А2Б3В5Г1", "PH_6") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж фразеалагізмамі і іх значэннем.

А. Біць у званы
Б. Забіваць галаву
В. Зачапіць рукі
Г. Лунаць у паднябессі

1. Знайсці для сябе які-небудзь занятак
2. Аказвацца ў цяжкім, бязвыхадным становішчы
3. Бясплённа марыць, не заўважаючы навакольнага
4. Настойліва звяртаць усеагульную ўвагу на тое, што выклікае трывогу. Абцяжарваць сябе ці каго-небудзь абавязкамі, думкамі", replyMarkup: PH3Options);
                break;

            case "truePH_5":
                var QuestionPH4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH4Button);
                break;

            case "PH_6":
                var retryQuestionPH3Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH3"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH3Button);
                break;



            //PH4

            case "QuestionPH4":
                var PH4Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А2Б3В5Г1", "PH_7") },
                new[] { InlineKeyboardButton.WithCallbackData("А2Б4В5Г3", "truePH_8") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж выдзеленымі фразеалагізмамі і іх сінтаксічнай роляй у сказе.

А. Не маглі звесці вочы да шэрага світання і мы (У. Глушакоў).
Б. Я ўвесь дзень настройваў сябе мужна і горда прыняць удар (З. Прыгодзіч).
В. Даўно маю намер пагутарыць з табою вока на вока (Т. Хадкевіч).
Г. Зноў у ім [Рыбаку] аднавілася прыціхлае было, але ўпартае жаданне даць дзёру (В. Быкаў).

1. Дзейнік.
2. Выказнік (частка выказніка).
3. Азначэнне.
4. Дапаўненне.
5. Акалічнасць.", replyMarkup: PH4Options);
                break;

            case "truePH_8":
                var QuestionPH5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH5Button);
                break;

            case "PH_7":
                var retryQuestionPH4Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH4"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH4Button);
                break;



            //PH5

            case "QuestionPH5":
                var PH5Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А4Б5В2Г1", "truePH_9") },
                new[] { InlineKeyboardButton.WithCallbackData("А4Б3В5Г2", "PH_10") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж выдзеленымі фразеалагізмамі і іх сінтаксічнай роляй у сказе.

А. Рыта... не любіла будаваць паветраных замкаў (А. Васілевіч).
Б. Набралі хлопцы рыбы колькі ўлезе і накіраваліся дадому (Я. Маўр).
В. Маці мая была вострая на язык (В. Стома).
Г. Можа,нябесная канцылярыявам надвор’е падкажа (А. Кудравец).

1. Дзейнік.
2. Выказнік (частка выказніка).
3. Азначэнне.
4. Дапаўненне.
5  Акалічнасць.", replyMarkup: PH5Options);
                break;

            case "truePH_10":
                var QuestionPH6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH6Button);
                break;

            case "PH_9":
                var retryQuestionPH5Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH5"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH5Button);
                break;



            //PH6

            case "QuestionPH6":
                var PH6Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А3Б1В2Г5", "truePH_11") },
                new[] { InlineKeyboardButton.WithCallbackData("А2Б3В5Г1", "PH_12") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж фразеалагізмамі і сінонімамі да іх.

А. Абводзіць вакол пальца
Б. Разводзіць рукамі
В. Канца-краю не відаць
Г. Забіваць дух

1. Дзіву давацца
2. Хоць гаць гаці
3. Браць на вудачку
4. Пападаць у пераплёт
5. Закрываць рот", replyMarkup: PH6Options);
                break;

            case "truePH_11":
                var QuestionPH7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH7Button);
                break;

            case "PH_12":
                var retryQuestionPH6Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH6"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH6Button);
                break;



            //PH7

            case "QuestionPH7":
                var PH7Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А3Б5В1Г4", "truePH_13") },
                new[] { InlineKeyboardButton.WithCallbackData("А3Б1В2Г5", "PH_14") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж выдзеленымі ў сказах фразеалагізмамі і сінонімамі да іх
А. Табе што, прыемна нас абодвух за нос вадзіць? (У. Каратке віч).
Б. Калі ты ўжо так крычаў, калі ты такі смелы, то зямлю грызі, а не адступай (І. Навуменка).
В. Міхал скінуў стрэльбу з пляча і ператварыўся ўвесь у слых (С. Александровіч).
Г. На лбе запішы, што прайшоў ужо час...  (К. Крапіва).

1. Навастрыць вушы.
2. Выходзіць за межы звычайна-га, прывычнага.
3. Абуваць у лапці.
4. Зарубіць на носе.
5. Разбіцца ў дошку.", replyMarkup: PH7Options);
                break;

            case "truePH_13":
                var QuestionPH8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH8Button);
                break;

            case "PH_13":
                var retryQuestionPH7Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH7"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH7Button);
                break;



            //PH8

            case "QuestionPH8":
                var PH8Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А2Б3В5Г1", "PH_15") },
                new[] { InlineKeyboardButton.WithCallbackData("А4Б3В5Г2", "truePH_16") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж выдзеленымі ў сказах спалучэннямі слоў і іх характарыстыкамі.

А. Колькі дзён працаваў — і ўсё кату па пяту.
Б. Ад нейкага белага доміка з усіх ног беглі да завода два хлопчыкі.
В. Уступіць месца брыгадзіра Гоман не мог ні пры якіх абставінах і таму адразу ж даў задні ход.
Г. Лепяшынскі не прымаў гульні слоў у гэтай спрэчцы.

1. Свабоднае словазлучэнне.
2. Фразеалагізм, які мае значэнне ‘дасціпны, каламбурны выраз’.
3. Фразеалагізм, які мае сінонім што ёсць духу.
4. Фразеалагізм, няправільна ўжыты ў сказе паводле свайго значэння.
5  Фразеалагізм, які ў сказе выконвае ролю выказніка.", replyMarkup: PH8Options);
                break;

            case "truePH_15":
                var QuestionPH9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH9Button);
                break;

            case "PH_16":
                var retryQuestionPH8Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH8"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH8Button);
                break;



            //PH9

            case "QuestionPH9":
                var PH9Options = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("А2Б3В1Г5", "truePH_17") },
                new[] { InlineKeyboardButton.WithCallbackData("А3Б1В2Г5", "PH_18") }
            });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж выдзеленымі ў сказах спалучэннямі слоў і іх характарыстыкамі.

А. Ён з агнём жартуе, але паспрабуй да яго падступіцца ды параду даць.
Б. Ніна Аляксандраўна, наш куратар, доўга сядзела, моўчкі слухала нашы выступленні, а потым папрасіла не пераліваць з пустога ў парожняе. 
В. Каманда — і ракета адарвалася ад зямлі.
Г. Дзень расце як на дражджах.

1. Свабоднае словазлучэнне.
2. Фразеалагізм, які мае значэнне ‘дзейнічаць неасцярожна’.
3. Фразеалагізм, які мае сінонім таўчы ваду ў ступе.
4. Фразеалагізм, няправільна ўжыты ў сказе паводле свайго значэння.
5. Фразеалагізм, які ў сказе выконвае ролю акалічнасці.", replyMarkup: PH9Options);
                break;

            case "truePH_17":
                var QuestionPH10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH10Button);
                break;

            case "PH_18":
                var retryQuestionPH9Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH9"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH9Button);
                break;



            //PH10

            case "QuestionPH10":
                var PH10Options = new InlineKeyboardMarkup(new[]
                {
                        new[] { InlineKeyboardButton.WithCallbackData("А3Б1В2Г5", "truePH_19") },
                        new[] { InlineKeyboardButton.WithCallbackData("А2Б4В5Г3", "PH_20") }
                     });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж выдзеленымі ў сказах спалучэннямі слоў і іх характарыстыкамі.

А. Вясна, само сабою разумеецца, чаканы госць у кожным доме.
Б. Там сцежкі мае зараслі ўсе травою, слядоў не відаць за густой лебядою.
В. На мой добры розум, дык я загадаў бы яму самому гэтую кашу расхлёбваць.
Г. — Што гэта ты нос павесіў? — глянуўшы на спахмурнелы твар Міхася, запытаў стары.

1. Свабоднае словазлучэнне.
2. Фразеалагізм, які мае значэнне ‘разблытваць якую-небудзь складаную, клапатлівую ці непрыемную справу’.
3. Фразеалагізм, які мае сінонім вядомая рэч.
4. Фразеалагізм, няправільна ўжыты ў сказе паводле свайго значэння.
5. Фразеалагізм, які ў сказе выконвае ролю выказніка.", replyMarkup: PH10Options);
                break;

            case "truePH_19":
                var QuestionPH11Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionPH11"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionPH11Button);
                break;

            case "PH_20":
                var retryQuestionPH10Button = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "QuestionPH10"));
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryQuestionPH10Button);
                break;



            //PH11

            case "QuestionPH11":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Малайчына😊
Працяг у распрацоўцы🫣");
                break;

        }
    }
}
