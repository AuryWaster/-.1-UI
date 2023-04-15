using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibSpline
{
    public class SplineDataItem
    {
        public double Coord { get; set; }
        public double Spline { get; set; }
        public double FirstDer { get; set; }
        public double SecondDer { get; set; }

        public string Repr
        {
            get { return this.ToString(); }
        }
        public SplineDataItem(double coord, double spline, double firstDer, double secondDer)
        {
            Coord = coord;
            Spline = spline;
            FirstDer = firstDer;
            SecondDer = secondDer;
        }
        public string ToString(string format = "{0:f3}") => $"Координаты: {string.Format(format, Coord)}" +
            $"\nЗначение сплайна: {string.Format(format, Spline)}" +
            $"\nПервая призводная: {string.Format(format, FirstDer)}" +
            $"\nВторая производная: {string.Format(format, SecondDer)}\n";
        public override string ToString() => $" Координаты: {Coord}\n Значение сплайна: {Spline}" +
            $"\n Первая производная: {FirstDer}\n Вторая производная: {SecondDer}\n";
    }
}
