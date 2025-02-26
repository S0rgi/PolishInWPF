using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPF_Proj.Service
{
    class Lex
    {
        private static readonly Dictionary<string, int> functions = new()
        {
            { "ln", 2 }, { "sin", 3 }, { "cos", 3 }, { "tan", 3 }, { "ctg", 3 }, { "sqrt", 4 }
        };

        private static readonly Dictionary<char, int> operations = new()
        {
            { '(', 0 }, { ')', 0 }, { '*', 2 }, { '/', 2 }, { '+', 1 }, { '-', 1 }
        };

        private static bool Is_alpha(char c) => ( c >= 'a' && c <= 'z' ) || ( c >= 'A' && c <= 'Z' );
        private static bool Is_function(string func) => functions.ContainsKey(func);
        private static int Get_operator_priority(char op) => operations[op];

        static int Process_number(string expression, List<string> output)
        {
            string pattern = @"-?\d+(\.\d+)?([eE][-+]?\d+)?";
            Match match = Regex.Match(expression, pattern);

            if ( match.Success && double.TryParse(match.Value, out _) )
            {
                output.Add(match.Value);
            }
            return match.Value.Length;
        }

        public static List<string> Shunting_yard(string expression)

        {
            List<string> output = new();
            Stack<string> op_stack = new();

            for ( int i = 0; i < expression.Length; i++ )
            {
                if ( char.IsDigit(expression[i]) )
                {
                    i += Process_number(expression.Substring(i), output) - 1;
                }
                else if ( expression[i] == 'x' )
                {
                    output.Add(expression.Substring(i, 1));
                }
                else if ( i + 2 < expression.Length && functions.ContainsKey(expression.Substring(i, 2)) )
                {
                    op_stack.Push(expression.Substring(i, 2));
                    i += 1;
                }
                else if ( i + 3 < expression.Length && functions.ContainsKey(expression.Substring(i, 3)) )
                {
                    op_stack.Push(expression.Substring(i, 3));
                    i += 2;
                }
                else if ( i + 4 < expression.Length && functions.ContainsKey(expression.Substring(i, 4)) )
                {
                    op_stack.Push(expression.Substring(i, 4));
                    i += 3;
                }
                else if ( expression[i] == '(' )
                {
                    op_stack.Push("(");
                }
                else if ( expression[i] == ')' )
                {
                    process_closing_parenthesis(output, op_stack);
                }
                else if ( operations.ContainsKey(expression[i]) )
                {
                    process_operator(expression[i], output, op_stack);
                }
            }

            while ( op_stack.Count > 0 )
            {
                output.Add(op_stack.Pop());
            }
            return output;
        }

        private static void process_operator(char opp, List<string> output, Stack<string> op_stack)
        {
            while ( op_stack.Count > 0 &&
                   operations.ContainsKey(op_stack.Peek()[0]) &&
                   Get_operator_priority(op_stack.Peek()[0]) >= Get_operator_priority(opp) )
            {
                output.Add(op_stack.Pop());
            }
            op_stack.Push(opp.ToString());
        }



        private static void process_closing_parenthesis(List<string> output, Stack<string> op_stack)
        {
            while ( op_stack.Count > 0 && op_stack.Peek() != "(" )
            {
                output.Add(op_stack.Pop());
            }
            if ( op_stack.Count > 0 && op_stack.Peek() == "(" )
            {
                op_stack.Pop();
            }
        }
    }
}
