using AuctionBot.RabbitMq;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

ITelegramBotClient bot = new TelegramBotClient("6132171959:AAFGj17FZcM_Iqses8-2-_6JZx0hDthSonY");
IRabbitMqService rabbitMqService = new RabbitMqService();

    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;
    var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
    bot.StartReceiving(
        HandleUpdateAsync,
        HandleErrorAsync,
        receiverOptions,
        cancellationToken
    );
Console.ReadLine();


async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
    {
        var message = update.Message;
        if (message.Text.ToLower() == "/register")
        {
            await botClient.SendTextMessageAsync(message.Chat, "Введите данные пользователя: Имя, Фамилия, Email, Пароль, номер телефона (строкой) и дату рождения (в формате дд.мм.гггг)");
            return;
        }

        rabbitMqService.SendMessage(message.Text);

        await botClient.SendTextMessageAsync(message.Chat, "Введи команду /register");
    }
}

async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
}
