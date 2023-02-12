using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolsAmountCalculator.Services
{
    public interface IOperationHandler
    {
        public string Handle(string message);
    }
}
