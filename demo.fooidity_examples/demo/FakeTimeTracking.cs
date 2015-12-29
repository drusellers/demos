using System;
using System.Linq;

namespace demo
{
    public class FakeTimeTracking
    {
        public void Generate()
        {
            var d = new DateTime(2015, 09, 01, 2, 0, 0);
            var weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday, };

            Func<DateTime, string> selectGna = (doo) =>
            {
                if (doo > new DateTime(2015, 09, 01))
                {
                    return "SF-495";
                }
                else if (doo > new DateTime(2015, 09, 09))
                {
                    return "SF-501";
                }
                return "SF-502";
            };

            foreach (var i in Enumerable.Range(0, 31))
            {
                var dd = d.AddDays(i);
                var gna = selectGna(dd);

                if (weekend.Contains(dd.DayOfWeek)) continue;

                Console.WriteLine("node bin/jira worklogadd {1} 30m \"Stand Up and Time Tracking\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"), gna);

                if (dd.DayOfWeek == DayOfWeek.Tuesday)
                {
                    Console.WriteLine("node bin/jira worklogadd {1} 90m \"Staff Meeting\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"), gna);
                    Console.WriteLine("node bin/jira worklogadd {1} 60m \"O3 Barcz\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"), gna);
                    Console.WriteLine("node bin/jira worklogadd PMO-6 300m \"Code\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"));
                }
                else if (dd.DayOfWeek == DayOfWeek.Monday)
                {
                    Console.WriteLine("node bin/jira worklogadd {1} 30m \"O3 Walz\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"), gna);
                    Console.WriteLine("node bin/jira worklogadd PMO-6 60m \"JDE Staff Meeting\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"));
                    Console.WriteLine("node bin/jira worklogadd PMO-6 360m \"Code\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"));
                }
                else if (dd.DayOfWeek == DayOfWeek.Thursday)
                {
                    Console.WriteLine("node bin/jira worklogadd {1} 60m \"Small Meeting\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"), gna);
                    Console.WriteLine("node bin/jira worklogadd PMO-6 360m \"Code\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"));
                }
                else
                {
                    Console.WriteLine("node bin/jira worklogadd PMO-6 450m \"Code\" -s \"{0}\"", dd.ToString("MM-dd-yyyy"));
                }
            }

            Console.ReadKey();
        } 
    }
}