using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymbolsAmountCalculator.Models;

namespace SymbolsAmountCalculator.Services
{
    public class MemoryStorage : IStorage
    {
        //Хранилище сессий
        private readonly ConcurrentDictionary<long, Session> _sessions;
        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }
        public Session GetSession(long chatId)
        {
            //возвращаем сессию  по ключуб если она есть
            if (_sessions.ContainsKey(chatId)) 
                return _sessions[chatId];

            //создаем и возвращаем сессию если такой не было
            var newSession = new Session() { OperationCode = "Characters" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
