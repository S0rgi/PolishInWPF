using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

using System.Windows.Controls;
using WPF_Proj.Models;
namespace WPF_Proj.VIews
{
    public class ViewGraph
    {
        Canvas GraphCanvas;
        GraphData data;
        public ViewGraph(Canvas canv,GraphData  grData) 
        {
            data = grData;
            GraphCanvas = canv;
        }
        public void DrawGraph(IEnumerable<Point> points)
        {
            GraphCanvas.Children.Clear();

            Polyline polyline = new Polyline
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            foreach ( var point in points )
            {
                polyline.Points.Add(new System.Windows.Point(
                    MapX(point.X),
                    MapY(point.Y)
                ));
            }

            GraphCanvas.Children.Add(polyline);
        }

        private double MapX(double x)
        {
            // Преобразуем X в координаты Canvas
            return ( x - data.StartX ) / ( data.EndX - data.StartX ) * GraphCanvas.Width;
        }

        private double MapY(double y)
        {
            // Преобразуем Y в координаты Canvas (инвертируем ось Y)
            return GraphCanvas.Height - ( y - data.StartY ) / ( data.EndY - data.StartY ) * GraphCanvas.Height;
        }

    }
}
