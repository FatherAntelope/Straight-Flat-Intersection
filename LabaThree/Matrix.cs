using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaThree
{
    class Matrix
    {
        public List<List<double>> matrix = new List<List<double>>();

        public Matrix
            (
            double m11, double m12, double m13, double m14,
            double m21, double m22, double m23, double m24,
            double m31, double m32, double m33, double m34,
            double m41, double m42, double m43, double m44
            )
        {
            matrix.Add(new List<double>());
            matrix[0].Add(m11); matrix[0].Add(m12); matrix[0].Add(m13); matrix[0].Add(m14);
            matrix.Add(new List<double>());
            matrix[1].Add(m21); matrix[1].Add(m22); matrix[1].Add(m23); matrix[1].Add(m24);
            matrix.Add(new List<double>());
            matrix[2].Add(m31); matrix[2].Add(m32); matrix[2].Add(m33); matrix[2].Add(m34);
            matrix.Add(new List<double>());
            matrix[3].Add(m41); matrix[3].Add(m42); matrix[3].Add(m43); matrix[3].Add(m44);
        }

        public Matrix(double m11, double m12, double m13, double m14)
        {
            matrix.Add(new List<double>());
            matrix[0].Add(m11); matrix[0].Add(m12); matrix[0].Add(m13); matrix[0].Add(m14);
        }



        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.matrix.Count == 4)
            {
                Matrix result = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                for (int i = 0; i < result.matrix[0].Count; i++)
                    for (int j = 0; j < result.matrix[0].Count; j++)
                        for (int k = 0; k < result.matrix[0].Count; k++)
                            result.matrix[i][j] += A.matrix[i][k] * B.matrix[k][j];
                return result;
            }
            else
            {
                Matrix result = new Matrix(0, 0, 0, 0);
                for (int i = 0; i < result.matrix[0].Count; i++)
                    for (int j = 0; j < result.matrix[0].Count; j++)
                        result.matrix[0][i] += A.matrix[0][j] * B.matrix[j][i];
                return result;
            }
        }
    }
}
