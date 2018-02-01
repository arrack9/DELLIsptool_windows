using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Linq;

namespace DELL_ISPtool
{
    public partial class Form_ISP : Form
    {
        BackgroundWorker bw = new BackgroundWorker();

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == 0x0219)   //if (m.Msg == WM_DEVICECHANGE) public const int WM_DEVICECHANGE = 0x0219;
        //    {
        //        // WM_DEVICECHANGE can have several meanings depending on the WParam value...
        //        switch (m.WParam.ToInt32())
        //        {
        //            case 7:  //DBT_DEVNODES_CHANGED
        //                if (ISP_Methed.MStar_USBConnect() == false)
        //                {
        //                    if (bw.IsBusy == true)
        //                    {
        //                        bw.CancelAsync();
        //                    }
        //                    update_NoteString();
        //                }
        //                else
        //                {
        //                    update_NoteString();
        //                }
        //                break;
        //                //case DBT_DEVICEARRIVAL:  //usb disk or flash
        //                //    break;
        //                //case DBT_DEVICEQUERYREMOVE:
        //                //    break;
        //                //case DBT_DEVICEREMOVECOMPLETE:
        //                //    break;
        //        }
        //        //MessageBox.Show(m.WParam.ToString());
        //    }
        //}

        MStarISP ISP_Methed = new MStarISP();
        MsgProgress ISP_MSG = new MsgProgress();
        //DrawProgressBar ISP_DrawBar = new DrawProgressBar();

        public Form_ISP()
        {
            ISP_MSG.SwitchUILangauge(MsgProgress.Language.L_Spanish);
            InitializeComponent();
            initBackgroundWorker();
            InitialState();
            StartPosition = FormStartPosition.CenterScreen; //設定window顯示在螢幕中心
            // let FilePaath to hide cusor
            Label lb = new Label();
            lb.Size = new Size(0, 0);
            FilePathValue.Controls.Add(lb);
            FilePathValue.GotFocus += new EventHandler(FilePathValue_GotFocus);
            ///////////////////////////////

            timer1.Start();
        }
        void FilePathValue_GotFocus(object sender, EventArgs e) // let FilePaath to hide cusor
        {
            FilePathValue.Controls[0].Focus();
        }

        private void InitialState()
        {
            panel1.Location = new System.Drawing.Point(0, 95);

            CheckMonitor();
            FindBinFile();
            Update_NoteString();

            //Test region
            Timer timer = new Timer();
            timer.Interval = 1500; //5秒啓動 
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
            /////////
        }
        private bool FindBinFile()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(Application.StartupPath);
                string strPath="";
                FileInfo[] fis = d.GetFiles();
                bool find = false;
                foreach (FileInfo fi in fis)
                {
                    if (fi.Extension == ".bin")
                    {
                        if (find == false)
                        {
                            find = true;
                            strPath = fi.FullName;
                        }
                        else if (find == true)// not only one .bin file
                        {
                            find = false;
                            break; ;   
                        }
                    }
                }
                if (find == true)
                {
                    LoadBinFile(strPath);
                    return ISP_Methed.GetIsLoadBinFile();
                }
                else
                    return false;
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
                return false;
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
                return false;
            }
        }
        private bool CheckMonitor()
        {
            ModelNoValue.Text = "P4317Q";
            CurrentFWValue.Text = "Not detected";
            return true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Hide();
            panel1.Dispose();
            //停止Timer 
            ((System.Windows.Forms.Timer)sender).Stop();
        }

        //Button click---Button_Close, Buton_Zoom, Button_Help**********************************
        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                this.Close();
                Environment.Exit(Environment.ExitCode);  //真正關掉視窗，後面的程式不會繼續在內部動作
            }
        }
        private void pictureBox_Zoom_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;  //視窗最小化
        }
        private void pictureBox_Help_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dell.com.tw/");  //開啟web browser
        }

        //視窗拖曳**************************************************************************
        private Point startPoint;

        private void pictureBox_Logo_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void pictureBox_Logo_MouseMove(object sender, MouseEventArgs e)
        {
            //使用右鍵拖曳無效
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                //新視窗的位置
                mousePos.Offset(startPoint.X, startPoint.Y);
                //改變視窗位置
                Location = mousePos;
            }
        }
        private void Form_ISP_MouseDown(object sender, MouseEventArgs e)
        {
            //startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void Form_ISP_MouseMove(object sender, MouseEventArgs e)
        {
            //////如果使用者使用的是左鍵按下，使用右鍵拖曳無效
            //MSGInforErr.Text = Control.MousePosition.ToString();
            //if (e.Button == MouseButtons.Left)
            //{
                
            //    Point mousePos = Control.MousePosition;
            //    //新視窗的位置
            //    mousePos.Offset(startPoint.X, startPoint.Y);
            //    //改變視窗位置
            //    Location = mousePos;
            //}
        }
        private void label_FilePath_MouseDown(object sender, MouseEventArgs e)
        {
            //startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void label_FilePath_MouseMove(object sender, MouseEventArgs e)
        {
            //如果使用者使用的是左鍵按下，使用右鍵拖曳無效
            //if (e.Button == MouseButtons.Left)
            //{
            //    Point mousePos = Control.MousePosition;
            //    //新視窗的位置
            //    mousePos.Offset(startPoint.X, startPoint.Y);
            //    //改變視窗位置
            //    Location = mousePos;
            //}
        }
        private void label_ModelNo_MouseDown(object sender, MouseEventArgs e)
        {
            //startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void label_ModelNo_MouseMove(object sender, MouseEventArgs e)
        {
            //如果使用者使用的是左鍵按下，使用右鍵拖曳無效
            //if (e.Button == MouseButtons.Left)
            //{
            //    Point mousePos = Control.MousePosition;
            //    //新視窗的位置
            //    mousePos.Offset(startPoint.X, startPoint.Y);
            //    //改變視窗位置
            //    Location = mousePos;
            //}
        }
        private void label_CurrentFW_MouseDown(object sender, MouseEventArgs e)
        {
            //startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void label_CurrentFW_MouseMove(object sender, MouseEventArgs e)
        {
            //如果使用者使用的是左鍵按下，使用右鍵拖曳無效
            //if (e.Button == MouseButtons.Left)
            //{
            //    Point mousePos = Control.MousePosition;
            //    //新視窗的位置
            //    mousePos.Offset(startPoint.X, startPoint.Y);
            //    //改變視窗位置
            //    Location = mousePos;
            //}
        }
        private void label_NewFW_MouseDown(object sender, MouseEventArgs e)
        {
            //startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void label_NewFW_MouseMove(object sender, MouseEventArgs e)
        {
            //如果使用者使用的是左鍵按下，使用右鍵拖曳無效
            //if (e.Button == MouseButtons.Left)
            //{
            //    Point mousePos = Control.MousePosition;
            //    //新視窗的位置
            //    mousePos.Offset(startPoint.X, startPoint.Y);
            //    //改變視窗位置
            //    Location = mousePos;
            //}
        }
        private void label_CKS_MouseDown(object sender, MouseEventArgs e)
        {
            //startPoint = new Point(-e.X + SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);
        }
        private void label_CKS_MouseMove(object sender, MouseEventArgs e)
        {
            //如果使用者使用的是左鍵按下，使用右鍵拖曳無效
            //if (e.Button == MouseButtons.Left)
            //{
            //    Point mousePos = Control.MousePosition;
            //    //新視窗的位置
            //    mousePos.Offset(startPoint.X, startPoint.Y);
            //    //改變視窗位置
            //    Location = mousePos;
            //}
        }
        //end 視窗拖曳***********************************************************************************

        // Update buttom pattern ****************************************************************
        private void button_update_Normal()
        {
            button_Update.FlatStyle = FlatStyle.Flat;
            button_Update.BackColor = Color.FromArgb(0, 133, 195);
            button_Update.FlatAppearance.BorderColor = Color.FromArgb(0, 133, 195);
            button_Update.FlatAppearance.BorderSize = 0;
            button_Update.ForeColor = Color.FromArgb(255, 255, 255);
            button_Update.Enabled = true;
        }
        private void button_update_Grayout()
        {
            //if (bw.IsBusy == true)
            {
                button_Update.FlatStyle = FlatStyle.Flat;
                button_Update.BackColor = Color.FromArgb(238, 238, 238);
                button_Update.ForeColor = Color.FromArgb(255, 0, 138);
                button_Update.FlatAppearance.BorderColor = Color.FromArgb(238, 238, 238);
                button_Update.Enabled = false;
            }
        }
        private void button_Update_MouseHover(object sender, EventArgs e)
        {
            if (bw.IsBusy == false)
            {
                //button_Update.BackColor = Color.FromArgb(87, 168, 230);
                //button_Update.FlatStyle = FlatStyle.Flat;
                //button_Update.FlatAppearance.BorderColor = Color.FromArgb(87, 168, 230);
            }
        }
        private void button_Update_MouseDown(object sender, MouseEventArgs e)
        {
            if (bw.IsBusy == false)
            {
                //button_Update_MouseLeave(sender, e);
                button_Update.FlatStyle = FlatStyle.Standard;
                button_Update.BackColor = Color.FromArgb(0, 133, 195);
                button_Update.FlatAppearance.BorderSize = 1;
                //button_Update.FlatAppearance.BorderColor = Color.FromArgb(255, 255, 255);
            }
        }
        private void button_Update_MouseLeave(object sender, EventArgs e)
        {
            if (bw.IsBusy == false)
            {
                button_update_Normal();
            }
        }
        private void button_Update_EnabledChanged(object sender, EventArgs e)
        {
            if (button_Update.Enabled == true)
                button_Update.ForeColor = Color.FromArgb(255, 255, 255);
            else
                button_Update.ForeColor = Color.FromArgb(255, 0, 138);

        }
        //end Update buttom pattern*********************************************************************

        //browse開啟bin file, show string ***************************************************************
        public void label_browse_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.CheckFileExists = true;                 //指定不存在的副檔名時要顯示警告對話方塊，屬性值為 true，否則為 false。
                openFileDialog1.Filter = "bin files (*.bin)|*.bin";     //只show .bin file的檔名
                openFileDialog1.Multiselect = false;                    //只允許選取一個檔案為 true，否則為 false
                if (openFileDialog1.ShowDialog() == DialogResult.OK)    //開啟檔案fail後，底下流程不在執行
                {
                    string strPath = openFileDialog1.FileName;
                    if (strPath != "")
                    {
                        LoadBinFile(strPath);
                        Update_NoteString();
                    }
                }
            }
        }
        private void LoadBinFile(string strPath)
        {
            ISP_Methed.SetBinFile(strPath);
            FilePathValue.Text = strPath;
            FilePathValue.SelectionStart = FilePathValue.TextLength;
            NewFWValue.Text = "Not detected";
            FileCKSValue.Text = ISP_Methed.GetIsLoadBinFile() ? "0x"+ String.Format("{0:X}", ISP_Methed.GetBinFileChecksum()) :"";    // format "0xFFFF"
        }

        public void Update_NoteString() //update Note icon與message
        {
            if (ISP_Methed.GetIsLoadBinFile() == false)
            {
                ISP_MSG.FileNotFound = true;
                pictureBox_NoteIcon2.Image = Properties.Resources.Error;
                MSGInforErr.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.FileNotFound);
                button_update_Grayout();
            }
            else
            {
                ISP_MSG.FileNotFound = false;
                pictureBox_NoteIcon2.Image = null;
                MSGInforErr.Text = "";
            }
            if (ISP_Methed.MStar_USBConnect() == false)
            {
                ISP_MSG.MonitorNotDetect = true;
                pictureBox_NoteIcon.Image = Properties.Resources.Error;
                MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.MonitorNotDetect);
                button_update_Grayout();
                if (bw.IsBusy == true)
                {
                    bw.CancelAsync();
                }
            }
            else
            {
                ISP_MSG.MonitorNotDetect = false;
                pictureBox_NoteIcon.Image = null;
                MSGInfor.Text = "";
            }
            if (ISP_MSG.FileNotFound == false && ISP_MSG.MonitorNotDetect == false)
            {
                if (bw.IsBusy == false)
                {
                    if (ISP_Methed.GetISPStep() == MStarISP.ISPStep.Standby)
                    {
                        if (button_Update.Enabled == false)
                            button_update_Normal();
                        pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                        MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.UpdatingNoted);
                    }
                    else if (ISP_Methed.GetISPStep() == MStarISP.ISPStep.Finished)
                    {
                        MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Success);//ISP_MSG.Msg_Temp[4];
                        pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                        MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.Normal);
                        button_Update.Text = "Close";
                    }
                }
                else if (bw.IsBusy == true)
                {
                    //progressBar1.Value = ISP_Methed.GetISPProgress();
                    ISP_DrawBar.Barvalue = ISP_Methed.GetISPProgress();
                    //MSGInforErr.Text = ISP_Methed.GetISPProgress().ToString();
                    switch (ISP_Methed.GetISPStep())
                    {
                        case MStarISP.ISPStep.Standby:
                            MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Preparing) + DotString();//ISP_MSG.Msg_Temp[0] + DotString();
                            pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                            MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.UpdatingNoted);
                            break;
                        case MStarISP.ISPStep.Erase:
                            MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Erasing) + DotString();//ISP_MSG.Msg_Temp[1] + DotString();
                            pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                            MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.UpdatingNoted);
                            break;
                        case MStarISP.ISPStep.Blanking:
                            MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Erasing) + DotString();//ISP_MSG.Msg_Temp[1] + DotString();
                            pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                            MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.UpdatingNoted);
                            break;
                        case MStarISP.ISPStep.Program:
                            MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Updating) + DotString();//ISP_MSG.Msg_Temp[2] + DotString();
                            pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                            MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.UpdatingNoted);
                            break;
                        case MStarISP.ISPStep.Verify:
                            MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Verifying) + DotString();//ISP_MSG.Msg_Temp[3] + DotString();
                            pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                            MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.UpdatingNoted);
                            break;
                        case MStarISP.ISPStep.Finished:
                            MSGProgress.Text = ISP_MSG.SwitchProgramStringLag(MsgProgress.ProgramInfo.Success) + DotString();//ISP_MSG.Msg_Temp[4] + DotString();
                            pictureBox_NoteIcon.Image = Properties.Resources.Info;  //絕對路徑下的圖檔
                            MSGInfor.Text = ISP_MSG.ReturnStatusString(MsgProgress.ErrInfo.Normal);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private string DotString()
        {
            if (ISP_MSG.preISPStep != ISP_Methed.GetISPStep())
            {
                ISP_MSG.preISPStep = ISP_Methed.GetISPStep();
                ISP_MSG.DotCount = 0;
            }
            else
            {
                ISP_MSG.DotCount=(ISP_MSG.DotCount+1)%4;
            }
            return ISP_MSG.DotCount == 1 ? "." : ISP_MSG.DotCount == 2 ? ".." : ISP_MSG.DotCount == 3 ? "..." : "";
        }
        private void initProgressBar()
        {
            //progressBar1.Step = 1;
        }

        private void initBackgroundWorker()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
#if false//true
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_StartISP() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashChipErase() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashBlanking2() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashProgram() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashVerify2() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_EndISP() == false) throw new ArgumentNullException();
#else
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_StartISP2() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashChipErase2()==false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashBlanking2() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashProgram2() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_FlashVerify2() == false) throw new ArgumentNullException();
                if (bw.CancellationPending == true) throw new ArgumentNullException();
                if (ISP_Methed.MStar_EndISP2() == false) throw new ArgumentNullException();

#endif
            }
            catch
            {
                e.Cancel = true;
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                button_Update.Text = "Close";
                timer1.Stop();
            }

            else if (!(e.Error == null))
            {
            }

            else
            {
                button_update_Normal();
                //progressBar1.Value = ISP_Methed.GetISPProgress();
                ISP_DrawBar.Barvalue = ISP_Methed.GetISPProgress();

                Update_NoteString();
                timer1.Stop();
            }
        }
        private void button_update_Click(object sender, EventArgs e)
        {
            if(ISP_Methed.GetISPStep()==MStarISP.ISPStep.Finished)
            {
                this.Close();
                Environment.Exit(Environment.ExitCode);  //真正關掉視窗，後面的程式不會繼續在內部動作
            }
            else if (bw.IsBusy == false)
            {
                button_update_Grayout();
                //progressBar1.Value = 0;
                timer1.Start();
                bw.RunWorkerAsync();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Update_NoteString();

        }

    }
}
