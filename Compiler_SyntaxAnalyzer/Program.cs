using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Compiler_SyntaxAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Input file is necessary.");
                return;
            }

            string inputFile = args[0];
            //string inputFile = "test.out";
            string currentDirectory = System.Environment.CurrentDirectory;
            string path = currentDirectory + $@"\{inputFile}";

            if (!File.Exists(path))
            {
                Console.WriteLine("The file with that name does not exist.");
                return;
            }

            var tokens = new List<Token>();
            foreach (var line in File.ReadLines(path))
            {
                var word = line.Split(',');
                tokens.Add(new Token((TokenName)Enum.Parse(typeof(TokenName), word[2]),
                    word[3], int.Parse(word[0]), int.Parse(word[1])));
            }

            var syntaxAnalyzer = new SyntaxAnalyzer();
            if (syntaxAnalyzer.Analyze(tokens))
                Console.WriteLine("Accept!!!!!");
            else
                Console.WriteLine("Fail!!!!");
        }
    }
}
