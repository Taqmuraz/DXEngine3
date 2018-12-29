using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TQ_DirectX9_Engine_3_0.GraphicPart;
using TQ_DirectX9_Engine_3_0.MathPart;
using TQ_DirectX9_Engine_3_0.ComponentsPart;
using TQ_DirectX9_Engine_3_0.FileManagement;
using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;

namespace TQ_DirectX9_Engine_3_0.ModelsManagement
{
    public class ModelsManager
    {

    }

    public class MeshDX : MeshData
    {
        d3d.Mesh dxMesh;
        d3d.Material dxMaterial;

        public MeshDX(d3d.Device device)
        {
            dxMesh = d3d.Mesh.Box(device, 1f, 1f, 1f);
            dxMaterial = (d3d.Material)typeof(d3d.Material).GetConstructor(new Type[0]).Invoke(new object[0]);
            dxMaterial.Ambient = System.Drawing.Color.Black;
            dxMaterial.Diffuse = System.Drawing.Color.White;
        }
        public override void Draw(Microsoft.DirectX.Direct3D.Device device, Renderer rnd)
        {
            device.RenderState.Ambient = System.Drawing.Color.Red;
            Vector euler = rnd.transform.eulerAngles * (float)Math.PI / 180f;
            Vector pos = rnd.transform.position;
            device.Transform.World = dx.Matrix.RotationYawPitchRoll(euler.y, euler.x, euler.z) * dx.Matrix.Translation(pos.x, pos.y, pos.z);
            device.Material = dxMaterial;
            dxMesh.DrawSubset(0);
        }
    }

    public class MeshPoints : MeshData
    {
        ColoredVector[] points;

        public MeshPoints(params ColoredVector[] cvs)
        {
            points = SetNormals(cvs);
        }

        public static ColoredVector[] SetNormals(ColoredVector[] origin)
        {

            for (int i = 0; i < origin.Length; i++)
            {
                origin[i] = new ColoredVector(origin[i].vector, Vector.back, origin[i].color);
            }

            return origin;
        }

        public ColoredVector[] GetPoints()
        {
            return points;
        }

        public override void Draw(Microsoft.DirectX.Direct3D.Device device, Renderer rnd)
        {
            ColoredVector[] cvs = ((MeshPoints)rnd.mesh).GetPoints();

            d3d.CustomVertex.PositionNormalColored[] verts = ColoredVector.ToDX_PNColored(cvs);



            device.BeginScene();
            device.VertexFormat = d3d.CustomVertex.PositionNormalColored.Format;
            device.DrawUserPrimitives(d3d.PrimitiveType.TriangleList, 1, verts);
            device.EndScene();
        }


        public static MeshData TriangleRGB(float size, Vector offset)
        {
            MeshData m = new MeshPoints
                (new ColoredVector(new Vector(-1, -1, 1) + offset, new Color(0.8f, 0.4f, 0)),
                new ColoredVector(new Vector(0, 1, 1) + offset, new Color(1f, 0f, 1f)),
                new ColoredVector(new Vector(1, -1, 1) + offset, new Color(0.5f, 0.5f, 1f)));
            return m;
        }
    }

    public abstract class MeshData
    {
        public virtual void Draw(Microsoft.DirectX.Direct3D.Device device, Renderer rnd)
        {
        }
    }
}
