using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    public enum TransitionType { SHIFT, GOTO, REDUCE }

    class Transition
    {
        public readonly TransitionType Type;
        public readonly int StateNumber;
        public readonly Production Production;

        public Transition(TransitionType type, int stateNumber)
        {
            Type = type;
            StateNumber = stateNumber;
        }

        public Transition(TransitionType type, Production production)
        {
            Type = type;
            Production = production;
        }
    }
}
