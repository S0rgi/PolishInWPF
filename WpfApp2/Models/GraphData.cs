using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj.Models
{
    public class GraphData
    {
        public double StartX { get; set; }
        public double EndX { get; set; }
        public double StartY { get; set; }
        public double EndY { get; set; }
        public string FunctionName { get; set; }
        public Point[] Points { get; set; }
    }
}
