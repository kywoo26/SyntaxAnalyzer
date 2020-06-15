using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    class State
    {
        private Dictionary<string, Transition> transition;
        public IReadOnlyDictionary<string, Transition> Transition { get { return transition; } }

        // By the content, do decision.
        public State()
        {
            transition = new Dictionary<string, Transition>();
        }

        public void Add(string content, Transition decision)
        {
            transition.Add(content, decision);
        }
    }
}
