using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabaThree
{
    class Complex
    {
        Point4D[] p4d;
        Point2D[] p2d, p2dT;
        Point4D p4dT;
        int lengthAxis, indent;
        Pen red, green, blue, black, aqua, darkOrange, orangeRed;
        public Complex(Point4D[] p4d, Point4D p4dT, int lengthAxis, int indent)
        {
            red = new Pen(Color.Red);
            green = new Pen(Color.Green);
            blue = new Pen(Color.Blue);
            black = new Pen(Color.Black);
            aqua = new Pen(Color.Aqua);
            darkOrange = new Pen(Color.DarkOrange);

            orangeRed = new Pen(Color.OrangeRed);

            this.p4dT = p4dT;
            this.p4d = p4d;
            this.lengthAxis = lengthAxis;
            this.indent = indent;
        }

        public void Circulates2D()
        {
            p2d = new Point2D[]
            {
                new Point2D(lengthAxis - p4d[0].X, lengthAxis, "Mx", p4d[0].color), //0
                new Point2D(lengthAxis, lengthAxis + p4d[0].Y, "My1", p4d[0].color), //1
                new Point2D(lengthAxis + p4d[0].Y, lengthAxis, "My3", p4d[0].color), //2
                new Point2D(lengthAxis, lengthAxis - p4d[0].Z, "Mz", p4d[0].color), //3
                new Point2D(lengthAxis - p4d[0].X, lengthAxis + p4d[0].Y, "M1", p4d[0].color), //4
                new Point2D(lengthAxis - p4d[0].X, lengthAxis - p4d[0].Z, "M2", p4d[0].color), //5
                new Point2D(lengthAxis + p4d[0].Y, lengthAxis - p4d[0].Z, "M3", p4d[0].color), //6

                new Point2D(lengthAxis - p4d[1].X, lengthAxis, "Nx", p4d[1].color), //7
                new Point2D(lengthAxis, lengthAxis + p4d[1].Y, "Ny1", p4d[1].color), //8
                new Point2D(lengthAxis + p4d[1].Y, lengthAxis, "Ny3", p4d[1].color), //9
                new Point2D(lengthAxis, lengthAxis - p4d[1].Z, "Nz", p4d[1].color), //10
                new Point2D(lengthAxis - p4d[1].X, lengthAxis + p4d[1].Y, "N1", p4d[1].color), //11
                new Point2D(lengthAxis - p4d[1].X, lengthAxis - p4d[1].Z, "N2", p4d[1].color), //12
                new Point2D(lengthAxis + p4d[1].Y, lengthAxis - p4d[1].Z, "N3", p4d[1].color), //13

                new Point2D(lengthAxis - p4d[2].X, lengthAxis, "Ax", p4d[2].color), //14
                new Point2D(lengthAxis, lengthAxis + p4d[2].Y, "Ay1", p4d[2].color), //15
                new Point2D(lengthAxis + p4d[2].Y, lengthAxis, "Ay3", p4d[2].color), //16
                new Point2D(lengthAxis, lengthAxis - p4d[2].Z, "Az", p4d[2].color), //17
                new Point2D(lengthAxis - p4d[2].X, lengthAxis + p4d[2].Y, "A1", p4d[2].color), //18
                new Point2D(lengthAxis - p4d[2].X, lengthAxis - p4d[2].Z, "A2", p4d[2].color), //19
                new Point2D(lengthAxis + p4d[2].Y, lengthAxis - p4d[2].Z, "A3", p4d[2].color), //20

                new Point2D(lengthAxis - p4d[3].X, lengthAxis, "Bx", p4d[3].color), //21
                new Point2D(lengthAxis, lengthAxis + p4d[3].Y, "By1", p4d[3].color), //22
                new Point2D(lengthAxis + p4d[3].Y, lengthAxis, "By3", p4d[3].color), //23
                new Point2D(lengthAxis, lengthAxis - p4d[3].Z, "Bz", p4d[3].color), //24
                new Point2D(lengthAxis - p4d[3].X, lengthAxis + p4d[3].Y, "B1", p4d[3].color), //25
                new Point2D(lengthAxis - p4d[3].X, lengthAxis - p4d[3].Z, "B2", p4d[3].color), //26
                new Point2D(lengthAxis + p4d[3].Y, lengthAxis - p4d[3].Z, "B3", p4d[3].color), //27

                new Point2D(lengthAxis - p4d[4].X, lengthAxis, "Cx", p4d[4].color), //28
                new Point2D(lengthAxis, lengthAxis + p4d[4].Y, "Cy1", p4d[4].color), //29
                new Point2D(lengthAxis + p4d[4].Y, lengthAxis, "Cy3", p4d[4].color), //30
                new Point2D(lengthAxis, lengthAxis - p4d[4].Z, "Cz", p4d[4].color), //32
                new Point2D(lengthAxis - p4d[4].X, lengthAxis + p4d[4].Y, "C1", p4d[4].color), //32
                new Point2D(lengthAxis - p4d[4].X, lengthAxis - p4d[4].Z, "C2", p4d[4].color), //33
                new Point2D(lengthAxis + p4d[4].Y, lengthAxis - p4d[4].Z, "C3", p4d[4].color), //34

                new Point2D(lengthAxis - p4d[5].X, lengthAxis, "Lx", p4d[5].color), //35
                new Point2D(lengthAxis, lengthAxis + p4d[5].Y, "Ly1", p4d[5].color), //36
                new Point2D(lengthAxis + p4d[5].Y, lengthAxis, "Ly3", p4d[5].color), //37
                new Point2D(lengthAxis, lengthAxis - p4d[5].Z, "Lz", p4d[5].color), //38
                new Point2D(lengthAxis - p4d[5].X, lengthAxis + p4d[5].Y, "L1", p4d[5].color), //39
                new Point2D(lengthAxis - p4d[5].X, lengthAxis - p4d[5].Z, "L2", p4d[5].color), //40
                new Point2D(lengthAxis + p4d[5].Y, lengthAxis - p4d[5].Z, "L3", p4d[5].color), //41

                new Point2D(indent, lengthAxis, p4d[6].name, p4d[6].color), //42
                new Point2D(lengthAxis, lengthAxis * 2 - indent, p4d[7].name + "1", p4d[7].color), //43
                new Point2D(lengthAxis * 2 - indent, lengthAxis, p4d[7].name + "3", p4d[7].color), //44
                new Point2D(lengthAxis, indent, p4d[8].name, p4d[8].color), //45
                new Point2D(lengthAxis, lengthAxis, p4d[9].name, p4d[9].color) //46
            };

            if(p4dT != null)
            {
                p2dT = new Point2D[]
                {
                    new Point2D(lengthAxis - p4dT.X, lengthAxis, "Tx", darkOrange), //0
                    new Point2D(lengthAxis, lengthAxis + p4dT.Y, "Ty1", darkOrange), //1
                    new Point2D(lengthAxis + p4dT.Y, lengthAxis, "Ty3", darkOrange), //2
                    new Point2D(lengthAxis, lengthAxis - p4dT.Z, "Tz", darkOrange), //3
                    new Point2D(lengthAxis - p4dT.X, lengthAxis + p4dT.Y, "T1", darkOrange), //4
                    new Point2D(lengthAxis - p4dT.X, lengthAxis - p4dT.Z, "T2", darkOrange), //5
                    new Point2D(lengthAxis + p4dT.Y, lengthAxis - p4dT.Z, "T3", darkOrange), //6
                };
            }
        }

        public void DrawAxis(Graphics complexDraw)
        {
            complexDraw.DrawLine(red, (float)p2d[46].X, (float)p2d[46].Y, (float)p2d[42].X, (float)p2d[42].Y); //O - X
            complexDraw.DrawLine(green, (float)p2d[46].X, (float)p2d[46].Y, (float)p2d[43].X, (float)p2d[43].Y); //O - Y1
            complexDraw.DrawLine(green, (float)p2d[46].X, (float)p2d[46].Y, (float)p2d[44].X, (float)p2d[44].Y); //O - Y3
            complexDraw.DrawLine(blue, (float)p2d[46].X, (float)p2d[46].Y, (float)p2d[45].X, (float)p2d[45].Y); //O - Z
        }


        public void DrawLines(Graphics complexDraw)
        {

            int s = 0;
            for (int i = 0; i < 6; i++)
            {
                complexDraw.DrawLine(p2d[0 + s].color, (float)p2d[0 + s].X, (float)p2d[0 + s].Y, (float)p2d[4 + s].X, (float)p2d[4 + s].Y); //Mx M1
                complexDraw.DrawLine(p2d[0 + s].color, (float)p2d[0 + s].X, (float)p2d[0 + s].Y, (float)p2d[5 + s].X, (float)p2d[5 + s].Y); //Mx M2
                complexDraw.DrawLine(p2d[0 + s].color, (float)p2d[2 + s].X, (float)p2d[2 + s].Y, (float)p2d[6 + s].X, (float)p2d[6 + s].Y); //My3 M3
                complexDraw.DrawLine(p2d[0 + s].color, (float)p2d[3 + s].X, (float)p2d[3 + s].Y, (float)p2d[6 + s].X, (float)p2d[6 + s].Y); //Mz M3
                complexDraw.DrawLine(p2d[0 + s].color, (float)p2d[3 + s].X, (float)p2d[3 + s].Y, (float)p2d[5 + s].X, (float)p2d[5 + s].Y); //Mz M2
                complexDraw.DrawLine(p2d[0 + s].color, (float)p2d[4 + s].X, (float)p2d[4 + s].Y, (float)p2d[1 + s].X, (float)p2d[1 + s].Y); //M1 My1

                if (p2d[1 + s].Y - lengthAxis > 0)
                {
                    complexDraw.DrawArc(p2d[0 + s].color, new Rectangle((int)p2d[46].X - ((int)p2d[2 + s].X - lengthAxis), (int)p2d[46].Y - ((int)p2d[2 + s].X - lengthAxis), (int)p2d[2 + s].X - lengthAxis + ((int)p2d[2 + s].X - lengthAxis), (int)p2d[2 + s].X - lengthAxis + ((int)p2d[2 + s].X - lengthAxis)), 0, 90);
                }
                s = s + 7;
            }

            complexDraw.DrawLine(orangeRed, (float)p2d[4].X, (float)p2d[4].Y, (float)p2d[11].X, (float)p2d[11].Y);
            complexDraw.DrawLine(orangeRed, (float)p2d[5].X, (float)p2d[5].Y, (float)p2d[12].X, (float)p2d[12].Y);
            complexDraw.DrawLine(orangeRed, (float)p2d[6].X, (float)p2d[6].Y, (float)p2d[13].X, (float)p2d[13].Y);

            complexDraw.DrawLine(black, (float)p2d[18].X, (float)p2d[18].Y, (float)p2d[25].X, (float)p2d[25].Y);
            complexDraw.DrawLine(black, (float)p2d[19].X, (float)p2d[19].Y, (float)p2d[26].X, (float)p2d[26].Y);
            complexDraw.DrawLine(black, (float)p2d[20].X, (float)p2d[20].Y, (float)p2d[27].X, (float)p2d[27].Y);

            complexDraw.DrawLine(black, (float)p2d[18].X, (float)p2d[18].Y, (float)p2d[32].X, (float)p2d[32].Y);
            complexDraw.DrawLine(black, (float)p2d[19].X, (float)p2d[19].Y, (float)p2d[33].X, (float)p2d[33].Y);
            complexDraw.DrawLine(black, (float)p2d[20].X, (float)p2d[20].Y, (float)p2d[34].X, (float)p2d[34].Y);

            complexDraw.DrawLine(black, (float)p2d[25].X, (float)p2d[25].Y, (float)p2d[32].X, (float)p2d[32].Y);
            complexDraw.DrawLine(black, (float)p2d[26].X, (float)p2d[26].Y, (float)p2d[33].X, (float)p2d[33].Y);
            complexDraw.DrawLine(black, (float)p2d[27].X, (float)p2d[27].Y, (float)p2d[34].X, (float)p2d[34].Y);

            if (p2dT != null)
            {
                complexDraw.DrawLine(p2dT[0].color, (float)p2dT[0].X, (float)p2dT[0].Y, (float)p2dT[4].X, (float)p2dT[4].Y);
                complexDraw.DrawLine(p2dT[0].color, (float)p2dT[0].X, (float)p2dT[0].Y, (float)p2dT[5].X, (float)p2dT[5].Y); 
                complexDraw.DrawLine(p2dT[0].color, (float)p2dT[2].X, (float)p2dT[2].Y, (float)p2dT[6].X, (float)p2dT[6].Y); 
                complexDraw.DrawLine(p2dT[0].color, (float)p2dT[3].X, (float)p2dT[3].Y, (float)p2dT[6].X, (float)p2dT[6].Y);
                complexDraw.DrawLine(p2dT[0].color, (float)p2dT[3].X, (float)p2dT[3].Y, (float)p2dT[5].X, (float)p2dT[5].Y); 
                complexDraw.DrawLine(p2dT[0].color, (float)p2dT[4].X, (float)p2dT[4].Y, (float)p2dT[1].X, (float)p2dT[1].Y);

                if ((int)p2dT[1].Y - lengthAxis > 0)
                {
                    complexDraw.DrawArc(p2dT[0].color, new Rectangle((int)p2d[46].X - ((int)p2dT[2].X - lengthAxis), (int)p2d[46].Y - ((int)p2dT[2].X - lengthAxis), (int)p2dT[2].X - lengthAxis + ((int)p2dT[2].X - lengthAxis), (int)p2dT[2].X - lengthAxis + ((int)p2dT[2].X - lengthAxis)), 0, 90);
                }
            }
        }


        public void DrawLabels(Graphics complexDraw)
        {
            for (int i = 0; i < p2d.Length; i++)
            {
                complexDraw.DrawString(p2d[i].name, new Font("Ariel", 7), Brushes.Black, (float)p2d[i].X, (float)p2d[i].Y);
            }
            if (p2dT != null)
            {
                for (int i = 0; i < p2dT.Length; i++)
                {
                    complexDraw.DrawString(p2dT[i].name, new Font("Ariel", 7), Brushes.Black, (float)p2dT[i].X, (float)p2dT[i].Y);
                }
            }
        }

        public void DrawPoints(Graphics complexDraw)
        {
            int radius = 4;
            for (int i = 0; i < p2d.Length; i++)
            {
                complexDraw.DrawEllipse(p2d[i].color, (float)p2d[i].X - radius / 2, (float)p2d[i].Y - radius / 2, radius, radius);
            }

            if (p2dT != null)
            {
                for (int i = 0; i < p2dT.Length; i++)
                {
                    complexDraw.DrawEllipse(p2dT[i].color, (float)p2dT[i].X - radius / 2, (float)p2dT[i].Y - radius / 2, radius, radius);
                }
            }
        }
    }
}
