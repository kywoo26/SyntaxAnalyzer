using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    class SyntaxAnalyzer
    {
        private SLRTable table; //코드 다 짜놓고 table[2][id] 이런식으로 접근 가능할지
        private Stack<int> stateStack;
        private List<string> sentinel;
        private int splitter;

        public SyntaxAnalyzer()
        {
            table = new SLRTable();
            stateStack = new Stack<int>();
            splitter = 0;
        }

        public bool Analyze(List<Token> tokens)
        {
            bool isAccepted = false;
            var state = table.State;
            sentinel = GetSentence(tokens);

            // Start from state 0.
            stateStack.Push(0);
            while (!isAccepted)
            {
                // Debug
                var temp = new List<string>();
                temp.AddRange(sentinel);
                temp.Insert(splitter, "|");
                foreach (var i in temp)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();

                int current = stateStack.Peek();
                string symbol = sentinel[splitter];
                Transition transition;

                if (table.State[current].Transition.TryGetValue(symbol, out transition))
                {
                    if (transition.Type == TransitionType.SHIFT)
                    {
                        // Debug
                        Console.WriteLine($"Shift and goto {current}");

                        Shift(transition.StateNumber);
                    }
                    else if (transition.Type == TransitionType.REDUCE)
                    {
                        if (transition.Production == CFG.Production[0])
                        {
                            isAccepted = true;
                            break;
                        }

                        // Debug
                        Console.Write($"Reduce by {transition.Production.StartSymbol} -> ");
                        var tempD = transition.Production.Derivation;
                        if (tempD.Count > 0)
                            tempD.ToList().ForEach(d => Console.Write(d + " "));
                        else
                            Console.Write("e");
                        Console.WriteLine();

                        Reduce(transition.Production);
                    }
                }
                else
                {
                    isAccepted = false;
                    break;
                }
            }

            return isAccepted;
        }

        private void Shift(int stateNumber)
        {
            splitter++;
            stateStack.Push(stateNumber);
        }

        private void Reduce(Production production)
        {
            int count = production.Derivation.Count;
            string startSymbol = production.StartSymbol;

            // This works well when derivation "ϵ", because count is 0.
            // ex) For aT*E|b$, Reduce by E -> T*E. count = |T*E| = 3.
            splitter -= count;                      // a|T*Eb$          
            sentinel.RemoveRange(splitter, count);  // a|b$
            sentinel.Insert(splitter, startSymbol); // a|Eb$        
            splitter++;                             // aE|b$

            // Pop 3 contents from the stack.
            for (int i = 0; i < count; i++)
                stateStack.Pop();

            // Push next state( = GOTO(top of stack number, E) )
            int top = stateStack.Peek();
            Transition transition;
            table.State[top].Transition.TryGetValue(startSymbol, out transition);
            stateStack.Push(transition.StateNumber);
        }

        private List<string> GetSentence(List<Token> tokens)
        {
            var sentence = new List<string>();
            foreach (var name in tokens.Select(t => t.Name))
                sentence.Add(CFG.Terminal[name]);
            sentence.Add("$");

            return sentence;
        }
    }
}
