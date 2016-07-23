using System;
using Polly;
using Polly.CircuitBreaker;

namespace demo.polly
{
    class Program
    {
        // - the policy can go into the container
        static void Main(string[] args)
        {
            //QUESTIONS: Where does the policy go?
            //ANSWER: Right under the controller so it can report back a usable status
            //        then i won't need a foodity toggle


            //QUESTION: How can i setup RETRY and CIRCUIT BREAKER
            //          Retry 3 times, then break for 10 minutes
            var policy = BreakCircuitAfterOneError();



            //simulate external callers
            for (int i = 0; i < 5; i++)
            {
                //controller is called
                var r = policy.ExecuteAndCapture(() => DoSomething());

                if (!(r.FinalException is BrokenCircuitException))
                {
                    SendPagerDutyAlert(r);
                    //do your thing Pager Duty
                }

                Console.WriteLine("{0}:{1}", r.Outcome, r.FinalException.Message);
            }


            Console.WriteLine("DONE");
            Console.ReadLine();

        }


        private static Policy BreakCircuitAfterOneError()
        {
            return Policy.Handle<Exception>()
               .CircuitBreaker(1, TimeSpan.FromMinutes(60));
        }

        private static Policy RetryPolicy()
        {
            var policyTimeSpans = new TimeSpan[5];
            policyTimeSpans[0] = TimeSpan.FromSeconds(0.5);
            policyTimeSpans[1] = TimeSpan.FromSeconds(1);
            policyTimeSpans[2] = TimeSpan.FromSeconds(1.5);

            var throttlePolicy = Policy.Handle<Exception>()
                .WaitAndRetry(3, (i) => policyTimeSpans[i]);

            return throttlePolicy;
        }

        private static void SendPagerDutyAlert(PolicyResult policyResult)
        {
            Console.WriteLine("---PAGER DUTY---");
            Console.WriteLine("{0}",policyResult.FinalException.Message);
            Console.WriteLine("---PAGER DUTY---");
        }

        private static void DoSomething()
        {
            int i = 0;
            throw new NotImplementedException();
        }
    }
}
