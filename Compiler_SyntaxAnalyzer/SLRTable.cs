using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    class SLRTable
    {
        private List<State> state;
        public IReadOnlyList<State> State { get { return state; } }

        public SLRTable()
        {
            state = GetStateListFromFile();
        }

        private List<State> GetStateListFromFile()
        {
            var stateList = new List<State>();
            var symbol = new List<string>();
            string tableFileName = "Compiler_SyntaxAnalyzer.Resources.Table.csv";

            // Create a csv file of SLR tables derived from DFA.
            // And build by making it resources.
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(tableFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    // To store the sting of contents at first line.
                    // ex) "*", ")", "CODE"
                    string line = reader.ReadLine();
                    symbol.AddRange(line.Split(','));

                    // To store the decision with state and symbol after the second line.
                    // ex) 1 => goto 1, S1 => shift 1, R2 => reduce 2,
                    while ((line = reader.ReadLine()) != null)
                    {
                        var state = new State();
                        var decision = line.Split(',');

                        for (int i = 1; i < decision.Length; i++)
                        {
                            if (String.IsNullOrEmpty(decision[i]))
                                continue;

                            Transition transition;
                            if (decision[i].StartsWith("s"))
                                transition = new Transition(TransitionType.SHIFT, int.Parse(decision[i].Substring(1)));
                            else if (decision[i].StartsWith("r"))
                                transition = new Transition(TransitionType.REDUCE, CFG.Production[int.Parse(decision[i].Substring(1))]);
                            else
                                transition = new Transition(TransitionType.GOTO, int.Parse(decision[i]));

                            state.Add(symbol[i], transition);
                        }

                        stateList.Add(state);
                    }
                }
            }

            return stateList;
        }
    }
}
