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
            GraphData data,
            double step)
        {
            var posfix = Lex.Shunting_yard(data.FunctionName);
            for ( double x = data.StartX; x <= data.EndX; x += step )
            {
                yield return new Point(
                    x,
                    Func.Read_PN(posfix, x.ToString()));
            }
        }

    }
}
