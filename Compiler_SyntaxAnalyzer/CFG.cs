using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    static class CFG
    {
        private static List<Production> productionList;
        public static IReadOnlyList<Production> Production { get { return productionList; } }
        public static Terminal Terminal = new Terminal();

        static CFG()
        {
            productionList = GetProductionListFromFile();
        }

        private static List<Production> GetProductionListFromFile()
        {
            var productionList = new List<Production>();
            string productionFileName = "Compiler_SyntaxAnalyzer.Resources.Production.txt";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(productionFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var grammar = line.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                        var production = new Production(grammar[0].Trim());
                        var sentinels = grammar[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        // If the derivation is "ϵ", nothing will be added in this production.
                        if (sentinels[0] != "ϵ")
                        {
                            foreach (var sentinel in sentinels)
                                production.Add(sentinel.Trim());
                        }

                        productionList.Add(production);
                    }
                }
            }

            return productionList;
        }
    }

    class Production
    {
        private string startSymbol;
        private List<string> derivation;

        public string StartSymbol { get { return startSymbol; } }
        public IReadOnlyList<string> Derivation { get { return derivation; } }

        public Production(string symbol)
        {
            startSymbol = symbol;
            derivation = new List<string>();
        }

        public void Add(string sentinel)
        {
            derivation.Add(sentinel);
        }
    }

    class Terminal
    {
        public string this[TokenName name]
        {
            get
            {
                if (name == TokenName.INT || name == TokenName.FLOAT
                    || name == TokenName.BOOL || name == TokenName.CHAR)
                    return "vtype";
                else if (name == TokenName.INT_VALUE)
                    return "num";
                else if (name == TokenName.FLOAT_VALUE)
                    return "float";
                else if (name == TokenName.CHAR_VALUE)
                    return "literal";
                else if (name == TokenName.IDENTIFIER)
                    return "id";
                else if (name == TokenName.IF)
                    return "if";
                else if (name == TokenName.ELSE)
                    return "else";
                else if (name == TokenName.WHILE)
                    return "while";
                else if (name == TokenName.FOR)
                    return "for";
                else if (name == TokenName.RETURN)
                    return "return";
                else if (name == TokenName.ADD || name == TokenName.SUBTRACT)
                    return "addsub";
                else if (name == TokenName.MULTIFLY || name == TokenName.DIVIDE)
                    return "multdiv";
                else if (name == TokenName.ASSIGN)
                    return "assign";
                else if (name == TokenName.LESS || name == TokenName.GREATER
                    || name == TokenName.LESSEQUAL || name == TokenName.GREATEREQUAL
                    || name == TokenName.EQUAL || name == TokenName.NOTEQUAL)
                    return "comp";
                else if (name == TokenName.SEMICOLON)
                    return "semi";
                else if (name == TokenName.COMMA)
                    return "comma";
                else if (name == TokenName.LPAREN)
                    return "lparen";
                else if (name == TokenName.RPAREN)
                    return "rparen";
                else if (name == TokenName.LBRACE)
                    return "lbrace";
                else if (name == TokenName.RBRACE)
                    return "rbrace";
                else
                    throw new Exception($"There's not terminal matched with token name : \"{name}\".");
            }
        }
    }
}
