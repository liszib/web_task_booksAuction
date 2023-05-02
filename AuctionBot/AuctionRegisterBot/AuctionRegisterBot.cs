using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class TelegramBot
{
    private readonly ITelegramBotClient bot = new TelegramBotClient("6132171959:AAFGj17FZcM_Iqses8-2-_6JZx0hDthSonY");

    public TelegramBot()
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message.Text.ToLower() == "/register")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Введите данные пользователя: Имя, Фамилия, Email, Пароль и дату рождения (в формате дд.мм.гггг)");
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat, "Введи команду /register");
        }
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }
}

