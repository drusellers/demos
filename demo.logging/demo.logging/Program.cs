using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace demo.logging
{
    class Program
    {
        static void Main(string[] args)
        {
            Serilog.LoggerConfiguration cfg = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    BufferBaseFilename = @"C:\temp\elk",
                    BufferLogShippingInterval = TimeSpan.FromSeconds(5),
                    MinimumLogEventLevel = LogEventLevel.Verbose
                });

            cfg.CreateLogger().Information("HI");
            Console.ReadKey(true);
        }
    }
}
