using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace demo.tpl
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationSource = new CancellationTokenSource(4000);


            Console.WriteLine("Press Ctrl+C to cancel..");
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                cancellationSource.Cancel();
            };

            Task t;
            try
            {
                t = DelayAsync(cancellationSource.Token);

                t.Wait(2000);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e=>true);

            }

            Console.WriteLine("Ended.");
            Console.ReadKey();
        }

        static async Task DelayAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancelled");
                    break;
                }


                Console.WriteLine("{0}: hi", DateTime.Now.ToLongTimeString());

                await Task.Delay(1000);
            }
        }

    }
}
