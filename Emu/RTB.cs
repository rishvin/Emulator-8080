using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace Emu
{
    class RTB : RichTextBox
    {
        GUI Gui;
        ContextMenuStrip CMenu;
        int curPos;

        public int CurPos
        {
            set
            {
                curPos = value;
            }
        }

        public RTB(GUI Gui)
        {
            InitRTB();
            InitCMenu();
            Font = new Font("Adobe Arabic", 10f);
            ForeColor = Color.Blue;
            this.Gui = Gui;
        }

        //Initialize the RTB 
        public void InitRTB()
        {
            this.MouseDown += new MouseEventHandler(RTB_MouseDown);
            this.KeyDown += new KeyEventHandler(RTB_KeyDown);
            this.SelectionChanged += new EventHandler(RTB_SelectionChanged);
            this.Dock = DockStyle.Fill;

        }

        void RTB_KeyDown(object sender, KeyEventArgs e)
        {
            int len=Lines.Length;
            Gui.txtLine.Text = len > 0 ? len.ToString() : "1";
            LdStr.DocTyped = true;
        }

        void RTB_SelectionChanged(object sender, EventArgs e)
        {
            int line, col, index;
            index = SelectionStart;
            line = GetLineFromCharIndex(index);
            Point pt = GetPositionFromCharIndex(index);
            pt.X = 0;
            col = index - GetCharIndexFromPosition(pt);
            Gui.txtCurLine.Text = (++line).ToString();
            
        }
        
        void CMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.ToString())
            {
                case "Cut":
                    Cut();
                    LdStr.DocTyped = true;
                    break;
                case "Copy":
                    Copy();
                    break;
                case "Paste":
                    Paste();
                    LdStr.DocTyped = true;
                    break;
                case "Select All":
                    SelectAll();
                    break;
                case "Font":
                    InitFont();
                    break;
            }

        }

        void RTB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                CMenu.Show();
        }
        void InitCMenu()
        {
            ToolStripMenuItem ciCut, ciCopy, ciPaste, ciFont, ciSelAll;
            ToolStripSeparator ciSep;

            CMenu = new ContextMenuStrip();
            ciCut = new ToolStripMenuItem();
            ciCopy = new ToolStripMenuItem();
            ciPaste = new ToolStripMenuItem();
            ciSep = new ToolStripSeparator();
            ciFont = new ToolStripMenuItem();
            ciSelAll = new ToolStripMenuItem();

            CMenu.Items.AddRange(new ToolStripItem[] {
            ciCut,
            ciCopy,
            ciPaste,
            ciSelAll,
            ciSep,
            ciFont});

            CMenu.Name = "CMenu";
            CMenu.Size = new Size(153, 120);
            ciSep.Name = "toolStripSeparator1";
            ciSep.Size = new Size(149, 6);

            ciCut.Name = "ciCut";
            ciCut.Size = new Size(152, 22);
            ciCut.Text = "Cut";

            ciCopy.Name = "ciCopy";
            ciCopy.Size = new Size(152, 22);
            ciCopy.Text = "Copy";

            ciPaste.Name = "ciPaste";
            ciPaste.Size = new Size(152, 22);
            ciPaste.Text = "Paste";

            ciSelAll.Name = "ciSelAll";
            ciPaste.Size = new Size(152, 22);
            ciSelAll.Text = "Select All";

            ciFont.Name = "ciFont";
            ciFont.Size = new Size(152, 22);
            ciFont.Text = "Font";

            this.CMenu.ItemClicked += new ToolStripItemClickedEventHandler(CMenu_ItemClicked);
        }

        public void InitFont()
        {
            FontDialog fd = new FontDialog();
            fd.ShowEffects = true;
            fd.AllowSimulations = true;
            fd.ShowDialog();
            Font = fd.Font;
        }
    }
}

