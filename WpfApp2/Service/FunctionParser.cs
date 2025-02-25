using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Proj.Models;
namespace WPF_Proj.Service
{
    public static class FunctionParser
    {
        public static IEnumerable<Point> CalculatePoints(
            string function,
            double xMin,
            double xMax,
            double step)
        {
            var posfix = Lex.Shunting_yard(function);
            for ( double x = xMin; x <= xMax; x += step )
            {
                yield return new Point(
                    x,
                    Func.Read_PN(posfix, x.ToString()));
            }
        }

    }
}
