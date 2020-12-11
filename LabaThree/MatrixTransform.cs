using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaThree
{
    class MatrixTransform
    {

        public void BaseTransformation(ref Matrix Rz, ref Matrix Rx, ref Matrix Mx, ref Matrix Cz, ref Matrix Pz, ref Matrix T, ref double cosRx, ref double cosRz, ref double sinRx, ref double sinRz, ref int lengthAxis)
        {
            Rx = RotationMatrixOx(cosRx, sinRx);
            Rz = RotationMatrixOz(cosRz, sinRz);
            Mx = MirrorMatrixYoZ();
            Pz = ProjectionMatrixXoY();
            T = ShiftMatrix(lengthAxis, lengthAxis, lengthAxis);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
       
        public void ComplexTransformationForCentral(ref Matrix Rz, ref Matrix Rx, ref Matrix Mx, ref Matrix Cz, ref Matrix Pz, ref Matrix T, ref Matrix Ao, ref Matrix Ac, ref double c)
        {
            Ao = OrthogonalTransformMatrixComplex(Rz, Rx, Mx, Pz, T); //для ортогонального чертежа
            Cz = PerspectiveOz(c);
            Ac = CentralTransformMatrixComplex(Rz, Rx, Mx, Cz, Pz, T); //для центрального чертежа
        }

        public void ComplexTransformationForOrthogonal(ref Matrix Rz, ref Matrix Rx, ref Matrix Mx, ref Matrix Cz, ref Matrix Pz, ref Matrix T, ref Matrix Ao)
        {
            Ao = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Ao = OrthogonalTransformMatrixComplex(Rz, Rx, Mx, Pz, T); //для ортогонального чертежа
        }


        public Matrix RotationMatrixOx(double cos, double sin)
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0, 
                0, cos, sin, 0, 
                0, -sin, cos, 0, 
                0, 0, 0, 1
                );

            return result;
        }

        public  Matrix RotationMatrixOy(double cos, double sin)
        {
            //
            Matrix result = new Matrix
                (
                cos, 0, -sin, 0,
                0, 1, 0, 0,
                sin, 0, cos, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix RotationMatrixOz(double cos, double sin)
        {
            //
            Matrix result = new Matrix
                (
                cos, sin, 0, 0,
                -sin, cos, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix ExtensionMatrix(double a, double b, double c)
        {
            //
            Matrix result = new Matrix
                (
                a, 0, 0, 0,
                0, b, 0, 0,
                0, 0, c, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix MirrorMatrixYoZ()
        {
            //
            Matrix result = new Matrix
                (
                -1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix MirrorMatrixXoZ()
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0,
                0, -1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix MirrorMatrixXoY()
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, -1, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix ShiftToCenter(double x0, double y0)
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                x0, y0, 0, 1
                );
            return result;
        }

        public Matrix ShiftMatrix(double a, double b, double c)
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                a, b, c, 1
                );
            return result;
        }

        public Matrix ProjectionMatrixYoZ()
        {
            //
            Matrix result = new Matrix
                (
                0, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix ProjectionMatrixXoZ()
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix ProjectionMatrixXoY()
        {
            //
            Matrix result = new Matrix
                (
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1
                );
            return result;
        }

        public Matrix PerspectiveOx(double c)
        {
            //
            Matrix result = new Matrix
                 (
                 1, 0, 0, (-1/c),
                 0, 1, 0, 0,
                 0, 0, 1, 0,
                 0, 0, 0, 1
                 );
            return result;
        }

        public Matrix PerspectiveOy(double c)
        {
            //
            Matrix result = new Matrix
                 (
                 1, 0, 0, 0,
                 0, 1, 0, (-1/c),
                 0, 0, 1, 0,
                 0, 0, 0, 1
                 );
            return result;
        }

        public Matrix PerspectiveOz(double c)
        {
            //
            Matrix result = new Matrix
                 (
                 1, 0, 0, 0,
                 0, 1, 0, 0,
                 0, 0, 1, (-1/c),
                 0, 0, 0, 1
                 );
            return result;
        }

        ///2

        public Matrix OrthogonalTransformMatrixComplex(Matrix Rz, Matrix Rx, Matrix Mx, Matrix Pz, Matrix T)
        {
            //
            Matrix result = new Matrix
                (
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
                );
            result = Rz * Rx;
            result = result * Mx;
            result = result * Pz;
            result = result * T;
            return result;
        }

        public Matrix CentralTransformMatrixComplex(Matrix Rz, Matrix Rx, Matrix Mx, Matrix Cz, Matrix Pz, Matrix T)
        {
            //
            Matrix result = new Matrix
                (
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
                );
            result = Rz * Rx;
            result = result * Mx;
            result = result * Cz;
            result = result * Pz;
            result = result * T;
            return result;
        }
    }
}
