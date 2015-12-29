using System;
using System.Collections.Generic;
using MVPSI.JAMS.Host;

namespace demo.jamsjob
{
    public class JamEmbedded : IJAMSHost
    {
        public void Initialize(IServiceProvider serviceProvider, Dictionary<string, object> attributes, Dictionary<string, object> parameters)
        {
            System.IO.File.Create(parameters["FileName"].ToString());
        }

        public FinalResults Execute(IServiceProvider serviceProvider, Dictionary<string, object> attributes, Dictionary<string, object> parameters)
        {
            System.IO.File.AppendAllText(parameters["FileName"].ToString(), "HI:"+ DateTime.Now);
            return new FinalResults(0,0,"SUCCESS");
        }

        public void Cancel(IServiceProvider serviceProvider)
        {
            // do nothing
        }

        public void Cleanup(IServiceProvider serviceProvider, Dictionary<string, object> attributes, Dictionary<string, object> parameters)
        {
            //do clean up - aka dispose
        }
    }
}