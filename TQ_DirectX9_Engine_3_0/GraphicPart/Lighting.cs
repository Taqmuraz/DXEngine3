using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;
using TQ_DirectX9_Engine_3_0.GraphicPart;
using TQ_DirectX9_Engine_3_0.ComponentsPart;
using TQ_DirectX9_Engine_3_0.MathPart;
using TQ_DirectX9_Engine_3_0.FileManagement;

namespace TQ_DirectX9_Engine_3_0.GraphicPart
{
    public class Lighting
    {
        public static LightComponent directionLight { get; private set; }

        public static void SetupLighting(d3d.Device device)
        {
            ComponentObject dirObj = new ComponentObject("DirectionalLight");
            directionLight = dirObj.AddComponent<LightComponent>();
            directionLight.type = d3d.LightType.Point;
            directionLight.diffuse = new Color(1, 1, 1);
            directionLight.transform.position = Vector.back;
            directionLight.range = 10000f;
            directionLight.d3dLight.Direction = new Microsoft.DirectX.Vector3(0, 0, 1);
            directionLight.SetEnabled(true);
            device.RenderState.Lighting = true;
        }
    }
}
