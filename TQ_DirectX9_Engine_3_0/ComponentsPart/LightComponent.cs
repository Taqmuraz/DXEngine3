using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dx = Microsoft.DirectX;
using d3d = Microsoft.DirectX.Direct3D;
using TQ_DirectX9_Engine_3_0.GraphicPart;
using TQ_DirectX9_Engine_3_0.FileManagement;

namespace TQ_DirectX9_Engine_3_0.ComponentsPart
{
    public class LightComponent : Component
    {
        public delegate void LightCreate();
        public static event LightCreate OnLightCreate;

        public static void ReWeightLights()
        {
            index = 0;
            OnLightCreate();
        }

        static int _index = 0;
        public static int index
        {
            get
            {
                return _index++;
            }
            private set
            {
                _index = value;
            }
        }

        public d3d.Light d3dLight { get; private set; }

        public d3d.LightType type { get; set; }
        public Color diffuse { get; set; }
        public float range { get; set; }

        bool _enabled = true;
        public bool enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                SetEnabled(_enabled);
            }
        }

        public LightComponent() : base ()
        {
            OnLightCreate += Init;
            ReWeightLights();
        }

        void Init()
        {
            d3dLight = DirectXPart.device.Lights[index];
            SetEnabled(enabled);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            OnLightCreate -= Init;
        }

        public void SetEnabled(bool enable)
        {
            d3dLight.Enabled = enabled;
        }

        public override void CameraUpdate(Microsoft.DirectX.Direct3D.Device device)
        {
            d3dLight.Type = type;
            d3dLight.Position = transform.position;
            d3dLight.Diffuse = System.Drawing.Color.FromArgb(diffuse.ToARGB());
            d3dLight.Attenuation0 = 0.2f;
            d3dLight.Range = range;
            //Debugger.Log(d3dLight.Position.ToString());
        }
    }
}
