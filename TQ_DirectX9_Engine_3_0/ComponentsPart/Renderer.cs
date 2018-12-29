using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TQ_DirectX9_Engine_3_0.ComponentsPart;
using TQ_DirectX9_Engine_3_0.ModelsManagement;
using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;
using TQ_DirectX9_Engine_3_0.MathPart;

namespace TQ_DirectX9_Engine_3_0.ComponentsPart
{
    public sealed class Renderer : Component
    {

        public static List<Renderer> orderToDraw
        {
            get
            {
                return _otd;
            }
        }
        static List<Renderer> _otd = new List<Renderer>();

        

        public MeshData mesh { get; set; }

        public Renderer()
            : base()
        {
            orderToDraw.Add(this);
        }

        public static void GlobalDraw(d3d.Device device)
        {
            device.BeginScene();
            foreach (var otd in orderToDraw)
            {
                otd.Draw(device);
            }
            device.EndScene();
        }

        public override void CameraUpdate(Microsoft.DirectX.Direct3D.Device device)
        {
            transform.eulerAngles += Vector.one;
        }

        public void Draw(d3d.Device device)
        {
            mesh.Draw(device, this);
        }

        public override void OnDestroy()
        {
            orderToDraw.Remove(this);
            base.OnDestroy();
        }
    }
}
