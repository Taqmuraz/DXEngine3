using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;
using TQ_DirectX9_Engine_3_0.MathPart;
using TQ_DirectX9_Engine_3_0.FileManagement;

namespace TQ_DirectX9_Engine_3_0.ComponentsPart
{
    public class Camera : Component
    {
        public Camera(float fow, float np, float fp, float ar)
            : base()
        {
            fieldOfView = fow;
            nearPlane = np;
            farPlane = fp;
            aspectRatio = ar;
            mainCamera = this;
        }
        public Camera()
            : base()
        {
            mainCamera = this;
        }

        public dx.Matrix cameraMatrix { get; private set; }
        public dx.Matrix lookMatrix { get; private set; }
        public static Camera mainCamera { get; private set; }

        public float fieldOfView { get; set; }
        public float nearPlane { get; set; }
        public float farPlane { get; set; }
        public float aspectRatio { get; set; }

        public override void CameraUpdate(d3d.Device device)
        {
            transform.position = Vector.back * 5;
            cameraMatrix = dx.Matrix.PerspectiveFovLH(fieldOfView, aspectRatio, nearPlane, farPlane);
            device.Transform.Projection = cameraMatrix;
            lookMatrix = dx.Matrix.LookAtLH(transform.position, Vector.zero, transform.up);
            device.Transform.View = lookMatrix;
            //Debugger.Log( new object[] { transform.position, transform.right, transform.up, transform.forward });
        }
    }
}
