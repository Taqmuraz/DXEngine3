using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TQ_DirectX9_Engine_3_0
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (EngineForm form = new EngineForm())
            {
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);

                form.Show();
                Application.Run(form);
            }
        }
    }
}
