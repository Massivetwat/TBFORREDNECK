using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Memory;

namespace TBFORREDNECK
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LEFTUP = 0x0004;
        private const int LEFTDOWN = 0x0002;

        string crosshairID = "";
        bool TBON = false;
        bool hackready = false;
        int PID;
        int CrossID;

        IntPtr MWhandle;
        IntPtr CWhandle;
        Mem m = new Mem();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread TB = new Thread(TRIGGERBOT) { IsBackground = true };
            TB.Start();
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker2.RunWorkerAsync();
        }

        private void TRIGGERBOT()
        {
            while (true)
            {
                if (TBON == true && hackready)
                {
                    crosshairID = textBox1.Text;
                    CrossID = m.ReadInt(crosshairID);
                    
                    

                    if (CrossID != 0 && isGameActive())
                    {
                        mouse_event(LEFTDOWN, 0, 0, 0, 0);
                        Thread.Sleep(1);
                        mouse_event(LEFTUP, 0, 0, 0, 0);
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(5);
                }
                Thread.Sleep(5);
            }
        }


        bool isGameActive()
        {
            MWhandle = FindWindow(null, "Immortal Redneck");
            CWhandle = GetForegroundWindow();
            return MWhandle == CWhandle ? true : false;
        }
            private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    PID = m.GetProcIdFromName("ImmortalRedneck");
                    if (PID > 0)
                    {
                        label3.Text = "proc id = " + PID.ToString();
                        m.OpenProcess(PID);
                        hackready = true;
                        Thread.Sleep(100);

                    }
                    else
                    {
                        label3.Text = "proc id = " + PID.ToString();
                        hackready = false;
                        Thread.Sleep(100);
                    }
                }
                catch
                {

                }
                Thread.Sleep(100);
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                
                
                    if (GetAsyncKeyState(Keys.F2) < 0)
                    {
                    checkBox1.Checked = false;
                        TBON = false;
                    Thread.Sleep(2);

                }
                    else if (GetAsyncKeyState(Keys.F1) < 0)
                    {
                    checkBox1.Checked = true;
                    TBON = true;
                    Thread.Sleep(2);
                }
                    Thread.Sleep(2);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                TBON = true;
            } else
            {
                TBON = false;
            }
        }
    }
}
