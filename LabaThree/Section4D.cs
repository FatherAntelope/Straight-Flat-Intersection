using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaThree
{
    class Section4D
    {
        public Point4D Start;
        public Point4D End;

        public Section4D()
        {
            Start = new Point4D(0,0,0,1);
            End = new Point4D(0,0,0,1);
        }

        public Section4D(Point4D Start, Point4D End)
        {
            this.Start = Start;
            this.End = End;
        }

        public Section4D(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            Start = new Point4D(x1, y1, z1, 1);
            End = new Point4D(x2, y2, z2, 1);
        }

        public bool Equals(Section4D Another)
        {
            if (Another.End.X != this.End.X || Another.End.Y != this.End.Y)
            {
                return false;
            }

            if (Another.Start.X != this.Start.X || Another.Start.Y != this.Start.Y)
            {
                return false;
            }

            return true;
        }

        public bool CheckNull()
        {
            if (Start == null || End == null)
                return true;
            return false;
        }
    }
}
