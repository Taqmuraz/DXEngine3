using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TQ_DirectX9_Engine_3_0.MathPart;
using d3d = Microsoft.DirectX.Direct3D;
using dx = Microsoft.DirectX;

namespace TQ_DirectX9_Engine_3_0.ComponentsPart
{
    public sealed class Transform : Component
    {
        public Matrix4x4 matrix { get; private set; }

        public Vector eulerAngles
        {
            get
            {
                return matrix.collumn_4;
            }
            set
            {
                matrix = matrix.Euler(value);
            }
        }

        public Vector position
        {
            get
            {
                return matrix.line_4;
            }
            set
            {
                matrix = matrix.Transpose(value);
            }
        }

        public Vector forward
        {
            get
            {
                return matrix.line_3;
            }
        }
        public Vector up
        {
            get
            {
                return matrix.line_2;
            }
        }
        public Vector right
        {
            get
            {
                return matrix.line_1;
            }
        }

        public Transform()
            : base()
        {
            matrix = Matrix4x4.rightHanded;
        }
    }
}
