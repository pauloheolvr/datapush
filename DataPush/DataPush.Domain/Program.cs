using DataPush.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DataPush.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = GetLogger<DownloadAndProcess>();
            var project = new DownloadAndProcess(loggerFactory);
            project.Run();
        }

        private static ILogger<TClass> GetLogger<TClass>()
        {
            var serilogLogger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();
            var loggerFactory = new LoggerFactory()
                .AddSerilog(serilogLogger)
                .CreateLogger<TClass>();
            return loggerFactory;
        }
    }
}
