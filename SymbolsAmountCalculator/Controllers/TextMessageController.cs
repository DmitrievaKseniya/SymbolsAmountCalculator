using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using SymbolsAmountCalculator.Configuration;
using SymbolsAmountCalculator.Services;

namespace SymbolsAmountCalculator.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _storage;
        private readonly OperationsManager _operationsManager;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage storage, OperationsManager operationsManager)
        {
            _telegramClient = telegramBotClient;
            _storage = storage;
            _operationsManager = operationsManager;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            try
            {
                switch (message.Text)
                {
                    case "/start":

                        //Объект представляющий кнопки
                        var buttons = new List<InlineKeyboardButton[]>();
                        buttons.Add(new[]
                        {
                            InlineKeyboardButton.WithCallbackData($"Кол-во символов", $"Characters"),
                            InlineKeyboardButton.WithCallbackData($"Сумма чисел", $"Amount")
                        });

                        //Передаем кнопки вместе с сообщением
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                            $"<b>Наш бот умеет рассчитывать кол-во символов в сообщении или может посчитать сумму чисел из сообщения введенных через пробел</b>{Environment.NewLine}",
                            cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                        break;
                    default:
                        var resulOperation = _operationsManager.GetResultOperation(message);
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, resulOperation, cancellationToken: ct);
                        break;
                }
            }
            catch (Exception ex)
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, ex.Message, cancellationToken: ct);
            }
        }
    }
}
