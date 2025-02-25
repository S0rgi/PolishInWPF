using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj.Service
{
    class Func
    {
        public static bool Is_operator(char a) => ( (int)a > 41 && (int)a < 48 && (int)a != 44 && (int)a != 46 );

        static bool Is_func(string a) => ( string.Compare(a, "sin") == 0 ) || ( string.Compare(a, "tan") == 0 ) || ( string.Compare(a, "cos") == 0 ) ||
                   ( string.Compare(a, "ctg") == 0 ) || ( string.Compare(a, "sqrt") == 0 ) || ( string.Compare(a, "ln") == 0 );



        public static double Read_PN(List<string> output, string x)
        {
            Stack<string> stack_numbers = new();
            double result;
            int flag = 0;
            for ( int i = 0; i < output.Count && output[i] != "" && flag == 0; i++ )
            {
                if ( ( output[i][0] ) == 'x' || double.TryParse(output[i], out _) || ( output[i][0] == '-' && ( output[i].Length ) > 1 ) )
                {
                    if ( ( output[i][0] ) == 'x' )
                    {
                        stack_numbers.Push(x);
                    }
                    else
                    {
                        stack_numbers.Push(output[i]);
                    }
                }
                else if ( Is_operator(output[i][0]) )
                {
                    flag = read_opp(output[i], stack_numbers);
                }
                else if ( Is_func(output[i]) )
                {
                    flag = read_function(output[i], stack_numbers);
                }
                else
                {
                    //throw new Exception("Error: Unknown token {0}\n", output[i]);
                }
            }
            if ( flag == 0 )
            {
                result = double.Parse(stack_numbers.Pop());
            }
            else
            {
                result = -10;
            }
            stack_numbers.Clear();
            return result;
        }
        static int read_function(string arr, Stack<string> numbers)
        {
            double result = 1;
            int flag = 0;
            double num = double.Parse(numbers.Pop());
            if ( string.Compare(arr, "sin") == 0 )
            {
                // result = sin with top of stack
                result = Math.Sin(num);
            }
            else if ( string.Compare(arr, "tan") == 0 )
            {
                // result = tan with top of stack
                if ( Math.Abs(Math.Cos(num)) > 1E-6 )
                {
                    result = Math.Tan(num);
                }
                else
                {
                    flag = 1;
                }
            }
            else if ( string.Compare(arr, "cos") == 0 )
            {
                // result = cos with top of stack
                result = Math.Cos(num);
            }
            else if ( string.Compare(arr, "ctg") == 0 )
            {
                // result = ctg with top of stack
                if ( Math.Abs(Math.Sin(num)) > 1E-6 )
                {
                    result = 1 / Math.Tan(num);
                }
                else
                {
                    flag = 1;
                }
            }
            else if ( string.Compare(arr, "sqrt") == 0 )
            {
                // result = sqrt with top of stack
                if ( num >= 0 )
                {
                    result = Math.Sqrt(num);
                }
                else
                {
                    flag = 1;
                }
            }
            else if ( string.Compare(arr, "ln") == 0 )
            {
                // result = ln with top of stack
                if ( num > 1E-6 )
                {
                    result = Math.Log(num);
                }
                else
                {
                    flag = 1;
                }
            }

            numbers.Push(result.ToString());
            return flag;
        }
        static int read_opp(string arr, Stack<string> numbers)
        {
            // top = top of stack, del top of stack
            // under = new top of stack, del top of stack
            int flag = 0;
            double result = 0;
            double top = double.Parse(numbers.Pop());
            double under = double.Parse(numbers.Pop());
            // printf("%lf %lf", top, under);
            switch ( arr[0] )
            {
                case '+':
                    // under + top
                    // printf("+");
                    result = under + top;
                    break;

                case '-':

                    result = under - top;

                    // under - top
                    break;

                case '*':
                    // under * top

                    result = under * top;
                    break;

                case '/':
                    // under / top

                    if ( Math.Abs(top) > 1E-6 )
                    {
                        result = under / top;
                    }
                    else
                    {
                        flag = 1;
                    }

                    break;

                default:
                    break;
            }

            numbers.Push(result.ToString());
            return flag;
        }
    }
}
