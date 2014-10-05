using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;
namespace Emu
{
    public partial class GUI : Form
    {
        bool fstTouch = true;
        bool fMem = false;
        bool fReg = false;
        Panel PanMsg;
        FlowLayoutPanel RegPan;
        Label XMsg, RegA, RegB, RegC, RegD, RegE, RegH, RegL, S, A, Z, P, C;
        RTB rtbTar;
        TextBox txtMsg;
        LdStr ldstr;
        Engine engine;
        DataGridView GridMem;
        internal Thread th;
        ArrayList memInd;
        public GUI()
        {
            Entry entry = new Entry();
            entry.Show();
            InitializeComponent();
            InitMsgConsole();
            InitRegBar();
            InitGridMem();
            XMsg.Click += new EventHandler(XMsg_Click);
            rtbTar = new RTB(this);
            ldstr = new LdStr(rtbTar);
            memInd = new ArrayList();
            engine = new Engine(rtbTar, this);
            InitMem();
            entry.Dispose();
            
        }

        DialogResult DiaResult()
        {
            DialogResult dg = DialogResult.No;

            if (rtbTar.Text != "" && (LdStr.DocSaved == false || LdStr.DocTyped == true))
            {
                dg = MessageBox.Show("Save the file", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }

            if (dg == DialogResult.Yes)
            {
                if (!LdStr.DocSaved)
                    if (ldstr.ShowSaveFD() == DialogResult.Cancel)
                        return DialogResult.Cancel;
                rtbTar.SaveFile(ldstr.SfdFpath, ldstr.SetSfdRTST());
                ldstr.SetDocFlags(true, false);
            }
            return dg;
        }


        private void mnCreate_Click(object sender, EventArgs e)
        {
            if (fstTouch)
                FstTouch();
            else
                if (DiaResult() == DialogResult.Cancel)
                    return;


            //      -CREATE FILE-
            rtbTar.Clear();
            ldstr.SetDocFlags(false, false);
            this.Text = "8080 Emulator - Noname";
            txtLine.Text = "1";
            txtCurLine.Text = "1";
            ldstr.SfdFpath = "";
            mnLoad.Enabled = true;
            rtbTar.Focus();
            
        }

        private void mnOpen_Click(object sender, EventArgs e)
        {
            if (DiaResult() == DialogResult.Cancel)
                return;

            //     -LOAD FILE-

            if (ldstr.ShowOpenFD() == DialogResult.Cancel)
                return;

            if (fstTouch)
                FstTouch();

            rtbTar.LoadFile(ldstr.OfdFpath, ldstr.SetOfdRTST());
            ldstr.SfdFpath = ldstr.OfdFpath;
            this.Text = "8080 Emulator - " + trimDocName(ldstr.OfdFpath);
            ldstr.OfdFpath = "";
            ldstr.SetDocFlags(true, false);
            mnLoad.Enabled = true;
        }

        private void mnSave_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == "Save As" || !LdStr.DocSaved)
                if (ldstr.ShowSaveFD() == DialogResult.Cancel)
                    return;
            if (LdStr.DocTyped || sender.ToString() == "Save As")
            {

                rtbTar.SaveFile(ldstr.SfdFpath, ldstr.SetOfdRTST());
            }

            ldstr.SetDocFlags(true, false);
            this.Text = "8080 Emulator - " + trimDocName(ldstr.SfdFpath);
        }

        string trimDocName(string fname)
        {
            int i;
            string docName = "";
            for (i = (fname.Length - 1); i >= 0; --i)
            {
                if (fname[i] == '\\')
                    break;
            }
            for (++i; i < fname.Length; ++i)
            {
                docName += fname[i].ToString();
            }
            return docName;
        }

        //Invoked when user creates or open file for the first time
        void FstTouch()
        {
            panel1.Controls.RemoveAt(0);
            panel1.Controls.Add(rtbTar);
            mnSaveAs.Enabled = true;
            mnSave.Enabled = true;
            fstTouch = false;
        }

        private void mnReg_Click(object sender, EventArgs e)
        {
            if (!fReg)
            {
                this.Controls.Remove(menuStrip1);
                this.Controls.Add(RegPan);
                this.Controls.Add(menuStrip1);
                mnReg.Text = "Hide Registers";
                fReg = true;
                return;
            }

            this.Controls.Remove(RegPan);
            mnReg.Text = "View Registers";
            fReg = false;

        }

        private void XMsg_Click(object sender, EventArgs e)
        {
            panel1.Controls.Remove(PanMsg);
            rtbTar.Size = panel1.ClientSize;
            rtbTar.AutoScrollOffset = new Point(0, 0);
        }

        private void mnExec_Click(object sender, EventArgs e)
        {
            th = new Thread(new ThreadStart(engine.Execute));
            panel1.Controls.Add(PanMsg);
            rtbTar.Size = panel1.ClientSize - PanMsg.Size;
            rtbTar.Dock = DockStyle.Top;
            //Clear Previous Error Messages
            txtMsg.Text = "";
            mnKill.Enabled = true;
            //engine.Execute();
            // engine.Execute();
            th.Start();
        }

        //Write Error Messages 
        public void WrErr(string err)
        {
            txtMsg.Text += err;
        }

        //Clear all the Error Listing
        public void ClrErr()
        {
            txtMsg.Text = "";
        }

		public void ToggleExecute(bool state)
		{
		    mnExecute.Enabled = state;
		}

		public void ToggleLoad(bool state)
		{
		    mnLoad.Enabled = state;
		}

		public void ToggleRst(bool state)
		{
		    mnRst.Enabled = state;
		}

		public void ToggleKill(bool state)
		{
		    mnKill.Enabled = state;
		}

        //Initialize Error Message Console
        void InitMsgConsole()
        {
            Label label2 = new Label();
            XMsg = new Label();
            txtMsg = new TextBox();
            PanMsg = new Panel();
            txtMsg.Multiline = true;
            txtMsg.BackColor = Color.White;
            txtMsg.ForeColor = Color.Blue;
            txtMsg.ReadOnly = true;
            txtMsg.ScrollBars = ScrollBars.Vertical;
            XMsg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            XMsg.AutoSize = true;
            XMsg.BackColor = SystemColors.ActiveCaption;
            XMsg.BorderStyle = BorderStyle.FixedSingle;
            txtMsg.Font = new Font("Lucida Console", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            XMsg.ForeColor = Color.Black;
            XMsg.Location = new Point(660, 2);
            XMsg.Size = new System.Drawing.Size(24, 24);
            XMsg.Text = "X";

            txtMsg.Dock = DockStyle.Bottom;
            txtMsg.Multiline = true;
            txtMsg.Size = new Size(679, 86);
            txtMsg.ReadOnly = true;

            label2.BackColor = SystemColors.ActiveCaption;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = Color.Black;
            label2.Size = new System.Drawing.Size(679, 20);
            label2.Text = "Messages";
            label2.TextAlign = ContentAlignment.MiddleCenter;

            PanMsg.Size = new Size(679, 100);
            PanMsg.Controls.Add(XMsg);
            PanMsg.Controls.Add(label2);
            PanMsg.Controls.Add(txtMsg);
            PanMsg.Dock = DockStyle.Bottom;

        }

        //Initialize Register Panel
        void InitRegBar()
        {
            RegPan = new FlowLayoutPanel();
            Label label0, label1, label4, label6, label8, label10, label12, label13, label14, label15, label16, label17;

            label0 = new Label();
            label1 = new Label();
            label4 = new Label();
            label6 = new Label();
            label8 = new Label();
            label10 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();

            RegA = new Label();
            RegB = new Label();
            RegC = new Label();
            RegD = new Label();
            RegE = new Label();
            RegH = new Label();
            RegL = new Label();
            Z = new Label();
            A = new Label();
            P = new Label();
            C = new Label();
            S = new Label();

            label1.AutoSize = true;
            label1.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Size = new System.Drawing.Size(16, 16);
            label1.Text = "A";

            RegA.AutoSize = true;
            RegA.BackColor = SystemColors.Control;
            RegA.BorderStyle = BorderStyle.FixedSingle;
            RegA.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegA.ForeColor = SystemColors.ActiveCaptionText;
            RegA.Name = "RegA";
            RegA.Text = "00000000";

            label0.AutoSize = true;
            label0.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label0.ForeColor = SystemColors.ActiveCaptionText;

            label0.Text = "B";

            RegB.AutoSize = true;
            RegB.BackColor = SystemColors.Control;
            RegB.BorderStyle = BorderStyle.FixedSingle;
            RegB.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegB.ForeColor = SystemColors.ActiveCaptionText;
            RegB.Text = "00000000";

            label4.AutoSize = true;
            label4.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Text = "C";

            RegC.AutoSize = true;
            RegC.BackColor = SystemColors.Control;
            RegC.BorderStyle = BorderStyle.FixedSingle;
            RegC.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegC.ForeColor = SystemColors.ActiveCaptionText;
            RegC.Text = "00000000";

            label6.AutoSize = true;
            label6.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Text = "D";

            RegD.AutoSize = true;
            RegD.BackColor = SystemColors.Control;
            RegD.BorderStyle = BorderStyle.FixedSingle;
            RegD.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegD.ForeColor = SystemColors.ActiveCaptionText;
            RegD.Text = "00000000";

            label8.AutoSize = true;
            label8.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Text = "E";

            RegE.AutoSize = true;
            RegE.BackColor = SystemColors.Control;
            RegE.BorderStyle = BorderStyle.FixedSingle;
            RegE.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegE.ForeColor = SystemColors.ActiveCaptionText;
            RegE.Text = "00000000";

            label10.AutoSize = true;
            label10.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label10.ForeColor = SystemColors.ActiveCaptionText;
            label10.Text = "H";

            RegH.AutoSize = true;
            RegH.BackColor = SystemColors.Control;
            RegH.BorderStyle = BorderStyle.FixedSingle;
            RegH.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegH.ForeColor = SystemColors.ActiveCaptionText;
            RegH.Text = "00000000";

            label12.AutoSize = true;
            label12.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label12.ForeColor = SystemColors.ActiveCaptionText;
            label12.Text = "L";

            RegL.AutoSize = true;
            RegL.BackColor = SystemColors.Control;
            RegL.BorderStyle = BorderStyle.FixedSingle;
            RegL.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            RegL.ForeColor = SystemColors.ActiveCaptionText;
            RegL.Text = "00000000";

            label13.AutoSize = true;
            label13.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label13.ForeColor = SystemColors.ActiveCaptionText;
            label13.Text = "Z";

            Z.AutoSize = true;
            Z.BackColor = SystemColors.Control;
            Z.BorderStyle = BorderStyle.FixedSingle;
            Z.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            Z.ForeColor = SystemColors.ActiveCaptionText;
            Z.Text = "0";

            label14.AutoSize = true;
            label14.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label14.ForeColor = SystemColors.ActiveCaptionText;
            label14.Text = "S";

            S.AutoSize = true;
            S.BackColor = SystemColors.Control;
            S.BorderStyle = BorderStyle.FixedSingle;
            S.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            S.ForeColor = SystemColors.ActiveCaptionText;
            S.Text = "0";

            label15.AutoSize = true;
            label15.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label15.ForeColor = SystemColors.ActiveCaptionText;
            label15.Text = "A";

            A.AutoSize = true;
            A.BackColor = SystemColors.Control;
            A.BorderStyle = BorderStyle.FixedSingle;
            A.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            A.ForeColor = SystemColors.ActiveCaptionText;
            A.Text = "0";

            label16.AutoSize = true;
            label16.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label16.ForeColor = SystemColors.ActiveCaptionText;
            label16.Text = "P";

            P.AutoSize = true;
            P.BackColor = SystemColors.Control;
            P.BorderStyle = BorderStyle.FixedSingle;
            P.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            P.ForeColor = SystemColors.ActiveCaptionText;
            P.Text = "0";

            label17.AutoSize = true;
            label17.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label17.ForeColor = SystemColors.ActiveCaptionText;
            label17.Text = "C";

            C.AutoSize = true;
            C.BackColor = SystemColors.Control;
            C.BorderStyle = BorderStyle.FixedSingle;
            C.Font = new Font("Miramonte", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            C.ForeColor = SystemColors.ActiveCaptionText;
            C.Text = "0";

            RegPan.Controls.Add(label1);
            RegPan.Controls.Add(RegA);
            RegPan.Controls.Add(label0);
            RegPan.Controls.Add(RegB);
            RegPan.Controls.Add(label4);
            RegPan.Controls.Add(RegC);
            RegPan.Controls.Add(label6);
            RegPan.Controls.Add(RegD);
            RegPan.Controls.Add(label8);
            RegPan.Controls.Add(RegE);
            RegPan.Controls.Add(label10);
            RegPan.Controls.Add(RegH);
            RegPan.Controls.Add(label12);
            RegPan.Controls.Add(RegL);
            RegPan.Controls.Add(label13);
            RegPan.Controls.Add(Z);
            RegPan.Controls.Add(label14);
            RegPan.Controls.Add(S);
            RegPan.Controls.Add(label15);
            RegPan.Controls.Add(A);
            RegPan.Controls.Add(label16);
            RegPan.Controls.Add(P);
            RegPan.Controls.Add(label17);
            RegPan.Controls.Add(C);
            RegPan.Dock = DockStyle.Top;
            RegPan.Size = new System.Drawing.Size(679, 21);
        }

        //Extract Registers
        public Label XtrctReg(string reg)
        {
            switch (reg)
            {
                case "a":
                    return RegA;
                case "b":
                    return RegB;
                case "c":
                    return RegC;
                case "d":
                    return RegD;
                case "e":
                    return RegE;
                case "h":
                    return RegH;
                case "l":
                    return RegL;
                case "s":
                    return S;
                case "z":
                    return Z;
                case "cc":
                    return C;
                case "p":
                    return P;
                case "aa":
                    return A;
            }
            return null;
        }

        //Write the Register value from source register
        public void LoadReg(Label Dest, Label Src)
        {
            Dest.Text = Src.Text;
        }
        public void LoadRegImmediate(Label Dest, string val)
        {
            Dest.Text = val;
        }
        public void LoadRegImmediate(string reg, string val)
        {
            XtrctReg(reg).Text = val;
        }
        public string GetMem(int address)
        {
            return GridMem.Rows[address].Cells[1].Value.ToString();
        }
        public void AddReg(string sr)
        {
            Label Src;
            Src = XtrctReg(sr);
            RegA.Text = (int.Parse(RegA.Text) + int.Parse(Src.Text)).ToString();
        }
        public void AddRegImmediate(string sr)
        {
            RegA.Text = (int.Parse(RegA.Text) + int.Parse(sr)).ToString();
        }
        void InitGridMem()
        {
            GridMem = new DataGridView();
            DataGridViewTextBoxColumn Location = new DataGridViewTextBoxColumn(); ;
            DataGridViewTextBoxColumn Data = new DataGridViewTextBoxColumn();
            GridMem.ForeColor = Color.Blue;
            Data.Frozen = true;
            Data.HeaderText = "Data";
            Data.ReadOnly = true;
            Location.Frozen = true;
            Location.HeaderText = "Address";
            Location.ReadOnly = true;
            GridMem.AllowUserToAddRows = false;
            GridMem.AllowUserToDeleteRows = false;
            GridMem.AllowUserToOrderColumns = true;
            GridMem.BorderStyle = BorderStyle.Fixed3D;
            GridMem.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            GridMem.Columns.AddRange(new DataGridViewColumn[] {
            Location,
            Data});
            GridMem.Columns[0].Width = 50;
            GridMem.Dock = DockStyle.Right;
            GridMem.ForeColor = Color.Black;
            GridMem.BackgroundColor = Color.White;
            GridMem.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            GridMem.RowHeadersVisible = false;
            GridMem.Rows.Add(65536);
        }

        private void mnMem_Click(object sender, EventArgs e)
        {
            if (!fMem)
            {
                GridMem.Size = new Size(125, panel1.Height);
                panel1.Controls.Add(GridMem);
                mnMenu.Text = "Hide Memory";
                fMem = true;
                return;
            }
            panel1.Controls.Remove(GridMem);
            mnMenu.Text = "View Memory";
            fMem = false;

        }
        void InitMem()
        {
            for (int i = 0; i <= 65535; ++i)
            {
                GridMem.Rows[i].Cells[0].Value = engine.ConvDeciToHex(i);
                GridMem.Rows[i].Cells[1].Value = "00000000";
            }
        }
        public void FillMem(string data, int loc)
        {
            GridMem.Rows[loc].Cells[1].Value = data;
            memInd.Add(loc);
        }
        private void mnLoad_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(PanMsg);
            rtbTar.Size = panel1.ClientSize - PanMsg.Size;
            rtbTar.Dock = DockStyle.Top;
            //Clear Previous Error Messages
            txtMsg.Text = "";
            if (engine.LoadInst())
            {
                mnExecute.Enabled = true;
            }
        }

        private void mnKill_Click(object sender, EventArgs e)
        {
            th.Abort();
            txtMsg.Text = "\r\nAbnormal Execution Termination";
            mnLoad.Enabled = true;
            mnKill.Enabled = false;
            mnRst.Enabled = true;
        }

        private void mnStepXect_Click(object sender, EventArgs e)
        {
            if (th == null)
            {
                th = new Thread(new ThreadStart(engine.Execute));
                th.Start();
            }
            //th.Resume();
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DiaResult() == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            if (th!=null && th.ThreadState.ToString() == "Running")
                th.Abort();
        }
        public void SetSign()
        {
            S.Text = "1";
        }
        public void RstSign()
        {
            S.Text = "0";
        }
        public void SetZero()
        {
            Z.Text = "1";
        }
        public void RstZero()
        {
            Z.Text = "0";
        }
        public void SetParity()
        {
            P.Text = "1";
        }
        public void RstParity()
        {
            P.Text = "0";
        }
        public void SetCarry()
        {
            C.Text = "1";
        }
        public void RstCarry()
        {
            C.Text = "0";
        }
        public void SetAllFlags()
        {
            Z.Text = "1";
            C.Text = "1";
            P.Text = "1";
            S.Text = "1";
        }

        public void RstAllFlags()
        {
            Z.Text = "0";
            C.Text = "0";
            P.Text = "0";
            S.Text = "0";
        }

        private void mnRst_Click(object sender, EventArgs e)
        {
            foreach (int i in memInd)
                GridMem.Rows[i].Cells[1].Value = "00000000";
            RstAllFlags();
            RegA.Text = RegB.Text = RegC.Text = RegD.Text = RegE.Text = RegH.Text = RegL.Text = "00000000";
        }

        private void mnFont_Click(object sender, EventArgs e)
        {
            rtbTar.InitFont();
        }
    }
}