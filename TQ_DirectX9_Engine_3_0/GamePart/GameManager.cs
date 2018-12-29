using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TQ_DirectX9_Engine_3_0.ComponentsPart;
using TQ_DirectX9_Engine_3_0.GraphicPart;
using TQ_DirectX9_Engine_3_0.MathPart;
using TQ_DirectX9_Engine_3_0.ModelsManagement;
using TQ_DirectX9_Engine_3_0.FileManagement;

namespace TQ_DirectX9_Engine_3_0.GamePart
{
    public class GameManager
    {
        public static void InitGame()
        {
            MeshData m = new MeshDX(DirectXPart.device);
            ComponentObject cObj = new ComponentObject("Renderer 0");
            Renderer rend = cObj.AddComponent<Renderer>();
            ComponentObject c_Obj_2 = new ComponentObject("Renderer 1");
            Renderer rend_2 = c_Obj_2.AddComponent<Renderer>();
            rend_2.mesh = m;
            rend_2.transform.position = Vector.right;
            rend.transform.position = -Vector.right;
            rend.mesh = m;
            SetupCamera();
            SetupLighting();
        }
        public static void SetupLighting()
        {
            Lighting.SetupLighting(DirectXPart.device);
        }
        public static void SetCameraParameters(Camera cam)
        {
            cam.aspectRatio = (float)EngineForm.mainForm.Width / (float)EngineForm.mainForm.Height;
            cam.farPlane = 100f;
            cam.fieldOfView = (float)Math.PI / 4;
            cam.nearPlane = 0.01f;
        }
        public static void SetupCamera()
        {
            ComponentObject cameraObject = new ComponentObject("MainCamera");
            Camera cam = cameraObject.AddComponent<Camera>();
            SetCameraParameters(cam);
        }
    }
}
