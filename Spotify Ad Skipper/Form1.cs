using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Spotify_Ad_Skipper.Properties;
using System.Diagnostics;

namespace Spotify_Ad_Skipper
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int VK_SPACE = 0x20;
        const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        const int VK_MEDIA_STOP = 0xB2;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        const int KEYEVENTF_KEYDOWN = 0x0000; // Keydown event
        const int KEYEVENTF_KEYUP = 0x0002;   // Keyup event

        const int SW_MINIMIZE = 6;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        String userPath;

        private NotifyIcon trayIcon;
        public Form1()
        {
           
            InitializeComponent();

            // bind the media stop key to restart spotify
            Form1.RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, VK_MEDIA_STOP); 

            // make an icon show up in the system tray for this program
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Exit", (object sender, EventArgs e) =>  // add an exit button to the menu when the icon is right clicked
                    {
                        trayIcon.Visible = false; 
                        Application.Exit(); 
                    }),

                    new MenuItem("Skip Ad", (object sender, EventArgs e) => // add a skip ad button to do the same as the hotkey
                    {
                        RestartSpotify();
                    })
                }),
                Visible = true
            };

            // when the tray icon is clicked
            trayIcon.Click += (sender, e) =>
            {
                MouseEventArgs mouseEventArgs = e as MouseEventArgs;
                if (mouseEventArgs.Button == MouseButtons.Left)
                    RestartSpotify();   // make the ad get skipped when the icon is left clicked
            };

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userPath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            BeginInvoke(new MethodInvoker(delegate
            {
                Hide();

                var processes = Process.GetProcessesByName("Spotify");  // launch spotify if its not yet open
                if (processes.Length == 0)
                    openSpotify();
            }));
        }

        protected override void WndProc(ref Message m) 
        {
            if (m.Msg == 0x0312) // if hotkey was pressed
            {
                RestartSpotify();
            }
            base.WndProc(ref m); // otherwise pass the message on
        }

        void RestartSpotify()
        {
            var processes = Process.GetProcessesByName("Spotify");
            foreach (var process in processes)
            {
                // close spotify nicely. killing spotify makes it forget what song it is on
                process.CloseMainWindow(); 
            }

            while (Process.GetProcessesByName("Spotify").Length > 0)  // wait for spotify to open before sending play button
                System.Threading.Thread.Sleep(100);  // dont burn the cpu

            openSpotify();

            // wait 1.5 seconds after opening spotify
            System.Threading.Thread.Sleep(1500); 

            sendPlaybutton();
        }

        void sendPlaybutton()
        {
            // Simulate pressing the Spacebar
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);

            System.Threading.Thread.Sleep(5); // Adjust the delay as needed

            // Simulate releasing the Spacebar 
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        void openSpotify()
        {
            Process.Start("Spotify", "--minimized"); // run spotify minimized to not disturb the user
        }
    }
}