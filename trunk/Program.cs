using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace ShipScrn
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists(@"d:\users\shared\ibillrunning.txt"))
            {

                FileStream fileStream = null;
                using (fileStream = File.Create(@"d:\users\shared\ibillrunning.txt"))
                {
                    byte myByte = 255;
                    fileStream.WriteByte(myByte);
                    fileStream.Close();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PackEntry());
        }
 
    }
}