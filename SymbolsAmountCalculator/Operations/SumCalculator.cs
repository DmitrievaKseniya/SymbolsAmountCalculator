using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymbolsAmountCalculator.Services;

namespace SymbolsAmountCalculator.Operations
{
    public class SumCalculator : IOperationHandler
    {
        public string Handle(string message)
        {
            string[] numberFromMessage = message.Split(' ');
            try
            {
                int sum = numberFromMessage.Select(n => int.Parse(n)).Sum();
                string resultMessage = $"Сумма чисел: {sum}";
                return resultMessage;
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка при расчете суммы.\nВозможно в сообщении указаны не только числа.\nПопробуйте снова");
            }
        }
    }
}
