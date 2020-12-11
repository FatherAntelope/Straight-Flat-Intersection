using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabaThree
{
    class Axonomertry
    {
        Point4D[] p4d, p4dTrans;
        Point2D[] p2d;
        Point4D p4dT;
        Point2D p2dT;
        Matrix Rz, Rx, Mx, Cz, Pz, T, Ao, Ac;
        MatrixTransform transformMatrix;
        Operations3D operations;
        Section4D section4d, sectionTrans;
        Section2D section2d;
        bool isVisM, isVisN, isVisMN, isNoVis, changeL;

        int lengthAxis, indent;
        double cosRz, cosRx, sinRz, sinRx, c,
               Lx, Ly, Lz, eps;
        Pen red, green, blue, black, aqua, darkOrange;

        public Axonomertry(Point4D[] p4d, Point4D p4dT, Section4D section4d, int lengthAxis, int indent, double eps, bool changeL)
        {
            red = new Pen(Color.Red);
            green = new Pen(Color.Green);
            blue = new Pen(Color.Blue);
            black = new Pen(Color.Black);
            aqua = new Pen(Color.Aqua, 2);
            darkOrange = new Pen(Color.DarkOrange, 2);

            this.changeL = changeL;

            this.p4d = p4d;
            p4dTrans = new Point4D[p4d.Length];
            p2d = new Point2D[p4d.Length];
            this.p4dT = p4dT;
            this.section4d = section4d;
            sectionTrans = new Section4D();
            section2d = new Section2D();


            transformMatrix = new MatrixTransform();
            operations = new Operations3D();
            Rz = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Rx = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Mx = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Pz = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            T = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            Ao = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Cz = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Ac = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            this.Lx = p4d[5].X;
            this.Ly = p4d[5].Y;
            this.Lz = p4d[5].Z;
            this.eps = eps;
            this.lengthAxis = lengthAxis;
            this.indent = indent;
        }

        void FormulaForCos()
        {
            if (checkCoords(Lx, Ly))
            {
                cosRz = Ly / Math.Sqrt(Math.Pow(Lx, 2) + Math.Pow(Ly, 2));
                cosRx = Lz / Math.Sqrt(Math.Pow(Lx, 2) + Math.Pow(Ly, 2) + Math.Pow(Lz, 2));
            }
            else
            {
                cosRz = cosRx = 1;
            }
        }

        void FormulaForSin()
        {
            if (checkCoords(Lx, Ly))
            {
                sinRz = Lx / Math.Sqrt(Math.Pow(Lx, 2) + Math.Pow(Ly, 2));
                sinRx = Math.Sqrt(Math.Pow(Lx, 2) + Math.Pow(Ly, 2)) / Math.Sqrt(Math.Pow(Lx, 2) + Math.Pow(Ly, 2) + Math.Pow(Lz, 2));
            }
            else
            {
                sinRz = sinRx = 0;
            }
        }

        bool checkCoords(double Lx, double Ly)
        {
            if (Lx == 0 && Ly == 0)
                return false;
            else
                return true;
        }

        public void calculateOrthogonal()
        {
            //if(changeL)
            transformMatrixForOrthogonal();
            transformPointsForOrthogonal();
            transformTforOrthogonal();
        }

        public void calculateCentral()
        {
            transformMatrixForCentral();
            transformPointsForCentral();
            transformTforCentral();
        }

        void calculateDataSinAndCos()
        {
            FormulaForCos();
            FormulaForSin();
        }

        void transformTforOrthogonal()
        {
            if (p4dT != null)
            {
                p4dT = p4dT * Ao;
                p2dT = transformPoint(p4dT);
            }
            else if(section4d != null)
            {
                sectionTrans.Start = section4d.Start * Ao;
                sectionTrans.End = section4d.End * Ao;

                section2d.Start = transformPoint(sectionTrans.Start);
                section2d.End = transformPoint(sectionTrans.End);
            }
        }

        void transformTforCentral()
        {
            if (p4dT != null)
            {
                p4dT = p4dT * Ac;
                p4dT = new Point4D
                        (
                        p4dT.X / p4dT.W,
                        p4dT.Y / p4dT.W,
                        p4dT.Z / p4dT.W,
                        p4dT.W / p4dT.W
                        );
                p2dT = transformPoint(p4dT);
            }
            else if (section4d != null)
            {
                sectionTrans.Start = section4d.Start * Ac;
                sectionTrans.End = section4d.End * Ac;

                sectionTrans.Start = new Point4D
                        (
                        sectionTrans.Start.X / sectionTrans.Start.W,
                        sectionTrans.Start.Y / sectionTrans.Start.W,
                        sectionTrans.Start.Z / sectionTrans.Start.W,
                        sectionTrans.Start.W / sectionTrans.Start.W
                        );

                sectionTrans.End = new Point4D
                        (
                        sectionTrans.End.X / sectionTrans.End.W,
                        sectionTrans.End.Y / sectionTrans.End.W,
                        sectionTrans.End.Z / sectionTrans.End.W,
                        sectionTrans.End.W / sectionTrans.End.W
                        );
                section2d.Start = transformPoint(sectionTrans.Start);
                section2d.End = transformPoint(sectionTrans.End);
            }
        }

        void transformMatrixForCentral()
        {
            calculateDataSinAndCos();
            transformMatrix.BaseTransformation(ref Rz, ref Rx, ref Mx, ref Cz, ref Pz, ref T, ref cosRx, ref cosRz, ref sinRx, ref sinRz, ref lengthAxis);
            c = Math.Sqrt(Lx * Lx + Ly * Ly + Lz * Lz);
            transformMatrix.ComplexTransformationForCentral(ref Rz, ref Rx, ref  Mx, ref Cz, ref Pz, ref T, ref Ao, ref Ac, ref c);
        }

        void transformMatrixForOrthogonal()
        {
            calculateDataSinAndCos();
            transformMatrix.BaseTransformation(ref Rz, ref Rx, ref Mx, ref Cz, ref Pz, ref T, ref cosRx, ref cosRz, ref sinRx, ref sinRz, ref lengthAxis);
            transformMatrix.ComplexTransformationForOrthogonal(ref Rz, ref Rx, ref Mx, ref Cz, ref Pz, ref T, ref Ao);
        }

        void transformPointsForCentral()
        {
            for(int i = 0; i < p4d.Length; i++)
            {
                if (p4d[i].name == "X" || p4d[i].name == "Y" || p4d[i].name == "Z")
                    p4dTrans[i] = p4d[i] * Ao;
                else
                {
                    p4dTrans[i] = p4d[i] * Ac;
                    p4dTrans[i] = new Point4D
                        (
                        p4dTrans[i].X / p4dTrans[i].W, 
                        p4dTrans[i].Y / p4dTrans[i].W, 
                        p4dTrans[i].Z / p4dTrans[i].W, 
                        p4dTrans[i].W / p4dTrans[i].W, 
                        p4dTrans[i].name, p4dTrans[i].color
                        );
                    
                }
                p2d[i] = transformPoint(p4dTrans[i]);
            }

        }

        void transformPointsForOrthogonal()
        {
            for (int i = 0; i < p4d.Length; i++)
            {
                p4dTrans[i] = p4d[i] * Ao;
                p2d[i] = transformPoint(p4dTrans[i]);
            }
        }

        public Point2D[] getArrPoint2D()
        {
            return p2d;
        }

        Point2D transformPoint(Point4D p4d)
        {
            return new Point2D(p4d.X, p4d.Y, p4d.name, p4d.color);
        }

        public void DrawAxis(Graphics axonomertryDraw)
        {
            axonomertryDraw.DrawLine(red, (float)p2d[6].X, (float)p2d[6].Y, (float)p2d[9].X, (float)p2d[9].Y); //X - O
            axonomertryDraw.DrawLine(green, (float)p2d[7].X, (float)p2d[7].Y, (float)p2d[9].X, (float)p2d[9].Y); //Y - O
            axonomertryDraw.DrawLine(blue, (float)p2d[8].X, (float)p2d[8].Y, (float)p2d[9].X, (float)p2d[9].Y); //Z - O
        }

        public void DrawLines(Graphics axonomertryDraw)
        {
            axonomertryDraw.DrawLine(aqua, (float)p2d[2].X, (float)p2d[2].Y, (float)p2d[3].X, (float)p2d[3].Y); //A - B
            axonomertryDraw.DrawLine(aqua, (float)p2d[2].X, (float)p2d[2].Y, (float)p2d[4].X, (float)p2d[4].Y); //A - C
            axonomertryDraw.DrawLine(aqua, (float)p2d[3].X, (float)p2d[3].Y, (float)p2d[4].X, (float)p2d[4].Y); //C - B

            operations.VisibleMN(p4d[0], p4d[1], p4d[2], p4d[3], p4d[4], p4d[5], p4dT, eps, ref isVisMN, ref isVisM, ref isVisN, ref isNoVis);

            if (isVisMN)
                axonomertryDraw.DrawLine(darkOrange, (float)p2d[0].X, (float)p2d[0].Y, (float)p2d[1].X, (float)p2d[1].Y); //M - N
            if (isVisM)
                axonomertryDraw.DrawLine(darkOrange, (float)p2d[0].X, (float)p2d[0].Y, (float)p2dT.X, (float)p2dT.Y); //M - T
            if (isVisN)
                axonomertryDraw.DrawLine(darkOrange, (float)p2d[1].X, (float)p2d[1].Y, (float)p2dT.X, (float)p2dT.Y); //N - T

            else if(section4d != null)
                axonomertryDraw.DrawLine(new Pen(Color.IndianRed, 2), (float)section2d.Start.X, (float)section2d.Start.Y, (float)section2d.End.X, (float)section2d.End.Y); //T1 - T2 
        }

        public void DrawLabels(Graphics axonomertryDraw)
        {
            for (int i = 0; i < p2d.Length; i++)
            {
                if (p4d[i].name == "L")
                    continue;
                axonomertryDraw.DrawString(p2d[i].name, new Font("Ariel", 7), Brushes.Black, (float)p2d[i].X, (float)p2d[i].Y);
            }

            if (p4dT != null)
                axonomertryDraw.DrawString("T", new Font("Ariel", 7), Brushes.Black, (float)p2dT.X - indent * 3, (float)p2dT.Y);
            else if (section4d != null)
            {
                axonomertryDraw.DrawString("T1", new Font("Ariel", 7), Brushes.Black, (float)section2d.Start.X - indent*3, (float)section2d.Start.Y);
                axonomertryDraw.DrawString("T2", new Font("Ariel", 7), Brushes.Black, (float)section2d.End.X - indent*3, (float)section2d.End.Y);
            }
        }

        public void DrawPoints(Graphics axonomertryDraw)
        {
            //
            int radius = 4;
            for (int i = 0; i < p2d.Length; i++)
            {
                if (p4d[i].name == "L")
                    continue;
                axonomertryDraw.DrawEllipse(p2d[i].color, (float)p2d[i].X - radius / 2, (float)p2d[i].Y - radius / 2, radius, radius);
            }
            if(p2dT != null)
                axonomertryDraw.DrawEllipse(new Pen(Color.IndianRed, 5), (float)p2dT.X - radius / 2, (float)p2dT.Y - radius / 2, radius, radius);
            else if (section4d != null)
            {
                axonomertryDraw.DrawEllipse(new Pen(Color.IndianRed, 5), (float)section2d.Start.X - radius / 2, (float)section2d.Start.Y - radius / 2, radius, radius);
                axonomertryDraw.DrawEllipse(new Pen(Color.IndianRed, 5), (float)section2d.End.X - radius / 2, (float)section2d.End.Y - radius / 2, radius, radius);
            }
        }
    }
}
