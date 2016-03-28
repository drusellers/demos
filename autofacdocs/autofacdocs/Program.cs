using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Builders;
using MassTransit.Configurators;

namespace autofacdocs
{
    public class Ping
    {
        public int Num { get; set; }
    }

    public class PingConsumer : MassTransit.IConsumer<Ping>
    {
        public async Task Consume(ConsumeContext<Ping> context)
        {
            foreach (var header in context.Headers.GetAll())
            {
                Console.WriteLine("{0}:{1}", header.Key, header.Value);
            }
            Thread.Sleep(100);
            Console.WriteLine(10 / context.Message.Num);
        }
    }

    public class PingFaultConsumer : MassTransit.IConsumer<Fault<Ping>>
    {
        public async Task Consume(ConsumeContext<Fault<Ping>> context)
        {
            var msg = $"You can't divide 10 by {context.Message.Message.Num}";
            Console.WriteLine(msg);
        }
    }

    class Program
    {
        static int Main(string[] args)
        {
            return DoStuff().Result;
        }

        static async Task<int> DoStuff()
        {
            var bc = Bus.Factory.CreateUsingInMemory(cfg =>
            {
                cfg.TransportConcurrencyLimit = 5;

                cfg.ReceiveEndpoint("customer_update_queue", ec =>
                {
                    ec.UseRetry(Retry.Immediate(5));
                    ec.Consumer<PingConsumer>();
                    ec.Consumer<PingFaultConsumer>();
                });
            });


            var h = bc.Start();

            Console.WriteLine("Started. Key to send data.");
            Console.ReadKey();
            var ep = bc.GetSendEndpoint(new Uri("loopback://localhost/customer_update_queue")).Result;
            for (int i = 1; i < 3; i++)
            {
                await ep.Send(new Ping
                {
                    Num = i
                });
                Console.WriteLine("Queue Message:  " + i);
            }
            Console.ReadKey();
            h.Stop();

            return 0;
        }
    }
}
