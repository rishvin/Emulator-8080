namespace Emu
{
    partial class GUI
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtBox2 = new System.Windows.Forms.TextBox();
            this.txtCurLine = new System.Windows.Forms.TextBox();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnReg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnInst = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnKill = new System.Windows.Forms.ToolStripMenuItem();
            this.mnStepXect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnRst = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(679, 18);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "Welcome";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 13);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(23, 23);
            this.toolStripStatusLabel2.Text = "Welcome";
            // 
            // txtLine
            // 
            this.txtLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLine.BackColor = System.Drawing.SystemColors.Control;
            this.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLine.Enabled = false;
            this.txtLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLine.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLine.Location = new System.Drawing.Point(572, 427);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(41, 15);
            this.txtLine.TabIndex = 18;
            this.txtLine.Text = "0";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBox1.Location = new System.Drawing.Point(527, 427);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 15);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "Tot Ln : ";
            // 
            // txtBox2
            // 
            this.txtBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox2.BackColor = System.Drawing.SystemColors.Control;
            this.txtBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox2.Enabled = false;
            this.txtBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBox2.Location = new System.Drawing.Point(429, 427);
            this.txtBox2.Name = "txtBox2";
            this.txtBox2.Size = new System.Drawing.Size(45, 15);
            this.txtBox2.TabIndex = 21;
            this.txtBox2.Text = "Cur Ln : ";
            // 
            // txtCurLine
            // 
            this.txtCurLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurLine.BackColor = System.Drawing.SystemColors.Control;
            this.txtCurLine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurLine.Enabled = false;
            this.txtCurLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurLine.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCurLine.Location = new System.Drawing.Point(474, 427);
            this.txtCurLine.Name = "txtCurLine";
            this.txtCurLine.Size = new System.Drawing.Size(41, 15);
            this.txtCurLine.TabIndex = 20;
            this.txtCurLine.Text = "0";
            // 
            // mnFile
            // 
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnCreate,
            this.mnOpen,
            this.mnSave,
            this.mnSaveAs});
            this.mnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(39, 19);
            this.mnFile.Text = "File";
            // 
            // mnCreate
            // 
            this.mnCreate.BackColor = System.Drawing.SystemColors.Control;
            this.mnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnCreate.Name = "mnCreate";
            this.mnCreate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnCreate.Size = new System.Drawing.Size(157, 22);
            this.mnCreate.Text = "Create";
            this.mnCreate.Click += new System.EventHandler(this.mnCreate_Click);
            // 
            // mnOpen
            // 
            this.mnOpen.BackColor = System.Drawing.SystemColors.Control;
            this.mnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnOpen.Name = "mnOpen";
            this.mnOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnOpen.Size = new System.Drawing.Size(157, 22);
            this.mnOpen.Text = "Open";
            this.mnOpen.Click += new System.EventHandler(this.mnOpen_Click);
            // 
            // mnSave
            // 
            this.mnSave.Enabled = false;
            this.mnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnSave.Name = "mnSave";
            this.mnSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnSave.Size = new System.Drawing.Size(157, 22);
            this.mnSave.Text = "Save";
            this.mnSave.Click += new System.EventHandler(this.mnSave_Click);
            // 
            // mnSaveAs
            // 
            this.mnSaveAs.Enabled = false;
            this.mnSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnSaveAs.Name = "mnSaveAs";
            this.mnSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnSaveAs.Size = new System.Drawing.Size(157, 22);
            this.mnSaveAs.Text = "Save As";
            this.mnSaveAs.Click += new System.EventHandler(this.mnSave_Click);
            // 
            // mnView
            // 
            this.mnView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnReg,
            this.mnMenu,
            this.mnFont});
            this.mnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.mnView.Name = "mnView";
            this.mnView.Size = new System.Drawing.Size(49, 19);
            this.mnView.Text = "Tools";
            // 
            // mnReg
            // 
            this.mnReg.BackColor = System.Drawing.SystemColors.Control;
            this.mnReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.mnReg.Name = "mnReg";
            this.mnReg.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnReg.Size = new System.Drawing.Size(196, 22);
            this.mnReg.Text = "View Registers";
            this.mnReg.Click += new System.EventHandler(this.mnReg_Click);
            // 
            // mnMenu
            // 
            this.mnMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.mnMenu.Name = "mnMenu";
            this.mnMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnMenu.Size = new System.Drawing.Size(196, 22);
            this.mnMenu.Text = "View Memory";
            this.mnMenu.Click += new System.EventHandler(this.mnMem_Click);
            // 
            // mnFont
            // 
            this.mnFont.Name = "mnFont";
            this.mnFont.Size = new System.Drawing.Size(196, 22);
            this.mnFont.Text = "Font";
            this.mnFont.Click += new System.EventHandler(this.mnFont_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile,
            this.mnView,
            this.mnInst,
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(679, 23);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnInst
            // 
            this.mnInst.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnLoad,
            this.mnExecute});
            this.mnInst.Name = "mnInst";
            this.mnInst.Size = new System.Drawing.Size(81, 19);
            this.mnInst.Text = "Instructions";
            // 
            // mnLoad
            // 
            this.mnLoad.Enabled = false;
            this.mnLoad.Name = "mnLoad";
            this.mnLoad.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.mnLoad.Size = new System.Drawing.Size(146, 22);
            this.mnLoad.Text = "Load";
            this.mnLoad.Click += new System.EventHandler(this.mnLoad_Click);
            // 
            // mnExecute
            // 
            this.mnExecute.Enabled = false;
            this.mnExecute.Name = "mnExecute";
            this.mnExecute.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.mnExecute.Size = new System.Drawing.Size(146, 22);
            this.mnExecute.Text = "Execute";
            this.mnExecute.Click += new System.EventHandler(this.mnExec_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnKill,
            this.mnStepXect,
            this.mnRst});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(56, 19);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // mnKill
            // 
            this.mnKill.Enabled = false;
            this.mnKill.Name = "mnKill";
            this.mnKill.Size = new System.Drawing.Size(156, 22);
            this.mnKill.Text = "Force Kill";
            this.mnKill.Click += new System.EventHandler(this.mnKill_Click);
            // 
            // mnStepXect
            // 
            this.mnStepXect.Enabled = false;
            this.mnStepXect.Name = "mnStepXect";
            this.mnStepXect.Size = new System.Drawing.Size(156, 22);
            this.mnStepXect.Text = "Step Execution";
            this.mnStepXect.Click += new System.EventHandler(this.mnStepXect_Click);
            // 
            // mnRst
            // 
            this.mnRst.Name = "mnRst";
            this.mnRst.Size = new System.Drawing.Size(156, 22);
            this.mnRst.Text = "Reset";
            this.mnRst.Click += new System.EventHandler(this.mnRst_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 404);
            this.panel1.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(679, 404);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // GUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(679, 445);
            this.Controls.Add(this.txtBox2);
            this.Controls.Add(this.txtCurLine);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI";
            this.Text = "8080 Emulator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtBox2;
        internal System.Windows.Forms.TextBox txtLine;
        internal System.Windows.Forms.TextBox txtCurLine;
        private System.Windows.Forms.ToolStripMenuItem mnFile;
        private System.Windows.Forms.ToolStripMenuItem mnCreate;
        private System.Windows.Forms.ToolStripMenuItem mnOpen;
        private System.Windows.Forms.ToolStripMenuItem mnSave;
        private System.Windows.Forms.ToolStripMenuItem mnSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnView;
        private System.Windows.Forms.ToolStripMenuItem mnReg;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnMenu;
        private System.Windows.Forms.ToolStripMenuItem mnInst;
        internal System.Windows.Forms.ToolStripMenuItem mnLoad;
        internal System.Windows.Forms.ToolStripMenuItem mnExecute;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnStepXect;
        internal System.Windows.Forms.ToolStripMenuItem mnKill;
        internal System.Windows.Forms.ToolStripMenuItem mnRst;
        private System.Windows.Forms.ToolStripMenuItem mnFont;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

