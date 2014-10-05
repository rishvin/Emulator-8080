using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace Emu
{
    class Engine
    {
        RTB rtb;
        GUI gui;
        //Program Counter
        int pc;
        Stack stk;
        Hashtable HT11, HT10, HT00, Mapper, Label, ResLabel;
        delegate void ExecFn(string bincode);
        delegate bool MapFn(string txt, int ln);
        public Engine(RTB rtb, GUI gui)
        {
            this.rtb = rtb;
            this.gui = gui;
            stk = new Stack();
            HT11 = new Hashtable();
            HT10 = new Hashtable();
            HT00 = new Hashtable();
            Mapper = new Hashtable();
            Label = new Hashtable();
            InitHT11();
            InitHT10();
            InitHT00();
            InitMapper();
            ResLabel = new Hashtable();
        }

        void InitHT11()
        {
            HT11.Add("11000011", new ExecFn(JmpFn));
            HT11.Add("11010010", new ExecFn(JncFn));
            HT11.Add("11011010", new ExecFn(JcFn));
            HT11.Add("11000010", new ExecFn(JnzFn));
            HT11.Add("11001010", new ExecFn(JzFn));
            HT11.Add("11101010", new ExecFn(JpeFn));
            HT11.Add("11100010", new ExecFn(JpoFn));
            HT11.Add("11110010", new ExecFn(JpFn));
            HT11.Add("11111010", new ExecFn(JmFn));
            HT11.Add("11111110", new ExecFn(CpiFn));
            HT11.Add("11001101", new ExecFn(CallFn));
            HT11.Add("11001001", new ExecFn(RetFn));
            HT11.Add("11000110", new ExecFn(AdiFn));
            HT11.Add("11011110", new ExecFn(SbiFn));
            HT11.Add("11000101", new ExecFn(PushFn));
            HT11.Add("11010101", new ExecFn(PushFn));
            HT11.Add("11100101", new ExecFn(PushFn));
            HT11.Add("11110101", new ExecFn(PushFn));
            HT11.Add("11000001", new ExecFn(PopFn));
            HT11.Add("11010001", new ExecFn(PopFn));
            HT11.Add("11100001", new ExecFn(PopFn));
            HT11.Add("11110001", new ExecFn(PopFn));
        }
        void InitHT10()
        {
            HT10.Add("10000000", new ExecFn(AddFn));
            HT10.Add("10000001", new ExecFn(AddFn));
            HT10.Add("10000010", new ExecFn(AddFn));
            HT10.Add("10000011", new ExecFn(AddFn));
            HT10.Add("10000100", new ExecFn(AddFn));
            HT10.Add("10000101", new ExecFn(AddFn));
            HT10.Add("10000110", new ExecFn(AddFn));
            HT10.Add("10010000", new ExecFn(SubFn));
            HT10.Add("10010001", new ExecFn(SubFn));
            HT10.Add("10010010", new ExecFn(SubFn));
            HT10.Add("10010011", new ExecFn(SubFn));
            HT10.Add("10010100", new ExecFn(SubFn));
            HT10.Add("10010101", new ExecFn(SubFn));
            HT10.Add("10010110", new ExecFn(SubFn));
            HT10.Add("10111000", new ExecFn(CmpFn));
            HT10.Add("10111001", new ExecFn(CmpFn));
            HT10.Add("10111010", new ExecFn(CmpFn));
            HT10.Add("10111011", new ExecFn(CmpFn));
            HT10.Add("10111100", new ExecFn(CmpFn));
            HT10.Add("10111101", new ExecFn(CmpFn));
            HT10.Add("10111110", new ExecFn(CmpFn));
        }

        void InitHT00()
        {
            HT00.Add("00000110", new ExecFn(MviFn));
            HT00.Add("00001110", new ExecFn(MviFn));
            HT00.Add("00010110", new ExecFn(MviFn));
            HT00.Add("00011110", new ExecFn(MviFn));
            HT00.Add("00100110", new ExecFn(MviFn));
            HT00.Add("00101110", new ExecFn(MviFn));
            HT00.Add("00110110", new ExecFn(MviFn));
            HT00.Add("00111110", new ExecFn(MviFn));

            HT00.Add("00000100", new ExecFn(InrFn));
            HT00.Add("00001100", new ExecFn(InrFn));
            HT00.Add("00010100", new ExecFn(InrFn));
            HT00.Add("00011100", new ExecFn(InrFn));
            HT00.Add("00100100", new ExecFn(InrFn));
            HT00.Add("00101100", new ExecFn(InrFn));
            HT00.Add("00110100", new ExecFn(InrFn));
            HT00.Add("00111100", new ExecFn(InrFn));

            HT00.Add("00000101", new ExecFn(DcrFn));
            HT00.Add("00001101", new ExecFn(DcrFn));
            HT00.Add("00010101", new ExecFn(DcrFn));
            HT00.Add("00011101", new ExecFn(DcrFn));
            HT00.Add("00100101", new ExecFn(DcrFn));
            HT00.Add("00101101", new ExecFn(DcrFn));
            HT00.Add("00110101", new ExecFn(DcrFn));
            HT00.Add("00111101", new ExecFn(DcrFn));

        }

        void InitMapper()
        {
            Mapper.Add(0, new MapFn(MoveMapper));
            Mapper.Add(1, new MapFn(MviMapper));
            Mapper.Add(2, new MapFn(AddMapper));
            Mapper.Add(3, new MapFn(AdiMapper));
            Mapper.Add(4, new MapFn(InrMapper));
            Mapper.Add(5, new MapFn(CmpMapper));
            Mapper.Add(6, new MapFn(CpiMapper));
            Mapper.Add(8, new MapFn(HaltMapper));
            Mapper.Add(9, new MapFn(DcrMapper));
            Mapper.Add(10, new MapFn(JmpMapper));
            Mapper.Add(11, new MapFn(JncMapper));
            Mapper.Add(12, new MapFn(JnzMapper));
            Mapper.Add(13, new MapFn(JzMapper));
            Mapper.Add(14, new MapFn(JpMapper));
            Mapper.Add(15, new MapFn(JmMapper));
            Mapper.Add(16, new MapFn(JpeMapper));
            Mapper.Add(17, new MapFn(JpoMapper));
            Mapper.Add(18, new MapFn(SubMapper));
            Mapper.Add(19, new MapFn(SbiMapper));
            Mapper.Add(20, new MapFn(PushMapper));
            Mapper.Add(21, new MapFn(PopMapper));
            Mapper.Add(22, new MapFn(CallMapper));
            Mapper.Add(23, new MapFn(RetMapper));
        }
        ExecFn Op11(string bincode)
        {
            return (ExecFn)HT11[bincode];
        }

        ExecFn Op10(string bincode)
        {
            return (ExecFn)HT10[bincode];
        }
        ExecFn Op00(string bincode)
        {
            return (ExecFn)HT00[bincode];
        }
        MapFn MpFn(int index)
        {
            return (MapFn)Mapper[index];
        }
        //*******************************************LOADER SECTION******************************

        public bool LoadInst()
        {
            bool noerr = true;
            string txt = "";
            pc = -1;
            int i, index;
            Match m1, m2;
            Label.Clear();
            ResLabel.Clear();
            if (rtb.Lines.Length <= 0)
            {
                gui.WrErr("\r\nError : Nothing to load");
                noerr = false;
            }
            for (i = 0; i < rtb.Lines.Length; ++i)
            {
                txt = rtb.Lines[i];
                
                //---------------LABEL SECTION-----------------
                if ((m1 = Regex.Match(txt, @"\s*\b\w+\s*:\s*\b")).Success)
                {
                    bool f = true;
                    m2 = Regex.Match(txt, @"\w+");
                    foreach (string l in Label.Keys)
                    {
                        if (l == m2.ToString())
                        {
                            gui.WrErr("\r\nError : Label " + m2.ToString() + " already exists ; Line = " + (i + 1).ToString());
                            noerr = false;
                            f = false;
                            break;
                        }
                    }
                    if (f)
                    {
                        Label.Add(m2.ToString(), pc + 1);
                    }
                    txt = txt.Substring(m1.Length);
                }
                //-------------------------------------------

                index = CmpIns(txt);
                if (index == 256)
                    noerr &= true;
                else
                    if (index >= 0)
                        noerr &= (bool)MpFn(index)(txt, i + 1);
                    else
                    {
                        gui.WrErr("\r\nError : No such Instruction exists = " + (i + 1).ToString());
                        noerr = false;
                    }
            }
            string addr = "";
            foreach (string ky in ResLabel.Keys)
            {

                if (Label.ContainsKey(ky))
                {
                    addr = ConvDeciToBin(Label[ky].ToString());
                    addr = addr.PadLeft(16, '0');
                    gui.FillMem(addr.Substring(8), (int)ResLabel[ky]);
                    gui.FillMem(addr.Substring(0, 8), (int)ResLabel[ky] + 1);
                    noerr &= true;
                }
                else
                {
                    gui.WrErr("\r\nError : Unable to resolve Label " + ky);
                    noerr = false;
                }
            }
            if (!noerr)
            {
                gui.WrErr("\r\nLoading Unsuccessfull");
                return false;
            }
            gui.WrErr("\r\nLoading Successfull");
            return true;
        }

        int CmpIns(string txt)
        {

            if (Regex.Match(txt, @"^\s*\bmov\b").Success)
            {
                return 0;
            }
            if (Regex.Match(txt, @"^\s*\bmvi\b").Success)
            {

                return 1;
            }
            if (Regex.Match(txt, @"^\s*\badd\b").Success)
            {

                return 2;
            }
            if (Regex.Match(txt, @"^\s*\badi\b").Success)
            {

                return 3;
            }
            if (Regex.Match(txt, @"^\s*\binr\b").Success)
            {

                return 4;
            }
            if (Regex.Match(txt, @"^\s*\bcmp\b").Success)
            {

                return 5;
            }
            if (Regex.Match(txt, @"^\s*\bcpi\b").Success)
            {

                return 6;
            }
            if (Regex.Match(txt, @"^\s*\bcpi\b").Success)
            {

                return 7;
            }
            if (Regex.Match(txt, @"^\s*\bhlt\b").Success)
            {

                return 8;

            }
            if (Regex.Match(txt, @"^\s*\bdcr\b").Success)
            {

                return 9;

            }
            if (Regex.Match(txt, @"^\s*\bjmp\b").Success)
            {

                return 10;

            }
            if (Regex.Match(txt, @"^\s*\bjnc\b").Success)
            {

                return 11;

            }
            if (Regex.Match(txt, @"^\s*\bjnz\b").Success)
            {

                return 12;

            }
            if (Regex.Match(txt, @"^\s*\bjz\b").Success)
            {

                return 13;

            }
            if (Regex.Match(txt, @"^\s*\bjp\b").Success)
            {

                return 14;

            }
            if (Regex.Match(txt, @"^\s*\bjm\b").Success)
            {

                return 15;

            }
            if (Regex.Match(txt, @"^\s*\bjpe\b").Success)
            {

                return 16;

            }
            if (Regex.Match(txt, @"^\s*\bjpo\b").Success)
            {

                return 17;

            }
            if (Regex.Match(txt, @"^\s*\bsub\b").Success)
            {

                return 18;

            }
            if (Regex.Match(txt, @"^\s*\bsbi\b").Success)
            {

                return 19;

            }
            if (Regex.Match(txt, @"^\s*\bpush\b").Success)
            {

                return 20;

            }
            if (Regex.Match(txt, @"^\s*\bpop\b").Success)
            {

                return 21;

            }
            if (Regex.Match(txt, @"^\s*\bcall\b").Success)
            {

                return 22;

            }
            if (Regex.Match(txt, @"^\s*\bret\b").Success)
            {

                return 23;

            }
            //Used in case user has left blank lines
            if (Regex.Match(txt, @"^\s*$").Success)
            {
                return 256;
            }
            return -1;
        }

        int GetLabelAdd(string lab)
        {
            foreach (string i in Label.Keys)
            {
                if (i == lab)
                    return (int)Label[i];
            }
            return -1;
        }

        //************************************EXECUTION SECTION********************************************
        public void Execute()
        {
            string bincode = "";
            //Disable Execute and Load Control till execution does not succeed
            gui.ToggleExecute(false);
			gui.ToggleLoad(false);
			gui.ToggleRst(false);

            try
            {
                for (pc = 0; pc <= 65535; )
                {
                    if ((bincode = gui.GetMem(pc)) == "01110110")
                        break;

                    switch (bincode.Substring(0, 2))
                    {
                        case "01":
                            MoveFn(bincode);
                            break;
                        case "00":
                            Op00(bincode)(bincode);
                            break;
                        case "10":
                            Op10(bincode)(bincode);
                            break;
                        case "11":
                            Op11(bincode)(bincode);
                            break;
                        default: ++pc;
                            break;

                    }
                }
            }
            catch (Exception e)
            {
                //gui.WrErr("\r\nIllegal Instruction " + bincode + ". Terminating Execution" + e.ToString());
            }

            finally
            {
                //Now enable the load control
                gui.ToggleLoad(true);
                gui.ToggleKill(false);
                gui.ToggleRst(true);
            }
        }

        //*****************************************INSTRUCTION SECTION**************************************

        bool HaltMapper(string txt, int ln)
        {
            gui.FillMem("01110110", ++pc);
            return true;
        }

        //-------------------------------MOV SECTION-------------------------------------

        bool MoveMapper(string txt, int ln)
        {
            string mcode = "01";
            if (Regex.Match(txt, @"\s*(\bmov\b)\s+(\b[a-ehl]\b)(\b\s*,\s*\b)(\b[a-ehl]\b)\s*$").Success)
            {
                string src, dest;
                dest = Regex.Match(txt, @"\b[a-ehl]\b").ToString();
                src = Regex.Match(txt, @"\b[a-ehl]\b\s*$").ToString();
                if (dest == src)
                {

                    gui.WrErr("\r\nSource and Destination cannot be same ; Line= " + ln.ToString());
                    return false;
                }
                mcode += GenRegCode(dest) + GenRegCode(src);
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bmov\b\s+(\bm\s*,\s*\b)(\b[a-ehl]\b)\s*$").Success)
            {
                string src;
                src = Regex.Match(txt, @"\b[a-ehl]\b").ToString();
                mcode += "110" + GenRegCode(src);
                gui.FillMem(mcode, ++pc);
                return true;
            }

            if (Regex.Match(txt, @"\s*\bmov\b\s+(\b[a-ehl]\b)(\b\s*,\s*m\b)\s*$").Success)
            {
                string dest;
                dest = Regex.Match(txt, @"\b[a-ehl]\b").ToString();
                mcode += GenRegCode(dest) + "110";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void MoveFn(string mcode)
        {
            pc++;
            Label Dest, Src;
            Dest = gui.XtrctReg(GetRegName(mcode.Substring(2, 3)));
            Src = gui.XtrctReg(GetRegName(mcode.Substring(5, 3)));
            if (Dest == null)
            {
                int add = ConvBinToDeci(gui.XtrctReg("h").Text + gui.XtrctReg("l").Text);
                gui.FillMem(Src.Text, add);
                return;
            }
            if (Src == null)
            {
                int add = ConvBinToDeci(gui.XtrctReg("h").Text + gui.XtrctReg("l").Text);
                Dest.Text = gui.GetMem(add);
                return;
            }
            gui.LoadReg(Dest, Src);
        }

        //-----------------------------------MVI SECTION------------------
        bool MviMapper(string txt, int ln)
        {
            string mcode = "00", temp;
            if (Regex.Match(txt, @"\s*\bmvi\b\s+(\b[a-ehl]\b)(\b\s*,\s*\b)(\d+)\s*$").Success)
            {
                temp = GenRegCode(Regex.Match(txt, @"\b[a-ehl]\b").ToString());
                mcode += temp + "110";
                temp = Regex.Match(txt, @"\d+").ToString();
                if (int.Parse(temp) > 255)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                temp = AdjBin(ConvDeciToBin(temp));
                gui.FillMem(mcode, ++pc);
                gui.FillMem(temp, ++pc);
                return true;
            }

            if (Regex.Match(txt, @"\s*\bmvi\b\s+\b[a-ehl]\b(\b\s*,\s*\b)\b[a-f0-9]+hs*$").Success)
            {
                temp = Regex.Match(txt, @"\b[a-ehl]\b").ToString();
                mcode += GenRegCode(temp) + "110";
                temp = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (temp.Length > 2)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                gui.FillMem(mcode, ++pc);
                gui.FillMem(AdjBin(ConvHexToBin(temp)), ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bmvi\b\s+\b[a-ehl]\b(\b\s*,\s*\b)\b[0-1]+bs*$").Success)
            {
                temp = Regex.Match(txt, @"\b[a-ehl]\b").ToString();
                mcode += GenRegCode(temp) + "110";
                temp = Regex.Match(txt, @"\d+").ToString();
                if (temp.Length > 8)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                gui.FillMem(mcode, ++pc);
                gui.FillMem(AdjBin(temp), ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bmvi\b\s+(\b[a-ehl]\b)(\b\s*,\s*'\b)(\b.\b')\s*$").Success)
            {
                int ch;
                temp = Regex.Match(txt, @"\b[a-ehl]\b").ToString();
                mcode += GenRegCode(temp) + "110";
                ch = (int)Convert.ToChar(Regex.Match(txt, @"\b.\b", RegexOptions.RightToLeft).ToString());
                temp = AdjBin(ConvDeciToBin(ch.ToString()));
                gui.FillMem(mcode, ++pc);
                gui.FillMem(temp, ++pc);
                return true;

            }

            if (Regex.Match(txt, @"\s*\bmvi\b\s+(\bm\b)(\b\s*,\s*\b)(\d+)\s*$").Success)
            {
                mcode += "110110";
                temp = Regex.Match(txt, @"\d+").ToString();
                if (int.Parse(temp) > 255)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                temp = AdjBin(ConvDeciToBin(temp));
                gui.FillMem(mcode, ++pc);
                gui.FillMem(temp, ++pc);
                return true;
            }

            if (Regex.Match(txt, @"\s*\bmvi\b\s+\bm\b(\b\s*,\s*\b)\b[a-f0-9]+hs*$").Success)
            {
                mcode += "110110";
                temp = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (temp.Length > 2)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                gui.FillMem(mcode, ++pc);
                gui.FillMem(AdjBin(ConvHexToBin(temp)), ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bmvi\b\s+\bm\b(\b\s*,\s*\b)\b[0-1]+bs*$").Success)
            {
                mcode += "110110";
                temp = Regex.Match(txt, @"\d+").ToString();
                if (temp.Length > 8)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                gui.FillMem(mcode, ++pc);
                gui.FillMem(AdjBin(temp), ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bmvi\b\s+(\bm\b)(\b\s*,\s*'\b)(\b.\b')\s*$").Success)
            {
                int ch;
                mcode += "110110";
                ch = (int)Convert.ToChar(Regex.Match(txt, @"\b.\b", RegexOptions.RightToLeft).ToString());
                temp = AdjBin(ConvDeciToBin(ch.ToString()));
                gui.FillMem(mcode, ++pc);
                gui.FillMem(AdjBin(temp), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void MviFn(string mcode)
        {
            if (mcode.Substring(2) == "110110")
            {
                gui.FillMem(gui.GetMem(++pc), ConvBinToDeci(gui.XtrctReg("h").Text + gui.XtrctReg("l").Text));
                ++pc;
                return;
            }
            Label Dest = gui.XtrctReg(GetRegName(mcode.Substring(2, 3)));
            gui.LoadRegImmediate(Dest, gui.GetMem(++pc));
            ++pc;
        }

        //-----------------------------ADD Section-----------------
        bool AddMapper(string txt, int ln)
        {
            string mcode = "10000";
            if (Regex.Match(txt, @"\s*\badd\b\s+\b[a-ehl]\b\s*$").Success)
            {
                mcode += GenRegCode(Regex.Match(txt, @"\b[a-ehl]\b").ToString());
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\badd\b\s+\bm\b\s*$").Success)
            {
                mcode += "110";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void AddFn(string mcode)
        {
            ++pc;
            int s = ConvBinToDeci(gui.XtrctReg(GetRegName(mcode.Substring(5, 3))).Text);
            s += ConvBinToDeci(gui.XtrctReg("a").Text);
            if (s == 0)
                gui.SetZero();
            else
                gui.RstZero();

            if (s > 255)
            {
                gui.SetCarry();
            }

            string rval = gui.XtrctReg("a").Text;
            if (rval[0] == '1')
                gui.SetSign();
            else
                gui.RstSign();

            if (CountParity(rval))
                gui.SetParity();
            else
                gui.RstParity();
            gui.LoadRegImmediate("a", AdjBin(ConvDeciToBin(s.ToString())));
        }

        //--------------------------ADI Section ---------------------
        bool AdiMapper(string txt, int ln)
        {
            string temp = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\badi\b\s+\d+\s*$").Success)
            {
                temp = ConvDeciToBin(Regex.Match(txt, @"\d+").ToString());
                f = true;
            }
            else
                if (Regex.Match(txt, @"\s*\badi\b\s+\b[a-f0-9]+hs*$").Success)
                {
                    temp = ConvHexToBin(Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString());
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\badi\b\s+\b[0-1]+bs*$").Success)
                    {
                        f = true;
                        temp = Regex.Match(txt, @"\d+").ToString();
                    }
            if (f)
            {

                if (temp.Length > 8)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                gui.FillMem("11000110", ++pc);
                gui.FillMem(AdjBin(temp), ++pc);
                return true;
            }

            return false;
        }

        void AdiFn(string mcode)
        {
            ++pc;
            int s = ConvBinToDeci(gui.XtrctReg("a").Text) + ConvBinToDeci(gui.GetMem(pc));
            if (s > 255)
            {
                gui.SetCarry();
            }
            else
                gui.RstCarry();
            string rval = AdjBin(ConvDeciToBin(s.ToString()));
            if (rval == "00000000")
                gui.SetZero();
            else
                gui.RstZero();
            if (rval[0] == '1')
                gui.SetSign();
            else
                gui.RstSign();
            if (CountParity(rval))
                gui.SetParity();
            else
                gui.RstParity();
            gui.LoadRegImmediate("a", rval);

            ++pc;
        }

        //-----------------------------SUB Section-----------------
        bool SubMapper(string txt, int ln)
        {
            string mcode = "10010";
            if (Regex.Match(txt, @"\s*\bsub\b\s+\b[a-ehl]\b\s*$").Success)
            {
                mcode += GenRegCode(Regex.Match(txt, @"\b[a-ehl]\b").ToString());
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bsub\b\s+\bm\b\s*$").Success)
            {
                mcode += "110";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void SubFn(string mcode)
        {
            ++pc;
            int s = ConvBinToDeci(gui.XtrctReg(GetRegName(mcode.Substring(5, 3))).Text);
            s -= ConvBinToDeci(gui.XtrctReg("a").Text);
            if (s < 0)
                s = 256 + s;
            if (s > 255)
                gui.SetCarry();
            else
                gui.RstCarry();
            string rval = AdjBin(ConvDeciToBin(s.ToString()));
            if (rval == "00000000")
                gui.SetZero();
            else
                gui.RstZero();
            if (rval[0] == '1')
                gui.SetSign();
            else
                gui.RstSign();

            if (CountParity(rval))
                gui.SetParity();
            else
                gui.RstParity();
            gui.LoadRegImmediate("a", rval);
        }

        //--------------------------ADI Section ---------------------
        bool SbiMapper(string txt, int ln)
        {
            string temp = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bsbi\b\s+\d+\s*$").Success)
            {
                temp = ConvDeciToBin(Regex.Match(txt, @"\d+").ToString());
                f = true;
            }
            else
                if (Regex.Match(txt, @"\s*\bsbi\b\s+\b[a-f0-9]+hs*$").Success)
                {
                    temp = ConvHexToBin(Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString());
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bsbi\b\s+\b[0-1]+bs*$").Success)
                    {
                        f = true;
                        temp = Regex.Match(txt, @"\d+").ToString();
                    }
            if (f)
            {

                if (temp.Length > 8)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                gui.FillMem("11011110", ++pc);
                gui.FillMem(AdjBin(temp), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void SbiFn(string mcode)
        {
            ++pc;
            int s = ConvBinToDeci(gui.XtrctReg("a").Text) - ConvBinToDeci(gui.GetMem(pc));
            if (s < 0)
                s = 256 + s;
            if (s > 255)
                gui.SetCarry();
            else
                gui.RstCarry();

            string rval = AdjBin(ConvDeciToBin(s.ToString()));
            if (rval == "00000000")
                gui.SetZero();
            else
                gui.RstZero();
            if (rval[0] == '1')
                gui.SetSign();
            else
                gui.RstSign();
            if (CountParity(rval))
                gui.SetParity();
            else
                gui.RstParity();
            gui.LoadRegImmediate("a", rval);
            ++pc;
        }
        //-------------------------------INR Section Start--------------
        bool InrMapper(string txt, int ln)
        {
            string mcode = "00";
            if (Regex.Match(txt, @"\s*\binr\b\s+\b[a-ehl]\b\s*$").Success)
            {
                mcode += GenRegCode(Regex.Match(txt, @"\b[a-ehl]\b").ToString()) + "100";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\binr\b\s+\bm\b\s*$").Success)
            {
                mcode += "110100";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void InrFn(string mcode)
        {
            int val;
            string rval = "";
            if (mcode.Substring(2, 3) == "110")
            {
                int add = ConvBinToDeci(gui.XtrctReg("h").Text + gui.XtrctReg("l").Text);
                val = ConvBinToDeci(gui.GetMem(add)) + 1;
                rval = AdjBin(ConvDeciToBin(val.ToString()));
                gui.FillMem(rval, add);
            }
            else
            {
                Label reg = gui.XtrctReg(GetRegName(mcode.Substring(2, 3)));
                val = ConvBinToDeci(reg.Text) + 1;
                rval = AdjBin(ConvDeciToBin(val.ToString()));
                gui.LoadRegImmediate(reg, rval);
            }

            if (rval == "00000000")
                gui.SetZero();
            else
                gui.RstZero();
            if (CountParity(rval))
                gui.SetParity();
            else
                gui.RstParity();
            if (rval[0] == '1')
                gui.SetSign();
            else
                gui.RstSign();
            ++pc;
        }
        //-------------------------------DCR Section Start--------------
        bool DcrMapper(string txt, int ln)
        {
            string mcode = "00";
            if (Regex.Match(txt, @"\s*\bdcr\b\s+\b[a-ehl]\b\s*$").Success)
            {
                mcode += GenRegCode(Regex.Match(txt, @"\b[a-ehl]\b").ToString()) + "101";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bdcr\b\s+\bm\b\s*$").Success)
            {
                mcode += "110101";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void DcrFn(string mcode)
        {
            int val;
            string rval;
            if (mcode.Substring(2, 3) == "110")
            {
                int add = ConvBinToDeci(gui.XtrctReg("h").Text + gui.XtrctReg("l").Text);
                val = ConvBinToDeci(gui.GetMem(add)) - 1;
                if (val < 0)
                    val = 256 + val;
                rval = AdjBin(ConvDeciToBin(val.ToString()));
                gui.FillMem(rval, add);
            }
            else
            {
                Label reg = gui.XtrctReg(GetRegName(mcode.Substring(2, 3)));
                val = ConvBinToDeci(reg.Text) - 1;
                if (val < 0)
                    val = 256 + val;
                rval = AdjBin(ConvDeciToBin(val.ToString()));
                gui.LoadRegImmediate(reg, rval);
            }

            if (rval == "00000000")
                gui.SetZero();
            else
                gui.RstZero();
            if (CountParity(rval))
                gui.SetParity();
            else
                gui.RstParity();
            if (rval[0] == '1')
                gui.SetSign();
            else
                gui.RstSign();

            ++pc;
        }
        //-----------------------------JMP SECTION--------------
        bool JmpMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjmp\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjmp\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjmp\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11000011", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }

            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11000011", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JmpFn(string mcode)
        {
            int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
            pc = address;
        }

        //-----------------------------JNC SECTION--------------
        bool JncMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjnc\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjnc\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjnc\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11010010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }
            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11010010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JncFn(string mcode)
        {
            if (gui.XtrctReg("cc").Text == "0")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }

        //-----------------------------JC SECTION--------------
        bool JcMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjc\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjc\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjc\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11011010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }

            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11011010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JcFn(string mcode)
        {
            if (gui.XtrctReg("cc").Text == "1")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }

        //-----------------------------JNZ SECTION--------------
        bool JnzMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjnz\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjnz\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjnz\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11000010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }

            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11000010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JnzFn(string mcode)
        {
            if (gui.XtrctReg("z").Text == "0")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }
        //-----------------------------JZ SECTION--------------
        bool JzMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjz\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjz\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjz\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11001010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }

            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11001010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JzFn(string mcode)
        {
            if (gui.XtrctReg("z").Text == "1")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }


        //-----------------------------JP SECTION--------------
        bool JpMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjp\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjp\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjp\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11110010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }


            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11110010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JpFn(string mcode)
        {
            if (gui.XtrctReg("s").Text == "0")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }
        //-----------------------------JM SECTION--------------
        bool JmMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjm\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjm\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjm\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11111010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }
            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11111010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JmFn(string mcode)
        {
            if (gui.XtrctReg("s").Text == "1")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }

        //-----------------------------JPE SECTION--------------
        bool JpeMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjpe\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjpe\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjpe\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11101010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }

            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11101010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JpeFn(string mcode)
        {
            if (gui.XtrctReg("p").Text == "1")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }

        //-----------------------------JPO SECTION--------------
        bool JpoMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bjpo\b\s+\b[a-f0-9]+h\s*$").Success)
            {
                add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                if (add.Length > 4)
                {
                    gui.WrErr("\r\nError : Unsupported Data Range");
                    return false;
                }
                add = ConvHexToBin(add);
                f = true;

            }
            else
                if (Regex.Match(txt, @"\s*\bjpo\b\s+\b[01]+b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[01]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 16)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bjpo\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11100010", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }
            if (f)
            {
                add = add.PadLeft(16, '0');
                gui.FillMem("11100010", ++pc);
                gui.FillMem(AdjBin(add.Substring(8)), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }
        void JpoFn(string mcode)
        {
            if (gui.XtrctReg("p").Text == "0")
            {
                int address = ConvBinToDeci(gui.GetMem(pc += 2) + gui.GetMem(--pc));
                pc = address;
                return;
            }
            pc += 3;
        }

        bool CmpMapper(string txt, int ln)
        {
            string mcode = "10111";
            if (Regex.Match(txt, @"\s*\bcmp\b\s+\b[a-ehl]\b\s*$").Success)
            {
                mcode += GenRegCode(Regex.Match(txt, @"\b[a-ehl]\b").ToString());
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bcmp\b\s+\bm\b\s*$").Success)
            {
                mcode += "110";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void CmpFn(string mcode)
        {
            int val;
            int aval = ConvBinToDeci(gui.XtrctReg("a").Text);
            if (mcode.Substring(5) == "110")
                val = ConvBinToDeci(gui.GetMem(ConvBinToDeci(gui.XtrctReg("h").Text + gui.XtrctReg("l").Text)));
            else
                val = ConvBinToDeci(gui.XtrctReg((GetRegName(mcode.Substring(5)))).Text);

            if (val < aval)
            {
                gui.RstZero();
                gui.RstCarry();
            }
            else
                if (val > aval)
                {
                    gui.RstZero();
                    gui.SetCarry();
                }
                else
                    gui.SetZero();
            ++pc;
        }

        bool CpiMapper(string txt, int ln)
        {
            bool f = false;
            string temp = "";
            if (Regex.Match(txt, @"\s*\bcpi\b\s+(\d+)\s*$").Success)
            {
                temp = Regex.Match(txt, @"\d+").ToString();
                if (int.Parse(temp) > 255)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                temp = AdjBin(ConvDeciToBin(temp));
                f = true;
            }
            else
                if (Regex.Match(txt, @"\s*\bcpi\b\s+\b[a-f0-9]+hs*$").Success)
                {
                    temp = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                    if (temp.Length > 2)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    temp = AdjBin(ConvHexToBin(temp));
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bcpi\b\s+\b[0-1]+bs*$").Success)
                    {
                        temp = Regex.Match(txt, @"\d+").ToString();
                        if (temp.Length > 8)
                        {
                            gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                            return false;
                        }
                        temp = AdjBin(temp);
                        f = true;
                    }
            if (f)
            {
                gui.FillMem("11111110", ++pc);
                gui.FillMem(temp, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void CpiFn(string mcode)
        {
            int val;
            int aval = ConvBinToDeci(gui.XtrctReg("a").Text);
            val = ConvBinToDeci(gui.GetMem(++pc));

            if (val < aval)
            {
                gui.RstZero();
                gui.RstCarry();
            }
            else
                if (val > aval)
                {
                    gui.RstZero();
                    gui.SetCarry();
                }
                else
                    gui.SetZero();
            ++pc;
        }

        bool PushMapper(string txt, int ln)
        {
            string mcode = "11";

            if (Regex.Match(txt, @"\s*\bpush\b\s+\b[bdh]\b\s*$").Success)
            {
                string rpair = Regex.Match(txt, @"\b[bdh]\b").ToString();
                if (rpair == "b")
                    mcode += "000101";
                else
                    if (rpair == "d")
                        mcode += "010101";
                    else
                        mcode += "100101";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bpush\b\s+\bpsw\b\s*$").Success)
            {
                mcode += "110101";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void PushFn(string mcode)
        {
            string val = mcode.Substring(2, 2);
            if (val == "00")
            {
                stk.Push(gui.XtrctReg("b").Text);
                stk.Push(gui.XtrctReg("c").Text);
                ++pc;
                return;
            }
            if (val == "01")
            {
                stk.Push(gui.XtrctReg("d").Text);
                stk.Push(gui.XtrctReg("e").Text);
                ++pc;
                return;
            }
            if (val == "10")
            {
                stk.Push(gui.XtrctReg("h").Text);
                stk.Push(gui.XtrctReg("l").Text);
                ++pc;
                return;
            }
            stk.Push(gui.XtrctReg("a").Text);
            val = gui.XtrctReg("s").Text + gui.XtrctReg("z").Text + "0" + gui.XtrctReg("aa").Text + "0" + gui.XtrctReg("p").Text + "1" + gui.XtrctReg("cc").Text;
            stk.Push(val);
            ++pc;
            return;
        }
        bool PopMapper(string txt, int ln)
        {
            string mcode = "11";

            if (Regex.Match(txt, @"\s*\bpop\b\s+\b[bdh]\b\s*$").Success)
            {
                string rpair = Regex.Match(txt, @"\b[bdh]\b").ToString();
                if (rpair == "b")
                    mcode += "000001";
                else
                    if (rpair == "d")
                        mcode += "010001";
                    else
                        mcode += "100001";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            if (Regex.Match(txt, @"\s*\bpop\b\s+\bpsw\b\s*$").Success)
            {
                mcode += "110001";
                gui.FillMem(mcode, ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void PopFn(string mcode)
        {
            string val = mcode.Substring(2, 2);
            try
            {

                if (val == "00")
                {
                    gui.XtrctReg("c").Text = stk.Pop().ToString();
                    gui.XtrctReg("b").Text = stk.Pop().ToString();
                    ++pc;
                    return;
                }
                if (val == "01")
                {
                    gui.XtrctReg("e").Text = stk.Pop().ToString();
                    gui.XtrctReg("d").Text = stk.Pop().ToString();
                    ++pc;
                    return;
                }
                if (val == "10")
                {
                    gui.XtrctReg("l").Text = stk.Pop().ToString();
                    gui.XtrctReg("h").Text = stk.Pop().ToString();
                    ++pc;
                    return;
                }

                val = stk.Pop().ToString();
                gui.XtrctReg("s").Text = val[0].ToString();
                gui.XtrctReg("z").Text = val[1].ToString();
                gui.XtrctReg("aa").Text = val[3].ToString();
                gui.XtrctReg("p").Text = val[5].ToString();
                gui.XtrctReg("cc").Text = val[7].ToString();
                gui.XtrctReg("a").Text = stk.Pop().ToString();
                ++pc;
                return;
            }
            catch (Exception) { ++pc; }
        }



        bool CallMapper(string txt, int ln)
        {
            string add = "";
            bool f = false;
            if (Regex.Match(txt, @"\s*\bcall\b\s+\b[01]+b\b\s*$").Success)
            {
                add = Regex.Match(txt, @"\d+").ToString();
                if (add.Length > 16)
                {
                    gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                    return false;
                }
                add = add.PadLeft(16, '0');
                f = true;
            }
            else
                if (Regex.Match(txt, @"\s*\bcall\b\s+\b[a-f0-9]+h\b\s*$").Success)
                {
                    add = Regex.Match(txt, @"\b[a-f0-9]+", RegexOptions.RightToLeft).ToString();
                    if (add.Length > 4)
                    {
                        gui.WrErr("\r\nError : Data Range Unsupported ; Line = " + ln.ToString());
                        return false;
                    }
                    add = ConvHexToBin(add);
                    add = add.PadLeft(16, '0');
                    f = true;
                }
                else
                    if (Regex.Match(txt, @"\s*\bcall\b\s+\b\w+\b\s*$").Success)
                    {
                        string lab = Regex.Match(txt, @"\w+", RegexOptions.RightToLeft).ToString();
                        add = GetLabelAdd(lab).ToString();
                        if (int.Parse(add) == -1)
                        {
                            ResLabel.Add(lab, pc + 2);
                            gui.FillMem("11001101", ++pc);
                            pc += 2;
                            return true;
                        }
                        add = ConvDeciToBin(add);
                        add = add.PadLeft(16, '0');
                        f = true;
                    }

            if (f)
            {
                gui.FillMem("11001101", ++pc);
                gui.FillMem(add.Substring(8), ++pc);
                gui.FillMem(add.Substring(0, 8), ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void CallFn(string bincode)
        {
            int add = ConvBinToDeci(gui.GetMem(pc + 2) + gui.GetMem(pc + 1));
            stk.Push(pc + 3);
            pc = add;
        }

        bool RetMapper(string txt, int ln)
        {

            if (Regex.Match(txt, @"\s*\bret\b\s*$").Success)
            {
                gui.FillMem("11001001", ++pc);
                return true;
            }
            gui.WrErr("\r\nError : Syntax ; Line = " + ln.ToString());
            return false;
        }

        void RetFn(string bincode)
        {
            pc = (int)stk.Pop();
        }

        //************************************************************************************************************************

        //***************************************REGISTER RELATED SECTION********************************************************
        string GenRegCode(string txt)
        {
            switch (txt)
            {
                case "a":
                    return "111";
                case "b":
                    return "000";
                case "c":
                    return "001";
                case "d":
                    return "010";
                case "e":
                    return "011";
                case "h":
                    return "100";
                case "l":
                    return "101";
            }
            return "";
        }

        string GetRegName(string rcode)
        {
            switch (rcode)
            {
                case "000":
                    return "b";
                case "001":
                    return "c";
                case "010":
                    return "d";
                case "011":
                    return "e";
                case "100":
                    return "h";
                case "101":
                    return "l";
                case "111":
                    return "a";
                default:
                    return "";
            }
        }
        //**************************************************************************************************************

        //********************************************CONVERTORS & RELATED SECTION**************************************
        //This function adjust the binary value to 8-bits.
        string AdjBin(string val)
        {
            if (val.Length < 8)
                return val.PadLeft(8, '0');
            return val.Substring(val.Length - 8);
        }

        bool CountParity(string val)
        {
            int c = 0;
            for (int i = 0; i < val.Length; ++i)
            {
                if (val[i] == '1')
                    c++;

            }
            return (c % 2 == 0);
        }
        int ConvBinToDeci(string val)
        {
            int deci = 0;
            int range = val.Length - 1;
            for (int i = range; i >= 0; --i)
            {
                deci += (int)(val[i] - 48) * (int)Math.Pow(2f, range - i);
            }
            return deci;
        }

        string ConvDeciToBin(string val)
        {
            int num = int.Parse(val);
            string temp = "";
            while (num > 1)
            {
                temp += (num % 2).ToString();
                num /= 2;
            }
            temp += num.ToString();
            val = "";
            for (int i = temp.Length - 1; i >= 0; --i)
                val += temp[i];
            return val;
        }

        //Mapping happens in reverse order,eg: 1->1000 instead of 0001
        string HexMapper(char ch)
        {
            string temp1 = "";
            switch (ch)
            {
                case '0':
                    temp1 = "0000";
                    break;
                case '1':
                    temp1 = "1000";
                    break;
                case '2':
                    temp1 = "0100";
                    break;
                case '3':
                    temp1 = "1100";
                    break;
                case '4':
                    temp1 = "0010";
                    break;
                case '5':
                    temp1 = "1010";
                    break;
                case '6':
                    temp1 = "0110";
                    break;
                case '7':
                    temp1 = "1110";
                    break;
                case '8':
                    temp1 = "0001";
                    break;
                case '9':
                    temp1 = "1001";
                    break;
                case 'a':
                    temp1 = "0101";
                    break;
                case 'b':
                    temp1 = "1101";
                    break;
                case 'c':
                    temp1 = "0011";
                    break;
                case 'd':
                    temp1 = "1011";
                    break;
                case 'e':
                    temp1 = "0111";
                    break;
                case 'f':
                    temp1 = "1111";
                    break;
            }
            return temp1;
        }

        string ConvHexToBin(string val)
        {

            string temp = "";
            for (int i = val.Length - 1; i >= 0; --i)
            {
                temp += HexMapper(val[i]);
            }

            val = "";
            for (int i = temp.Length - 1; i >= 0; --i)
                val += temp[i];
            return val;
        }


        public string ConvDeciToHex(int num)
        {
            int rem;
            string res = "", numb = "";
            do
            {
                rem = (num % 16);
                switch (rem)
                {
                    case 10:
                        res += "A";
                        break;
                    case 11:
                        res += "B";
                        break;
                    case 12:
                        res += "C";
                        break;
                    case 13:
                        res += "D";
                        break;
                    case 14:
                        res += "E";
                        break;
                    case 15:
                        res += "F";
                        break;
                    default:
                        res += rem.ToString();
                        break;
                }
                num = num / 16;
            } while (num > 9);
            res += num;
            numb = "";
            for (int i = res.Length - 1; i >= 0; --i)
                numb += res[i].ToString();
            return numb;
        }
        //------------------------------END----------------------------------      
    }
}