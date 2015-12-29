using System;
using System.Collections.Generic;
using System.Linq;
using MVPSI.JAMS;

namespace demo.jams
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = Server.GetServer("localhost");
//            Submit.Info si;
//            Submit.Load(out si, @"\Shared Fulfillment\vendors", s, Submit.Type.Setup);
//            si.Submit();

            var jobName = @"\MRG\Fulfillment\Drop Ship Monitor\DSM-GATHER-PURCHASE-ORDERS";

            CreateJob(jobName,s);
            

            Console.ReadKey(true);
        }

        private static void CreateJob(string jobName, Server server)
        {
            
        }

        


    }

    public class JobInfo
    {
        public string InstallationDir { get; set; }
        public string Exe { get; set; }

        public string Root = "Shared Fulfillment";
        public string System = "Drop Ship Monitoring";
        public string Name { get; set; }
    }

    public class JamService
    {
        private Server _server;

        public void InstallJob(JobInfo info)
        {
            var parts = info.Root
                .Split('\\')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
            var pq = new Queue<string>(parts);

            InnerCreateJob("", pq);
        }

        void InnerCreateJob(string path, Queue<string> left)
        {
            var curr = left.Dequeue();
            var fullPath = string.Join("\\", path, curr);

            if (left.Count == 0)
            {
                if (!Job.Exists(fullPath, _server))
                {
                    var j = new Job();
                    j.Source = "echo DSM";
                    j.SaveAs(fullPath, _server);
                }
                else
                {
                    Job j;
                    Job.Load(out j, fullPath, _server);
                    j.Source = "echo DSM YO";
                    j.Update(_server);
                }

                return;
            }


            if (!Folder.Exists(fullPath))
            {
                var f = new Folder();
                f.SaveAs(fullPath, _server);
            }

            InnerCreateJob(fullPath, left);
        }
    }
}
