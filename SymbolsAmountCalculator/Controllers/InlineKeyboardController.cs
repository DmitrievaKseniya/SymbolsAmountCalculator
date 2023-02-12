using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using SymbolsAmountCalculator.Services;
using Telegram.Bot.Types.Enums;

namespace SymbolsAmountCalculator.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            //Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).OperationCode = callbackQuery.Data;

            //Генерируем информационное сообщение
            string operationText = callbackQuery.Data switch
            {
                "Characters" => "Расчет кол-ва символов",
                "Amount" => "Расчет суммы чисел",
                _ => string.Empty
            };

            //Отправляем в ответ уведомление
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбранная операция - {operationText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять  в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
