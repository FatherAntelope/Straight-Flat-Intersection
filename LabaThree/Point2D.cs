using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabaThree
{
    class Point2D
    {
        public double X = 0, Y = 0;
        public string name;
        public Pen color;
        public Point2D(double X, double Y, string name, Pen color)
        {
            this.X = X;
            this.Y = Y;
            this.name = name;
            this.color = color;
        }
        public Point2D(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
