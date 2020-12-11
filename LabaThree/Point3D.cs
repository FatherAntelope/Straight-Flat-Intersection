using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabaThree
{
    class Point3D
    {
        public int X = 0, Y = 0, Z = 0;
        public string name;
        public Pen color;
        public Point3D(int X, int Y, int Z, string name, Pen color)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.name = name;
            this.color = color;
        }
    }
}
