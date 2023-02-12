using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using SymbolsAmountCalculator.Controllers;
using SymbolsAmountCalculator.Services;
using SymbolsAmountCalculator.Configuration;

namespace SymbolsAmountCalculator
{
    internal class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();

            //Подключаем контроллеры сообщений и кнопок
            services.AddTransient<CalculationAmountController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<OperationsManager>();

            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6216775254:AAGOUJ7h8RdBNpSL9tBOxJ-RfyiYRnFbyrs"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6216775254:AAGOUJ7h8RdBNpSL9tBOxJ-RfyiYRnFbyrs"
            };
        }
    }
}