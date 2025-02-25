using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj.Models
{
    public class Point
    {
        double X { get; set; }
        double Y { get; set; }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
