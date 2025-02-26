using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj.Models
{
    public class GraphData
    {
        [Key]
        public int Id { get; set; }

        public double StartX { get; set; }
        public double EndX { get; set; }
        public double StartY { get; set; }
        public double EndY { get; set; }
        public string FunctionName { get; set; }

        [NotMapped] // Не сохраняем в БД как есть
        public Point[] Points { get; set; }

        public string SerializedPoints
        {
            get => string.Join(";", Points?.Select(p => $"{p.X},{p.Y}") ?? new List<string>());
            set => Points = value?.Split(';')
                .Select(p => p.Split(','))
                .Where(p => p.Length == 2 && double.TryParse(p[0], out _) && double.TryParse(p[1], out _))
                .Select(p => new Point(double.Parse(p[0]), double.Parse(p[1])))
                .ToArray() ?? new Point[0];
        }
    }
}
