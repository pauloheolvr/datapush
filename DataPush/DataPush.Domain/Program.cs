using DataPush.Domain.Entities;
using DataPush.Infra.Sql;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DataPush.Domain
{
    class Program
    {
        private static ApplicationContext _context = new ApplicationContext();

        static void Main(string[] args)
        {
            DeleteAndCreateDataBase();
            var downloadAndProcess = DownloadDatas();
            downloadAndProcess.Run();
        }

        private static DownloadAndProcess DownloadDatas()
        {
            var loggerFactory = GetLogger<DownloadAndProcess>();
            var project = new DownloadAndProcess(loggerFactory);
            return project;
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

        public static void DeleteAndCreateDataBase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}