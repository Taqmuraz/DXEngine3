using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TQ_DirectX9_Engine_3_0.FileManagement
{
    public class Debugger
    {
        public static string path
        {
            get
            {
                return Application.StartupPath;
            }
        }
        public static void Log(Exception ea)
        {
            Log(ea.Message + "\nSTACK TRACE : " + ea.StackTrace + "\nDATA : " + ea.Data + "\nHELP LINK : " + ea.HelpLink + "\nINNER EXCEPTION : " + ea.InnerException + "\nSOURCE : " + ea.Source + "\nTARGET SITE : " + ea.TargetSite);
        }
        public static void Log(string text)
        {
            StreamWriter sw = new StreamWriter(path + "/LOG.txt");
            sw.WriteLine(text);
            sw.Close();
        }
        public static void Log(string[] texts)
        {
            string resoult = "";
            foreach (string s in texts)
            {
                resoult = resoult + "*\n" + s + "\n#";
            }
            Log(resoult);
        }
        public static void Log(object[] objs)
        {
            Log(objs.Select((object o) => o.ToString()).ToArray());
        }
    }
}
