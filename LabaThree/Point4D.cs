using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace LabaThree
{
    class Point4D
    {
        public double X = 0, Y = 0, Z = 0, W = 0;
        public string name;
        public Pen color;
        public Point4D(double X, double Y, double Z, double W, string name, Pen color)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
            this.name = name;
            this.color = color;
        }

        public Point4D(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }

        public Matrix PointToMatrix(Point4D point)
        {
            //
            return new Matrix(point.X, point.Y, point.Z, point.W);
        }

        public static Point4D MatrixToPoint(Matrix A, Point4D point)
        {
            //
            return new Point4D(A.matrix[0][0], A.matrix[0][1], A.matrix[0][2], A.matrix[0][3], point.name, point.color);
        }

        public static Point4D operator *(Point4D point, Matrix A)
        {
            Matrix Temp = point.PointToMatrix(point);
            return MatrixToPoint((Temp * A), point);
        }

        public bool Equals(Point4D point)
        {
            return X == point.X && Y == point.Y && Z == point.Z;
        }
    }
}
