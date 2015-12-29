
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;

namespace demo.roslyn
{
    public class TryIt
    {
        public static async Task<bool> Start()
        {
            var name = AppDomain.CurrentDomain.FriendlyName;
            var options = ScriptOptions.Default.AddNamespaces("demo.roslyn")
                .AddNamespaces("System")
                .AddReferences(typeof (Standard).Assembly);
            var global = new Global2{Vendor = new Vendor {MfCost = "abc"}};

            var script = await CSharpScript.RunAsync(@"

Standard c(Vendor v)
{
    var s = new Standard();
    s.Cost = v.MfCost;
    return s;
}
var i = c(Vendor);
", options, global);

            var standard = script.Variables.First(v => v.Name == "i").Value;
            //standard.Cost = "abc"

            return true;
        }

        public static async Task<bool> Eval()
        {
            var script = CSharpScript.Create(@"

Destination.Cost = Source.MfCost;

", ScriptOptions.Default.AddNamespaces("demo.roslyn")
                .AddReferences(typeof(Standard).Assembly), globalsType: typeof(Global<Vendor, Standard>));

            var globals = new Global<Vendor, Standard>(new Vendor { MfCost = "hi" });
            var scriptState = await script.EvaluateAsync(globals);


            //globals.Destination.Cost =="hi";

            return true;
        }

        public void Go()
        {
            Task.Run<bool>((Func<Task<bool>>)Start).Wait();
        }
    }

    public class Global2
    {
        public Vendor Vendor { get; set; }
    }
    
    public class Global<TSource, TDestination> where TSource :new() where TDestination : new()
    {
        public Global(TSource source)
        {
            Source = source;
            Destination = new TDestination();
        }
        public TSource Source { get; set; }
        public TDestination Destination { get; set; }
    }

    public class Vendor
    {
        public string MfCost { get; set; }
    }

    public class Standard
    {
        public string Cost { get; set; }
    }
}
