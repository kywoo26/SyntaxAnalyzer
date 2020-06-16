using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    public enum TokenName
    {
        IDENTIFIER,                                                 //identifier
        INT_VALUE, CHAR_VALUE, BOOL_VALUE, FLOAT_VALUE,             //value
        INT, CHAR, BOOL, FLOAT, IF, ELSE, WHILE, FOR, RETURN,       //keyword
        ADD, SUBTRACT, MULTIFLY, DIVIDE,                            //  ┐
        LSHIFT, RSHIFT, AND, OR, ASSIGN,                            //operator
        LESS, GREATER, LESSEQUAL, GREATEREQUAL, EQUAL, NOTEQUAL,    //  ┘
        SEMICOLON, LBRACE, RBRACE, LPAREN, RPAREN, COMMA,           //context symbol
        DEFAULT
    }

    public class Token
    {
        public TokenName Name { get; set; }
        public string Value { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Token(TokenName name = TokenName.DEFAULT, string value = "", int row = 0, int col = 0)
        {
            this.Name = name;
            this.Value = value;
            this.Row = row;
            this.Col = col;
        }
    }
}
