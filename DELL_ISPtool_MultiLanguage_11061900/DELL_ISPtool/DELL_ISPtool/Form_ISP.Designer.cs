
using System.Drawing;

namespace DELL_ISPtool
{
    partial class Form_ISP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ISP));
            this.label_FilePath = new System.Windows.Forms.Label();
            this.label_ModelNo = new System.Windows.Forms.Label();
            this.label_CurrentFW = new System.Windows.Forms.Label();
            this.label_NewFW = new System.Windows.Forms.Label();
            this.label_FileCKS = new System.Windows.Forms.Label();
            this.label_browse = new System.Windows.Forms.Label();
            this.button_Update = new System.Windows.Forms.Button();
            this.FilePathValue = new System.Windows.Forms.TextBox();
            this.FileCKSValue = new System.Windows.Forms.Label();
            this.ModelNoValue = new System.Windows.Forms.Label();
            this.CurrentFWValue = new System.Windows.Forms.Label();
            this.NewFWValue = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_initial = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MSGInfor = new System.Windows.Forms.Label();
            this.MSGProgress = new System.Windows.Forms.Label();
            this.MSGInforErr = new System.Windows.Forms.Label();
            this.pictureBox_NoteIcon2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_NoteIcon = new System.Windows.Forms.PictureBox();
            this.pictureBox_Help = new System.Windows.Forms.PictureBox();
            this.pictureBox_Zoom = new System.Windows.Forms.PictureBox();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.pictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.ISP_DrawBar = new DELL_ISPtool.DrawProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NoteIcon2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NoteIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Help)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Zoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISP_DrawBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label_FilePath
            // 
            resources.ApplyResources(this.label_FilePath, "label_FilePath");
            this.label_FilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.label_FilePath.Name = "label_FilePath";
            this.label_FilePath.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_FilePath_MouseDown);
            this.label_FilePath.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_FilePath_MouseMove);
            // 
            // label_ModelNo
            // 
            resources.ApplyResources(this.label_ModelNo, "label_ModelNo");
            this.label_ModelNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.label_ModelNo.Name = "label_ModelNo";
            this.label_ModelNo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_ModelNo_MouseDown);
            this.label_ModelNo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_ModelNo_MouseMove);
            // 
            // label_CurrentFW
            // 
            resources.ApplyResources(this.label_CurrentFW, "label_CurrentFW");
            this.label_CurrentFW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.label_CurrentFW.Name = "label_CurrentFW";
            this.label_CurrentFW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_CurrentFW_MouseDown);
            this.label_CurrentFW.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_CurrentFW_MouseMove);
            // 
            // label_NewFW
            // 
            resources.ApplyResources(this.label_NewFW, "label_NewFW");
            this.label_NewFW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.label_NewFW.Name = "label_NewFW";
            this.label_NewFW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_NewFW_MouseDown);
            this.label_NewFW.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_NewFW_MouseMove);
            // 
            // label_FileCKS
            // 
            resources.ApplyResources(this.label_FileCKS, "label_FileCKS");
            this.label_FileCKS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.label_FileCKS.Name = "label_FileCKS";
            this.label_FileCKS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_CKS_MouseDown);
            this.label_FileCKS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_CKS_MouseMove);
            // 
            // label_browse
            // 
            resources.ApplyResources(this.label_browse, "label_browse");
            this.label_browse.BackColor = System.Drawing.Color.White;
            this.label_browse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(195)))));
            this.label_browse.Name = "label_browse";
            this.label_browse.Click += new System.EventHandler(this.label_browse_Click);
            // 
            // button_Update
            // 
            this.button_Update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(195)))));
            this.button_Update.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_Update.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(195)))));
            this.button_Update.FlatAppearance.BorderSize = 0;
            this.button_Update.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(168)))), ((int)(((byte)(230)))));
            resources.ApplyResources(this.button_Update, "button_Update");
            this.button_Update.ForeColor = System.Drawing.Color.White;
            this.button_Update.Name = "button_Update";
            this.button_Update.UseVisualStyleBackColor = false;
            this.button_Update.EnabledChanged += new System.EventHandler(this.button_Update_EnabledChanged);
            this.button_Update.Click += new System.EventHandler(this.button_update_Click);
            this.button_Update.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_Update_MouseDown);
            this.button_Update.MouseLeave += new System.EventHandler(this.button_Update_MouseLeave);
            this.button_Update.MouseHover += new System.EventHandler(this.button_Update_MouseHover);
            // 
            // FilePathValue
            // 
            this.FilePathValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.FilePathValue.BackColor = System.Drawing.SystemColors.Window;
            this.FilePathValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePathValue.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.FilePathValue, "FilePathValue");
            this.FilePathValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FilePathValue.HideSelection = false;
            this.FilePathValue.Name = "FilePathValue";
            this.FilePathValue.ReadOnly = true;
            this.FilePathValue.ShortcutsEnabled = false;
            // 
            // FileCKSValue
            // 
            this.FileCKSValue.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.FileCKSValue, "FileCKSValue");
            this.FileCKSValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FileCKSValue.Name = "FileCKSValue";
            // 
            // ModelNoValue
            // 
            this.ModelNoValue.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.ModelNoValue, "ModelNoValue");
            this.ModelNoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ModelNoValue.Name = "ModelNoValue";
            // 
            // CurrentFWValue
            // 
            this.CurrentFWValue.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.CurrentFWValue, "CurrentFWValue");
            this.CurrentFWValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.CurrentFWValue.Name = "CurrentFWValue";
            // 
            // NewFWValue
            // 
            this.NewFWValue.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.NewFWValue, "NewFWValue");
            this.NewFWValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.NewFWValue.Name = "NewFWValue";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label_initial);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DELL_ISPtool.Properties.Resources.bluecircle;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label_initial
            // 
            resources.ApplyResources(this.label_initial, "label_initial");
            this.label_initial.BackColor = System.Drawing.SystemColors.Window;
            this.label_initial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label_initial.Name = "label_initial";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // MSGInfor
            // 
            resources.ApplyResources(this.MSGInfor, "MSGInfor");
            this.MSGInfor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.MSGInfor.Name = "MSGInfor";
            // 
            // MSGProgress
            // 
            resources.ApplyResources(this.MSGProgress, "MSGProgress");
            this.MSGProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.MSGProgress.Name = "MSGProgress";
            // 
            // MSGInforErr
            // 
            resources.ApplyResources(this.MSGInforErr, "MSGInforErr");
            this.MSGInforErr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.MSGInforErr.Name = "MSGInforErr";
            // 
            // pictureBox_NoteIcon2
            // 
            resources.ApplyResources(this.pictureBox_NoteIcon2, "pictureBox_NoteIcon2");
            this.pictureBox_NoteIcon2.Name = "pictureBox_NoteIcon2";
            this.pictureBox_NoteIcon2.TabStop = false;
            // 
            // pictureBox_NoteIcon
            // 
            resources.ApplyResources(this.pictureBox_NoteIcon, "pictureBox_NoteIcon");
            this.pictureBox_NoteIcon.Name = "pictureBox_NoteIcon";
            this.pictureBox_NoteIcon.TabStop = false;
            // 
            // pictureBox_Help
            // 
            this.pictureBox_Help.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox_Help, "pictureBox_Help");
            this.pictureBox_Help.Name = "pictureBox_Help";
            this.pictureBox_Help.TabStop = false;
            this.pictureBox_Help.Click += new System.EventHandler(this.pictureBox_Help_Click);
            // 
            // pictureBox_Zoom
            // 
            this.pictureBox_Zoom.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox_Zoom, "pictureBox_Zoom");
            this.pictureBox_Zoom.Name = "pictureBox_Zoom";
            this.pictureBox_Zoom.TabStop = false;
            this.pictureBox_Zoom.Click += new System.EventHandler(this.pictureBox_Zoom_Click);
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox_Close, "pictureBox_Close");
            this.pictureBox_Close.Name = "pictureBox_Close";
            this.pictureBox_Close.TabStop = false;
            this.pictureBox_Close.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // pictureBox_Logo
            // 
            resources.ApplyResources(this.pictureBox_Logo, "pictureBox_Logo");
            this.pictureBox_Logo.Name = "pictureBox_Logo";
            this.pictureBox_Logo.TabStop = false;
            this.pictureBox_Logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Logo_MouseDown);
            this.pictureBox_Logo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Logo_MouseMove);
            // 
            // ISP_DrawBar
            // 
            this.ISP_DrawBar.Barvalue = 0;
            this.ISP_DrawBar.Dellcolor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(157)))), ((int)(((byte)(250)))));
            resources.ApplyResources(this.ISP_DrawBar, "ISP_DrawBar");
            this.ISP_DrawBar.Maximum = 100;
            this.ISP_DrawBar.Minimum = 0;
            this.ISP_DrawBar.Name = "ISP_DrawBar";
            this.ISP_DrawBar.Rect_height = 10;
            this.ISP_DrawBar.Rect_width = 640;
            this.ISP_DrawBar.Rect_x = 40;
            this.ISP_DrawBar.Rect_y = 315;
            this.ISP_DrawBar.TabStop = false;
            // 
            // Form_ISP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.FilePathValue);
            this.Controls.Add(this.FileCKSValue);
            this.Controls.Add(this.NewFWValue);
            this.Controls.Add(this.CurrentFWValue);
            this.Controls.Add(this.ModelNoValue);
            this.Controls.Add(this.pictureBox_NoteIcon2);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.label_browse);
            this.Controls.Add(this.pictureBox_NoteIcon);
            this.Controls.Add(this.label_FileCKS);
            this.Controls.Add(this.label_NewFW);
            this.Controls.Add(this.label_CurrentFW);
            this.Controls.Add(this.label_ModelNo);
            this.Controls.Add(this.label_FilePath);
            this.Controls.Add(this.pictureBox_Help);
            this.Controls.Add(this.pictureBox_Zoom);
            this.Controls.Add(this.pictureBox_Close);
            this.Controls.Add(this.pictureBox_Logo);
            this.Controls.Add(this.MSGInfor);
            this.Controls.Add(this.MSGProgress);
            this.Controls.Add(this.MSGInforErr);
            this.Controls.Add(this.ISP_DrawBar);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_ISP";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_ISP_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_ISP_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NoteIcon2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NoteIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Help)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Zoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISP_DrawBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Logo;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.PictureBox pictureBox_Zoom;
        private System.Windows.Forms.PictureBox pictureBox_Help;
        private System.Windows.Forms.Label label_FilePath;
        private System.Windows.Forms.Label label_ModelNo;
        private System.Windows.Forms.Label label_CurrentFW;
        private System.Windows.Forms.Label label_NewFW;
        private System.Windows.Forms.Label label_FileCKS;
        private System.Windows.Forms.PictureBox pictureBox_NoteIcon;
        private System.Windows.Forms.Label label_browse;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.PictureBox pictureBox_NoteIcon2;
        private System.Windows.Forms.TextBox FilePathValue;
        private System.Windows.Forms.Label ModelNoValue;
        private System.Windows.Forms.Label CurrentFWValue;
        private System.Windows.Forms.Label NewFWValue;
        private System.Windows.Forms.Label FileCKSValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_initial;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label MSGInfor;
        private System.Windows.Forms.Label MSGProgress;
        private System.Windows.Forms.Label MSGInforErr;
        private DrawProgressBar ISP_DrawBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}