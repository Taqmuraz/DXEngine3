using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TQ_DirectX9_Engine_3_0.MathPart;
using TQ_DirectX9_Engine_3_0.ModelsManagement;
using TQ_DirectX9_Engine_3_0.ComponentsPart;
using TQ_DirectX9_Engine_3_0.FileManagement;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace TQ_DirectX9_Engine_3_0.GraphicPart
{

    public class GraphicsManager
    {
        public static void MainRendering(Microsoft.DirectX.Direct3D.Device device)
        {
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer,
                System.Drawing.Color.CornflowerBlue, 1.0f, 0);

            foreach (Component c in Component.allComponents)
            {
                c.CameraUpdate(device);
            }

            Renderer.GlobalDraw(device);

            device.Present();
        }
    }

    public struct ColoredVector
    {
        public Vector vector;
        public Color color;
        public Vector normal;

        public ColoredVector(Vector _v, Color _c)
        {
            vector = _v;
            color = _c;
            normal = Vector.zero;
        }
        public ColoredVector(Vector _v, Vector _n, Color _c)
        {
            vector = _v;
            color = _c;
            normal = _n;
        }
        //
        public Microsoft.DirectX.Direct3D.CustomVertex.TransformedColored ToDX_TColored()
        {
            return new Microsoft.DirectX.Direct3D.CustomVertex.TransformedColored
                (vector.x, vector.y, vector.z, Vector.w, color.ToARGB());
        }
        public static Microsoft.DirectX.Direct3D.CustomVertex.TransformedColored[] ToDX_TColored(ColoredVector[] origin)
        {
            return origin.Select((ColoredVector cv) => cv.ToDX_TColored()).ToArray();
        }
        //
        public Microsoft.DirectX.Direct3D.CustomVertex.PositionColored ToDX_PColored()
        {
            return new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored
                (vector.x, vector.y, vector.z, color.ToARGB());
        }
        public static Microsoft.DirectX.Direct3D.CustomVertex.PositionColored[] ToDX_PColored(ColoredVector[] origin)
        {
            return origin.Select((ColoredVector cv) => cv.ToDX_PColored()).ToArray();
        }
        //
        public Microsoft.DirectX.Direct3D.CustomVertex.PositionNormalColored ToDX_PNColored()
        {
            return new Microsoft.DirectX.Direct3D.CustomVertex.PositionNormalColored
                (vector, normal, color.ToARGB());
        }
        public static Microsoft.DirectX.Direct3D.CustomVertex.PositionNormalColored[] ToDX_PNColored(ColoredVector[] origin)
        {
            return origin.Select((ColoredVector cv) => cv.ToDX_PNColored()).ToArray();
        }

        public override string ToString()
        {
            return vector.ToString() + '\n' + color.ToString();
        }
    }
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(float _r, float _g, float _b, float _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }
        public Color(float _r, float _g, float _b)
        {
            r = _r;
            g = _g;
            b = _b;
            a = 1.0f;
        }

        public override string ToString()
        {
            return "Color : (" + r + ',' + g + ',' + b + ',' + a + "), ARGB (" + ToARGB() + ')';
        }

        public int ToARGB()
        {
            return System.Drawing.Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255)).ToArgb();
        }
    }
}
