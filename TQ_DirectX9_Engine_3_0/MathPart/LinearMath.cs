using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;

namespace TQ_DirectX9_Engine_3_0.MathPart
{

    public class LinearMath
    {
        public static float Pow(float a, int b)
        {
            float r = 1;
            for (int i = 0; i < b; i++)
            {
                r *= a;
            }
            return r;
        }
    }
    public struct Matrix4x4
    {
        float m11;
        float m12;
        float m13;
        float m14;
        float m21;
        float m22;
        float m23;
        float m24;
        float m31;
        float m32;
        float m33;
        float m34;
        float m41;
        float m42;
        float m43;
        float m44;

        public Vector collumn_1
        {
            get
            {
                return new Vector(m11, m21, m31);
            }
        }
        public Vector collumn_2
        {
            get
            {
                return new Vector(m12, m22, m32);
            }
        }
        public Vector collumn_3
        {
            get
            {
                return new Vector(m13, m23, m33);
            }
        }
        public Vector collumn_4
        {
            get
            {
                return new Vector(m14, m24, m34);
            }
            private set
            {
                m14 = value.x;
                m24 = value.y;
                m34 = value.z;
            }
        }

        public Vector line_1
        {
            get
            {
                return new Vector(m11, m12, m13);
            }
        }
        public Vector line_2
        {
            get
            {
                return new Vector(m21, m22, m23);
            }
        }
        public Vector line_3
        {
            get
            {
                return new Vector(m31, m32, m33);
            }
        }
        public Vector line_4
        {
            get
            {
                return new Vector(m41, m42, m43);
            }
        }

        public Vector BasisToDir (Vector point)
        {
            Vector euler = collumn_4;
            dx.Quaternion rotation = dx.Quaternion.RotationYawPitchRoll(euler.y, euler.x, euler.z);

            float num = rotation.X * 2;
            float num2 = rotation.Y * 2;
            float num3 = rotation.Z * 2;
            float num4 = rotation.X * num;
            float num5 = rotation.Y * num2;
            float num6 = rotation.Z * num3;
            float num7 = rotation.X * num2;
            float num8 = rotation.X * num3;
            float num9 = rotation.Y * num3;
            float num10 = rotation.W * num;
            float num11 = rotation.W * num2;
            float num12 = rotation.W * num3;
            Vector result;
            result.x = (1 - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1 - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1 - (num4 + num5)) * point.z;
            return result;
        }

        public float determinant
        {
            get
            {
                return MatrixMath.GetCorner(new float[4, 4]
                {
                    {m11, m12, m13, m14},
                    {m21, m22, m23, m24},
                    {m31, m32, m33, m34},
                    {m41, m42, m43, m44}
                });
            }
        }

        public Matrix4x4(Vector _line1, Vector _line2, Vector _line3, Vector _line4, Vector _collumn_4)
        {
            m11 = _line1.x; m12 = _line1.y; m13 = _line1.z; m14 = 0f;
            m21 = _line2.x; m22 = _line2.y; m23 = _line2.z; m24 = 0f;
            m31 = _line3.x; m32 = _line3.y; m33 = _line3.z; m34 = 0f;
            m41 = _line4.x; m42 = _line4.y; m43 = _line4.z; m44 = 1f;
            m14 = _collumn_4.x;
            m24 = _collumn_4.y;
            m34 = _collumn_4.z;
        }

        public static Matrix4x4 rightHanded
        {
            get
            {
                return new Matrix4x4(Vector.right, Vector.up, Vector.forward, Vector.zero, Vector.zero);
            }
        }

        public Matrix4x4 Transpose(Vector position)
        {
            return World(position, collumn_4);
        }
        public Matrix4x4 World(Vector position, Vector euler)
        {
            Vector right = BasisToDir(Vector.right);
            Vector up = BasisToDir(Vector.up);
            Vector forward = BasisToDir(Vector.forward);
            return new Matrix4x4(right, up, forward, position, euler);
        }
        public Matrix4x4 Euler(Vector euler)
        {
            return World(line_4, euler);
        }
    }
    public struct Vector
    {
        public float x;
        public float y;
        public float z;

        public const float w = 1.0f;

        public Vector(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public Vector(float _x, float _y)
        {
            x = _x;
            y = _y;
            z = 0f;
        }

        public override string ToString()
        {
            return "Vector : (" + x + ',' + y + ',' + z + ')';
        }

        public Vector normal
        {
            get
            {
                return this / magnitude;
            }
        }

        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y + z * z);
            }
        }

        public static Vector operator + (Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector operator - (Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector operator - (Vector a)
        {
            return new Vector(-a.x, -a.y, -a.z);
        }
        public static Vector operator * (Vector a, float b)
        {
            return new Vector(a.x * b, a.y * b, a.z * b);
        }
        public static Vector operator /(Vector a, float b)
        {
            return new Vector(a.x / b, a.y / b, a.z / b);
        }
        public static implicit operator dx.Vector3 (Vector a)
        {
            return new dx.Vector3(a.x, a.y, a.z);
        }
        public static implicit operator Vector(dx.Vector3 a)
        {
            return new Vector(a.X, a.Y, a.Z);
        }
        public static Vector right
        {
            get
            {
                return new Vector(1, 0, 0);
            }
        }
        public static Vector left
        {
            get
            {
                return -right;
            }
        }
        public static Vector zero
        {
            get
            {
                return new Vector(0, 0, 0);
            }
        }
        public static Vector up
        {
            get
            {
                return new Vector(0, 1, 0);
            }
        }
        public static Vector down
        {
            get
            {
                return -up;
            }
        }
        public static Vector forward
        {
            get
            {
                return new Vector(0, 0, 1);
            }
        }
        public static Vector back
        {
            get
            {
                return -forward;
            }
        }
        public static Vector one
        {
            get
            {
                return new Vector(1, 1, 1);
            }
        }
    }
    public class MatrixMath
    {
        public static float[,] SumMatrix(float[,] a, float[,] b)
        {
            float[,] c = new float[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }
        public static float[,] UnsumMatrix(float[,] a, float[,] b)
        {
            float[,] c = new float[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }
            return c;
        }
        public static float[,] Transponation(float[,] a)
        {
            float[,] c = new float[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = a[j, i];
                }
            }
            return c;
        }

        public static float GetCornerFrom2x2(float[,] a)
        {
            return a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0];
        }

        public static float GetMinor(float[,] a, int a_i, int a_j)
        {
            if (a.GetLength(0) < 2)
            {
                return 0;
            }

            float[,] min = new float[a.GetLength(0) - 1, a.GetLength(1) - 1];
            for (int i = 0; i < min.GetLength(0); i++)
            {
                for (int j = 0; j < min.GetLength(1); j++)
                {
                    int i_p = i < a_i ? 0 : 1;
                    int j_p = j < a_j ? 0 : 1;
                    min[i, j] = a[i + i_p, j + j_p];
                }
            }
            return GetCorner(min);
        }

        public static float GetCorner(float[,] a)
        {
            if (a.GetLength(0) != a.GetLength(1))
            {
                return 0;
            }
            if (a.GetLength(0) == 2 && a.GetLength(1) == 2)
            {
                return GetCornerFrom2x2(a);
            }
            else
            {
                float detA = 0;
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    int i = 0;
                    detA += a[i, j] * AriphmeticInc(a, i, j);
                }
                return detA;
            }
        }

        public static float AriphmeticInc(float[,] a, int a_i, int a_j)
        {
            float min = GetMinor(a, a_i, a_j);
            return LinearMath.Pow(-1, (a_i) + (a_j)) * min;
        }

        public static float[,] PowMatrix(float[,] a, float[,] b)
        {

            /*int ma = a.GetLength(0);
            int mb = b.GetLength(0);
            int nb = b.GetLength(1);

            double[,] r = new double[ma, nb];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < nb; j++)
                {
                    for (int k = 0; k < mb; k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;*/

            float[,] c = new float[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    float sum = 0;
                    for (int n = 0; n < a.GetLength(1); n++)
                    {
                        sum += a[i, n] * b[n, j];
                    }
                    c[i, j] = sum;
                }
            }
            return c;
        }
    }
}
