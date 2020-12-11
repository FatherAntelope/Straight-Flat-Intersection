using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaThree
{
    class Section2D
    {
        public Point2D Start;
        public Point2D End;

        public Section2D()
        {
            Start = new Point2D(0, 0);
            End = new Point2D(0, 0);
        }

        public Section2D(Point2D Start, Point2D End)
        {
           this.Start = Start;
           this.End = End;
        }

        public Section2D(double x1, double y1, double x2, double y2)
        {
            Start = new Point2D(x1, y1);
            End = new Point2D(x2, y2);
        }

        public bool Equals(Section2D Another)
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
    }
}
