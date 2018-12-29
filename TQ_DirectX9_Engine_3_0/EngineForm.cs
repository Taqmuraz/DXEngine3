using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using TQ_DirectX9_Engine_3_0.ComponentsPart;
using TQ_DirectX9_Engine_3_0.GraphicPart;
using TQ_DirectX9_Engine_3_0.ModelsManagement;
using TQ_DirectX9_Engine_3_0.GamePart;
using TQ_DirectX9_Engine_3_0.FileManagement;

namespace TQ_DirectX9_Engine_3_0
{

    public class DirectXPart
    {

        public static Device device { get; private set; }

        static void InitializeGraphics()
        {
            Screen screen = Screen.PrimaryScreen;
            EngineForm.mainForm.Bounds = screen.Bounds;

            PresentParameters presentParameters = new PresentParameters();
            presentParameters.Windowed = true;
            presentParameters.SwapEffect = SwapEffect.Discard;
            presentParameters.EnableAutoDepthStencil = true;
            presentParameters.AutoDepthStencilFormat = DepthFormat.D16;
            device = new Device(0, DeviceType.Hardware, EngineForm.mainForm,
                CreateFlags.SoftwareVertexProcessing, presentParameters);
            // просто ради примера, если картинка должна сохранять пропорции с экраном
            device.DeviceResizing += new CancelEventHandler(CancelResize);
        }
        public static void InitializeEngine()
        {
            InitializeGraphics();
            GameManager.InitGame();
        }
        public static void OnPaint(PaintEventArgs e)
        {
            GraphicsManager.MainRendering(device);
        }
        static void CancelResize(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            GameManager.SetCameraParameters(Camera.mainCamera);
        }
    }

    public partial class EngineForm : Form
    {

        public static EngineForm mainForm { get; private set; }

        public EngineForm()
        {
            mainForm = this;
            InitializeStyle();
            InitializeComponent();
            DirectXPart.InitializeEngine();
        }
        void InitializeStyle()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            DirectXPart.OnPaint(e);
            this.Invalidate();
        }
    }
}
