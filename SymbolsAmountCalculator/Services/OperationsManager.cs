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
using SymbolsAmountCalculator.Operations;


namespace SymbolsAmountCalculator.Services
{
    public class OperationsManager
    {
        private readonly IStorage _storage;
        private readonly ITelegramBotClient _telegramClient;

        public OperationsManager(IStorage storage, ITelegramBotClient telegramClient)
        {
            _storage = storage;
            _telegramClient = telegramClient;
        }

        public string GetResultOperation(Message message)
        {
            var operationCode = _storage.GetSession(message.Chat.Id).OperationCode;
            var oper = GetOperation(operationCode);
            return oper.Handle(message.Text);
        }

        private IOperationHandler GetOperation(string code)
        {
            switch (code)
            {
                case "Characters":
                    return new Operations.SymbolsAmountCalculator();
                case "Amount":
                    return new SumCalculator();
                default: 
                    return null;
            }
        }
    }
}
