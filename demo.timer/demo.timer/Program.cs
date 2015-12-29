using System;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace demo.timer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BOOT: " + DateTime.Now + " " + Thread.CurrentThread.ManagedThreadId);

            var ft = new FancyTimer(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("HI: " + DateTime.Now + " " + Thread.CurrentThread.ManagedThreadId);
            });
            
            Console.ReadKey();
        }
    }


    public class FancyTimer : IDisposable
    {
        System.Threading.Timer _timer;
        readonly Action _wrappedAction;

        private TimeSpan _startNow = TimeSpan.Zero;
        private TimeSpan _interval = TimeSpan.FromSeconds(1);

        public FancyTimer(Action wrapperAction)
        {
            _timer = new System.Threading.Timer(Callback, null, _startNow, _interval);
            _wrappedAction = wrapperAction;
        }

        private void Callback(object state)
        {
            try
            {
                //stop ticking
                _timer.Change(Timeout.Infinite, Timeout.Infinite);

                _wrappedAction();
            }
            catch (Exception)
            {
                //log here 
            }
            finally
            {
                //restart ticking
                _timer.Change(_interval, Timeout.InfiniteTimeSpan);
            }
        }


        public void Dispose()
        {
            _timer.Dispose();
            
            _timer = null;
        }
    }
}
