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
                t = TrackHighwaterMark(cancellationSource.Token);

                t.Wait(2000);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e=>true);

            }


            Console.WriteLine("Ended.");
            Console.ReadKey();
        }

        static int Concurrent;
        static int HighWaterC = 0;

        static async Task TrackHighwaterMark(CancellationToken token)
        {
            var tasks = new List<Task>();

            foreach (var i in Enumerable.Range(0,1000))
            {
                var t = Task.Run(() =>
                {
                    Interlocked.Increment(ref Concurrent);


                    if (Concurrent > HighWaterC)
                    {
                        Interlocked.CompareExchange(ref HighWaterC, Concurrent, HighWaterC);

                    }

                    Console.WriteLine(i);
                    Thread.Sleep(100);

                    Interlocked.Decrement(ref Concurrent);
                });

                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Concurrent:{Concurrent} - HighWater: {HighWaterC}");
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
