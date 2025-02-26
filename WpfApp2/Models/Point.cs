using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace WPF_Proj.Models
{
    public class Point
    {
        public double X { get;  }
        public double Y { get;  }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Print()
        {
            MessageBox.Show($"x : {X,5} , y : {Y,5}");
        }
    }
}
