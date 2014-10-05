using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Emu
{
    
    class LdStr
    {
        SaveFileDialog sfd;
        OpenFileDialog ofd;
        RichTextBox rtbTar;

        static internal bool DocSaved = true;
        static internal bool DocTyped = false;

        public string OfdFpath
        {
            set
            {
                ofd.FileName = value;
            }
            get
            {
                return ofd.FileName;
            }
        }
        public string SfdFpath
        {
            set
            {
                sfd.FileName = value;
            }
            get
            {
                return sfd.FileName;
            }
        }

        public LdStr(RichTextBox rtbTar)
        {
            sfd = new SaveFileDialog();
            this.sfd.DefaultExt = "rtf";
            this.sfd.FileName = "Noname";
            this.sfd.Filter = "Rtf|*.rtf|Txt|*.txt|All Files|*.*";
            this.sfd.RestoreDirectory = true;
            ofd = new OpenFileDialog();
            this.ofd.DefaultExt = "rtf";
            this.ofd.FileName = "openFileDialog1";
            this.ofd.Filter = "Rtf|*.rtf|Txt|*.txt|All Files|*.*";

            this.rtbTar = rtbTar;
        }

        public DialogResult ShowSaveFD()
        {
            return sfd.ShowDialog();
        }

        public DialogResult ShowOpenFD()
        {
            return ofd.ShowDialog();
        }

        public RichTextBoxStreamType SetSfdRTST()
        {
            if (sfd.FilterIndex == 2)
                return RichTextBoxStreamType.PlainText;
            return RichTextBoxStreamType.RichText;
        }

        public RichTextBoxStreamType SetOfdRTST()
        {
            if (ofd.FilterIndex == 2)
                return RichTextBoxStreamType.PlainText;
            return RichTextBoxStreamType.RichText;
        }

        public void SetDocFlags(bool docSvd, bool docTpd)
        {
            DocSaved = docSvd;
            DocTyped = docTpd;
        }
    }
}
