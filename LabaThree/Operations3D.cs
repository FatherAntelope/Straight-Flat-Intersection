using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LabaThree
{
    class Operations3D
    {
        string statusMessage = "";
        double calculateLengthVector(double x, double y, double z)
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        double calculateScalarVector(double x1, double y1, double z1, double w1 ,double x2, double y2, double z2, double w2)
        {
            return x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2;
        }

        double calculateCosVectors(double x1, double y1, double z1, double w1,  double x2, double y2, double z2, double w2)
        {
            if (calculateLengthVector(x1, y1, z1) == 0 || calculateLengthVector(x2, y2, z2) == 0)
                return calculateScalarVector(x1, y1, z1, w1, x2, y2, z2, w2);
            return 
                (calculateScalarVector(x1, y1, z1, w1, x2, y2, z2, w2)) 
                /
                (calculateLengthVector(x1, y1, z1) * calculateLengthVector(x2, y2, z2)
            );
        }

        Point4D calculateCompositionVectors(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double x = y1 * z2 - z1 * y2,
                   y = z1 * x2 - x1 * z2,
                   z = x1 * y2 - y1 * x2;
            return new Point4D(x, y, z, 1);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////

        //метод Ньюэла
        double[] methodNewell(Point4D p1, Point4D p2, Point4D p3) 
        {
            double[] coefficient = new double[4];
            coefficient[0] = (p1.Y - p2.Y) * (p1.Z + p2.Z) + 
                             (p2.Y - p3.Y) * (p2.Z + p3.Z) + 
                             (p3.Y - p1.Y) * (p3.Z + p1.Z);

            coefficient[1] = (p1.Z - p2.Z) * (p1.X + p2.X) + 
                             (p2.Z - p3.Z) * (p2.X + p3.X) + 
                             (p3.Z - p1.Z) * (p3.X + p1.X);

            coefficient[2] = (p1.X - p2.X) * (p1.Y + p2.Y) + 
                             (p2.X - p3.X) * (p2.Y + p3.Y) + 
                             (p3.X - p1.X) * (p3.Y + p1.Y);

            coefficient[3] = -(coefficient[0] * p1.X + 
                               coefficient[1] * p1.Y + 
                               coefficient[2] * p1.Z);
            return coefficient;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////

        //вырожденность плоскости в прямую (проверка)
        bool checkFlatAtLine(double kA, double kB, double kC)
        {
            return kA == 0 && kB == 0 && kC == 0;
        }

        //вырожденность плоскости в точку (проверка)
        bool checkFlatAtPoint(Point4D A, Point4D B, Point4D C)
        {
            return A.Equals(B) && B.Equals(C);
        }

        //вырожденность прямой (проверка)
        int checkDegenLine(Point4D p1, Point4D p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z)
                return 1; //вырождена
            return 0; //не вырождена
        }

        //вырожденность плоскости (проверка)
        int checkDegenFlat(Point4D p1, Point4D p2, Point4D p3) 
        {
            double[] coefficient = methodNewell(p1, p2, p3);
            double a = coefficient[0],
                   b = coefficient[1],
                   c = coefficient[2];

            if(checkFlatAtLine(a, b, c))
            {
                if (checkFlatAtPoint(p1, p2, p3))
                    return 2;
                return 1;
            }
            return 0;
        }

        //принадлежность точки плоскости
        bool checkPointBelongFlat(double kA, double kB, double kC, double kD, Point4D p)
        {
            if (p.X * kA + p.Y * kB + p.Z * kC + kD == 0)
                return true;
            return false;
        }

        int checkLineBelongFlat(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C)
        {
            double[] coefficient = methodNewell(A, B, C);
            double a = coefficient[0],
                   b = coefficient[1],
                   c = coefficient[2],
                   d = coefficient[3];
            if (checkPointBelongFlat(a, b, c, d, M) && checkPointBelongFlat(a, b, c, d, N))
                return 1;
            return 0;
        }

        bool checkPointBelongLine(Point4D M, Point4D N, Point4D A, double eps)
        {
            if (M.Equals(A) || N.Equals(A))
            {
                return true;
            }
            double t = Math.Abs(calculateCosVectors(N.X - M.X,
                                           N.Y - M.Y,
                                           N.Z - M.Z,
                                           N.W - M.W,
                                           A.X - M.X,
                                           A.Y - M.Y,
                                           A.Z - M.Z,
                                           A.W - M.W));
            return 1 - eps < t && t < 1 + eps;
        }

        //проверка параллельности прямой и плоскости
        int checkLineParallelFlat(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C, double eps)
        {
            if(checkLineBelongFlat(M, N, A, B, C) == 0)
            {
                double[] coefficient = methodNewell(A, B, C);
                double a = coefficient[0],
                       b = coefficient[1],
                       c = coefficient[2],
                       d = coefficient[3];

                if (a * (M.X - N.X) + b * (M.Y - N.Y) + c * (M.Z - N.Z) == 0)
                    return 1;
            }


            //if (intersectionFlatLine(M, N, A, B, C, eps) == null && checkLineBelongFlat(M, N, A, B, C) == 0)
            //    return 1;
            return 0;
        }

        //проверка параллельности двух прямых
        int checkParallelLines(Point4D M, Point4D N, Point4D A, Point4D B, double eps)
        {
            /*
            Point4D Q1 = new Point4D(M.X + B.X - A.X,
                                     M.Y + B.Y - A.Y,
                                     M.Z + B.Z - A.Z, 1);
            if (checkPointBelongLine(M, N, Q1, eps))
                return 1;

            Point4D Q2 = new Point4D(A.X + N.X - M.X,
                                     A.Y + N.Y - M.Y,
                                     A.Z + N.Z - M.Z, 1);
            if (checkPointBelongLine(A, B, Q2, eps))
                return 1;
            */

            if(checkСrossLines(M, N, A, B) == 0)
            {
                double ValueA = calculateCosVectors(N.X - M.X,
                                               N.Y - M.Y,
                                               N.Z - M.Z,
                                               N.W - M.W,
                                               N.X - A.X,
                                               N.Y - A.Y,
                                               N.Z - A.Z,
                                               N.W - A.W);
                double ValueB = calculateCosVectors(A.X - B.X,
                                               A.Y - B.Y,
                                               A.Z - B.Z,
                                               A.W - B.W,
                                               A.X - N.X,
                                               A.Y - N.Y,
                                               A.Z - N.Z,
                                               A.W - N.W);
                if(ValueA != 0 && ValueB != 0)
                    if (Math.Abs(ValueA) == Math.Abs(ValueB))
                        return 1;
            }
            return 0;
        }

        //проверка скрещивания прямых
        int checkСrossLines(Point4D M, Point4D N, Point4D A, Point4D B) 
        {
            double[] coefficient;
            if(checkDegenFlat(A, B, M) == 1)
            {
                coefficient = methodNewell(A, B, N);
                double a = coefficient[0],
                       b = coefficient[1],
                       c = coefficient[2],
                       d = coefficient[3];
                if (checkPointBelongFlat(a, b, c, d, M))
                    return 0;
                return 1;
            }
            else
            {
                coefficient = methodNewell(A, B, M); //ОШИБКА!!!
                double a = coefficient[0],
                       b = coefficient[1],
                       c = coefficient[2],
                       d = coefficient[3];
                if (checkPointBelongFlat(a, b, c, d, N))
                    return 0;
                return 1;
            }
        }

        //проверка совпадения двух прямых
        int checkSameLines(Point4D M, Point4D N, Point4D A, Point4D B, double eps) 
        {
            if(checkPointBelongLine(M, N, A, eps))
            {
                if (checkPointBelongLine(M, N, B, eps))
                    return 1;
            }

            if (checkPointBelongLine(A, B, M, eps))
            {
                if (checkPointBelongLine(A, B, N, eps))
                    return 1;
            }

            return 0;
        }

        //MN - прямая, ABC - плоскость
        //пересечение прямой с плоскостью
        Point4D intersectionFlatLine(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C, double eps)
        {
            double[] coefficient = methodNewell(A, B, C);
            double a = coefficient[0],
                   b = coefficient[1],
                   c = coefficient[2],
                   d = coefficient[3];
            double t = (-1) * ((M.X * a + M.Y * b + M.Z * c + d) /
                ((N.X - M.X) * a + (N.Y - M.Y) * b + (N.Z - M.Z) * c));
            if ((-1) * eps <= t && t <= eps + 1)
            {
                Point4D T = new Point4D(0, 0, 0, 1);
                T.X = (M.X + (N.X - M.X) * t);
                T.Y = (M.Y + (N.Y - M.Y) * t);
                T.Z = (M.Z + (N.Z - M.Z) * t);
                return T;
            }
            return null; //должно вернуть 0
        }

        //MN - прямая, ABC - прямая
        //пересечение двух прямых
        Point4D intersectionLines(Point4D M, Point4D N, Point4D A, Point4D B, double eps)
        {
            if (checkСrossLines(M, N, A, B) == 0 &&
                checkSameLines(M, N, A, B, eps) == 0 &&
                checkParallelLines(M, N, A, B, eps) == 0 && !A.Equals(B))
            {
                Point4D Vector = calculateCompositionVectors(B.X - A.X,
                                                             B.Y - A.Y,
                                                             B.Z - A.Z,
                                                             N.X - M.X,
                                                             N.Y - M.Y,
                                                             N.Z - M.Z);

                Point4D Z = new Point4D(Vector.X + A.X, Vector.Y + A.Y, Vector.Z + A.Z, 1);
                Point4D T = intersectionFlatLine(M, N, A, B, Z, eps);
                return T;
            }
            return null;
        }


        //MN - прямая, ABC - точка
        //принадлежность точки к отрезку прямой
        Point4D intersectionPointLine(Point4D M, Point4D N, Point4D A, double eps)
        {
            if (checkPointBelongLine(M, N, A, eps))
                return A;
            else
            {
                if (A.Equals(M) || A.Equals(N))
                    return A;
            }

            return null;
        }

        Point4D intersectionPointLine2(Point4D M, Point4D N, Point4D A, double eps)
        {
            if (checkPointBelongLine(M, N, A, eps))
            {
                double value1 = calculateLengthVector(M.X - A.X, M.Y - A.Y, M.Z - A.Z) +
                    calculateLengthVector(N.X - A.X, N.Y - A.Y, N.Z - A.Z);
                double value2 = calculateLengthVector(N.X - M.X, N.Y - M.Y, N.Z - M.Z);
                if (value1 == value2)
                    return A;
            }
            return null;
        }


        //MN - точка, ABC - плоскость
        //принадлежность точки к плоскости
        Point4D intersectionPointFlat(Point4D M, Point4D A, Point4D B, Point4D C)
        {
            double[] coefficient = methodNewell(A, B, C);
            double a = coefficient[0],
                   b = coefficient[1],
                   c = coefficient[2],
                   d = coefficient[3];

            if (checkPointBelongFlat(a, b, c, d, M))
                return M;
            return null;
        }


        //MN - точка, ABC - точка
        //Совпадение точек
        Point4D intersectionPoints(Point4D M, Point4D A)
        {
            if (M.Equals(A))
                return M;
            return null;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////

        int checkLookAtObserved(double A, double B, double C, double D, Point4D L, double eps)
        {
            if (calculateCosVectors(A, B, C, D, L.X, L.Y, L.Z, L.W) < 0 - eps)
                return 0;
            return 1;
        }

        int checkPositionFlat(double A, double B, double C, double D, Point4D L, double eps)
        {
            if (calculateCosVectors(A, B, C, D, L.X, L.Y, L.Z, L.W) < eps)
                return 0;
            return 1;
        }

        Section4D sectionVisibleLine(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C, Point4D L, Point4D T, double eps)
        {
            double[] coefficient = methodNewell(A, B, C);
            double a = coefficient[0],
                   b = coefficient[1],
                   c = coefficient[2],
                   d = coefficient[3];

            if (checkDegenFlat(A, B, C) > 0)
            {
                return new Section4D(M, N);
            }

            if(checkLookAtObserved(a, b, c, d, L, eps) == 0)
            {
                a = -1 * a;
                b = -1 * b;
                c = -1 * c;
                d = -1 * d;
            }

            if(checkPositionFlat(a, b, c , d, L, eps) == 0)
            {
                return new Section4D(M, N);
            }

            if(calculateCosVectors(a,b,c,d, M.X, M.Y, M.Z, N.W) >= 0)
            {
                if (calculateCosVectors(a, b, c, d, N.X, N.Y, N.Z, N.W) >= 0)
                    return new Section4D(M, N);
                else
                    return new Section4D(M, T);
            }
            else
            {
                if (calculateCosVectors(a, b, c, d, N.X, N.Y, N.Z, N.W) > 0)
                    return new Section4D(T, N);
                else
                    return null;
            }
        }

        public void VisibleMN(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C, Point4D L, Point4D T, double eps, ref bool isVisMN, ref bool isVisM, ref bool isVisN, ref bool isNoVis)
        {
            isVisMN = false; isVisM = false; isVisN = false; isNoVis = false;
            Section4D VisibleSection = sectionVisibleLine(M, N, A, B, C, L, T, eps);
            Section4D compare1 = new Section4D(M, N);
            Section4D compare2 = new Section4D(M, T);
            Section4D compare3 = new Section4D(T, N);

            if (VisibleSection == null)
                return;
            if(!compare1.CheckNull())
                if (VisibleSection.Equals(compare1))
                {
                    isVisMN = true;
                    return;
                }

            if (!compare2.CheckNull())
                if (VisibleSection.Equals(compare2))
                {
                    isVisM = true;
                    return;
                }

            if (!compare3.CheckNull())
                if (VisibleSection.Equals(compare3))
                {
                    isVisN = true;
                    return;
                }
            isNoVis = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////

        public Point4D pointTIntersection(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C, double eps, ref Label mesABC, ref Label mesMN)
        {
            Point4D T = null;
            if(checkDegenLine(M, N) == 0 && checkDegenFlat(A, B, C) == 0)
            {
                mesABC.Text = "Плоскость ABC"; mesMN.Text = "Прямая MN";
                T = intersectionFlatLine(M, N, A, B, C, eps);
                if(T != null)
                    statusMessage = "Пересечение прямой с плоскостью";
                else
                {
                    if(checkLineParallelFlat(M,N,A,B,C, eps) == 1)
                        statusMessage = "Прямая и плоскость параллельны";
                    else
                        statusMessage = "Прямая и плоскость не пересекаются";
                }
                    
                return T;
            }

            if(checkDegenLine(M, N) == 0 && checkDegenFlat(A, B, C) == 1)
            {
                if (A.Equals(B))
                    mesABC.Text = "Прямая AC(BC)";
                if (A.Equals(C))
                    mesABC.Text = "Прямая AB(BC)";
                if (B.Equals(C))
                    mesABC.Text = "Прямая AB(AC)";

                mesMN.Text = "Прямая MN";
                

                if (intersectionLines(M, N, A, B, eps) != null)
                {
                    statusMessage = "Прямые MN и AB пересекаютcя";
                    return intersectionLines(M, N, A, B, eps);
                }
                else
                    statusMessage = "Прямые MN и AB не пересекаются";

                if (intersectionLines(M, N, B, C, eps) != null)
                {
                    statusMessage = "Прямые MN и BC пересекаютcя";
                    return intersectionLines(M, N, B, C, eps);
                }
                else
                    statusMessage = "Прямые MN и BC не пересекаются";

                if (intersectionLines(M, N, A, C, eps) != null)
                {
                    statusMessage = "Прямые MN и AC пересекаютcя";
                    return intersectionLines(M, N, A, C, eps);
                }
                else
                    statusMessage = "Прямые MN и AC не пересекаются";

                if(checkParallelLines(M, N, A, B, eps) == 1 && !A.Equals(B))
                    statusMessage = "Прямые MN и AB параллельны";
                if (checkParallelLines(M, N, A, C, eps) == 1 && !A.Equals(C))
                    statusMessage = "Прямые MN и AC параллельны";
                if (checkParallelLines(M, N, B, C, eps) == 1 && !B.Equals(C))
                    statusMessage = "Прямые MN и BC параллельны";

                if (checkСrossLines(M, N, A, B) == 1)
                    statusMessage = "Прямые MN и AB скрещиваются";
                if (checkСrossLines(M, N, A, C) == 1)
                    statusMessage = "Прямые MN и AC скрещиваются";
                if (checkСrossLines(M, N, B, C) == 1)
                    statusMessage = "Прямые MN и BC скрещиваются";

            }

            if (checkDegenLine(M, N) == 0 && checkDegenFlat(A, B, C) == 2)
            {
                mesABC.Text = "Точка A(B, C)"; mesMN.Text = "Прямая MN";
                T = intersectionPointLine2(M, N, A, eps);
                if(T != null)
                    statusMessage = "Пересечение прямой с точкой";
                else
                    statusMessage = "Прямая и точка не пересекаются";
                return T;
            }

            if(checkDegenLine(M, N) == 1 && checkDegenFlat(A, B, C) == 0)
            {
                mesABC.Text = "Плоскость ABC"; mesMN.Text = "Точка M(N)";
                
                T = intersectionPointFlat(M, A, B, C);
                if (T != null)
                    statusMessage = "Пересечение точки с плоскостью";
                else
                    statusMessage = "Точка и плоскость не пересекаются";
                return T;
            }

            if (checkDegenLine(M, N) == 1 && checkDegenFlat(A, B, C) == 1)
            {
                if (A.Equals(B))
                    mesABC.Text = "Прямая AC(BC)";
                if (A.Equals(C))
                    mesABC.Text = "Прямая AB(BC)";
                if (B.Equals(C))
                    mesABC.Text = "Прямая AB(AC)";

                mesMN.Text = "Точка M(N)";

                if (intersectionPointLine(A, B, M, eps) != null)
                    T = intersectionPointLine(A, B, M, eps);
                if(intersectionPointLine(B, C, M, eps) != null)
                    T = intersectionPointLine(B, C, M, eps);
                if (intersectionPointLine(A, C, N, eps) != null)
                    T = intersectionPointLine(A, C, N, eps);

                if (T != null)
                    statusMessage = "Пересечение прямой с точкой";
                else
                    statusMessage = "Прямая и точка не пересекаются";
                return T;
            }

            if (checkDegenLine(M, N) == 1 && checkDegenFlat(A, B, C) == 2)
            {
                mesABC.Text = "Точка A(B, C)"; mesMN.Text = "Точка M(N)";
                
                T = intersectionPoints(M, A);

                if (T != null)
                    statusMessage = "Пересечение двух точек";
                else
                    statusMessage = "Две точки не пересекаются";
                return T;
            }
            //statusMessage = "";
            return null;
        }


        public Section4D pointsTIntersection(Point4D M, Point4D N, Point4D A, Point4D B, Point4D C, double eps)
        {
            if (checkDegenLine(M, N) == 0 && checkDegenFlat(A, B, C) == 0)
                if (checkLineBelongFlat(M, N, A, B, C) == 1)
                {
                    statusMessage = "Прямая лежит на плоскости";
                    return new Section4D(M, N);
                }
                    

            //if (checkDegenLine(M, N) == 0 && checkDegenFlat(A, B, C) == 1)
            //    if (checkSameLines(M, N, A, B, eps) == 1 || checkSameLines(M, N, A, C, eps) == 1 || checkSameLines(M, N, B, C, eps) == 1)
            //    {
            //        statusMessage = "Прямая лежит на прямой";
            //        return new Section4D(M, N);
            //    }

            if (checkDegenLine(M, N) == 0 && checkDegenFlat(A, B, C) == 1)
            {
                if(checkSameLines(M, N, A, B, eps) == 1 && !A.Equals(B))
                {
                    statusMessage = "Прямая лежит на прямой";
                    return new Section4D(M, N);
                }
                if (checkSameLines(M, N, A, C, eps) == 1 && !A.Equals(C))
                {
                    statusMessage = "Прямая лежит на прямой";
                    return new Section4D(M, N);
                }
                if (checkSameLines(M, N, B, C, eps) == 1 && !B.Equals(C))
                {
                    statusMessage = "Прямая лежит на прямой";
                    return new Section4D(M, N);
                }
            }
            return null;
        }
        public string GetStatus()
        {
            return statusMessage;
        }
    }
}
