using Spotify_Ad_Skipper.Properties;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace Spotify_Ad_Skipper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

  

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MyCustomApplicationContext());
            Application.Run(new Form1());
        }
    }
    /*
    public class MyCustomApplicationContext : ApplicationContext
    {
        //private NotifyIcon trayIcon;


        /*
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        //[DllImport("user32.dll")]
        //public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                MessageBox.Show("Catched");//Replace this statement with your desired response to the Hotkey.
            }
            base.WndProc(ref m);
        }
        

    

    public MyCustomApplicationContext()
        {

            /*Boolean success = this.RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, 0x42);//Set hotkey as 'b'
            if (success == true)
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Error");
            }
            

            

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };

        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }

        
    }*/
}
