using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Core;
using Serilog.Events;

namespace Hearts.Application
{
    public class BaseTest
    {
        private Logger logger;
        protected ILoggerFactory loggerFactory;

        [SetUp]
        public void Init()
        {
            var logFilename = Path.Combine("Logs", NUnit.Framework.TestContext.CurrentContext.Test.FullName + ".log");
            DeleteLogFile(logFilename);
            CreateLogger(logFilename);
            loggerFactory = new SerilogLoggerFactory(logger, false);
        }

        [TearDown]
        public void Cleanup()
        {
            logger.Dispose();
        }

        protected ILogger<T> GetLogger<T>()
        {
            return loggerFactory.CreateLogger<T>();
        }

        private void CreateLogger(string filename)
        {
            logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.File(filename)
                        .CreateLogger();
        }

        private void DeleteLogFile(string filename)
        {
            File.Delete(filename);
        }

    }
}