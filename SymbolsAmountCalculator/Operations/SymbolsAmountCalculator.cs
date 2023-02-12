using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymbolsAmountCalculator.Services;

namespace SymbolsAmountCalculator.Operations
{
    public class SymbolsAmountCalculator : IOperationHandler
    {
        public string Handle(string message)
        {
            string resultMessage = $"Количество символов в сообщении: {message.Length}";
            return resultMessage;
        }
    }
}
