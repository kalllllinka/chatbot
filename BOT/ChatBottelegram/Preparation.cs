using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public static class Preparation
{
    public static async Task Start(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {

        string response1 = "Пачнем жа падрыхтоўку 🙃";
        await botClient.SendTextMessageAsync(chatId, response1);


        string response2 = "Бот распрацаваны на аснове зборнікаў тэстаў ЦТ / ЦЭ па беларусскай мове 2015-2023 гадоў, Рэспубліканскага інстытута кантролю ведаў";
        await botClient.SendTextMessageAsync(chatId, response2);


        var inlineKeyboard = new InlineKeyboardMarkup(new[]
{
    
    
        new[] {InlineKeyboardButton.WithCallbackData("Частка А", "partA") },
        new[] {InlineKeyboardButton.WithCallbackData("Частка В", "partB") }
    
});

        string response3 = "Выберы частку:";
        await botClient.SendTextMessageAsync(chatId, response3, replyMarkup: inlineKeyboard);

    }
    public static async void HandleCallback(TelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        if (callbackQuery == null)
        {
            throw new ArgumentNullException(nameof(callbackQuery), "CallbackQuery cannot be null.");
        }

        var chatId = callbackQuery.Message?.Chat?.Id;
        var data = callbackQuery.Data;

        if (chatId == null)
        {
            throw new ArgumentNullException(nameof(chatId), "Chat ID cannot be null.");
        }

        switch (data)
        {
            case "partA":
                await botClient.SendTextMessageAsync(chatId.Value, "Пераход да Часткі А");

                var buttonspartA = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("паэма “(Г/г)ражына”, аловак (А/а)ндрэя", "A_1") },
                new[] { InlineKeyboardButton.WithCallbackData("дрэва (Я/я)сень, рыба (С/с)елядзец", "A_2") },
                new[] { InlineKeyboardButton.WithCallbackData("птушка (С/с)нягір, стыль (А/арыка)", "A_3") }
            });

                await botClient.SendTextMessageAsync(chatId.Value, "Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць з вялікай літары:", replyMarkup: buttonspartA);
                break;

            case "A_1":
                var QuestionKeyboardA1 = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA1") }
            });
                await botClient.SendTextMessageAsync(chatId.Value, "✅правільна✅", replyMarkup: QuestionKeyboardA1);
                break;

            case "A_2":
            case "A_3":
                var retryKeyboardPartA = new InlineKeyboardMarkup(new[]
                {
                new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryPartA") }
            });
                await botClient.SendTextMessageAsync(chatId.Value, "❌няправільна❌");
                await botClient.SendTextMessageAsync(chatId.Value, @"У беларускай мове з вялікай літары пішуцца наступныя словы:
1.Першае слова ў сказе.
2.Першае слова пасля двукроп’я.
3.Усе словы ў назвах дзяржаўных і нацыянальных, ваенных і культурных рэліквій.
4.Усе словы ў поўных назвах ордэнаў, медалёў.
5.Аднаслоўныя ўласныя назвы і першае слова ў састаўных назвах кніг, газет, часопісаў, устаноў, арганізацый, заводаў, караблёў, марак машын.
6.Усе словы ў назвах дзяржаўных органаў і арганізацый.", replyMarkup: retryKeyboardPartA);
                break;

            //A1

            case "QuestionA1":
                var QuestionKeyboardA1Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("падрыхтоўка, пакарміў", "A4") },
                    new[] { InlineKeyboardButton.WithCallbackData("страўс, сауна, воук", "A5") },
                    new[] { InlineKeyboardButton.WithCallbackData("дыназаур, Аурора, уураж", "A6") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце правільна напісаныя словы (формы слоў):", replyMarkup: QuestionKeyboardA1Again);
                break;

            case "A4":
                var QuestionKeyboardA2 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA2") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA2);
                break;

            case "A5":
            case "A6":
                var retryKeyboardA1 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA1") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Правільна напісаныя словы (формы слоў) залежаць ад кантэксту і правіл беларускай мовы.  Вось некаторыя прыклады:
1.У выразе “горад Слуцк”, слова “Слуцк” напісана правільна, бо гэта ўласная назва населенага пункта.
2.У выразе “Васілѐў партфель”, слова “Васілѐў” напісана правільна, бо гэта прыналежны прыметнік, утвораны ад уласнага назоўніка пры дапамозе суфікса -ѐў-.
3.У выразе “грыб падбярозавік”, слова “падбярозавік” напісана правільна, бо гэта назва грыба, якая з’яўляецца агульнай і пішацца з малой літары.
4.У выразе “запрасіў”, слова “запрасіў” напісана правільна.У выразе “лаўка”, слова “лаўка” напісана правільна.", replyMarkup: retryKeyboardA1);
                break;

            case "retryA1":
                var buttonsRetryA1 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("падрыхтоўка, пакарміў", "A4") },
                    new[] { InlineKeyboardButton.WithCallbackData("страўс, сауна, воук", "A5") },
                    new[] { InlineKeyboardButton.WithCallbackData("дыназаур, Аурора, уураж", "A6") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце правільна напісаныя словы (формы слоў):", replyMarkup: buttonsRetryA1);
                break;

            //A2

            case "QuestionA2":
                var QuestionKeyboardA2Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("шматмоў_е, едз_це, высіл_вацца", "A7") },
                    new[] { InlineKeyboardButton.WithCallbackData("мадэл_ер, здароў_е, салаў_і", "A8") },
                    new[] { InlineKeyboardButton.WithCallbackData("прэм_ер, мнагабор_е, аб_інець", "A9") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце словы, у якіх на месцы пропуску трэбы пісаць апостраф:", replyMarkup: QuestionKeyboardA2Again);
                break;

            case "A9":
                var QuestionKeyboardA3 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA3") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA3);
                break;

            case "A8":
            case "A7":
                var retryKeyboardA2 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA2") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Апостраф у беларускай мове пішацца ў наступных выпадках:
1.Пры асобным вымаўленні зычных з наступным галосным: е, ё, ю, я і націскным і.
2.У сярэдзіне слова пасля  б, в, м, п, ф,  г, к, х,  д, т і  р перад літарамі е, ё, і, ю, я.
3.У складаных словах з першай часткай двух-, трох-, чатырох-, шмат- перад літарамі е, ё, ю, я.
Апостраф не пішацца:
Пасля ў (у нескладовага) перад літарамі е, ё, і, ю, я: абаўецца, саўюць, салаў.", replyMarkup: retryKeyboardA2);
                break;

            case "retryA2":
                var buttonsRetryA2 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("шматмоў_е, едз_це, высіл_вацца", "A7") },
                    new[] { InlineKeyboardButton.WithCallbackData("мадэл_ер, здароў_е, салаў_і", "A8") },
                    new[] { InlineKeyboardButton.WithCallbackData("прэм_ер, мнагабор_е, аб_інець", "A9") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце словы, у якіх на месцы пропуску трэбы пісаць апостраф:", replyMarkup: buttonsRetryA2);
                break;

            //A3

            case "QuestionA3":
                var QuestionKeyboardA3Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(вела) трэнажор, (спец) абутак", "A10") },
                    new[] { InlineKeyboardButton.WithCallbackData("(горад) спадарожнік,жар_птушка", "A11") },
                    new[] { InlineKeyboardButton.WithCallbackData("(паў) Барысава, Бялыніцкі_Біруля", "A12") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце словы якія трэба пісаць разам:", replyMarkup: QuestionKeyboardA3Again);
                break;

            case "A10":
                var QuestionKeyboardA4 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA4") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA4);
                break;

            case "A11":
            case "A12":
                var retryKeyboardA3 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA3") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Правілы для напісання словаў разам у беларускай мове могуць быць наступнымі: 
1. Словазлучэнні з прыназоўнікамі: Звычайна словы пішуцца разам з іх прыназоўнікамі, калі аны ўтвараюць адзін канструктыў, адносіны ці агульную з'яву (напрыклад, кнігазборчы, птушкападобны). 
2. Словазлучэнні з дзеяслоўнікамі: Пішуцца разам словы, якія змяшчаюць дзеяслоў і іншыя часціны мовы, калі яны выяўляюць аднолькавы працэс, дзеянне або стан (напрыклад, па-суседску, уздоўждарожны). 
3. Утварэнні ад канструктыўных словазлучэнняў: Словы, якія ўтвораныя ад словазлучэнняў або выразаў, пішуцца разам, калі яны маюць аднолькавую з'яву (напрыклад, калі-нібудзь, дзень за днём). 
4. Утварэнні ад прыметнікаў: Словы, якія ўтвораныя ад прыметнікаў і іншых часцін мовы, пішуцца разам, калі яны апісваюць адзін аб'ект, якасць ці стан (напрыклад, маладушны, жоўтасць). 
5. Складаныя выразы: Пішуцца разам словы, якія складаюцца з некалькі частак і маюць аднокавую з'яву ці ўтвараюць адзін лагічны блок (напрыклад, дзяржаўна-праўны, пяцігадовы). ", replyMarkup: retryKeyboardA3);
                break;

            case "retryA3":
                var buttonsRetryA3 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(вела) трэнажор, (спец) абутак", "A10") },
                    new[] { InlineKeyboardButton.WithCallbackData("(горад) спадарожнік,жар_птушка", "A11") },
                    new[] { InlineKeyboardButton.WithCallbackData("(паў) Барысава, Бялыніцкі_Біруля", "A12") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце словы якія трэба пісаць разам:", replyMarkup: buttonsRetryA3);
                break;

            //A4

            case "QuestionA4":
                var QuestionKeyboardA4Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(Усходне) Сібірскае мора", "A13") },
                    new[] { InlineKeyboardButton.WithCallbackData("(бела) валосы хлопчык", "A14") },
                    new[] { InlineKeyboardButton.WithCallbackData("(балотна) ўтвальны працэс", "A15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце прыклады , у якіх выделеныя словы трэба пісаць праз злучок:", replyMarkup: QuestionKeyboardA4Again);
                break;

            case "A13":
                var QuestionKeyboardA5 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA5") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA5);
                break;

            case "A14":
            case "A15":
                var retryKeyboardA4 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA4") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Праз злучок пішуцца: 
1) прыслоўі па-першае, па-другое, па-трэцяе і г. д.; 
2) прыслоўі з прыстаўкай па-, утвораныя ад давальнага склону поўных прыметнікаў і прыналежных займеннікаў з суфіксамі -аму, -яму, -ому, -му: па-зімоваму, па-мойму; прыслоўі на -ску, -цку ад ад- носных прыметнікаў: па-таварыску, па-брацку; прыслоўі з суфіксам -ы ад прыметнікаў: па-дзіцячы, па-мядзведжы; 
3) складаныя прыслоўі, утвораныя паўтарэннем аднолькавых, блізкіх або супрацьлеглых па значэнні слоў (можа далучацца прыстаўка): светла-светла, крыж-накрыж, сюды-туды, як-ніяк, больш-менш; 
4) прыслоўі з прыстаўкай абы- і постфіксамі -небудзь, -колечы: абы-дзе, дзе-небудзь, як-колечы; 
5) тэхнічны тэрмін на-гара, Некаторыя прыслоўі з прыстаўкамі падобны на спалучэнні назоўнікаў, займеннікаў і лічэбнікаў з прыназоўнікамі: звярнуў убок — звярнуў у бок лесу, зрабілі ўтрох — у трох кнігах.
Для размежавання такіх напісанняў неабходна ведаць наступныя правілы: 
1) назоўнікі з прыназоўнікамі абазначаюць прадмет і маюць пры сабе паясняльныя словы: упаў з верху (чаго?) дрэва; 
2) лічэбнікі і займеннікі дапасуюцца да назоўнікаў: прыехалі ў дзвюх машынах; прыслоўі прымыкаюць да дзеясловаў: прыйшлі ўдзвюх.", replyMarkup: retryKeyboardA4);
                break;

            case "retryA4":
                var buttonsRetryA4 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(Усходне) Сібірскае мора", "A13") },
                    new[] { InlineKeyboardButton.WithCallbackData("(бела) валосы хлопчык", "A14") },
                    new[] { InlineKeyboardButton.WithCallbackData("(балотна) ўтвальны працэс", "A15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце прыклады , у якіх выделеныя словы трэба пісаць праз злучок:", replyMarkup: buttonsRetryA4);
                break;

            //A5


            case "QuestionA5":
                var QuestionKeyboardA5Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("сказаць (да) места, (па) іншаму зразумець ", "A16") },
                    new[] { InlineKeyboardButton.WithCallbackData("прыехать (з) далёку", "A17") },
                    new[] { InlineKeyboardButton.WithCallbackData("(лён) даўгунец, сказаць (то) сказаў", "A18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце прыклады, у якіх выделеныя словы трэба пісаць разам:", replyMarkup: QuestionKeyboardA5Again);
                break;

            case "A17":
                var QuestionKeyboardA6 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA6") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA6);
                break;

            case "A16":
            case "A18":
                var retryKeyboardA5 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA5") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Словы “да, з, што, на, па, усё, за, у, як, без” могуць быць напісаныя разам у беларускай мове ў наступных выпадках:
1.Прыстаўкі: Гэтыя словы могуць быць напісаныя разам з іншымі словамі, калі яны выступаюць у ролі прыстаўкі. Напрыклад, “падаць”, “забыць”, “навучыць”, “пазнаёміцца”, “захаваць”, “увайсці”, “як-небудзь”, ""бездумны"".
2.Складаныя словы: Гэтыя словы могуць быць напісаныя разам у складаных словах. Напрыклад, “поўдзень”, “заўтра”, “навыгляд”, “поўгода”, “усёцела”, “заўсёды”, “усёдзень”, “якраз”, ""бездакорны"".
3.Словазлучэнні: Гэтыя словы могуць быць напісаныя разам у словазлучэннях. Напрыклад, “даўжыня”, “сшытак”, “навысотах”, “паўдарогі”, “усётакі”, “заўтрашні”, “усёдзённы”, “яктолькі”, ""бездакорны"".", replyMarkup: retryKeyboardA5);
                break;

            case "retryA5":
                var buttonsRetryA5 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("сказаць (да) места, (па) іншаму зразумець ", "A16") },
                    new[] { InlineKeyboardButton.WithCallbackData("прыехать (з) далёку", "A17") },
                    new[] { InlineKeyboardButton.WithCallbackData("(лён) даўгунец, сказаць (то) сказаў", "A18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце прыклады, у якіх выделеныя словы трэба пісаць разам:", replyMarkup: buttonsRetryA5);
                break;

            //A6

            case "QuestionA6":
                var QuestionKeyboardA6Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(не) складнае пытанне, (не) пасаджаныя ", "A19") },
                    new[] { InlineKeyboardButton.WithCallbackData("печ (не) пабелена,(не) паспрабаваўшы ", "A20")},
                    new[] { InlineKeyboardButton.WithCallbackData("(не) лінгвіст, (не) давыканаць", "A21") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце прыклады, у якіх часціцу НЕ з выдзеленымі словамі трэба пісаць асобна:", replyMarkup: QuestionKeyboardA6Again);
                break;

            case "A20":
                var QuestionKeyboardA7 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA7") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA7);
                break;

            case "A19":
            case "A21":
                var retryKeyboardA6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA6") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Як часціца не, так і часціца ні могуць выражаць і адмоўнае, і сцвярджальнае значэнне — гэта залежыць ад пэўных выпадкаў ужывання. 
Часціца НЕ адносіцца да адмоўных. Яе асноўнае значэнне — адмаўленне таго, што выражана словам, з якім яна ўжыта: Жыццё не можа надакучыць (Ф. Баторын). Не чутно салаўя, не ўскрыкваюць совы (Н. Маеўская). 
Сцвярджальнай часціца не становіцца ў наступных выпадках: 
1) калі яна ўжыта двойчы — пры кожнай частцы састаўнога выказніка: не мог не пайсці, не мог не выканаць; 
2) у спалучэннях хто не, дзе не, дзе толькі не, куды не і інш., калі яны ўжываюцца ў незалежных пытальна- клічных сказах: Чаго толькі не ведаў мой дзед! (С. Лобач); 
3) калі стаіць перад назоўнікам з прыназоўнікам без: зірнуў не без цікавасці, зрабіў не без карысці; 
4) калі стаіць пасля часціц ледзь, чуць, амаль або пасля злучніка пакуль: ледзь не спазніліся, пакуль не позна; 
5) у спалучэнні са словамі нельга, немагчыма: нельга не паверыць, немагчыма не прыйсці;
6) у спалучэннях не больш за, не меньш за. Часціца ні — узмацняльная. Яе роля — узмацняць або адмаўленне, або сцвярджэнне. 
Часціца НI ўзмацняе адмаўленне, ужо выражанае ў сказе іншымі сродкамі: 
1) у адмоўных сказах, дзе адмаўленне выражана часціцай не або словамі нельга, няма, немагчыма: Няма ні душы на вуліцы; у спалучэннях тыпу ні на момант, ні на хвіліну; 
2) у няпоўных сказах, у якіх выказнік з адмоўем падразумяваецца: Ні гуку ў адказ; 
3) у ролі злучніка і ў адмоўных сказах пры пералічэнні аднародных членаў сказа: Ні дождж, ні жар для яго [каменя] не страшны (Я. Колас); 
4) у пабуджальных сказах загаднага характару: Ні з месца! Часціца ні ўзмацняе сцвярджэнне ў даданых сказах, дзе выкарыстоўваецца ў спалучэнні з займеннікамі і прыслоўямі: дзе ні, хто ні, што ні, куды ні, адкуль ні і інш. 
У такія спалучэнні можа ўваходзіць часціца б (дзе б ні, хто б ні і г. д.): Толькі дзе за морам ні жыў я, Беларусь мая снілася мне (Г. Бураўкін). 
Часціца ні ўжываецца ў фразеалагізмах са значэннем няпэўнасці: ні тое ні сёе, ні рыба ні мяса і інш. 
Неабходна адрозніваць спалучэнні не раз (многа разоў) і ні разу (ні аднаго разу); не адзін (некалькі) і ні адзін (ніхто).", replyMarkup: retryKeyboardA6);
                break;

            case "retryA6":
                var buttonsRetryA6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(не) складнае пытанне, (не) пасаджаныя ", "A19") },
                    new[] { InlineKeyboardButton.WithCallbackData("печ (не) пабелена,(не) паспрабаваўшы ", "A20")},
                    new[] { InlineKeyboardButton.WithCallbackData("(не) лінгвіст, (не) давыканаць", "A21") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце прыклады, у якіх часціцу НЕ з выдзеленымі словамі трэба пісаць асобна:", replyMarkup: buttonsRetryA6);
                break;

            //A7

            case "QuestionA7":
                var QuestionKeyboardA7Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("прабачыць знаёмага, вышываць па ўзорам(не) складнае пытанне, (не) пасаджаныя ", "A22") },
                    new[] { InlineKeyboardButton.WithCallbackData("абодва хлопцы, жыць у суседстве", "A23")},
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце сінтаксічныя канструкцыі, якія адпавядаюць нормам беларускай літаратурнай мовы:", replyMarkup: QuestionKeyboardA7Again);
                break;

            case "A23":
                var QuestionKeyboardA8 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA8") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA8);
                break;

            case "A22":
                var retryKeyboardA7 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA7") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Сінтаксічныя канструкцыі, якія адпавядаюць нормам беларускай літаратурнай мовы, вельмі розныя і залежаць ад кантэксту. Вось некаторыя прыклады:
1.Дзякаваць сябру: Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. У беларускай мове можна выкарыстоўваць формулу “дзякаваць + даватальны прыслоўе” для выказвання падзякі.
2.За пяць метраў ад магазіна: Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. Гэта ўласцівая канструкцыя для выказвання месцазнаходжання ў беларускай мове.
3.Жыць у суседстве: Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. Гэта ўласцівая канструкцыя для выказвання месцазнаходжання ў беларускай мове.
4.Лепшы ўсіх: Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. Гэта канструкцыя, дзе прыслоўе “лепшы” адносіцца да ўсіх (прыслоўе займенніка “ўсіх”) у сэнсе лепшага.", replyMarkup: retryKeyboardA7);
                break;

            case "retryA7":
                var buttonsRetryA7 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("прабачыць знаёмага, вышываць па ўзорам(не) складнае пытанне, (не) пасаджаныя ", "A22") },
                    new[] { InlineKeyboardButton.WithCallbackData("абодва хлопцы, жыць у суседстве", "A23")},
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Адзначце сінтаксічныя канструкцыі, якія адпавядаюць нормам беларускай літаратурнай мовы:", replyMarkup: buttonsRetryA7);
                break;

            //A8

            case "QuestionA8":
                var QuestionKeyboardA8Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3 ", "A24") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5 ", "A25")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску абявязкова ставіцца працяжнік:
 1) Адлегласць ＿ не перашкода для сяброў.
 2) Выхоўваць ＿ адказная справа.
 3) Маміна ўсмешка ＿ нібыта сонейка.
 4) Міхаіл Ляпеха ＿ першы захавальнік сядзібы Францішка Багушэвіча.
 5) Майскія дзянькі ＿ што трэба.", replyMarkup: QuestionKeyboardA8Again);
                break;

            case "A25":
                var QuestionKeyboardA9 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA9") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA9);
                break;

            case "A24":
                var retryKeyboardA8 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA8") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Працяжнік у беларускай мове ставіцца на месцы пропуску ў наступных выпадках:
1.Дзейнік і выказнік выражаны назоўнікамі ў назоўным склоне: ""Юравічы – паселішча вельмі старажытнае"".
2.Дзейнік і выказнік выражаны колькаснымі лічэбнікамі ў Н. с. ці адзін з іх выражаны назоўнікам, а другі – лічэбнікам у Н. с.: ""Плошча поля – два гектары"".
3.Абодва галоўныя члены ці адзін з іх выражаны інфінітывам (неазначальнай формай дзеяслова): ""Жыць – Радзіме служыць"".
4.Выказнік выражаны ўстойлівым спалучэннем слоў (фразеалагізмам): ""Нівы – вокам не абняць"".
5.Перад выказнікам стаяць словы гэта, вось, значыць, гэта значыць: ""Плошча поля – гэта наша радзіма, наш дом"".", replyMarkup: retryKeyboardA8);
                break;

            case "retryA8":
                var buttonsRetryA8 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3 ", "A24") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5 ", "A25")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску абявязкова ставіцца працяжнік:
 1) Адлегласць ＿ не перашкода для сяброў.
 2) Выхоўваць ＿ адказная справа.
 3) Маміна ўсмешка ＿ нібыта сонейка.
 4) Міхаіл Ляпеха ＿ першы захавальнік сядзібы Францішка Багушэвіча.
 5) Майскія дзянькі ＿ што трэба.", replyMarkup: buttonsRetryA8);
                break;

            //A9

            case "QuestionA9":
                var QuestionKeyboardA9Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("3, 1, 4 ", "A26") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4 ", "A27")},
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A28") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Нізашто не прайду міма спеву скрыпкі, ці грання цымбалаў.
2) Уладзіміру Караткевічу ў аднолькавай ступені былі падуладны паэзія, проза, драматургія, і публіцыстыка, і крытыка, і мастацкі пераклад.
3) Самая дзівосная сцяжынка бяжыць сярод шырокага, жытнёвага поля.
4) Па вішнях, ды па яблынях майскае сонца разліта.
5) Францыск Скарына з'яўляецца пачынальнікам не толькі беларускай, але і ўсёй усходнеславянскай вершатворчасці.", replyMarkup: QuestionKeyboardA9Again);
                break;

            case "A28":
                var QuestionKeyboardA10 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA10") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA10);
                break;

            case "A27":
            case "A26":
                var retryKeyboardA9 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA9") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Пунктуацыя ў беларускай мове вельмі важная для правільнай пастаноўкі інтанацый і зразумеласці сказаў. Вось некаторыя агульныя правілы пунктуацыі:
1.Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.
2.Пытальнік ставіцца на канцы пытальных сказаў.
3.Знак выкліку ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4.Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5.Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6.Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння пропускаў у тэксце.", replyMarkup: retryKeyboardA9);
                break;

            case "retryA9":
                var buttonsRetryA9 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("3, 1, 5 ", "A26") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A27")},
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A28") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Нізашто не прайду міма спеву скрыпкі, ці грання цымбалаў.
2) Уладзіміру Караткевічу ў аднолькавай ступені былі падуладны паэзія, проза, драматургія, і публіцыстыка, і крытыка, і мастацкі пераклад.
3) Самая дзівосная сцяжынка бяжыць сярод шырокага, жытнёвага поля.
4) Па вішнях, ды па яблынях майскае сонца разліта.
5) Францыск Скарына з'яўляецца пачынальнікам не толькі беларускай, але і ўсёй усходнеславянскай вершатворчасці.:", replyMarkup: buttonsRetryA9);
                break;

            //A10

            case "QuestionA10":
                var QuestionKeyboardA10Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 5", "A29") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3, 4 ", "A30")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Унікальнай у сусветнай літаратуры стала, створаная Янкам Брылём разам з Алесем Адамовічам ш Уладзімірам Калеснікам, кніга «Я з вогненнай вёскі…».
2) Арфаграфія, ці інакш праваніс, цесна звязана з арфаэпіяй.
3) Славіцца мой родны куток, акрамя прыроды, людской дабрынёй.
4) Справіўшы апошні баль, уступіла зіма свае правы вясне.
5) Залацінкі дрэвам кастрычнік дорыць, не скупячыся.", replyMarkup: QuestionKeyboardA10Again);
                break;

            case "A30":
                var QuestionKeyboardA11 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA11") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA11);
                break;

            case "A29":
                var retryKeyboardA10 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA10") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Пунктуацыя ў беларускай мове вельмі важная для правільнай пастаноўкі інтанацый і зразумеласці сказаў. Вось некаторыя агульныя правілы пунктуацыі:
1.Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.
2.Пытальнік ставіцца на канцы пытальных сказаў.
3.Знак выкліку ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4.Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5.Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6.Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння пропускаў у тэксце.", replyMarkup: retryKeyboardA10);
                break;

            case "retryA10":
                var buttonsRetryA10 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 5", "A29") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3, 4 ", "A30")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Унікальнай у сусветнай літаратуры стала, створаная Янкам Брылём разам з Алесем Адамовічам ш Уладзімірам Калеснікам, кніга «Я з вогненнай вёскі…».
2) Арфаграфія, ці інакш праваніс, цесна звязана з арфаэпіяй.
3) Славіцца мой родны куток, акрамя прыроды, людской дабрынёй.
4) Справіўшы апошні баль, уступіла зіма свае правы вясне.
5) Залацінкі дрэвам кастрычнік дорыць, не скупячыся.", replyMarkup: buttonsRetryA10);
                break;

            //A11

            case "QuestionA11":
                var QuestionKeyboardA11Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A31") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A32") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A33") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A34")},
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску (пропускаў) трэба паставіць коску (коскі): 
1) Прыродныя багацці бясцэнныя, аднак＿ яны небясконцыя.
2) Шчасця і радасці＿ трэба думаць＿ не назапасішся.
3) Амаль＿ усе беларускія гаспадыні ў квашаную капусту дадаюць журавіны.
4) Найсмачнейшы＿ ці трэба ў гэтым сумнявацца? ＿ спечаны сваімі рукамі хлеб.
5) Самай першай настаўніцай для дзіцяці з'яўляецца＿ вядома＿ мама.", replyMarkup: QuestionKeyboardA11Again);
                break;

            case "A31":
                var QuestionKeyboardA12 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA12") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA12);
                break;

            case "A32":
            case "A33":
            case "A34":

                var retryKeyboardA11 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA11") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове коска ставіцца ў наступных выпадках:
1.Паміж адносна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
2.Паміж часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва, калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3.Перад злучнікам а кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
4.Паміж развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
5.Паміж разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
6.Паміж групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
7.У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.", replyMarkup: retryKeyboardA11);
                break;

            case "retryA11":
                var buttonsRetryA11 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A31") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A32") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A33") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A34")},
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску (пропускаў) трэба паставіць коску (коскі): 
1) Прыродныя багацці бясцэнныя, аднак＿ яны небясконцыя.
2) Шчасця і радасці＿ трэба думаць＿ не назапасішся.
3) Амаль＿ усе беларускія гаспадыні ў квашаную капусту дадаюць журавіны.
4) Найсмачнейшы＿ ці трэба ў гэтым сумнявацца? ＿ спечаны сваімі рукамі хлеб.
5) Самай першай настаўніцай для дзіцяці з'яўляецца＿ вядома＿ мама.", replyMarkup: buttonsRetryA11);
                break;

            //A12

            case "QuestionA12":
                var QuestionKeyboardA12Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4 ", "A35") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 5", "A36")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназлучаныя сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Ціха і бязвоблачна.
2) Каля хаты гармонік іграе, а за рэчкай салоўка спявае.
3) Зноў жаўцее лотаць, і сінеюць фіялкі.
4) Чыстая вада журчала ў лозах і таемна шапацеў ля самага берага ракі сухі чарот.
5) Імгненне - і ажывае сонны гай.", replyMarkup: QuestionKeyboardA12Again);
                break;

            case "A36":
                var QuestionKeyboardA13 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA13") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA13);
                break;

            case "A35":
                var retryKeyboardA12 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA12") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У складаназлучаных сказах у беларускай мове пунктуацыя вельмі важная. Вось некаторыя агульныя правілы:
1.Між часткамі складаназлучанага сказа ставіцца коска, калі ў другой частцы сказа заключаецца значэнне выніку, наступства, рэзкага супрацьпастаўлення, асабліва перад злучнікам ""і"".
2.Між аднасна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3.Між часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва, калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
4.Перад злучнікам “а” кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
5.Між развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
6.Між разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
7.Між групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
8.У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.", replyMarkup: retryKeyboardA12);
                break;

            case "retryA12":
                var buttonsRetryA12 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4 ", "A35") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 5", "A36")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназлучаныя сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Ціха і бязвоблачна.
2) Каля хаты гармонік іграе, а за рэчкай салоўка спявае.
3) Зноў жаўцее лотаць, і сінеюць фіялкі.
4) Чыстая вада журчала ў лозах і таемна шапацеў ля самага берага ракі сухі чарот.
5) Імгненне - і ажывае сонны гай.", replyMarkup: buttonsRetryA12);
                break;

            //A13

            case "QuestionA13":
                var QuestionKeyboardA13Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5 ", "A37") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 4", "A38")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A39")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 5", "A40")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 5", "A41")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназалежныя сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Вядома, што, калі Уладзімір Караткевіч напісаў першыя апавяданні, то ён сам жа іх і праілюстраваў.
2) Люблю наш адвечны бор, дзе ўзносяць хвоі ўгору шапкі і дзе елкі рассцілаюць лапкі.
3) Калі мароз-мастак узяўся ўсур'ёз гаспадарыць маладзічок толькі на хвілінку паказаўся з-за хмары.
4) Вясны не адчуеш, пакуль не прыляцяць птушкі.
5) Бесядзь, што працякае побач з мястэчкам Саматэвічы Аркадзь Куляшоў лічыў ракой свайго лёсу.", replyMarkup: QuestionKeyboardA13Again);
                break;

            case "A38":
                var QuestionKeyboardA14 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA14") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA14);
                break;

            case "A37":
            case "A39":
            case "A40":
            case "A41":
                var retryKeyboardA13 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA13") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У складаназалежных сказах у беларускай мове пунктуацыя вельмі важная. Вось некаторыя агульныя правілы:
1.Між часткамі складаназалежнага сказа ставіцца коска, калі ў другой частцы сказа заключаецца значэнне выніку, наступства, рэзкага супрацьпастаўлення, асабліва перад злучнікам ""і"".
2.Між аднасна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3.Між часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва, калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
4.Перад злучнікам “а” кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
5.Між развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
6.Між разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва, калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
7.Між групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
8.У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.", replyMarkup: retryKeyboardA13);
                break;

            case "retryA13":
                var buttonsRetryA13 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3, 5 ", "A37") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 4", "A38")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A39")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 5", "A40")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 5", "A41")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназалежныя сказы, у якіх няма парушэння пунктуацыйных нормаў:
1) Вядома, што, калі Уладзімір Караткевіч напісаў першыя апавяданні, то ён сам жа іх і праілюстраваў.
2) Люблю наш адвечны бор, дзе ўзносяць хвоі ўгору шапкі і дзе елкі рассцілаюць лапкі.
3) Калі мароз-мастак узяўся ўсур'ёз гаспадарыць маладзічок толькі на хвілінку паказаўся з-за хмары.
4) Вясны не адчуеш, пакуль не прыляцяць птушкі.
5) Бесядзь, што працякае побач з мястэчкам Саматэвічы Аркадзь Куляшоў лічыў ракой свайго лёсу.", replyMarkup: buttonsRetryA13);
                break;

            //A14

            case "QuestionA14":
                var QuestionKeyboardA14Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2  ", "A42") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 3", "A43")},
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3", "A44")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A45")},
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A46")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, якія адпавядаюць прыведзенай схеме (улічыце, што знакі прыпынку паміж часткамі сказаў не пастаўлены): [ ]: [ ].
1) Важна ходзяць па балоце ў чырвоных ботах буслы нехта папрасіў зязюльку шчыра палічыць гады́.
2) Сакавік прыйшоў пад серабрысты шум ручаёў сакавік душу ўлагодзіў чароўным свістам шпакоў.
3) Вып'еш глыток крынічнай вады стомленасці як не было.
4) Гарэзамі-непакорамі называю нездарма свае першыя ў жыцці канькі бягуць яны па лёдзе ў розныябакі.
5) Слухаю птушак і разумею ў кожнай з іх адметная песня.", replyMarkup: QuestionKeyboardA14Again);
                break;

            case "A46":
                var QuestionKeyboardA15 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA15);
                break;

            case "42":
            case "43":
            case "44":
            case "45":
                var retryKeyboardA14 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA14") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове канструкцыя “[ ]: [ ].” выкарыстоўваецца ў наступных выпадках:
1.Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералічэннем.
2.Квадратныя дужківыкарыстоўваюцца для ўстаўкі дадатковай інфармацыі, якая не з’яўляецца часткай асноўнага тэксту, але якая можа быць карыснай для чытача.", replyMarkup: retryKeyboardA14);
                break;

            case "retryA14":
                var buttonsRetryA14 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2  ", "A42") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 3", "A43")},
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3", "A44")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A45")},
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A46")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, якія адпавядаюць прыведзенай схеме (улічыце, што знакі прыпынку паміж часткамі сказаў не пастаўлены): [ ]: [ ].
1) Важна ходзяць па балоце ў чырвоных ботах буслы нехта папрасіў зязюльку шчыра палічыць гады́.
2) Сакавік прыйшоў пад серабрысты шум ручаёў сакавік душу ўлагодзіў чароўным свістам шпакоў.
3) Вып'еш глыток крынічнай вады стомленасці як не было.
4) Гарэзамі-непакорамі называю нездарма свае першыя ў жыцці канькі бягуць яны па лёдзе ў розныябакі.
5) Слухаю птушак і разумею ў кожнай з іх адметная песня.", replyMarkup: buttonsRetryA14);
                break;

            //A15

            case "QuestionA15":
                var QuestionKeyboardA15Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4", "A47") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 5", "A48")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце нумары пропускаў, на месцы якіх у складаным сказе з рознымі відамі сувязі частак трэба паставіць коску:
Лірычныя мініяцюры Змітрака Бядулі 1) __  многія з якіх я перачытаў неаднойчы, нікога не пакінуць абыякавым па той прычыне 2) __  што ў іх пісьменнік сцвярджае наступныя важныя рэчы 3) __  кожны чалавек павінен жыць у гармоніі з самім сабой, павінен абавязкова стварыць уласнымі рукамі нешта цудоўнае 4) __  каб памножыць прыгажосць у свеце 5) __  і каб навучыцца цаніць дабро, прыўнесенае ў жыццё іншымі людзьмі.", replyMarkup: QuestionKeyboardA15Again);
                break;

            case "A47":
                var QuestionKeyboardA16 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA16") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA16);
                break;

            case "A48":
                var retryKeyboardA15 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове коска ставіцца ў наступных выпадках:
1.Паміж адносна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
2.Паміж часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3.Перад злучнікам а кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
4.Паміж развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
5.Паміж разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
6.Паміж групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
7.У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.", replyMarkup: retryKeyboardA15);
                break;

            case "retryA15":
                var buttonsRetryA15 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 4", "A47") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 5", "A48")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце нумары пропускаў, на месцы якіх у складаным сказе з рознымі відамі сувязі частак трэба паставіць коску:
Лірычныя мініяцюры Змітрака Бядулі 1) __  многія з якіх я перачытаў неаднойчы, нікога не пакінуць абыякавым па той прычыне 2) __  што ў іх пісьменнік сцвярджае наступныя важныя рэчы 3) __  кожны чалавек павінен жыць у гармоніі з самім сабой, павінен абавязкова стварыць уласнымі рукамі нешта цудоўнае 4) __  каб памножыць прыгажосць у свеце 5) __  і каб навучыцца цаніць дабро, прыўнесенае ў жыццё іншымі людзьмі.", replyMarkup: buttonsRetryA15);
                break;

            //A16

            case "QuestionA16":
                var QuestionKeyboardA16Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("пытальнік, кропка ", "A49") },
                    new[] { InlineKeyboardButton.WithCallbackData("коска,  працяжнік", "A50")},
                    new[] { InlineKeyboardButton.WithCallbackData("двукроп'е, пытальнік", "A51")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прапушчаныя ў сказе з простай мовай знакі прыпынку:
«Перад навальніцай самы́ 1) __ дзяліўся са мной ведамі сусед-рыбак2) __ падымаюцца на паверхню вады і плёскаюцца».", replyMarkup: QuestionKeyboardA16Again);
                break;

            case "A50":
                var QuestionKeyboardA17 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA17") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA17);
                break;

            case "A49":
            case "A51":
                var retryKeyboardA16 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA16") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове знакі прыпынкуў вельмі важныя для зразумеласці сказаў. Вось некаторыя агульныя правілы:
1.Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.2.    Пытальнік ставіцца на канцы пытальных сказаў.
3.Клічнік ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4.Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5.Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6.Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння прапускаў у тэксце.", replyMarkup: retryKeyboardA16);
                break;

            case "retryA16":
                var buttonsRetryA16 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("пытальнік, кропка ", "A49") },
                    new[] { InlineKeyboardButton.WithCallbackData("коска,  працяжнік", "A50")},
                    new[] { InlineKeyboardButton.WithCallbackData("двукроп'е, пытальнік", "A51")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прапушчаныя ў сказе з простай мовай знакі прыпынку:
«Перад навальніцай самы́ 1) __ дзяліўся са мной ведамі сусед-рыбак2) __ падымаюцца на паверхню вады і плёскаюцца».", replyMarkup: buttonsRetryA16);
                break;



            //A17

            case "QuestionA17":
                var QuestionKeyboardA17Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("кніга (П/п)аліны, рака (В/в)ілія", "A52") },
                    new[] { InlineKeyboardButton.WithCallbackData("дрэва (В/в)ольха", "A53")},
                    new[] { InlineKeyboardButton.WithCallbackData("кваліфікаваны (Х/х)ірург", "A54")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць з вялікай літары:", replyMarkup: QuestionKeyboardA17Again);
                break;

            case "A52":
                var QuestionKeyboardA18 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA18);
                break;

            case "A53":
            case "A54":
                var retryKeyboardA17 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA17") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове з вялікай літары пішуцца наступныя словы:
1. Першае слова ў сказе.
2. Першае слова пасля двукроп’я.
3. Усе словы ў назвах дзяржаўных і нацыянальных, ваенных і культурных рэліквій.
4. Усе словы ў поўных назвах ордэнаў, медалёў.
5. Аднаслоўныя ўласныя назвы і першае слова ў састаўных назвах кніг, газет, часопісаў, устаноў, арганізацый, заводаў, караблёў, марак машын.
6. Усе словы ў назвах дзяржаўных органаў і арганізацый.");
                break;

            case "retryA17":
                var buttonsRetryA17 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("кніга (П/п)аліны, рака (В/в)ілія", "A52") },
                    new[] { InlineKeyboardButton.WithCallbackData("дрэва (В/в)ольха", "A53")},
                    new[] { InlineKeyboardButton.WithCallbackData("кваліфікаваны (Х/х)ірург", "A54")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць з вялікай літары:", replyMarkup: buttonsRetryA17);
                break;

            //A18

            case "QuestionA18":
                var QuestionKeyboardA18Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("лаўка, шоўк, будаваў", "A55") },
                    new[] { InlineKeyboardButton.WithCallbackData("кансіліўм, кілаграмау", "A56")},
                    new[] { InlineKeyboardButton.WithCallbackData("аукцыён, люстэркау", "A57")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце правільна напісаныя словы (формы слоў):", replyMarkup: QuestionKeyboardA18Again);
                break;

            case "A55":
                var QuestionKeyboardA19 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA19") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA19);
                break;

            case "A56":
            case "A57":
                var retryKeyboardA18 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Правільна напісаныя словы (формы слоў) залежаць ад кантэксту і правіл беларускай мовы.  

Вось некаторыя прыклады:
1. У выразе “горад Слуцк”, слова “Слуцк” напісана правільна, бо гэта ўласная назва населенага пункта.
2. У выразе “Васілѐў партфель”, слова “Васілѐў” напісана правільна, бо гэта прыналежны прыметнік, утвораны ад уласнага назоўніка пры дапамозе суфікса -ѐў-.
3. У выразе “грыб падбярозавік”, слова “падбярозавік” напісана правільна, бо гэта назва грыба, якая з’яўляецца агульнай і пішацца з малой літары.
4. У выразе “запрасіў”, слова “запрасіў” напісана правільна.У выразе “лаўка”, слова “лаўка” напісана правільна.");
                break;

            case "retryA18":
                var buttonsRetryA18 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("лаўка, шоўк, будаваў", "A55") },
                    new[] { InlineKeyboardButton.WithCallbackData("кансіліўм, кілаграмау", "A56")},
                    new[] { InlineKeyboardButton.WithCallbackData("аукцыён, люстэркау", "A57")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце правільна напісаныя словы (формы слоў):", replyMarkup: buttonsRetryA18);
                break;

            //A19

            case "QuestionA19":
                var QuestionKeyboardA19Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("пад_езд, вераб_іны", "A58") },
                    new[] { InlineKeyboardButton.WithCallbackData("прыслоў_е, паштал_ён", "A59")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць апостраф:", replyMarkup: QuestionKeyboardA19Again);
                break;

            case "A58":
                var QuestionKeyboardA20 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA20") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA20);
                break;

            case "A59":
                var retryKeyboardA19 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA19") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Апостраф у беларускай мове пішацца ў наступных выпадках:

1. Пры асобным вымаўленні зычных з наступным галосным: е, ё, ю, я і націскным і.
2. У сярэдзіне слова пасля  б, в, м, п, ф,  г, к, х,  д, т і  р перад літарамі е, ё, і, ю, я.
3. У складаных словах з першай часткай двух-, трох-, чатырох-, шмат- перад літарамі е, ё, ю, я.

Апостраф не пішацца:

1.    Пасля ў (у нескладовага) перад літарамі е, ё, і, ю, я: абаўецца, саўюць, салаў.");
                break;

            case "retryA19":
                var buttonsRetryA19 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("пад_езд, вераб_іны", "A58") },
                    new[] { InlineKeyboardButton.WithCallbackData("прыслоў_е, паштал_ён", "A59")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, у якіх на месцы пропуску трэба пісаць апостраф:", replyMarkup: buttonsRetryA19);
                break;


            //A20

            case "QuestionA20":
                var QuestionKeyboardA20Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(дом) музей, (паў) Оршы", "A60") },
                    new[] { InlineKeyboardButton.WithCallbackData("(сена) касілка, (агра) турызм", "A61")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, якія трэба пісаць разам:", replyMarkup: QuestionKeyboardA20Again);
                break;

            case "A61":
                var QuestionKeyboardA21 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA21") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA21);
                break;

            case "A60":
                var retryKeyboardA20 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA20") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Правілы для напісання словаў разам у беларускай мове могуць быць наступнымі: 1

1. Словазлучэнні з прыназоўнікамі: 
Звычайна словы пішуцца разам з іх прыназоўнікамі, калі аны ўтвараюць адзін канструктыў, адносіны ці агульную з'яву (напрыклад, кнігазборчы, птушкападобны). 

2. Словазлучэнні з дзеяслоўнікамі: 
Пішуцца разам словы, якія змяшчаюць дзеяслоў і іншыя часціны мовы, калі яны выяўляюць аднолькавы працэс, дзеянне або стан (напрыклад, па-суседску, уздоўждарожны). 

3. Утварэнні ад канструктыўных словазлучэнняў: 
Словы, якія ўтвораныя ад словазлучэнняў або выразаў, пішуцца разам, калі яны маюць аднолькавую з'яву (напрыклад, калі-нібудзь, дзень за днём). 

4. Утварэнні ад прыметнікаў: 
Словы, якія ўтвораныя ад прыметнікаў і іншых часцін мовы, пішуцца разам, калі яны апісваюць адзін аб'ект, якасць ці стан (напрыклад, маладушны, жоўтасць). 

5. Складаныя выразы: 
Пішуцца разам словы, якія складаюцца з некалькі частак і маюць аднокавую з'яву ці ўтвараюць адзін лагічны блок (напрыклад, дзяржаўна-праўны, пяцігадовы). ");
                break;

            case "retryA20":
                var buttonsRetryA20 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(дом) музей, (паў) Оршы", "A60") },
                    new[] { InlineKeyboardButton.WithCallbackData("(сена) касілка, (агра) турызм", "A61")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце словы, якія трэба пісаць разам:", replyMarkup: buttonsRetryA20);
                break;

            //A21

            case "QuestionA21":
                var QuestionKeyboardA21Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(Паўднёва) Усходняя Азія", "A62") },
                    new[] { InlineKeyboardButton.WithCallbackData("(бульба) апрацоўчы цэх", "A63")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць праз злучок:", replyMarkup: QuestionKeyboardA21Again);
                break;

            case "A62":
                var QuestionKeyboardA22 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA22") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA22);
                break;

            case "A63":
                var retryKeyboardA21 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA21") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Праз злучок пішуцца: 

1) прыслоўі па-першае, па-другое, па-трэцяе і г. д.; 
2) прыслоўі з прыстаўкай па-, утвораныя ад давальнага склону поўных прыметнікаў і прыналежных займеннікаў з суфіксамі -аму, -яму, -ому, -му: па-зімоваму, па-мойму; прыслоўі на -ску, -цку ад ад- носных прыметнікаў: па-таварыску, па-брацку; прыслоўі з суфіксам -ы ад прыметнікаў: па-дзіцячы, па-мядзведжы; 
3) складаныя прыслоўі, утвораныя паўтарэннем аднолькавых, блізкіх або супрацьлеглых па значэнні слоў (можа далучацца прыстаўка): светла-светла, крыж-накрыж, сюды-туды, як-ніяк, больш-менш; 
4) прыслоўі з прыстаўкай абы- і постфіксамі -небудзь, -колечы: абы-дзе, дзе-небудзь, як-колечы; 
5) тэхнічны тэрмін на-гара, Некаторыя прыслоўі з прыстаўкамі падобны на спалучэнні назоўнікаў, займеннікаў і лічэбнікаў з прыназоўнікамі: звярнуў убок — звярнуў у бок лесу, зрабілі ўтрох — у трох кнігах. 

Для размежавання такіх напісанняў неабходна ведаць наступныя правілы: 

1) назоўнікі з прыназоўнікамі абазначаюць прадмет і маюць пры сабе паясняльныя словы: упаў з верху (чаго?) дрэва; 
2) лічэбнікі і займеннікі дапасуюцца да назоўнікаў: прыехалі ў дзвюх машынах; прыслоўі прымыкаюць да дзеясловаў: прыйшлі ўдзвюх.");
                break;

            case "retryA21":
                var buttonsRetryA21 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(Паўднёва) Усходняя Азія", "A62") },
                    new[] { InlineKeyboardButton.WithCallbackData("(бульба) апрацоўчы цэх", "A63")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць праз злучок:", replyMarkup: buttonsRetryA21);
                break;

            //A22

            case "QuestionA22":
                var QuestionKeyboardA22Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(усё) роўна купіць", "A64") },
                    new[] { InlineKeyboardButton.WithCallbackData("(што) дзень чытаць, адкрылі (за) летась", "A65")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць разам:", replyMarkup: QuestionKeyboardA22Again);
                break;

            case "A65":
                var QuestionKeyboardA23 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA23") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA23);
                break;

            case "A64":
                var retryKeyboardA22 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA22") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Словы “да, з, што, на, па, усё, за, у, як, без” могуць быць напісаныя разам у беларускай мове ў наступных выпадках:

1.Прыстаўкі: Гэтыя словы могуць быць напісаныя разам з іншымі словамі, калі яны выступаюць у ролі прыстаўкі. 
Напрыклад, “падаць”, “забыць”, “навучыць”, “пазнаёміцца”, “захаваць”, “увайсці”, “як-небудзь”, ""бездумны"".

2. Складаныя словы: Гэтыя словы могуць быць напісаныя разам у складаных словах. 
Напрыклад, “поўдзень”, “заўтра”, “навыгляд”, “поўгода”, “усёцела”, “заўсёды”, “усёдзень”, “якраз”, ""бездакорны"".

3. Словазлучэнні: Гэтыя словы могуць быць напісаныя разам у словазлучэннях. 
Напрыклад, “даўжыня”, “сшытак”, “навысотах”, “паўдарогі”, “усётакі”, “заўтрашні”, “усёдзённы”, “яктолькі”, ""бездакорны"".");
                break;

            case "retryA22":
                var buttonsRetryA22 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(усё) роўна купіць", "A64") },
                    new[] { InlineKeyboardButton.WithCallbackData("(што) дзень чытаць, адкрылі (за) летась", "A65")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх выдзеленыя словы трэба пісаць разам:", replyMarkup: buttonsRetryA22);
                break;


            //A23

            case "QuestionA23":
                var QuestionKeyboardA23Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(не) пабеленыя сцены", "A66") },
                    new[] { InlineKeyboardButton.WithCallbackData("мэбля (не) перавезена", "A67")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"ААдзначце прыклады, у якіх часціцу НЕ з выдзеленымі словамі трэба пісаць асобна:", replyMarkup: QuestionKeyboardA23Again);
                break;

            case "A67":
                var QuestionKeyboardA24 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA24") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA24);
                break;

            case "A66":
                var retryKeyboardA23 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA23") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Як часціца не, так і часціца ні могуць выражаць і адмоўнае, і сцвярджальнае значэнне — гэта залежыць ад пэўных выпадкаў ужывання. 

Часціца не адносіцца да адмоўных. 
Яе асноўнае значэнне — адмаўленне таго, што выражана словам, з якім яна ўжыта: Жыццё не можа надакучыць (Ф. Баторын). 
Не чутно салаўя, не ўскрыкваюць совы (Н. Маеўская). 

Сцвярджальнай часціца не становіцца ў наступных выпадках: 

1) калі яна ўжыта двойчы — пры кожнай частцы састаўнога выказніка: не мог не пайсці, не мог не выканаць; 
2) у спалучэннях хто не, дзе не, дзе толькі не, куды не і інш., калі яны ўжываюцца ў незалежных пытальна- клічных сказах: Чаго толькі не ведаў мой дзед! (С. Лобач); 
3) калі стаіць перад назоўнікам з прыназоўнікам без: зірнуў не без цікавасці, зрабіў не без карысці; 
4) калі стаіць пасля часціц ледзь, чуць, амаль або пасля злучніка пакуль: ледзь не спазніліся, пакуль не позна; 
5) у спалучэнні са словамі нельга, немагчыма: нельга не паверыць, немагчыма не прыйсці; 
6) у спалучэннях не больш за, не меньш за. 


Часціца ні — узмацняльная. Яе роля — узмацняць або адмаўленне, або сцвярджэнне. 

Часціца ні ўзмацняе адмаўленне, ужо выражанае ў сказе іншымі сродкамі: 

1) у адмоўных сказах, дзе адмаўленне выражана часціцай не або словамі нельга, няма, немагчыма: Няма ні душы на вуліцы; у спалучэннях тыпу ні на момант, ні на хвіліну; 
2) у няпоўных сказах, у якіх выказнік з адмоўем падразумяваецца: Ні гуку ў адказ; 
3) у ролі злучніка і ў адмоўных сказах пры пералічэнні аднародных членаў сказа: Ні дождж, ні жар для яго [каменя] не страшны (Я. Колас); 
4) у пабуджальных сказах загаднага характару: Ні з месца! Часціца ні ўзмацняе сцвярджэнне ў даданых сказах, дзе выкарыстоўваецца ў спалучэнні з займеннікамі і прыслоўямі: дзе ні, хто ні, што ні, куды ні, адкуль ні і інш. У такія спалучэнні можа ўваходзіць часціца б (дзе б ні, хто б ні і г. д.): Толькі дзе за морам ні жыў я, Беларусь мая снілася мне (Г. Бураўкін). 

Часціца ні ўжываецца ў фразеалагізмах са значэннем няпэўнасці: ні тое ні сёе, ні рыба ні мяса і інш. 
Неабходна адрозніваць спалучэнні не раз (многа разоў) і ні разу (ні аднаго разу); не адзін (некалькі) і ні адзін (ніхто).");
                break;

            case "retryA23":
                var buttonsRetryA23 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("(не) пабеленыя сцены", "A66") },
                    new[] { InlineKeyboardButton.WithCallbackData("мэбля (не) перавезена", "A67")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прыклады, у якіх часціцу НЕ з выдзеленымі словамі трэба пісаць асобна:", replyMarkup: buttonsRetryA23);
                break;

            //A24


            case "QuestionA24":
                var QuestionKeyboardA24Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("створаны на ўзор, два алоўкі", "A68") },
                    new[] { InlineKeyboardButton.WithCallbackData("ажаніцца на аднакурсніцы", "A69")},
                    new[] { InlineKeyboardButton.WithCallbackData("раскласці па палічкам", "A70")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сінтаксічныя канструкцыі, якія адпавядаюць нормам беларускай літаратурнай мовы:", replyMarkup: QuestionKeyboardA24Again);
                break;

            case "A68":
                var QuestionKeyboardA25 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA25") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA25);
                break;

            case "693":
            case "A70":
                var retryKeyboardA24 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA24") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Сінтаксічныя канструкцыі, якія адпавядаюць нормам беларускай літаратурнай мовы, вельмі розныя і залежаць ад кантэксту.

Вось некаторыя прыклады:

1. Дзякаваць сябру: Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. 
У беларускай мове можна выкарыстоўваць формулу “дзякаваць + даватальны прыслоўе” для выказвання падзякі.

2. За пяць метраў ад магазіна: 
Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. 
Гэта ўласцівая канструкцыя для выказвання месцазнаходжання ў беларускай мове.

3. Жыць у суседстве: 
Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. 
Гэта ўласцівая канструкцыя для выказвання месцазнаходжання ў беларускай мове.

4. Лепшы ўсіх: 
Гэта сінтаксічная канструкцыя, якая адпавядае нормам беларускай літаратурнай мовы. 
Гэта канструкцыя, дзе прыслоўе “лепшы” адносіцца да ўсіх (прыслоўе займенніка “ўсіх”) у сэнсе лепшага.");
                break;

            case "retryA24":
                var buttonsRetryA24 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("створаны на ўзор, два алоўкі", "A68") },
                    new[] { InlineKeyboardButton.WithCallbackData("ажаніцца на аднакурсніцы", "A69")},
                    new[] { InlineKeyboardButton.WithCallbackData("раскласці па палічкам", "A70")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сінтаксічныя канструкцыі, якія адпавядаюць нормам беларускай літаратурнай мовы:", replyMarkup: buttonsRetryA24);
                break;

            //A25

            case "QuestionA25":
                var QuestionKeyboardA25Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2", "A71") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4, 5", "A72")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску абавязкова ставіцца працяжнік:

1) Пасля завеі лес ＿ што казка.
2) Мая бабуля ＿ не доктар, але дакладна ведае пра лекавыя магчымасці ледзь не кожнай зёлкі.
3) Поле ＿ вокам не акінуць.
4) Вышываць ＿ займальны занятак.
5) Школа ＿ наш другі дом.", replyMarkup: QuestionKeyboardA25Again);
                break;

            case "A72":
                var QuestionKeyboardA26 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA26") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA26);
                break;

            case "A71":
                var retryKeyboardA25 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA25") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Працяжнік у беларускай мове ставіцца на месцы пропуску ў наступных выпадках:

1. Дзейнік і выказнік выражаны назоўнікамі ў назоўным склоне: ""Юравічы – паселішча вельмі старажытнае"".

2.    Дзейнік і выказнік выражаны колькаснымі лічэбнікамі ў Н. с. ці адзін з іх выражаны назоўнікам, а другі – лічэбнікам у Н. с.: ""Плошча поля – два гектары"".

3.    Абодва галоўныя члены ці адзін з іх выражаны інфінітывам (неазначальнай формай дзеяслова): ""Жыць – Радзіме служыць"".

4.    Выказнік выражаны ўстойлівым спалучэннем слоў (фразеалагізмам): ""Нівы – вокам не абняць"".

5. Перад выказнікам стаяць словы гэта, вось, значыць, гэта значыць: ""Плошча поля – гэта наша радзіма, наш дом"".");
                break;

            case "retryA25":
                var buttonsRetryA25 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2", "A71") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4, 5", "A72")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску абавязкова ставіцца працяжнік:

1) Пасля завеі лес ＿ што казка.
2) Мая бабуля ＿ не доктар, але дакладна ведае пра лекавыя магчымасці ледзь не кожнай зёлкі.
3) Поле ＿ вокам не акінуць.
4) Вышываць ＿ займальны занятак.
5) Школа ＿ наш другі дом.", replyMarkup: buttonsRetryA25);
                break;

            //A26

            case "QuestionA26":
                var QuestionKeyboardA26Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4, 5", "A73") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3", "A74")},
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A75")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A76")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 5", "A77")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Зямлю беларускую са сцежкай вузкай, ды з чырвонай рабінай заву я любімай.

2) 3 ільнянога валакна некалі не толькі стваралі найпрыгажэйшыя прадметы побыту, але і рабілі цацкі для дзяцей.

3) Незвычайны лёс Ефрасінні Полацкай заўжды прыцягваў увагу гісторыкаў, мастакоў, і скульптараў, і пісьменнікаў.

4) У вузенькі паясок роднай ракі няхай заўжды любуюцца сабой з высокага, летняга неба звонкія жаўрукі.

5) Люблю я пахадзіць па сунічных палянах, ці па грыбных баравінах.", replyMarkup: QuestionKeyboardA26Again);
                break;

            case "A74":
                var QuestionKeyboardA27 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA27") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA27);
                break;

            case "A73":
            case "A75":
            case "A76":
            case "A77":
                var retryKeyboardA26 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA26") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Пунктуацыя ў беларускай мове вельмі важная для правільнай пастаноўкі інтанацыі і зразумеласці сказаў. 

Вось некаторыя агульныя правілы пунктуацыі:

1. Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.
2. Пытальнік ставіцца на канцы пытальных сказаў.
3. Клічнік ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4. Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5. Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6. Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння пропускаў у тэксце.");
                break;

            case "retryA26":
                var buttonsRetryA26 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4, 5", "A73") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3", "A74")},
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A75")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A76")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 5", "A77")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Зямлю беларускую са сцежкай вузкай, ды з чырвонай рабінай заву я любімай.

2) 3 ільнянога валакна некалі не толькі стваралі найпрыгажэйшыя прадметы побыту, але і рабілі цацкі для дзяцей.

3) Незвычайны лёс Ефрасінні Полацкай заўжды прыцягваў увагу гісторыкаў, мастакоў, і скульптараў, і пісьменнікаў.

4) У вузенькі паясок роднай ракі няхай заўжды любуюцца сабой з высокага, летняга неба звонкія жаўрукі.

5) Люблю я пахадзіць па сунічных палянах, ці па грыбных баравінах.", replyMarkup: buttonsRetryA26);
                break;

            //A27

            case "QuestionA27":
                var QuestionKeyboardA27Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A78") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5", "A79")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Майскія яблыні, што нявесты.
2) Пабеглі паўсюль вузенькія, быццам істужкі, вясеннія ручайкі.
3) Увесь чэрвень сонейка грэла, як след.
4) Шматгадовыя назіранні людзей за з'явамі прыроды сёння падаюцца як прыкметы.
5) Спіць у небе, нібы ў сіняй калысцы, белая хмарка", replyMarkup: QuestionKeyboardA27Again);
                break;

            case "A79":
                var QuestionKeyboardA28 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA28") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA28);
                break;

            case "A78":
                var retryKeyboardA27 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA27") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Пунктуацыя ў беларускай мове вельмі важная для правільнай пастаноўкі інтанацыі і зразумеласці сказаў. 

Вось некаторыя агульныя правілы пунктуацыі:

1. Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.
2. Пытальнік ставіцца на канцы пытальных сказаў.
3. Клічнік ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4. Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5. Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6. Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння пропускаў у тэксце.");
                break;

            case "retryA27":
                var buttonsRetryA27 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3", "A78") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 4, 5", "A79")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Майскія яблыні, што нявесты.
2) Пабеглі паўсюль вузенькія, быццам істужкі, вясеннія ручайкі.
3) Увесь чэрвень сонейка грэла, як след.
4) Шматгадовыя назіранні людзей за з'явамі прыроды сёння падаюцца як прыкметы.
5) Спіць у небе, нібы ў сіняй калысцы, белая хмарка", replyMarkup: buttonsRetryA27);
                break;

            //A28

            case "QuestionA28":
                var QuestionKeyboardA28Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3 , 4", "A80") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A81")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Падарыла нам восень, апрача куфра з золатам, карункавыя кросны павучкоў.
2) Рамантычным творам з'яўляецца, заснаваная на беларускім фальклоры, балада Адама Міцкевіча «Свіцязянка».
3) Абарваўшы шмат лістоў з бярозкі, ветрык узяўся за вяз
4) Святаяннік, ці інакш зверабой, лічыцца лекавай раслінай.
5) Дарэмна праз паўстанкі экспрэсы ідуць, не спыняючыся", replyMarkup: QuestionKeyboardA28Again);
                break;

            case "A80":
                var QuestionKeyboardA29 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA29") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA29);
                break;

            case "A81":
                var retryKeyboardA28 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA28") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Пунктуацыя ў беларускай мове вельмі важная для правільнай пастаноўкі інтанацыі і зразумеласці сказаў. 

Вось некаторыя агульныя правілы пунктуацыі:

1. Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.
2. Пытальнік ставіцца на канцы пытальных сказаў.
3. Клічнік ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4. Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5. Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6. Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння пропускаў у тэксце.");
                break;

            case "retryA28":
                var buttonsRetryA28 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3 , 4", "A80") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A81")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Падарыла нам восень, апрача куфра з золатам, карункавыя кросны павучкоў.
2) Рамантычным творам з'яўляецца, заснаваная на беларускім фальклоры, балада Адама Міцкевіча «Свіцязянка».
3) Абарваўшы шмат лістоў з бярозкі, ветрык узяўся за вяз
4) Святаяннік, ці інакш зверабой, лічыцца лекавай раслінай.
5) Дарэмна праз паўстанкі экспрэсы ідуць, не спыняючыся", replyMarkup: buttonsRetryA28);
                break;

            //A29

            case "QuestionA29":
                var QuestionKeyboardA29Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 5", "A82") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2 ", "A83")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4 ", "A84")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску (пропускаў) трэба паставіць коску (коскі):

1) Хлеб＿ ці не адзіны?＿ з'яўляецца інтэрнацыянальнай стравай.
2) Амаль ＿ ніколі не бываю адзінокім, бо ўсё жыццё сябрую з кнігай.
3) Заўжды ＿ на маю думку ＿ дабро перамагаць павінна.
4) У кожным раёне Беларусі ＿ паверце＿ ёсць куточак з дзівоснай прыродай.
5) У вёсцы зранку ціха, аднак＿ у полі гуляе і варожыць на рамонках ветрык неўгамонны.", replyMarkup: QuestionKeyboardA29Again);
                break;

            case "A84":
                var QuestionKeyboardA30 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA30") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA30);
                break;

            case "A82":
            case "A83":
                var retryKeyboardA29 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA29") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове коска ставіцца ў наступных выпадках:

1. Паміж адносна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
2. Паміж часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва, калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3. Перад злучнікам а кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
4. Паміж развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
5. Паміж разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
6. Паміж групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
7. У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.");
                break;

            case "retryA29":
                var buttonsRetryA29 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 5", "A82") },
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2 ", "A83")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4 ", "A84")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, у якіх на месцы пропуску (пропускаў) трэба паставіць коску (коскі):

1) Хлеб＿ ці не адзіны?＿ з'яўляецца інтэрнацыянальнай стравай.
2) Амаль ＿ ніколі не бываю адзінокім, бо ўсё жыццё сябрую з кнігай.
3) Заўжды ＿ на маю думку ＿ дабро перамагаць павінна.
4) У кожным раёне Беларусі ＿ паверце＿ ёсць куточак з дзівоснай прыродай.
5) У вёсцы зранку ціха, аднак＿ у полі гуляе і варожыць на рамонках ветрык неўгамонны.", replyMarkup: buttonsRetryA29);
                break;


            //A30

            case "QuestionA30":
                var QuestionKeyboardA30Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 3", "A85") },
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A86")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназлучаныя сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Каманда - і на ваду спускаецца беласнежны карабель.
2) Адзін і пры месяцы робіць, а другі і пры сонцы спіць.
3) Бясхмарна і цёпла.
4) Зноў ападае-кру́жыцца лістота, і збіраюцца гусі на сейм перад дальняй дарогай.
5) Поле туманнае спала ў цёплай расе і не спяшаўся прачынацца старасвецкі лес.", replyMarkup: QuestionKeyboardA30Again);
                break;

            case "A85":
                var QuestionKeyboardA31 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA31") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA31);
                break;


            case "A86":
                var retryKeyboardA30 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA30") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У складаназлучаных сказах у беларускай мове пунктуацыя вельмі важная. 

Вось некаторыя агульныя правілы:

1. Між часткамі складаназлучанага сказа ставіцца коска, калі ў другой частцы сказа заключаецца значэнне выніку, наступства, рэзкага супрацьпастаўлення, асабліва перад злучнікам ""і"".
2. Між аднасна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3. Між часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва, калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
4. Перад злучнікам “а” кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
5. Між развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
6. Між разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
7. Між групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
8. У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.");
                break;

            case "retryA30":
                var buttonsRetryA30 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2, 3", "A85") },
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A86")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназлучаныя сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Каманда - і на ваду спускаецца беласнежны карабель.
2) Адзін і пры месяцы робіць, а другі і пры сонцы спіць.
3) Бясхмарна і цёпла.
4) Зноў ападае-кру́жыцца лістота, і збіраюцца гусі на сейм перад дальняй дарогай.
5) Поле туманнае спала ў цёплай расе і не спяшаўся прачынацца старасвецкі лес.", replyMarkup: buttonsRetryA30);
                break;

            //A31

            case "QuestionA31":
                var QuestionKeyboardA31Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3, 4", "A87") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A88")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A89")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A90")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназалежныя сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Кажуць, што, калі бяроза раней за вольху распусціцца, то лета будзе сухое і сонечнае.
2) Японская пяцірадковая вершаваная форма, якая ў нерыфмаваным выглядзе стала выкарыстоўвацца для навучання творчаму мысленню мае назву “сінквейн”.
3) Калодзеж калодзежам не назавеш, калі з яго не бяруць вады.
4) Каб сабраць адзін кілаграм нектару пчале даводзіцца абляцець да пяцідзесяці тысяч кветак.
5) Народныя песні нясуць у сабе ўсё тое, чым былі напоўнены думы чалавека і чым жыла яго душа.", replyMarkup: QuestionKeyboardA31Again);
                break;

            case "A88":
                var QuestionKeyboardA32 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA32") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA32);
                break;

            case "A87":
            case "A89":
            case "A90":
                var retryKeyboardA31 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA31") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У складаназалежных сказах у беларускай мове пунктуацыя вельмі важная. 

Вось некаторыя агульныя правілы:

1. Між часткамі складаназалежнага сказа ставіцца коска, калі ў другой частцы сказа заключаецца значэнне выніку, наступства, рэзкага супрацьпастаўлення, асабліва перад злучнікам ""і"".
2. Між аднасна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3. Між часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва, калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
4. Перад злучнікам “а” кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
5. Між развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
6. Між разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва, калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
7. Між групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
8. У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.");
                break;

            case "retryA31":
                var buttonsRetryA31 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 3, 4", "A87") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 5", "A88")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A89")},
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A90")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце складаназалежныя сказы, у якіх няма парушэння пунктуацыйных нормаў:

1) Кажуць, што, калі бяроза раней за вольху распусціцца, то лета будзе сухое і сонечнае.
2) Японская пяцірадковая вершаваная форма, якая ў нерыфмаваным выглядзе стала выкарыстоўвацца для навучання творчаму мысленню мае назву “сінквейн”.
3) Калодзеж калодзежам не назавеш, калі з яго не бяруць вады.
4) Каб сабраць адзін кілаграм нектару пчале даводзіцца абляцець да пяцідзесяці тысяч кветак.
5) Народныя песні нясуць у сабе ўсё тое, чым былі напоўнены думы чалавека і чым жыла яго душа.", replyMarkup: buttonsRetryA31);
                break;

            //A32


            case "QuestionA32":
                var QuestionKeyboardA32Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2", "A91") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4, 5", "A92")},
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A93")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A94")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, якія адпавядаюць прыведзенай схеме  (улічыце, што знакі прыпынку паміж часткамі сказаў не пастаўлены): [ ]: [ ].

1) Гляджу на вёску свайго дзяцінства і разумею я нарадзілася ў казачным кутку.
2) Нездарма выплываюць сёння камбайны на бясконцы прастор набыло ўжо жыта колер янтарнага сонца.
3) Ужо жыта каласіцца ўжо сінеюць васількі.
4) Зайшоў верасень у сады цяністыя ўпалі на дол грушы залацістыя.
5) Залацяцца шышкі на высокіх елках па ствалах соснаў сцякае бурштынавымі кроплямі жывіца.", replyMarkup: QuestionKeyboardA32Again);
                break;

            case "A91":
                var QuestionKeyboardA33 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA33") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA33);
                break;

            case "A92":
            case "A93":
            case "A94":
                var retryKeyboardA32 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA32") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове канструкцыя “[ ]: [ ].” выкарыстоўваецца ў наступных выпадках:

1. Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералічэннем.
2. Квадратныя дужківыкарыстоўваюцца для ўстаўкі дадатковай інфармацыі, якая не з’яўляецца часткай асноўнага тэксту, але якая можа быць карыснай для чытача.");
                break;

            case "retryA32":
                var buttonsRetryA32 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 2", "A91") },
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4, 5", "A92")},
                    new[] { InlineKeyboardButton.WithCallbackData("4, 5", "A93")},
                    new[] { InlineKeyboardButton.WithCallbackData("3, 4", "A94")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце сказы, якія адпавядаюць прыведзенай схеме  (улічыце, што знакі прыпынку паміж часткамі сказаў не пастаўлены): [ ]: [ ].

1) Гляджу на вёску свайго дзяцінства і разумею я нарадзілася ў казачным кутку.
2) Нездарма выплываюць сёння камбайны на бясконцы прастор набыло ўжо жыта колер янтарнага сонца.
3) Ужо жыта каласіцца ўжо сінеюць васількі.
4) Зайшоў верасень у сады цяністыя ўпалі на дол грушы залацістыя.
5) Залацяцца шышкі на высокіх елках па ствалах соснаў сцякае бурштынавымі кроплямі жывіца.", replyMarkup: buttonsRetryA32);
                break;


            //A33

            case "QuestionA33":
                var QuestionKeyboardA33Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A95") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3 , 5", "A96")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце нумары пропускаў, на месцы якіх ускладаным сказе з рознымі відамі сувязі частак трэба паставіць коску:
Вандруючы па Браслаўшчыне, не магунадзівіцца 1) __ стаяць паўсюль у мудрым задуменні гордыя, велічныя лясы, зіхацяць у акружэнні задуменных лясоў блакітныя азеры    2) __  кожнае з якіх сваёй невыказнай прыгажосцю нагадвае 3) __ што чалавек знаходзіцца сярод некранутай прыроды 4) __ і што прырода гэта патрабуе самых беражлівых адносін   5) __ бо без яе не існуе жыцця.", replyMarkup: QuestionKeyboardA33Again);
                break;

            case "A96":
                var QuestionKeyboardA34 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA34") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA34);
                break;

            case "A95":
                var retryKeyboardA33 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA33") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове коска ставіцца ў наступных выпадках:

1. Паміж адносна незалежнымі састаўнымі часткамі сказа, якія аб’ядноўваюцца ў адзін складаны сказ без злучнікаў, а самі часткі разгорнутыя і (або) маюць свае знакі прыпынку.
2. Паміж часткамі складанага сказа, якія звязваюцца паміж сабой далучальнымі злучнікамі але, толькі, аднак, усё ж, тым не менш і інш., асабліва калі гэтыя часткі разгорнутыя і (або) маюць свае знакі прыпынку.
3. Перад злучнікам а кропка з коскай ставіцца толькі ў тым выпадку, калі састаўныя часткі, якія звязваюцца гэтым злучнікам, разгорнутыя і маюць свае знакі прыпынку.
4. Паміж развітымі аднароднымі членамі, калі пры адным з іх ёсць коскі.
5. Паміж разгорнутымі даданымі часткамі складаназалежнага сказа, падпарадкаванымі адной і той жа галоўнай, калі паміж даданымі няма злучальнага злучніка, асабліва калі ў сярэдзіне такіх даданых ёсць свае даданыя часткі.
6. Паміж групамі адносна незалежных састаўных частак бяззлучнікавага сказа, а таксама паміж групамі даданых частак, якія адносяцца да адной галоўнай.
7. У канцы рубрык пералічэння, калі яны не з’яўляюцца самастойнымі сказамі, але дастаткова разгорнутыя і (або) маюць свае знакі прыпынку.");
                break;

            case "retryA33":
                var buttonsRetryA33 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("1, 4", "A95") },
                    new[] { InlineKeyboardButton.WithCallbackData("2, 3 , 5", "A96")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце нумары пропускаў, на месцы якіх ускладаным сказе з рознымі відамі сувязі частак трэба паставіць коску:
Вандруючы па Браслаўшчыне, не магунадзівіцца 1) __ стаяць паўсюль у мудрым задуменні гордыя, велічныя лясы, зіхацяць у акружэнні задуменных лясоў блакітныя азеры    2) __  кожнае з якіх сваёй невыказнай прыгажосцю нагадвае 3) __ што чалавек знаходзіцца сярод некранутай прыроды 4) __ і што прырода гэта патрабуе самых беражлівых адносін   5) __ бо без яе не існуе жыцця.", replyMarkup: buttonsRetryA33);
                break;


            //A34


            case "QuestionA34":
                var QuestionKeyboardA34Again = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("двукроп'е, кропка", "A97") },
                    new[] { InlineKeyboardButton.WithCallbackData("кропка, пытальнік", "A98")},
                    new[] { InlineKeyboardButton.WithCallbackData("коска, працяжнік", "A99")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прапушчаныя ў сказе з простай мовай знакі прыпынку:
«Створаныя на беларускай мове арабскім пісьмом рукапісныя кнігі  1) __  паведаміў экскурсавод   2) __  называюцца кітабамі».", replyMarkup: QuestionKeyboardA34Again);
                break;

            case "A99":
                var QuestionKeyboardA35 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionA35") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionKeyboardA35);
                break;

            case "A98":
            case "A97":
                var retryKeyboardA34 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Паспрабуй яшчэ раз", "retryA34") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"У беларускай мове знакі прыпынкуў вельмі важныя для зразумеласці сказаў.

Вось некаторыя агульныя правілы:

1. Кропка ставіцца на канцы сказаў, якія выказваюць закончаную думку.
2. Пытальнік ставіцца на канцы пытальных сказаў.
3. Клічнік ставіцца на канцы сказаў, якія выказваюць эмоцыі, наказы, прызывы.
4. Коска выкарыстоўваецца для абазначэння аднародных членаў сказа, а таксама для выдзялення частак сказа, якія маюць асобны сінтаксічны статус.
5. Двукроп’е ставіцца перад непасрэднім цытаваннем, а таксама перад пералікамі.
6. Працяжнік выкарыстоўваецца для выдзялення частак сказа, якія маюць асобны сінтаксічны статус, а таксама для абазначэння прапускаў у тэксце.");
                break;

            case "retryA34":
                var buttonsRetryA34 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("двукроп'е, кропка", "A97") },
                    new[] { InlineKeyboardButton.WithCallbackData("кропка, пытальнік", "A98")},
                    new[] { InlineKeyboardButton.WithCallbackData("коска, працяжнік", "A99")}
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Адзначце прапушчаныя ў сказе з простай мовай знакі прыпынку:
«Створаныя на беларускай мове арабскім пісьмом рукапісныя кнігі  1) __  паведаміў экскурсавод   2) __  называюцца кітабамі».", replyMarkup: buttonsRetryA34);
                break;

            //A35

            case "QuestionA35":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Малайчына😊
Працяг у распрацоўцы🫣");
                break;


            //B


            case "partB":
                await botClient.SendTextMessageAsync(chatId, "Пераход да Часткі В");

                var buttonspartB = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("гардэроб", "B_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("світэр", "trueB_2") },
                    new[] { InlineKeyboardButton.WithCallbackData("востравугольны", "B_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("пачарнелы", "B_4") },
                    new[] { InlineKeyboardButton.WithCallbackData("драўляны", "B_5") }
                });

                await botClient.SendTextMessageAsync(chatId, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonspartB);
               
                break;
            case "B_1":
            case "B_3":
            case "B_4":
            case "B_5":
                var retryKeyboardPartB = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrypartB") }
                });
                await botClient.SendTextMessageAsync(chatId, "❌няправільна❌", replyMarkup: retryKeyboardPartB);
                break;

            case "trueB_2":
                var buttonsCorrectSpellingtrueB_2 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("світор", "B1_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("світар", "trueB2_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("світер", "B3_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("світяр", "B4_1") },
                    new[] { InlineKeyboardButton.WithCallbackData("світр", "B5_1") }
                });

                await botClient.SendTextMessageAsync(chatId, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_2);
                break;

            //B2

            case "QuestionB2":
                var buttonspartB2 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("земляк", "trueB_6") },
                    new[] { InlineKeyboardButton.WithCallbackData("вогнетрывалы", "B_7") },
                    new[] { InlineKeyboardButton.WithCallbackData("смешнаваты", "B_8") },
                    new[] { InlineKeyboardButton.WithCallbackData("роўнядзь", "B_9") },
                    new[] { InlineKeyboardButton.WithCallbackData("герой", "B_10") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonspartB2);
                break;

            case "B_7":
            case "B_8":
            case "B_9":
            case "B_10":
                var retryKeyboardQuestionB2 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB2") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB2);
                break; ;

            case "trueB_6":
                var buttonsCorrectSpellingtrueB_6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("зямлек", "B1_2") },
                    new[] { InlineKeyboardButton.WithCallbackData("зямляк", "trueB2_2") },
                    new[] { InlineKeyboardButton.WithCallbackData("зямлек", "B3_2") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_6);
                break;

            case "B1_2":
            case "B3_2":
                var retryKeyboardtrueB_6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_6") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_6);
                break;

            case "trueB2_2":
                var QuestionB3Keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB3") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB3Keyboard);
                break;

            case "retryQuestionB2":
                var retryQuestionB2 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("земляк", "trueB_6") },
                    new[] { InlineKeyboardButton.WithCallbackData("вогнетрывалы", "B_7") },
                    new[] { InlineKeyboardButton.WithCallbackData("смешнаваты", "B_8") },
                    new[] { InlineKeyboardButton.WithCallbackData("роўнядзь", "B_9") },
                    new[] { InlineKeyboardButton.WithCallbackData("герой", "B_10") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryQuestionB2);
                break;

            case "retrytrueB_6":
                var retrybuttonsCorrectSpellingtrueB_6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("зямлек", "B1_2") },
                    new[] { InlineKeyboardButton.WithCallbackData("зямляк", "trueB2_2") },
                    new[] { InlineKeyboardButton.WithCallbackData("зямлек", "B3_2") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retrybuttonsCorrectSpellingtrueB_6);
                break;

            //B3

            case "QuestionB3":
                var buttonspartB3 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("дудка", "B_11") },
                    new[] { InlineKeyboardButton.WithCallbackData("паказчык", "B_12") },
                    new[] { InlineKeyboardButton.WithCallbackData("спрошчаны", "B_13") },
                    new[] { InlineKeyboardButton.WithCallbackData("радасны", "B_14") },
                    new[] { InlineKeyboardButton.WithCallbackData("даклаччык", "trueB_15") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonspartB3);
                break;

            case "B_11":
            case "B_12":
            case "B_13":
            case "B_14":
                var retryKeyboardQuestionB3 = new InlineKeyboardMarkup(new[]
        {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB3") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB3);
                break; ;

            case "trueB_15":
                var buttonsCorrectSpellingtrueB_15 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дакладчык", "trueB1_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("дакладнік", "B2_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("даклачік", "B3_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("даклачэк", "B4_3") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_15);
                break;

            case "B2_3":
            case "B3_3":
            case "B4_3":
                var retryKeyboardtrueB_15 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_15);
                break;

            case "trueB1_3":
                var QuestionB4Keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB4") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB4Keyboard);
                break;

            case "retryQuestionB3":
                var retryQuestionB3 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дудка", "B_11") },
                    new[] { InlineKeyboardButton.WithCallbackData("паказчык", "B_12") },
                    new[] { InlineKeyboardButton.WithCallbackData("спрошчаны", "B_13") },
                    new[] { InlineKeyboardButton.WithCallbackData("радасны", "B_14") },
                    new[] { InlineKeyboardButton.WithCallbackData("даклаччык", "trueB_15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryQuestionB3);
                break;

            case "retrytrueB_15":
                var retrybuttonsCorrectSpellingtrueB_15 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дакладчык", "trueB1_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("дакладнік", "B2_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("даклачік", "B3_3") },
                    new[] { InlineKeyboardButton.WithCallbackData("даклачэк", "B4_3") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retrybuttonsCorrectSpellingtrueB_15);
                break;

            //B4

            case "QuestionB4":
                var buttonspartB4 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("хваляванне", "B_16") },
                    new[] { InlineKeyboardButton.WithCallbackData("рассадзіць", "B_17") },
                    new[] { InlineKeyboardButton.WithCallbackData("кассір", "trueB_18") },
                    new[] { InlineKeyboardButton.WithCallbackData("ліццё", "B_19") },
                    new[] { InlineKeyboardButton.WithCallbackData("маліннік", "B_20") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonspartB4);
                break;

            case "B_16":
            case "B_17":
            case "B_19":
            case "B_20":
                var retryKeyboardQuestionB4 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB4") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB4);
                break; ;

            case "trueB_18":
                var buttonsCorrectSpellingtrueB_18 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("косір", "B1_4") },
                    new[] { InlineKeyboardButton.WithCallbackData("кассір", "B2_4") },
                    new[] { InlineKeyboardButton.WithCallbackData("касір", "trueB3_4") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_18);
                break;

            case "B1_4":
            case "B2_4":
                var retryKeyboardtrueB_18 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_18);
                break;

            case "trueB3_4":
                var QuestionB5Keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB5") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB5Keyboard);
                break;

            case "retryQuestionB4":
                var retryQuestionB4 = new InlineKeyboardMarkup(new[]
                {
                     new[] { InlineKeyboardButton.WithCallbackData("хваляванне", "B_16") },
                     new[] { InlineKeyboardButton.WithCallbackData("рассадзіць", "B_17") },
                     new[] { InlineKeyboardButton.WithCallbackData("кассір", "trueB_18") },
                     new[] { InlineKeyboardButton.WithCallbackData("ліццё", "B_19") },
                     new[] { InlineKeyboardButton.WithCallbackData("маліннік", "B_20") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryQuestionB4);
                break;

            case "retrytrueB_18":
                var retrybuttonsCorrectSpellingtrueB_18 = new InlineKeyboardMarkup(new[]
                {
                     new[] { InlineKeyboardButton.WithCallbackData("косір", "B1_4") },
                     new[] { InlineKeyboardButton.WithCallbackData("кассір", "B2_4") },
                     new[] { InlineKeyboardButton.WithCallbackData("касір", "trueB3_4") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retrybuttonsCorrectSpellingtrueB_18);
                break;


            //B5

            case "QuestionB5":
                var buttonspartB5 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("цукерцы", "trueB_21") },
                    new[] { InlineKeyboardButton.WithCallbackData("цукерке", "B_22") },
                    new[] { InlineKeyboardButton.WithCallbackData("цукерка", "B_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("цукерку", "B_24") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберыце  правільнае напісанненазоўніка ЦУКЕРКА ў форме давальнага склону адзіночнага ліку:", replyMarkup: buttonspartB5);
                break;

            case "B_22":
            case "B_23":
            case "B_24":
                var retryKeyboardQuestionB5 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB5") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB5);
                break; ;

            case "trueB_21":
                var QuestionB6Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB6") }
                 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB6Keyboard);
                break;

            case "retryQuestionB5":
                var retryQuestionB5 = new InlineKeyboardMarkup(new[]
                {
                     new[] { InlineKeyboardButton.WithCallbackData("цукерцы", "trueB_21") },
                     new[] { InlineKeyboardButton.WithCallbackData("цукерке", "B_22") },
                     new[] { InlineKeyboardButton.WithCallbackData("цукерка", "B_23") },
                     new[] { InlineKeyboardButton.WithCallbackData("цукерку", "B_24") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберыце  правільнае напісанненазоўніка ЦУКЕРКА ў форме давальнага склону адзіночнага ліку.", replyMarkup: retryQuestionB5);
                break;



            //B6

            case "QuestionB6":
                var buttonspartB6 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("восямнаццаць", "B_25") },
                    new[] { InlineKeyboardButton.WithCallbackData("васемнаццаць", "B_26") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнацаць", "B_27") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнаццаць", "trueB_28") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнаццать", "B_29") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнадцаць", "B_30") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберыце правільнае напісанне ліка 18 словам:", replyMarkup: buttonspartB6);
                break;

            case "B_25":
            case "B_26":
            case "B_27":
            case "B_29":
            case "B_30":
                var retryKeyboardQuestionB6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB6") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB6);
                break; ;

            case "trueB_28":
                var QuestionB7Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB7") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB7Keyboard);
                break;

            case "retryQuestionB6":
                var retryQuestionB6 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("восямнаццаць", "B_25") },
                    new[] { InlineKeyboardButton.WithCallbackData("васемнаццаць", "B_26") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнацаць", "B_27") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнаццаць", "trueB_28") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнаццать", "B_29") },
                    new[] { InlineKeyboardButton.WithCallbackData("васямнадцаць", "B_30") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберыце правільнае напісанне ліка 18 словам:", replyMarkup: retryQuestionB6);
                break;



            //B7

            case "QuestionB7":
                var buttonspartB7 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("крыніцы", "B_31") },
                    new[] { InlineKeyboardButton.WithCallbackData("вадзіцай", "B_32") },
                    new[] { InlineKeyboardButton.WithCallbackData("у маім", "B_33") },
                    new[] { InlineKeyboardButton.WithCallbackData("краі", "B_34") },
                    new[] { InlineKeyboardButton.WithCallbackData("Звініце", "trueB_35") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Выберыце  граматычную аснову наступнага сказа:
Звініце, крыніцы, у краі маім родным чыстай вадзіцай!", replyMarkup: buttonspartB7);
                break;

            case "B_31":
            case "B_32":
            case "B_33":
            case "B_34":
                var retryKeyboardQuestionB7 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB7") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB7);
                break; ;

            case "trueB_35":
                var QuestionB8Keyboard = new InlineKeyboardMarkup(new[]
               {
                     new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB8") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB8Keyboard);
                break;

            case "retryQuestionB7":
                var retryQuestionB7 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("крыніцы", "B_31") },
                    new[] { InlineKeyboardButton.WithCallbackData("вадзіцай", "B_32") },
                    new[] { InlineKeyboardButton.WithCallbackData("у маім", "B_33") },
                    new[] { InlineKeyboardButton.WithCallbackData("краі", "B_34") },
                    new[] { InlineKeyboardButton.WithCallbackData("Звініце", "trueB_35") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Выберыце  граматычную аснову наступнага сказа:
Звініце, крыніцы, у краі маім родным чыстай вадзіцай!", replyMarkup: retryQuestionB7);
                break;




            //B8

            case "QuestionB8":
                var buttonspartB8 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("разважаў", "B_36") },
                    new[] { InlineKeyboardButton.WithCallbackData("планаваў", "trueB_37") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, прапанаваных у дужках, вызначце тое, што адпавядае сэнсу сказа. Выберыце гэта слова ў той граматычнай форме, у якой яно прапанавана:
У творах Францішка Багушэвіча, які свой першы верш спачатку (разважаў, планаваў) назваць «Жалейка», кожны чытач заўважае музычны лейтматыў: дудка, смык, скрыпачка…", replyMarkup: buttonspartB8);
                break;

            case "B_36":
                var retryKeyboardQuestionB8 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB8") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB8);
                break; ;

            case "trueB_37":
                var QuestionB9Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB9") }
                 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB9Keyboard);
                break;

            case "retryQuestionB8":
                var retryQuestionB8 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("разважаў", "B_36") },
                    new[] { InlineKeyboardButton.WithCallbackData("планаваў", "trueB_37") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, прапанаваных у дужках, вызначце тое, што адпавядае сэнсу сказа. Выберыце гэта слова ў той граматычнай форме, у якой яно прапанавана:
У творах Францішка Багушэвіча, які свой першы верш спачатку (разважаў, планаваў) назваць «Жалейка», кожны чытач заўважае музычны лейтматыў: дудка, смык, скрыпачка…", replyMarkup: retryQuestionB8);
                break;


            //B9

            case "QuestionB9":
                var buttonspartB9 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("А1Б5В3Г2", "B_38") },
                    new[] { InlineKeyboardButton.WithCallbackData("Б1А5Г2В3", "B_39") },
                    new[] { InlineKeyboardButton.WithCallbackData("А5Б3В2Г4", "trueB_40") },
                    new[] { InlineKeyboardButton.WithCallbackData("А3Б1В4Г5", "B_41") },
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б1В3Г5", "B_42") },
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж фразеалагізмамі і словамі-суправаджальнікамі, з якімі фразеалагізмы ўжываюцца ў маўленні. 
Адказ выберыце ў выглядзе спалучэння літар і лічбаў, захоўваючы алфавітную паслядоўнасць літар.
Памятайце, што некаторыя даныя могуць не выкарыстоўвацца ўвогуле.

A. … краем вока.			 
Б. … з распасцёртымі рукамі. 
B. … з чыстай старонкі.		
Г. … пра запас.		

1. Памераць
2. Пачынаць
3. Сустракаць
4. Мець
5. Бачыць", replyMarkup: buttonspartB9);
                break;

            case "B_38":
            case "B_39":
            case "B_41":
            case "B_42":
                var retryKeyboardQuestionB9 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB9") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB9);
                break; ;

            case "trueB_40":
                var QuestionB10Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB10") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB10Keyboard);
                break;

            case "retryQuestionB9":
                var retryQuestionB9 = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("А1Б5В3Г2", "B_38") },
                new[] { InlineKeyboardButton.WithCallbackData("Б1А5Г2В3", "B_39") },
                new[] { InlineKeyboardButton.WithCallbackData("А5Б3В2Г4", "trueB_40") },
                new[] { InlineKeyboardButton.WithCallbackData("А3Б1В4Г5", "B_41") },
                new[] { InlineKeyboardButton.WithCallbackData("А2Б1В3Г5", "B_42") },
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Устанавіце адпаведнасць паміж фразеалагізмамі і словамі-суправаджальнікамі, з якімі фразеалагізмы ўжываюцца ў маўленні. 
Адказ выберыце ў выглядзе спалучэння літар і лічбаў, захоўваючы алфавітную паслядоўнасць літар.
Памятайце, што некаторыя даныя могуць не выкарыстоўвацца ўвогуле.

A. … краем вока.			 
Б. … з распасцёртымі рукамі. 
B. … з чыстай старонкі.		
Г. … пра запас.		

1. Памераць
2. Пачынаць
3. Сустракаць
4. Мець
5. Бачыць", replyMarkup: retryQuestionB9);
                break;




            //B10

            case "QuestionB10":
                var buttonspartB10 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("А3Б1В5Г2", "trueB_43") },
                    new[] { InlineKeyboardButton.WithCallbackData("Б1А3В5Г2", "B_44") },
            new[] { InlineKeyboardButton.WithCallbackData("В5А1Б4Г2", "B_45") },
            new[] { InlineKeyboardButton.WithCallbackData("А5Б3В2Г4", "B_46") },
            new[] { InlineKeyboardButton.WithCallbackData("А1Б2В3Г5", "B_47") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Вызначце марфемную будову прыведзеных слоў і ўстанавіце адпаведнасць. 
Адказ выберыце ў выглядзе спалучэння літар і лічбаў, захоўваючы алфавітную паслядоўнасць літар.
 Памятайце, што некаторыя даныя могуць не выкарыстоўвацца ўвогуле.

A. Баяністка.
Б. Жыць.
B. Удзячны.
Г. Дакумент.

1. Слова, якое не мае канчатка.
2. Слова, якое мае нулявы канчатак.
3. Слова, якое мае 2 суфіксы.
4. Слова, якое мае постфікс.
5. Слова, якое мае прыстаўку.", replyMarkup: buttonspartB10);
                break;

            case "B_44":
            case "B_45":
            case "B_46":
            case "B_47":
                var retryKeyboardQuestionB10 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB10") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB10);
                break; ;

            case "trueB_43":
                var QuestionB11Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB11") }
                 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB11Keyboard);
                break;

            case "retryQuestionB10":
                var retryQuestionB10 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("А3Б1В5Г2", "trueB_43") },
                    new[] { InlineKeyboardButton.WithCallbackData("Б1А3В5Г2", "B_44") },
                    new[] { InlineKeyboardButton.WithCallbackData("В5А1Б4Г2", "B_45") },
                    new[] { InlineKeyboardButton.WithCallbackData("А5Б3В2Г4", "B_46") },
                    new[] { InlineKeyboardButton.WithCallbackData("А1Б2В3Г5", "B_47") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Вызначце марфемную будову прыведзеных слоў і ўстанавіце адпаведнасць. 
Адказ выберыце ў выглядзе спалучэння літар і лічбаў, захоўваючы алфавітную паслядоўнасць літар.
 Памятайце, што некаторыя даныя могуць не выкарыстоўвацца ўвогуле.

A. Баяністка.
Б. Жыць.
B. Удзячны.
Г. Дакумент.

1. Слова, якое не мае канчатка.
2. Слова, якое мае нулявы канчатак.
3. Слова, якое мае 2 суфіксы.
4. Слова, якое мае постфікс.
5. Слова, якое мае прыстаўку.", replyMarkup: retryQuestionB10);
                break;



            //B11

            case "QuestionB11":
                var buttonspartB11 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б5В3Г4", "B_48") },
                    new[] { InlineKeyboardButton.WithCallbackData("А3Б1В5Г2", "B_49") },
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б5В3Г4", "B_50") },
                    new[] { InlineKeyboardButton.WithCallbackData("А5Б3В2Г4", "B_51") },
                    new[] { InlineKeyboardButton.WithCallbackData("А4Б2В1Г5", "trueB_52") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Вызначце спосаб утварэння прыведзеных слоў і ўстанавіце адпаведнасць.
Адказ выберыце ў выглядзе спалучэння літар і лічбаў, захоўваючы алфавітную паслядоўнасць літар.
Памятайце, што некаторыя даныя могуць не выкарыстоўвацца ўвогуле.

A. Разнесці.
Б. Вынаходлівасць.
B. Наплечны.
Г. Выхад.

1. Прыставачна-суфіксальны.
2. Суфіксальны.
3. Асноваскладанне.
4. Прыставачны.
5. Бяссуфіксны (нульсуфіксальны).", replyMarkup: buttonspartB11);
                break;

            case "B_48":
            case "B_49":
            case "B_50":
            case "B_51":
                var retryKeyboardQuestionB11 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB11") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB11);
                break; ;

            case "trueB_52":
                var QuestionB12Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB12") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB12Keyboard);
                break;

            case "retryQuestionB11":
                var retryQuestionB11 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б5В3Г4", "B_48") },
                    new[] { InlineKeyboardButton.WithCallbackData("А3Б1В5Г2", "B_49") },
                    new[] { InlineKeyboardButton.WithCallbackData("А2Б5В3Г4", "B_50") },
                    new[] { InlineKeyboardButton.WithCallbackData("А5Б3В2Г4", "B_51") },
                    new[] { InlineKeyboardButton.WithCallbackData("А4Б2В1Г5", "trueB_52") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Вызначце спосаб утварэння прыведзеных слоў і ўстанавіце адпаведнасць.
Адказ выберыце ў выглядзе спалучэння літар і лічбаў, захоўваючы алфавітную паслядоўнасць літар.
Памятайце, што некаторыя даныя могуць не выкарыстоўвацца ўвогуле.

A. Разнесці.
Б. Вынаходлівасць.
B. Наплечны.
Г. Выхад.

1. Прыставачна-суфіксальны.
2. Суфіксальны.
3. Асноваскладанне.
4. Прыставачны.
5. Бяссуфіксны (нульсуфіксальны).", replyMarkup: retryQuestionB11);
                break;





            //B12

            case "QuestionB12":
                var buttonspartB12 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст афіцыйнага стылю", "B_53") },
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст мастацкага стылю", "trueB_54") },
                    new[] { InlineKeyboardButton.WithCallbackData("гэкст гутарковага стылю", "B_55") },
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст навуковага стылю", "B_56") },
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст публіцыстычнага стылю", "B_57") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Вызначце правільнае сцверджанне ў дачыненні да тэксту. 

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB12);
                break;

            case "B_53":
            case "B_55":
            case "B_56":
            case "B_57":
                var retryKeyboardQuestionB12 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB12") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB12);
                break; ;

            case "trueB_54":
                var QuestionB13Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB13") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB13Keyboard);
                break;

            case "retryQuestionB12":
                var retryQuestionB12 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст афіцыйнага стылю", "B_53") },
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст мастацкага стылю", "trueB_54") },
                    new[] { InlineKeyboardButton.WithCallbackData("гэкст гутарковага стылю", "B_55") },
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст навуковага стылю", "B_56") },
                    new[] { InlineKeyboardButton.WithCallbackData("тэкст публіцыстычнага стылю", "B_57") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Вызначце правільнае сцверджанне ў дачыненні да тэксту. 

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB12);
                break;




            //B13

            case "QuestionB13":
                var buttonspartB13 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("КАБ", "B_58") },
                    new[] { InlineKeyboardButton.WithCallbackData("ЯМУ", "trueB_59") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, выдзеленых у 14-м сказе тэксту, вызначце тое, якое з'яўляецца сродкам сувязі 13-га і 14-га сказаў. Выберыце гэта слова так, як яно пададзена ў 14-м сказе. 

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB13);
                break;

            case "B_58":
                var retryKeyboardQuestionB13 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB13") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB13);
                break; ;

            case "trueB_59":
                var QuestionB14Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB14") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB14Keyboard);
                break;

            case "retryQuestionB13":
                var retryQuestionB13 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("КАБ", "B_58") },
                    new[] { InlineKeyboardButton.WithCallbackData("ЯМУ", "trueB_59") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, выдзеленых у 14-м сказе тэксту, вызначце тое, якое з'яўляецца сродкам сувязі 13-га і 14-га сказаў. Выберыце гэта слова так, як яно пададзена ў 14-м сказе. 

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB13);
                break;




            //B14

            case "QuestionB14":
                var buttonspartB14 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("лясны", "trueB_60") },
                    new[] { InlineKeyboardButton.WithCallbackData("лясісты", "B_61") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, прапанаваных у дужках у 15-м сказе тэксту, вызначце тое, якое павінна быць ужыта ў дадзенымсказе.
Выберыце гэта слова ў той граматычнай форме, у якой яно прапанавана.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB14);
                break;

            case "B_61":
                var retryKeyboardQuestionB14 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB14") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB14);
                break; ;

            case "trueB_60":
                var QuestionB15Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB15Keyboard);
                break;

            case "retryQuestionB14":
                var retryQuestionB14 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("лясны", "trueB_60") },
                    new[] { InlineKeyboardButton.WithCallbackData("лясісты", "B_61") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, прапанаваных у дужках у 15-м сказе тэксту, вызначце тое, якое павінна быць ужыта ў дадзенымсказе.
Выберыце гэта слова ў той граматычнай форме, у якой яно прапанавана.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB14);
                break;




            //B15

            case "QuestionB15":
                var buttonspartB15 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("прыкмеціўшы", "B_62") },
                    new[] { InlineKeyboardButton.WithCallbackData("пясчанага", "trueB_63") },
                    new[] { InlineKeyboardButton.WithCallbackData("страйнешай", "B_64") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Знайдзіце ў 1-м сказе тэксту слова, у якім ёсць зычны гук [ш].
 Выберыце гэта слова так, як яно пададзена ў 1-м сказе.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB15);
                break;

            case "B_62":
            case "B_64":
                var retryKeyboardQuestionB15 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB15") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB15);
                break; ;

            case "trueB_63":
                var QuestionB16Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB16") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB16Keyboard);
                break;

            case "retryQuestionB15":
                var retryQuestionB15 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("прыкмеціўшы", "B_62") },
                    new[] { InlineKeyboardButton.WithCallbackData("пясчанага", "trueB_63") },
                    new[] { InlineKeyboardButton.WithCallbackData("страйнешай", "B_64") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Знайдзіце ў 1-м сказе тэксту слова, у якім ёсць зычны гук [ш]. 
Выберыце гэта слова так, як яно пададзена ў 1-м сказе.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB15);
                break;



            //B16
            case "QuestionB16":
                var buttonspartB16 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("невялікі", "trueB_65") },
                    new[] { InlineKeyboardButton.WithCallbackData("неаялікую", "B_66") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 дзвюх граматычных формаў прыметніка, прапанаваных у дужках у 16-м сказе тэксту, вызначце тую, у якой ён павінен быць ужыты ў дадзеным сказе. 
Выберыце прыметнік у гэтай граматычнай форме.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB16);
                break;

            case "B_66":
                var retryKeyboardQuestionB16 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB17") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB16);
                break; ;

            case "trueB_65":
                var QuestionB17Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB17") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB17Keyboard);
                break;

            case "retryQuestionB16":
                var retryQuestionB16 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("невялікі", "B_65") },
                    new[] { InlineKeyboardButton.WithCallbackData("неаялікую", "trueB_66") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 дзвюх граматычных формаў прыметніка, прапанаваных у дужках у 16-м сказе тэксту, вызначце тую, у якой ён павінен быць ужыты ў дадзеным сказе. 
Выберыце прыметнік у гэтай граматычнай форме.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB16);
                break;




            //B17

            case "QuestionB17":
                var buttonspartB17 = new InlineKeyboardMarkup(new[]
               {
    new[] { InlineKeyboardButton.WithCallbackData("чароўны", "B_67") },
    new[] { InlineKeyboardButton.WithCallbackData("спыніў", "B_68") },
    new[] { InlineKeyboardButton.WithCallbackData("самый", "trueB_69") },
    new[] { InlineKeyboardButton.WithCallbackData("спявак - жаваранак", "B_70") }
});

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Знайдзіце ў 4-м сказе тэксту слова, якое не адпавядае нормам беларускай літаратурнай мовы. 

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі.
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах. 
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB17);
                break;

            case "B_67":
            case "B_68":
            case "B_70":
                var retryKeyboardQuestionB17 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB17") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB17);
                break; ;

            case "trueB_69":
                var buttonsCorrectSpellingtrueB_69 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("самый", "B1_17") },
                    new[] { InlineKeyboardButton.WithCallbackData("самы", "trueB2_17") },
                    new[] { InlineKeyboardButton.WithCallbackData("самі", "B3_17") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_69);
                break;

            case "B1_17":
            case "B3_17":
                var retryKeyboardtrueB_69 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_69") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_69);
                break;

            case "trueB2_17":
                var QuestionB18Keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB18Keyboard);
                break;

            case "retryQuestionB17":
                var retryQuestionB17 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("чароўны", "B_67") },
                    new[] { InlineKeyboardButton.WithCallbackData("спыніў", "B_68") },
                    new[] { InlineKeyboardButton.WithCallbackData("самый", "trueB_69") },
                    new[] { InlineKeyboardButton.WithCallbackData("спявак - жаваранак", "B_70") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Знайдзіце ў 4-м сказе тэксту слова, якое не адпавядае нормам беларускай літаратурнай мовы. 

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі.
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах. 
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB17);
                break;

            case "retrytrueB_69":
                var retrybuttonsCorrectSpellingtrueB_69 = new InlineKeyboardMarkup(new[]
                {
                     new[] { InlineKeyboardButton.WithCallbackData("самый", "B1_17") },
                    new[] { InlineKeyboardButton.WithCallbackData("самы", "trueB2_17") },
                    new[] { InlineKeyboardButton.WithCallbackData("самі", "B3_17") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retrybuttonsCorrectSpellingtrueB_69);
                break;




            //B18
            case "QuestionB18":
                var buttonspartB18 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("паказываў", "B_71") },
                    new[] { InlineKeyboardButton.WithCallbackData("паказваў", "trueB_72") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух дзеясловаў, прапанаваных у дужках у 13-м сказе тэксту, вызначце той, які адпавядае нормам беларускай літаратурнай мовы. Выберыце гэты дзеяслоў у той граматычнай форме, у якой ён прапанаваны.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB18);
                break;

            case "B_71":
                var retryKeyboardQuestionB18 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB18") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB18);
                break; ;

            case "trueB_72":
                var QuestionB19Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB19") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB19Keyboard);
                break;

            case "retryQuestionB18":
                var retryQuestionB18 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("паказываў", "B_71") },
                    new[] { InlineKeyboardButton.WithCallbackData("паказваў", "trueB_72") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух дзеясловаў, прапанаваных у дужках у 13-м сказе тэксту, вызначце той, які адпавядае нормам беларускай літаратурнай мовы. Выберыце гэты дзеяслоў у той граматычнай форме, у якой ён прапанаваны.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB18);
                break;






            //B19
            case "QuestionB19":
                var buttonspartB19 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("ПЕРАЖЫТА", "B_73") },
                    new[] { InlineKeyboardButton.WithCallbackData("ПЕРАЖЫТУ", "B_74") },
                    new[] { InlineKeyboardButton.WithCallbackData("ПЕРАЖЫТЫ", "trueB_75") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Ад асновы дзеяслова, выдзеленага ў 17-м сказе тэксту, утварыце поўны дзеепрыметнік залежнага стануі выберыце яго ў форме назоўнага склону адзіночнага ліку мужчынскага роду.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB19);
                break;

            case "B_73":
            case "B_74":
                var retryKeyboardQuestionB19 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB19") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB19);
                break; ;

            case "trueB_75":
                var QuestionB20Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB20") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB20Keyboard);
                break;

            case "retryQuestionB19":
                var retryQuestionB19 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("ПЕРАЖЫТА", "B_73") },
                    new[] { InlineKeyboardButton.WithCallbackData("ПЕРАЖЫТУ", "B_74") },
                    new[] { InlineKeyboardButton.WithCallbackData("ПЕРАЖЫТЫ", "trueB_75") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Ад асновы дзеяслова, выдзеленага ў 17-м сказе тэксту, утварыце поўны дзеепрыметнік залежнага стануі выберыце яго ў форме назоўнага склону адзіночнага ліку мужчынскага роду.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB19);
                break;



            //B20
            case "QuestionB20":
                var buttonspartB20 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("у", "B_76") },
                    new[] { InlineKeyboardButton.WithCallbackData("за", "trueB_77") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух прыназоўнікаў, прапанаваных у дужках у 10-м сказе тэксту, вызначце той, які павінен быць ужытыўдадзеным сказе. Выберыце гэты прыназоўнік.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB20);
                break;

            case "B_76":
                var retryKeyboardQuestionB20 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB20") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB20);
                break; ;

            case "trueB_77":
                var QuestionB21Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB21") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB21Keyboard);
                break;

            case "retryQuestionB20":
                var retryQuestionB20 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("у", "B_76") },
                    new[] { InlineKeyboardButton.WithCallbackData("за", "trueB_77") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух прыназоўнікаў, прапанаваных у дужках у 10-м сказе тэксту, вызначце той, які павінен быць ужытыўдадзеным сказе. Выберыце гэты прыназоўнік.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB20);
                break;


            //B21
            case "QuestionB21":
                var buttonspartB21 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Ні", "trueB_78") },
                    new[] { InlineKeyboardButton.WithCallbackData("Нe", "B_79") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 дзвюх часціц, прапанаваных у дужках у 11-м сказе тэксту, вызначце тую, якая павінна быць ужыта ў да-дзеным сказе. Выберыце гэту часціцу.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB21);
                break;

            case "B_79":
                var retryKeyboardQuestionB21 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB21") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB21);
                break; ;

            case "trueB_78":
                var QuestionB22Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB22") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB22Keyboard);
                break;

            case "retryQuestionB21":
                var retryQuestionB21 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("Ні", "trueB_78") },
                    new[] { InlineKeyboardButton.WithCallbackData("Нe", "B_79") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух прыназоўнікаў, прапанаваных у дужках у 10-м сказе тэксту, вызначце той, які павінен быць ужытыўдадзеным сказе. Выберыце гэты прыназоўнік.

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB21);
                break;




            //B22
            case "QuestionB22":
                var buttonspartB22 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("ВІДАЦЬ", "B_80") },
                    new[] { InlineKeyboardButton.WithCallbackData("АХУТАНА", "trueB_81") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, выдзеленых у 3-м сказе тэксту, вызначце тое, якое з'яўляецца выказнікам. 
Выберыце гэта слова так, як яно пададзена ў 3-м сказе

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: buttonspartB22);
                break;

            case "B_80":
                var retryKeyboardQuestionB22 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB22") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB22);
                break;

            case "trueB_81":
                var QuestionB23Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB23") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB23Keyboard);
                break;

            case "retryQuestionB22":
                var retryQuestionB22 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("ВІДАЦЬ", "B_80") },
                    new[] { InlineKeyboardButton.WithCallbackData("АХУТАНА", "trueB_81") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, выдзеленых у 3-м сказе тэксту, вызначце тое, якое з'яўляецца выказнікам. 
Выберыце гэта слова так, як яно пададзена ў 3-м сказе

(1) У думках-мроях Алесь і не прыкмеціў, як наблізіўся да пясчанага пагорка, дзе побач са стройнай бярозкай расла, цягнулася ўвысь маладая сасонка. 
(2) Тут хлопец стаў думаць, як яму ісці да чыгункі. 
(3) Даўжэйшай палявой сцяжынкай або карацейшай лясной, што, відаць, ахутана нейкай тайнай. 
(4) Яго развагі на хвіліну спыніў самый чароўны паднябесны спявак - жаваранак. 
(5) Нешта дужа прыгожае пачаў вызвоньваць у вышыні птах.
(6) Алесь усміхнуўся: «Вось жа шчыруе! дня яму не хапіла, каб нарадавацца вясне»
(7) Пажадаўшы ўсё ж скараціць шлях, хлопец пакрочыў праз лес. 
(8) Увайшоўшы ў лясное царства, ён адчуў, як ад свежага паветра ўсё цела налілося прыемнай лёгкасцю. 
(9) Лёгкасцю і бадзерасцю. 
(10) Раптам Алесь спыніўся як укопаны: крокаў (у, за) дзесяць ад сябе ён угледзеў магутнага лася. 
(11) (Ні, не) разу яшчэ так блізка не даводзілася хлопцу бачыць сахатага. 
(12) А лось, грацыёзна павярнуўшы ў бок нечаканага сустрэчнага галаву, паварушыў вушамі, страсянуў грывай і таксама прыпыніўся. 
(13) Усім сваім выглядам звер (паказываў, паказваў) чалавеку дружалюбнасць.
(14) Яму самаму, здавалася, вельмі важна было, каб чалавек не баяцся яго і скранууся з месца першым.
(15) Алесь зразумеў, што (лясны, лясісты) велікан не уяўляе для яго ніякай пагрозы. 
(16) Хлопец хуценька пераскочыў праз (невялікі, неаялікую) насыт з ігліцы і пайшоў далей. 
(17) «Якое цікавае спатканне мне давялося перажыць!» - падумаў услых Алесь.", replyMarkup: retryQuestionB22);
                break;

            //B23


            case "QuestionB23":

                var buttonsQuestionB23 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дэпутат", "B_82") },
                    new[] { InlineKeyboardButton.WithCallbackData("зачарсцвелы", "trueB_83") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутэр", "B_84") },
                    new[] { InlineKeyboardButton.WithCallbackData("даліна", "B_85") },
                    new[] { InlineKeyboardButton.WithCallbackData("дробназярністы", "B_86") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonsQuestionB23);
                break;

            case "B_82":
            case "B_83":
            case "B_85":
            case "B_86":
                var retryKeyboardQuestionB23 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB23") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB23);
                break;

            case "trueB_84":
                var buttonsCorrectSpellingtrueB_84 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("скутэр", "B1_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутер", "B2_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутор", "B3_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутар", "trueB4_23") }
                        });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_84);
                break;

            case "B1_23":
            case "B3_23":
            case "B4_23":
                var retryKeyboardtrueB_84 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_84") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_84);
                break;

            case "trueB4_23":
                var QuestionB24Keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB23") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB24Keyboard);
                break;

            case "retryQuestionB23":
                var retryButtonsQuestionB23 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дэпутат", "B_82") },
                    new[] { InlineKeyboardButton.WithCallbackData("зачарсцвелы", "trueB_83") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутэр", "B_84") },
                    new[] { InlineKeyboardButton.WithCallbackData("даліна", "B_85") },
                    new[] { InlineKeyboardButton.WithCallbackData("дробназярністы", "B_86") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryButtonsQuestionB23);
                break;

            case "retrytrueB_84":
                var retryButtonsCorrectSpellingtrueB_84 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("скутэр", "B1_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутер", "B2_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутор", "B3_23") },
                    new[] { InlineKeyboardButton.WithCallbackData("скутар", "trueB4_23") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retryButtonsCorrectSpellingtrueB_84);
                break;


            //B24

            case "QuestionB24":

                var buttonsQuestionB24 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("перыметр", "B_87") },
                    new[] { InlineKeyboardButton.WithCallbackData("веяць", "B_88") },
                    new[] { InlineKeyboardButton.WithCallbackData("абмеркаваць", "B_89") },
                    new[] { InlineKeyboardButton.WithCallbackData("дзедуля", "trueB_90") },
                    new[] { InlineKeyboardButton.WithCallbackData("землекапалка", "B_91") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonsQuestionB24);
                break;

            case "B_87":
            case "B_88":
            case "B_89":
            case "B_91":
                var retryKeyboardQuestionB24 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB24") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB24);
                break;

            case "trueB_90":
                var buttonsCorrectSpellingtrueB_90 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дзядуля", "trueB1_24") },
                    new[] { InlineKeyboardButton.WithCallbackData("дядуля", "B2_24") },
                    new[] { InlineKeyboardButton.WithCallbackData("дзедуля", "B3_24") }
                        });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_90);
                break;

            case "B2_24":
            case "B3_24":
                var retryKeyboardtrueB_90 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_90") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_90);
                break;

            case "trueB1_24":
                var QuestionB25Keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB25") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB25Keyboard);
                break;

            case "retryQuestionB24":
                var retryButtonsQuestionB24 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("перыметр", "B_87") },
                    new[] { InlineKeyboardButton.WithCallbackData("веяць", "B_88") },
                    new[] { InlineKeyboardButton.WithCallbackData("абмеркаваць", "B_89") },
                    new[] { InlineKeyboardButton.WithCallbackData("дзедуля", "trueB_90") },
                    new[] { InlineKeyboardButton.WithCallbackData("землекапалка", "B_91") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryButtonsQuestionB24);
                break;

            case "retrytrueB_90":
                var retryButtonsCorrectSpellingtrueB_90 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("дзядуля", "trueB1_24") },
                    new[] { InlineKeyboardButton.WithCallbackData("дядуля", "B2_24") },
                    new[] { InlineKeyboardButton.WithCallbackData("дзедуля", "B3_24") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retryButtonsCorrectSpellingtrueB_90);
                break;


            //B25


            case "QuestionB25":

                var buttonsQuestionB25 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("вычышчаць", "B_92") },
    new[] { InlineKeyboardButton.WithCallbackData("дагляччык", "trueB_93") },
    new[] { InlineKeyboardButton.WithCallbackData("сумесны", "B_94") },
    new[] { InlineKeyboardButton.WithCallbackData("змазчык", "B_95") },
    new[] { InlineKeyboardButton.WithCallbackData("будка", "B_96") }
});

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonsQuestionB25);
                break;

            case "B_92":
            case "B_94":
            case "B_95":
            case "B_96":
                var retryKeyboardQuestionB25 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB25") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB25);
                break;

            case "trueB_93":
                var buttonsCorrectSpellingtrueB_93 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("дагледчык", "B1_25") },
    new[] { InlineKeyboardButton.WithCallbackData("даглядчік", "B2_25") },
    new[] { InlineKeyboardButton.WithCallbackData("даглядчык", "trueB3_25") }
        });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_93);
                break;

            case "B1_25":
            case "B2_25":
                var retryKeyboardtrueB_93 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_93") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_93);
                break;

            case "trueB3_25":
                var QuestionB26Keyboard = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB26") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB26Keyboard);
                break;

            case "retryQuestionB25":
                var retryButtonsQuestionB25 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("вычышчаць", "B_92") },
    new[] { InlineKeyboardButton.WithCallbackData("дагляччык", "trueB_93") },
    new[] { InlineKeyboardButton.WithCallbackData("сумесны", "B_94") },
    new[] { InlineKeyboardButton.WithCallbackData("змазчык", "B_95") },
    new[] { InlineKeyboardButton.WithCallbackData("будка", "B_96") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryButtonsQuestionB25);
                break;

            case "retrytrueB_93":
                var retryButtonsCorrectSpellingtrueB_93 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("дагледчык", "B1_25") },
    new[] { InlineKeyboardButton.WithCallbackData("даглядчік", "B2_25") },
    new[] { InlineKeyboardButton.WithCallbackData("даглядчык", "trueB3_25") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retryButtonsCorrectSpellingtrueB_93);
                break;



            //B26

            case "QuestionB26":

                var buttonsQuestionB26 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("калоссе", "B_97") },
    new[] { InlineKeyboardButton.WithCallbackData("расставіць", "B_98") },
    new[] { InlineKeyboardButton.WithCallbackData("прэсса", "trueB_99") },
    new[] { InlineKeyboardButton.WithCallbackData("каменне", "B_100") },
    new[] { InlineKeyboardButton.WithCallbackData("асіннік", "B_101") }
});

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: buttonsQuestionB26);
                break;

            case "B_97":
            case "B_98":
            case "B_100":
            case "B_101":
                var retryKeyboardQuestionB26 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB26") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB26);
                break;

            case "trueB_99":
                var buttonsCorrectSpellingtrueB_99 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("преса", "B1_26") },
    new[] { InlineKeyboardButton.WithCallbackData("прэсса", "B2_26") },
    new[] { InlineKeyboardButton.WithCallbackData("прэса", "trueB3_26") },
    new[] { InlineKeyboardButton.WithCallbackData("пресса", "B4_26") }
        });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: buttonsCorrectSpellingtrueB_99);
                break;

            case "B1_26":
            case "B2_26":
            case "B4_26":
                var retryKeyboardtrueB_99 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retrytrueB_99") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardtrueB_99);
                break;

            case "trueB3_26":
                var QuestionB27Keyboard = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB27") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB27Keyboard);
                break;

            case "retryQuestionB26":
                var retryButtonsQuestionB26 = new InlineKeyboardMarkup(new[]
                {

    new[] { InlineKeyboardButton.WithCallbackData("калоссе", "B_97") },
    new[] { InlineKeyboardButton.WithCallbackData("расставіць", "B_98") },
    new[] { InlineKeyboardButton.WithCallbackData("прэсса", "trueB_99") },
    new[] { InlineKeyboardButton.WithCallbackData("каменне", "B_100") },
    new[] { InlineKeyboardButton.WithCallbackData("асіннік", "B_101") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "3 прапанаваных слоў вызначце тое, якое не адпавядае арфаграфічнай норме, і выберыце яго правільнае напісанне:", replyMarkup: retryButtonsQuestionB26);
                break;

            case "retrytrueB_99":
                var retryButtonsCorrectSpellingtrueB_99 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("преса", "B1_26") },
    new[] { InlineKeyboardButton.WithCallbackData("прэсса", "B2_26") },
    new[] { InlineKeyboardButton.WithCallbackData("прэса", "trueB3_26") },
    new[] { InlineKeyboardButton.WithCallbackData("пресса", "B4_26") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберы правільнае напісанне:", replyMarkup: retryButtonsCorrectSpellingtrueB_99);
                break;


            //B27

            case "QuestionB27":
                var buttonspartB27 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("навуцы", "trueB_102") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуце", "B_103") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуке", "B_104") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуца", "B_105") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуцу", "B_106") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Выберыце правільны назоўнік НАВУКА ў форме давальнага склону адзіночнага ліку.", replyMarkup: buttonspartB27);
                break;

            case "B_103":
            case "B_104":
            case "B_105":
            case "B_106":
                var retryKeyboardQuestionB27 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB27") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB27);
                break; ;

            case "trueB_102":
                var QuestionB28Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB28") }
                 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB28Keyboard);
                break;

            case "retryQuestionB27":
                var retryQuestionB27 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("навуцы", "trueB_102") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуце", "B_103") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуке", "B_104") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуца", "B_105") },
                    new[] { InlineKeyboardButton.WithCallbackData("навуцу", "B_106") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @" Выберыце правільны назоўнік НАВУКА ў форме давальнага склону адзіночнага ліку.", replyMarkup: retryQuestionB27);
                break;


            //B28

            case "QuestionB28":
                var buttonspartB28 = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("шаснаццаць", "trueB_107") },
                    new[] { InlineKeyboardButton.WithCallbackData("шаснадцаць", "B_108") },
                    new[] { InlineKeyboardButton.WithCallbackData("шеснаццаць", "B_109") },
                    new[] { InlineKeyboardButton.WithCallbackData("шаснаццать", "B_110") },
                    new[] { InlineKeyboardButton.WithCallbackData("шаснатаць", "B_111") }
                });

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Выберыце правільна напісаны лік 16 словам.", replyMarkup: buttonspartB28);
                break;

            case "B_108":
            case "B_109":
            case "B_110":
            case "B_111":
                var retryKeyboardQuestionB28 = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB28") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB28);
                break; ;

            case "trueB_107":
                var QuestionB29Keyboard = new InlineKeyboardMarkup(new[]
               {
                    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB29") }
                 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB29Keyboard);
                break;

            case "retryQuestionB28":
                var retryQuestionB28 = new InlineKeyboardMarkup(new[]
            {
                    new[] { InlineKeyboardButton.WithCallbackData("шаснаццаць", "trueB_107") },
                    new[] { InlineKeyboardButton.WithCallbackData("шаснадцаць", "B_108") },
                    new[] { InlineKeyboardButton.WithCallbackData("шеснаццаць", "B_109") },
                    new[] { InlineKeyboardButton.WithCallbackData("шаснаццать", "B_110") },
                    new[] { InlineKeyboardButton.WithCallbackData("шаснатаць", "B_111") }
                });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @" Выберыце правільна напісаны лік 16 словам.", replyMarkup: retryQuestionB28);
                break;


            //B29

            case "QuestionB29":
                var buttonspartB29 = new InlineKeyboardMarkup(new[]
               {
    new[] { InlineKeyboardButton.WithCallbackData("Сяброў", "B_112") },
    new[] { InlineKeyboardButton.WithCallbackData("згадліцеся", "B_113") },
    new[] { InlineKeyboardButton.WithCallbackData("набываюць", "trueB_114") },
    new[] { InlineKeyboardButton.WithCallbackData("краме", "B_115") }
});

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Выберыце граматычную аснову наступнага сказа:

Сяброў, згадліцеся, набываюць не ў краме.", replyMarkup: buttonspartB29);
                break;

            case "B_112":
            case "B_113":
            case "B_115":
                var retryKeyboardQuestionB29 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB29") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB29);
                break; ;

            case "trueB_114":
                var QuestionB30Keyboard = new InlineKeyboardMarkup(new[]
               {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB30") }
 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB30Keyboard);
                break;

            case "retryQuestionB29":
                var retryQuestionB29 = new InlineKeyboardMarkup(new[]
            {
    new[] { InlineKeyboardButton.WithCallbackData("Сяброў", "B_112") },
    new[] { InlineKeyboardButton.WithCallbackData("згадліцеся", "B_113") },
    new[] { InlineKeyboardButton.WithCallbackData("набываюць", "trueB_114") },
    new[] { InlineKeyboardButton.WithCallbackData("краме", "B_115") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @" Выберыце граматычную аснову наступнага сказа:

Сяброў, згадліцеся, набываюць не ў краме.", replyMarkup: retryQuestionB29);
                break;

            //B30

            case "QuestionB30":
                var buttonspartB30 = new InlineKeyboardMarkup(new[]
               {
    new[] { InlineKeyboardButton.WithCallbackData("Дапытлівую", "B_116") },
    new[] { InlineKeyboardButton.WithCallbackData("цікавую", "trueB_117") }
});

                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"3 двух слоў, прапанаваных у дужках, вызначце тое, што адпавядае сэнсу сказа. Выберыце гэта слова ў той граматычнай форме, у якой яно прапанавана: 

(Дапытлівую, цікавую) спробупераказаць свабодным вершам прадмовы Францыска Скарыны зрабіў Алесь Разанай.", replyMarkup: buttonspartB30);
                break;

            case "B_116":
                var retryKeyboardQuestionB30 = new InlineKeyboardMarkup(new[]
                {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш паспрабаваць яшчэ раз", "retryQuestionB30") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "❌няправільна❌", replyMarkup: retryKeyboardQuestionB30);
                break; ;

            case "trueB_117":
                var QuestionB31Keyboard = new InlineKeyboardMarkup(new[]
               {
    new[] { InlineKeyboardButton.WithCallbackData("Можаш перайсці да наступнага пытання", "QuestionB31") }
 });
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "✅правільна✅", replyMarkup: QuestionB31Keyboard);
                break;

            case "retryQuestionB30":
                var retryQuestionB30 = new InlineKeyboardMarkup(new[]
            {
    new[] { InlineKeyboardButton.WithCallbackData("Дапытлівую", "B_116") },
    new[] { InlineKeyboardButton.WithCallbackData("цікавую", "trueB_117") }
});
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @" 3 двух слоў, прапанаваных у дужках, вызначце тое, што адпавядае сэнсу сказа. Выберыце гэта слова ў той граматычнай форме, у якой яно прапанавана: 

(Дапытлівую, цікавую) спробупераказаць свабодным вершам прадмовы Францыска Скарыны зрабіў Алесь Разанай. правільны назоўнік НАВУКА ў форме давальнага склону адзіночнага ліку.", replyMarkup: retryQuestionB30);
                break;


            //B31

            case "QuestionB31":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Малайчына😊
Працяг у распрацоўцы🫣");
                break;

            default:
                break;



        }
    }
}
