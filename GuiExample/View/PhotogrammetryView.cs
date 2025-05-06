using ABB.Robotics.Controllers;
using FocalSpec.GuiExample.View;
using Rapid;
using System;
using System.Windows.Forms;

namespace Photogrammetry
{
    public partial class PhotogrammetryView : Form
    {
        private readonly ILogicMethods _logic;
        public MainView mainView;
        RapidFunctions Rap;

        public PhotogrammetryView(MainView mainView)
        {
            this.mainView = mainView;
            InitializeComponent();
            Rap = new RapidFunctions(this);
        }

        // Log a string to the Log Buffer
        public void LogMessage(string MSG)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.richTextBox1.AppendText(DateTime.Now.ToString() + ":   ");
            this.richTextBox1.AppendText(MSG);
            this.richTextBox1.AppendText("\n\r");
            this.richTextBox1.ScrollToCaret();
        }

        public void UpdateFormFields()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            Rap.Stop();
            this.richTextBox1.Enabled = true;
            this.richTextBox1.ReadOnly = true;

        }

        public void UpdateGrid(string msg)
        {
            this.richTextBox1.Text = msg;
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text = text;
            }
        }
        delegate void SetTextCallback(string text);

        private void btn_SaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "log files (*.log)|*.txt|All files (*.*)|*.*";
            file.DefaultExt = ".log";

            if (file.ShowDialog() == DialogResult.OK)
                this.richTextBox1.SaveFile(file.FileName, RichTextBoxStreamType.PlainText);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created By Ryan Mecham\n"
                + "Modified By The Xperts: Gerti, Carter, Rumman, Zerhye\n"
                + "In collaboration with:\n"
                + "Professor Dr. Haoyu Wang, CCSU,\n"
                + "Graduate Lab Assistant Ryan Sharp and\n"
                + "The University Of Connecticut Masters Of Engineering\n\n");
        }

        // Scan For ABB Controllers and Add To List View
        private void btn_ScanCTRLS_Click(object sender, EventArgs e)
        {
            ControllerInfoCollection ControllerList = Rap.ScanControllers();
            ListViewItem item = null;
            this.listView_Controllers.Items.Clear();
            foreach (ControllerInfo controllerInfo in ControllerList)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                item.Tag = controllerInfo;
                this.listView_Controllers.Items.Add(item);
            }
        }

        private void btn_ConnectCTRL_Click(object sender, EventArgs e)
        {
            if (btn_ConnectCTRL.Text == "Connect")
            {
                Rap.ConnectController(listView_Controllers.SelectedItems[0]);
                Rap.controller.Logon(UserInfo.DefaultUser);
                if (Rap.controller.Connected == true)
                {
                    btn_ConnectCTRL.Text = "Disconnect";
                    if (Rap.controller.OperatingMode == ControllerOperatingMode.Auto)
                    {
                        Rap.tasks = Rap.controller.Rapid.GetTasks();
                        //  Rap.tasks[0].Stop();
                    }
                    else
                        MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
                else
                    LogMessage("Connect Failed");
            }
            else
                btn_ConnectCTRL.Text = "Connect";
        }

        //Show instructions for finding and binding the USB id of the camera must be run as administrator
        private void cameraHardwareIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" +
               "run CMD as administrator, execute the following \n" +
               "usbipd --list \n" +
               "usbipd bind --hardware-id {hardwareID of camera} \n" +
               "Copy and paste Hardware id to this box \n" +
               "Note: this will persist across reboots if you want to unbind run \n" +
               "      usbipd unbind --all");
        }

        private void btn_StopRap_Click(object sender, EventArgs e)
        {
            Rap.Stop();
        }

        private void btn_StartRAP_Click(object sender, EventArgs e)
        {
            Rap.Start();
        }

        private void btn_RapContinue_Click(object sender, EventArgs e)
        {
            Rap.PhotoSequence();
        }

        private void btn_SelectFolder_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                //Use folder path
            }
            else
            {
                //Operation aborted by the user
            }
        }

        private void PhotogrammetryView_Load(object sender, EventArgs e)
        {

        }
    }
}

public class AutoClosingMessageBox
{
    System.Threading.Timer _timeoutTimer;
    string _caption;
    AutoClosingMessageBox(string text, string caption, int timeout)
    {
        _caption = caption;
        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeout, System.Threading.Timeout.Infinite);
        MessageBox.Show(text, caption);
    }

    public static void Show(string text, string caption, int timeout)
    {
        new AutoClosingMessageBox(text, caption, timeout);
    }

    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow(null, _caption);
        if (mbWnd != IntPtr.Zero)
            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }
    const int WM_CLOSE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

}