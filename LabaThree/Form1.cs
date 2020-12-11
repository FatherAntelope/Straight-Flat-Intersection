using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabaThree
{
    public partial class Form1 : Form
    {
        int Mx, My, Mz,
            Nx, Ny, Nz,
            Ax, Ay, Az,
            Bx, By, Bz,
            Cx, Cy, Cz,
            Lx, Ly, Lz;
        Point4D T;
        Section4D sectionTpoints;
        bool changeL, isOrthogonal, errorsAvailable;
        double eps;
        int lengthAxis, indent;
        string errorMsg;
        Point4D[] p4d;
        Operations3D operations = new Operations3D();

        Point2D[] p2d;
        Axonomertry axonomertry;
        Complex complex;
        Bitmap bitmapAxonometry, bitmapComplex;

        private void trackAx_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackAy_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackAz_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackMx_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackMy_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackMz_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackNx_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackNy_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackNz_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackBx_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackBy_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackBz_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackCx_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackCy_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackCz_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void trackE_Scroll(object sender, EventArgs e)
        {
            changeL = false;
            allOperations();
        }

        private void radioBtnCenter_CheckedChanged(object sender, EventArgs e)
        {
            changeL = true;
            inputData(trackMx.Value, trackMy.Value, trackMz.Value, trackNx.Value, trackNy.Value, trackNz.Value, trackAx.Value, trackAy.Value, trackAz.Value, trackBx.Value, trackBy.Value, trackBz.Value, trackCx.Value, trackCy.Value, trackCz.Value, trackLx.Value, trackLy.Value, trackLz.Value, trackE.Value);
            drawAndCalculateAxonometry();
        }

        private void radioBtnOrthogonal_CheckedChanged(object sender, EventArgs e)
        {
            changeL = true;
            inputData(trackMx.Value, trackMy.Value, trackMz.Value, trackNx.Value, trackNy.Value, trackNz.Value, trackAx.Value, trackAy.Value, trackAz.Value, trackBx.Value, trackBy.Value, trackBz.Value, trackCx.Value, trackCy.Value, trackCz.Value, trackLx.Value, trackLy.Value, trackLz.Value, trackE.Value);
            drawAndCalculateAxonometry();
        }

        private void trackLx_Scroll(object sender, EventArgs e)
        {
            changeL = true;
            inputData(trackMx.Value, trackMy.Value, trackMz.Value, trackNx.Value, trackNy.Value, trackNz.Value, trackAx.Value, trackAy.Value, trackAz.Value, trackBx.Value, trackBy.Value, trackBz.Value, trackCx.Value, trackCy.Value, trackCz.Value, trackLx.Value, trackLy.Value, trackLz.Value, trackE.Value);
            allOperations();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackLy_Scroll(object sender, EventArgs e)
        {
            changeL = true;
            allOperations();
        }

        private void trackLz_Scroll(object sender, EventArgs e)
        {
            changeL = true;
            allOperations();
        }

        public Form1()
        {
            InitializeComponent();
            indent = 5;
            lengthAxis = (Math.Min(pictureAxonometry.Width, pictureAxonometry.Height) / 2) - indent;
            changeL = true;
            allOperations();
        }

        void allOperations()
        {
            inputData(trackMx.Value, trackMy.Value, trackMz.Value, trackNx.Value, trackNy.Value, trackNz.Value, trackAx.Value, trackAy.Value, trackAz.Value, trackBx.Value, trackBy.Value, trackBz.Value, trackCx.Value, trackCy.Value, trackCz.Value, trackLx.Value, trackLy.Value, trackLz.Value, trackE.Value);
            intersectionT();
            //тут должно быть получение видимости
            drawAndCalculateAxonometry();
            drawAndCalculateComplex();
            editText();
        }

        void intersectionT()
        {
            T = operations.pointTIntersection(p4d[0], p4d[1], p4d[2], p4d[3], p4d[4], eps, ref lblFlatXYZ, ref lblSectionXYZ);
            if (T == null)
                sectionTpoints = operations.pointsTIntersection(p4d[0], p4d[1], p4d[2], p4d[3], p4d[4], eps);
        }

        void inputData
            (
            int Mx, int My, int Mz,
            int Nx, int Ny, int Nz,
            int Ax, int Ay, int Az,
            int Bx, int By, int Bz,
            int Cx, int Cy, int Cz,
            int Lx, int Ly, int Lz,
            double eps
            )
        {
            if (radioBtnOrthogonal.Checked)
                isOrthogonal = true;
            else
                isOrthogonal = false;

            this.Mx = Mx; this.My = My; this.Mz = Mz;
            this.Nx = Nx; this.Ny = Ny; this.Nz = Nz;
            this.Ax = Ax; this.Ay = Ay; this.Az = Az;
            this.Bx = Bx; this.By = By; this.Bz = Bz;
            this.Cx = Cx; this.Cy = Cy; this.Cz = Cz;
            this.Lx = Lx; this.Ly = Ly; this.Lz = Lz;
            this.eps = Math.Pow(10, -eps);

            inputArrPoints
                (
                Mx, My, Mz,
                Nx, Ny, Nz,
                Ax, Ay, Az,
                Bx, By, Bz,
                Cx, Cy, Cz,
                Lx, Ly, Lz,
                lengthAxis, indent
            );
        }

        void inputArrPoints
            (
            int Mx, int My, int Mz,
            int Nx, int Ny, int Nz,
            int Ax, int Ay, int Az,
            int Bx, int By, int Bz,
            int Cx, int Cy, int Cz,
            int Lx, int Ly, int Lz,
            int lengthAxis, int indent
            )
        {
            int wL = isOrthogonal ? 0 : 1;

            p4d = new Point4D[]
            {
                new Point4D(Mx, My, Mz, 1, "M", new Pen (Color.PaleVioletRed)), //0
                new Point4D(Nx, Ny, Nz, 1, "N", new Pen (Color.Peru)), //1
            
                new Point4D(Ax, Ay, Az, 1, "A", new Pen (Color.Firebrick)), //2
                new Point4D(Bx, By, Bz, 1, "B", new Pen (Color.MediumSlateBlue)), //3
                new Point4D(Cx, Cy, Cz, 1, "C", new Pen (Color.Teal)), //4

                new Point4D(Lx, Ly, Lz, wL, "L", new Pen (Color.SlateGray, 2)), //5

                new Point4D(lengthAxis - indent, 0, 0, 1, "X", new Pen (Color.Red)), //6
                new Point4D(0, lengthAxis - indent, 0, 1, "Y", new Pen (Color.Green)), //7
                new Point4D(0, 0, lengthAxis - indent, 1, "Z", new Pen (Color.Blue)), //8
                new Point4D(0, 0, 0, 1, "O", new Pen (Color.WhiteSmoke)) //9
            };
        }

        void calculateAxonometry()
        {
            if (isOrthogonal)
            {
                if (checkDefinionOL())
                {
                    calculateAxonometryOrthogonal();
                    errorsAvailable = false;
                }  
                else
                    errorsAvailable = true;
            }
            else
            {
                if (checkDefinionOL() && checkDefinionLT() && checkIntersectionScreenCT())
                {
                    calculateAxonometryCentral();
                    errorsAvailable = false;
                }  
                else
                    errorsAvailable = true;
            }
        }

        void editText()
        {
            lblECount.Text = "E = 10^(-" + trackE.Value + ")";
            lblAxyz.Text = "A(" + trackAx.Value + "; " + trackAy.Value + "; " + trackAz.Value + ")";
            lblBxyz.Text = "B(" + trackBx.Value + "; " + trackBy.Value + "; " + trackBz.Value + ")";
            lblCxyz.Text = "C(" + trackCx.Value + "; " + trackCy.Value + "; " + trackCz.Value + ")";

            lblMxyz.Text = "M(" + trackMx.Value + "; " + trackMy.Value + "; " + trackMz.Value + ")";
            lblNxyz.Text = "N(" + trackNx.Value + "; " + trackNy.Value + "; " + trackNz.Value + ")";

            lblLxyz.Text = "L(" + trackLx.Value + "; " + trackLy.Value + "; " + trackLz.Value + ")";

            if(T != null)
            {
                lblTxyz.Text = "T(" + (float)T.X + "; " + (float)T.Y + "; " + (float)T.Z + ")";
                
            }
            else if(sectionTpoints != null)
            {
                //lblTxyz.Text = "T1(" + (float)sectionTpoints.Start.X + "; " + (float)sectionTpoints.Start.Y + "; " + (float)sectionTpoints.Start.Z + "), " +
                //               "T2(" + (float)sectionTpoints.End.X + "; " + (float)sectionTpoints.End.Y + "; " + (float)sectionTpoints.End.Z + ") ";
                //lblTxyz.Text = "T1(" + "беск." + "; " + "беск." + "; " + "беск." + "), " +
                //               "T2(" + "беск." + "; " + "беск." + "; " + "беск." + ") ";
                lblTxyz.Text = "";
            }
            else if(T == null)
            {
                //lblStatus.Text = "";
                lblTxyz.Text = "";
            }
            lblStatus.Text = operations.GetStatus();
        }

        void calculateAxonometryCentral()
        {
            axonomertry = new Axonomertry(p4d, T, sectionTpoints, lengthAxis, indent, eps, changeL);
            axonomertry.calculateCentral();
        }

        void calculateAxonometryOrthogonal()
        {
            axonomertry = new Axonomertry(p4d, T, sectionTpoints, lengthAxis, indent, eps, changeL);
            axonomertry.calculateOrthogonal();
        }

        void drawAxonometry()
        {
            pictureAxonometry.Refresh();
            pictureAxonometry.Image = null;
            bitmapAxonometry = new Bitmap(pictureAxonometry.Width, pictureAxonometry.Height);
            if(errorsAvailable == false)
            {
                if (checkIntersectionPointScreen())
                {
                    axonomertry.DrawPoints(Graphics.FromImage(bitmapAxonometry));
                    axonomertry.DrawLabels(Graphics.FromImage(bitmapAxonometry));
                    axonomertry.DrawLines(Graphics.FromImage(bitmapAxonometry));
                    axonomertry.DrawAxis(Graphics.FromImage(bitmapAxonometry));
                }
                else
                    Graphics.FromImage(bitmapAxonometry).DrawString(errorMsg, new Font("Times New Roman", 18), new SolidBrush(Color.Black), 10, 10);
            }
            else
                Graphics.FromImage(bitmapAxonometry).DrawString(errorMsg, new Font("Times New Roman", 18), new SolidBrush(Color.Black), 10, 10);

            pictureAxonometry.Image = bitmapAxonometry;
        }

        void calculateComplex()
        {

            complex = new Complex(p4d, T, lengthAxis, indent);
            complex.Circulates2D();
        }

        void drawComplex()
        {
            pictureComplex.Refresh();
            pictureComplex.Image = null;
            bitmapComplex = new Bitmap(pictureComplex.Width, pictureComplex.Height);
            complex.DrawPoints(Graphics.FromImage(bitmapComplex));
            complex.DrawLabels(Graphics.FromImage(bitmapComplex));
            complex.DrawLines(Graphics.FromImage(bitmapComplex));
            complex.DrawAxis(Graphics.FromImage(bitmapComplex));
            pictureComplex.Image = bitmapComplex;
        }

        void drawAndCalculateAxonometry()
        {
            calculateAxonometry();
            drawAxonometry(); 
        }

        void drawAndCalculateComplex()
        {
            calculateComplex();
            drawComplex();
        }

        bool checkDefinionOL()
        {
            //
            if (Lx == 0 && Ly == 0 && Lz == 0)
            {
                errorMsg = "Вектор OL не определен!";
                return false;
            }
            else
                return true;
        }

        bool checkDefinionLT()
        {
            //

            if (isOrthogonal == false && T != null && Lx == T.X && Ly == T.Y && Lz == T.Z)
            {
                errorMsg = "Вектор LT не определен!";
                return false;
            }
            else if (isOrthogonal == false && Lx == Mx && Ly == My && Lz == Mz)
            {
                errorMsg = "Вектор LM не определен!";
                return false;
            }
            else if (isOrthogonal == false && Lx == Nx && Ly == Ny && Lz == Nz)
            {
                errorMsg = "Вектор LN не определен!";
                return false;
            }
            else if (isOrthogonal == false && Lx == Ax && Ly == Ay && Lz == Az)
            {
                errorMsg = "Вектор LA не определен!";
                return false;
            }
            else if (isOrthogonal == false && Lx == Bx && Ly == By && Lz == Bz)
            {
                errorMsg = "Вектор LB не определен!";
                return false;
            }
            else if (isOrthogonal == false && Lx == Cx && Ly == Cy && Lz == Cz)
            {
                errorMsg = "Вектор LC не определен!";
                return false;
            }
            else
                return true;
        }

        bool checkIntersectionScreenCT()
        {
            //
            if (isOrthogonal == false && T != null && (-Lx * (T.X - Lx) - Ly * (T.Y - Ly) - Lz * (T.Z - Lz)) <= 0)
            {
                errorMsg = "Вектор LT не пересекает плоскость экрана!";
                return false;
            }
            else if (isOrthogonal == false && (-Lx * (Mx - Lx) - Ly * (My - Ly) - Lz * (Mz - Lz)) <= 0)
            {
                errorMsg = "Вектор LM не пересекает плоскость экрана!";
                return false;
            }
            else if (isOrthogonal == false && (-Lx * (Nx - Lx) - Ly * (Ny - Ly) - Lz * (Nz - Lz)) <= 0)
            {
                errorMsg = "Вектор LN не пересекает плоскость экрана!";
                return false;
            }
            else if (isOrthogonal == false && (-Lx * (Ax - Lx) - Ly * (Ay - Ly) - Lz * (Az - Lz)) <= 0)
            {
                errorMsg = "Вектор LA не пересекает плоскость экрана!";
                return false;
            }
            else if (isOrthogonal == false && (-Lx * (Bx - Lx) - Ly * (By - Ly) - Lz * (Bz - Lz)) <= 0)
            {
                errorMsg = "Вектор LT не пересекает плоскость экрана!";
                return false;
            }
            else if (isOrthogonal == false && (-Lx * (Cx - Lx) - Ly * (Cy - Ly) - Lz * (Cz - Lz)) <= 0)
            {
                errorMsg = "Вектор LC не пересекает плоскость экрана!";
                return false;
            }
            else
                return true;
        }

        bool checkIntersectionPointScreen()
        {
            //
            p2d = axonomertry.getArrPoint2D();
            for (int i = 0; i < p2d.Length; i++)
            {
                if (p2d[i].name == "L")
                    continue;
                if (p2d[i].X < 0 || p2d[i].X > pictureAxonometry.Width - indent ||
                   p2d[i].Y < 0 || p2d[i].Y > pictureAxonometry.Height - indent || p2d == null)
                {
                    errorMsg = "Выход за границы экрана!";
                    return false;
                }
            }
            return true;
        }
    }
}
