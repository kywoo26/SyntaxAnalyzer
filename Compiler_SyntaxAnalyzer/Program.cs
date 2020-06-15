using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new SLRTable();
            int i = 0;

            foreach (var s in table.State)
            {
                Console.WriteLine(i++);
                foreach (var t in s.Transition)
                {
                    Console.WriteLine($"{t.Key}, {t.Value.TransitionType}, {t.Value.Destination}");
                }
            }
        }
    }
}
