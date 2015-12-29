using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace demo.hound
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(@"
public class Bob
{
    public string Name { get; set; }  
    public string Bob { get; set; }
}
");
            var bob = x.GetRoot().ChildNodes().First();
            walk(0, bob);

            Console.ReadKey();
        }

        private static void walk(int depth, SyntaxNodeOrToken node)
        {
            trace(depth, node);
            trace(depth, node.Kind());
            var trivia = node.GetTrailingTrivia();
            foreach (var t in trivia)
            {
                //looking for white space trivia before the end of line trivia
                trace(depth, string.Format("TRIVIA: {0}", t.Kind()));
            }

            foreach (var n in node.ChildNodesAndTokens())
            {
                walk(depth + 1, n);
            }
        }

        static void trace(int depth, object arg)
        {
            Console.WriteLine("{0}{1}",new string(' ',depth), arg);
        }




    }
}
