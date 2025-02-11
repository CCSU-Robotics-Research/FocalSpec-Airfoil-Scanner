using ABB.Robotics.Controllers;
using System;
using System.Windows.Forms;




namespace Photogrametry
{


    public partial class Form1 : Form
    {
        
        

        RapidFunctions Rap;

        // Create the correct version of gphoto library connection
       gphoto_SSH gphoto;

        public string s_hardwareID;
        public int i_Frames, i_Interval;
        public Form1()
        {
                        
            InitializeComponent();
            Listmethod();
            Rap = new RapidFunctions(this);
            gphoto = new gphoto_SSH(this);


            // Pass RapidFunctions and gPhoto to each other
            Rap.gphoto = gphoto;
            gphoto.Rap = Rap;

            //On Load Conversions
            s_hardwareID = this.hardwareID.Text.ToString();
            i_Frames = ((int)this.Frames.Value);
            i_Interval = ((int)this.Interval.Value);
            
            //Set the Running Indicator to Red on Startup
            this.SubRunning.BackColor = System.Drawing.Color.Red;


        }


        public void Listmethod()
        {
           

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
                + "In collaboration with:\n"
                + "Professor Dr. Haoyu Wang, CCSU and\n"
                + "The University Of Connecticut Masters Of Engineering\n\n");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       
        //Scan For ABB Controllers and Add To List View
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
                    {
                        MessageBox.Show(
                            "Automatic mode is required to start execution from a remote client.");
                    }
                }
                else
                {
                    LogMessage("Connect Failed");
                }

            }
            else
            {


                btn_ConnectCTRL.Text = "Connect";
            }


        }

        private void WSLClose_Click(object sender, EventArgs e)
        {
             gphoto.SubSystemStop();
                                       
        }

        private void btn_SubSystemStart(object sender, EventArgs e)
        {
            gphoto.SubSystemStart();
        }

        private void hardwareID_TextChanged(object sender, EventArgs e)
        {
            s_hardwareID = hardwareID.Text.ToString();


        }

        private void btn_Capture_Click(object sender, EventArgs e)
        {
            gphoto.CaptureAndDownload();
        }

        private void Interval_ValueChanged(object sender, EventArgs e)
        {
            i_Interval = ((int)this.Interval.Value);
        }

        private void Frames_ValueChanged(object sender, EventArgs e)
        {
            i_Frames = ((int)this.Frames.Value);
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btn_WSLStart_Click(object sender, EventArgs e)
        {
            gphoto.SubSystemStart();
            if (this.SubRunning.Text == "Running")
            {
                groupBox3.Enabled = true;
            }
        }

        private void btn_WSLStop_Click(object sender, EventArgs e)
        {
            gphoto.SubSystemStop();
            groupBox3.Enabled = false;
        }

        private void CameraFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_StopRapClick_1(object sender, EventArgs e)
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

        private void lbl_WSLStatus_Click(object sender, EventArgs e)
        {

        }

        private void SubRunning_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            gphoto.SSHStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gphoto.GetCameraCount();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            gphoto.SSHEnd();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btn_SelectFolder_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                gphoto.LocalFolder = folderDialog.SelectedPath;
                //Use folder path
            }
            else
            {
                //Operation aborted by the user
            }
        }

        private void CameraFolder_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gphoto.SubSystemStop();
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