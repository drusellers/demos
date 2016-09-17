using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.RollingFile;

namespace demo.logging
{
    class Program
    {
        static void Main(string[] args)
        {
            Serilog.LoggerConfiguration cfg = new LoggerConfiguration()
//                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
//                {
//                    AutoRegisterTemplate = true,
//                    BufferBaseFilename = @"C:\temp\elk",
//                    BufferLogShippingInterval = TimeSpan.FromSeconds(5),
//                    MinimumLogEventLevel = LogEventLevel.Verbose
//                })
                .WriteTo.Console(LogEventLevel.Information)
                .WriteTo.Sink(new RollingFileSink("log.txt",new JsonFormatter(), 2097152, 5, Encoding.UTF8 ))
                ;

            var logger = cfg.CreateLogger();


            logger.Debug("HI:");
            logger.Verbose("HI:");
            logger.Information("HI:");
            logger.Error("HI:");
            logger.Warning("HI:");
            logger.Fatal("HI:");

            var x = $@"Debug: {logger.IsEnabled(LogEventLevel.Debug)}
Verbose: {logger.IsEnabled(LogEventLevel.Verbose)}
Info: {logger.IsEnabled(LogEventLevel.Information)}
Error: {logger.IsEnabled(LogEventLevel.Error)}
Warning: {logger.IsEnabled(LogEventLevel.Warning)}
Fatal: {logger.IsEnabled(LogEventLevel.Fatal)}";

            logger.Fatal(x);

            Console.ReadKey(true);
        }
    }
}
