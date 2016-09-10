using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace demo.hound
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(File.ReadAllText("sample.txt"));
            var root = x.GetRoot().ChildNodes().First();
            new TrailingWhiteSpaceRule().Walk(root);

            Console.WriteLine("Done");
            Console.ReadKey();
        }


    }

    public class TrailingWhiteSpaceRule
    {
        public void Walk(SyntaxNodeOrToken root)
        {
            var q = new Queue<SyntaxNodeOrToken>();

            q.Enqueue(root);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (current == null)
                    continue;

                foreach (var child in current.ChildNodesAndTokens())
                {
                    q.Enqueue(child);
                }

                Check(current);
            }
        }

        void Check(SyntaxNodeOrToken node)
        {
            var trivia = node.GetTrailingTrivia();
            for (int i = 0; i < trivia.Count; i++)
            {
                var inspect = trivia[i];
                if (inspect.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    if (i > 0 && trivia[i - 1].IsKind(SyntaxKind.WhitespaceTrivia))
                    {
                        Console.WriteLine($"Found whitespace at {node.GetLocation().GetLineSpan().EndLinePosition}");
                    }
                }
            }
        }
    }
}
